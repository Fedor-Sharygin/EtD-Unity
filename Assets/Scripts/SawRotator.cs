using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawRotator : MonoBehaviour
{

    public float mRttnSpd = 5f;
    void Update()
    {
        /// rotate the saw every frame by angle
        transform.Rotate(0, 0, mRttnSpd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

}
