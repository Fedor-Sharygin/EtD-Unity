using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Vector3 mOriginalPosition;
    private void Awake()
    {
        mOriginalPosition = transform.position;
    }

    private float mCurWait = 0f;
    private const float mIdleTime = .4f;
    private void Update()
    {
        if (transform.position == mHidePosition && mCurWait < mIdleTime)
        {
            mCurWait += Time.deltaTime;
        }
    }

    [SerializeField] private Vector3 mHidePosition;
    [SerializeField] private List<Reactive> mReactive;
    [SerializeField] private float mWeightRequired;
    private bool activated = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Rigidbody2D>().mass >= mWeightRequired)
        {
            if (!activated)
            {
                activated = true;
                foreach (Reactive r in mReactive)
                    r.React();
            }
            transform.position = mHidePosition;
            mCurWait = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && mCurWait >= mIdleTime)
        {
            transform.position = mOriginalPosition;
        }
    }

}
