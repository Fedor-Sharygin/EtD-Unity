using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCredits : MonoBehaviour
{

    [SerializeField] private Camera mCamera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (MoveCredits nmv in GameObject.FindObjectsOfType<MoveCredits>())
                nmv.Activate();
            mCamera.transform.position = new Vector3(0f, -44.3f, -10f);
        }
    }

}
