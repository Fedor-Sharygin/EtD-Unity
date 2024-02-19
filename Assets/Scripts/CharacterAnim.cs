using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{

    private Animator mAnimator;
    private void Start()
    {
        mAnimator = gameObject.GetComponent<Animator>();
        gameObject.GetComponent<SpriteRenderer>().size = new Vector2(.5f, .5f);
    }

    private void Update()
    {
        if (FindObjectOfType<SlimeController>().mPlayCredits)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            mAnimator.SetTrigger("isJumping");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SizeChanges nSizeChanges = collision.GetComponent<SizeChanges>();
        if (nSizeChanges != null)
        {
            mAnimator.SetTrigger("sizeChanging");
        }
    }

}
