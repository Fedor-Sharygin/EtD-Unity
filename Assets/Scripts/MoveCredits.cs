using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCredits : MonoBehaviour
{
    private bool active = false;
    [SerializeField] private float mDelay = 0;
    [SerializeField] private float mSpeed = 0;
    void Update()
    {
        if (active)
        {
            if (mDelay > 0f)
            {
                mDelay -= Time.deltaTime;
                return;
            }

            Vector3 curPos = transform.position;
            curPos.x += mSpeed * Time.deltaTime;
            transform.position = curPos;
        }
    }

    public void Activate()
    {
        active = true;
    }

}
