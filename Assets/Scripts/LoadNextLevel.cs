using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            AudioPlayer nAudioPlayer = GameObject.FindObjectOfType<AudioPlayer>();
            nAudioPlayer.GetComponent<AudioSource>().volume = .2f;
            nAudioPlayer.PlaySound(2);
        }
    }

}
