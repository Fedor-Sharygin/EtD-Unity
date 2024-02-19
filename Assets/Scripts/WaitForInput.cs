using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitForInput : MonoBehaviour
{

    public float mDelay;
    [SerializeField] private TMPro.TextMeshProUGUI mText;
    [SerializeField] private SpriteRenderer mSpaceSprite;

    private bool sGameLoading = false;
    void Update()
    {
        if (sGameLoading)
        {
            return;
        }

        if (mDelay > 0)
        {
            mDelay -= Time.deltaTime;
            return;
        }

        mText.gameObject.SetActive(true);
        mSpaceSprite.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            sGameLoading = true;
            SceneManager.LoadSceneAsync(2);
            AudioPlayer nAudioPlayer = GameObject.FindObjectOfType<AudioPlayer>();
            nAudioPlayer.GetComponent<AudioSource>().volume = .2f;
            nAudioPlayer.PlaySound(2);
        }
    }

}
