using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitForInput : MonoBehaviour
{

    public float mDelay;
    [SerializeField] private TMPro.TextMeshProUGUI mText;
    [SerializeField] private SpriteRenderer mSpaceSprite;
    void Update()
    {
        if (mDelay > 0)
        {
            mDelay -= Time.deltaTime;
            return;
        }

        mText.gameObject.SetActive(true);
        mSpaceSprite.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync(2);
            AudioPlayer nAudioPlayer = GameObject.FindObjectOfType<AudioPlayer>();
            nAudioPlayer.GetComponent<AudioSource>().volume = .2f;
            nAudioPlayer.PlaySound(2);
        }
    }

}
