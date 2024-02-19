using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{

    private Rigidbody2D mRigidBody;
    private ParticleSystem mParticleSystem;

    private AudioPlayer mAudioPlayer;

    public float mJumpForce = 5f;
    public float mSpeed = 5f;
    private Vector2 mVelocity;
    private bool mGrounded = false;

    public bool mPlayCredits { get; private set; } = false;

    void Start()
    {
        mRigidBody = gameObject.GetComponent<Rigidbody2D>();
        mParticleSystem = gameObject.GetComponent<ParticleSystem>();

        mAudioPlayer = GameObject.FindObjectOfType<AudioPlayer>();
    }


    #if UNITY_ANDROID || UNITY_IOS
    private static float fScreenControllerPercent = .15f;
    private static float fLeftScreenControllerSize = (float)Screen.width * fScreenControllerPercent;
    private static float fRightScreenControllerSize = (float)Screen.width - fLeftScreenControllerSize;
    
    private static float fScreenJumpPercent = .4f;
    private static float fScreenJumpSize = (float)Screen.height * (1f - fScreenJumpPercent);
    #endif
    void Update()
    {
        if (mPlayCredits)
        {
            return;
        }

        mVelocity = mRigidBody.velocity;

        #if UNITY_ANDROID || UNITY_IOS
        
        int iLeftTouchCnt = 0;
        int iRightTouchCnt = 0;
        int iJumpTouchCnt = 0;
        foreach (Touch curTouch in Input.touches)
        {
            float iXCurPos = curTouch.position.x;
            if (iXCurPos < fLeftScreenControllerSize)
            {
                iLeftTouchCnt++;
            }
            else if (iXCurPos > fRightScreenControllerSize)
            {
                iRightTouchCnt++;
            }

            if (curTouch.position.y > fScreenJumpSize)
            {
                iJumpTouchCnt++;
            }
        }
        mVelocity.x = mSpeed * (iLeftTouchCnt > iRightTouchCnt ? -1 : (iLeftTouchCnt < iRightTouchCnt ? 1 : 0));
        if (mGrounded && iJumpTouchCnt > 0)
        {
            mGrounded = false;
            mVelocity.y = mJumpForce;
            mAudioPlayer.GetComponent<AudioSource>().volume = 1f;
            mAudioPlayer.PlaySound(1);
        }

        #else

        mVelocity.x = Input.GetAxis("hor") * mSpeed;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && mGrounded)
        {
            mGrounded = false;
            mVelocity.y = mJumpForce;
            mAudioPlayer.GetComponent<AudioSource>().volume = 1f;
            mAudioPlayer.PlaySound(1);
        }
        
        #endif

        mRigidBody.velocity = mVelocity;
    }

    void FixedUpdate()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        mParticleSystem.Play();

        SizeChanges nSizeChanges = collision.GetComponent<SizeChanges>();

        if (nSizeChanges != null)
        {
            if (collision.gameObject.tag == "Saw")
            {
                float nSize = Mathf.Min(nSizeChanges.mNewSize, transform.localScale.x);
                transform.localScale = new Vector3(nSize, nSize, nSize);
                mRigidBody.mass = Mathf.Min(nSizeChanges.mNewWeight, mRigidBody.mass);
                mJumpForce = Mathf.Max(nSizeChanges.mNewJumpVel, mJumpForce);
                mAudioPlayer.GetComponent<AudioSource>().volume = .14f;
                mAudioPlayer.PlaySound(3);
            }

            if (collision.gameObject.tag == "SlimeDrop")
            {
                float nSize = Mathf.Max(nSizeChanges.mNewSize, transform.localScale.x);
                transform.localScale = new Vector3(nSize, nSize, nSize);
                mRigidBody.mass = Mathf.Max(nSizeChanges.mNewWeight, mRigidBody.mass);
                mJumpForce = Mathf.Min(nSizeChanges.mNewJumpVel, mJumpForce);
                mAudioPlayer.GetComponent<AudioSource>().volume = .9f;
                mAudioPlayer.PlaySound(0);
            }
        }

        if (collision.gameObject.tag == "PlayCredits")
        {
            mPlayCredits = true;
            transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        mParticleSystem.Play();
        if (Physics2D.Raycast(transform.position, Vector2.down, 1f) ||
            Physics2D.Raycast(transform.position - new Vector3(transform.localScale.x / 3 * 2, 0, 0), Vector2.down, 1f) ||
            Physics2D.Raycast(transform.position + new Vector3(transform.localScale.x / 3 * 2, 0, 0), Vector2.down, 1f))
        {
            mGrounded = true;
        }
    }

}

