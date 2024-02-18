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

    private bool mPlayCredits = false;

    void Start()
    {
        mRigidBody = gameObject.GetComponent<Rigidbody2D>();
        mParticleSystem = gameObject.GetComponent<ParticleSystem>();

        mAudioPlayer = GameObject.FindObjectOfType<AudioPlayer>();
    }

    void Update()
    {
        if (!mPlayCredits)
        {
            mVelocity = mRigidBody.velocity;
            mVelocity.x = Input.GetAxis("hor") * mSpeed;
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && mGrounded)
            {
                mGrounded = false;
                mVelocity.y = mJumpForce;
                mAudioPlayer.GetComponent<AudioSource>().volume = 1f;
                mAudioPlayer.PlaySound(1);
            }

            mRigidBody.velocity = mVelocity;
        }
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

