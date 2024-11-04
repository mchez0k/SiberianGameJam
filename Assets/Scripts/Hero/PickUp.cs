using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{
    public int crystalsCollected = 0;
    public int crystalsToCollect = 3;
    public AudioSource AudioSource;

    private void Start()
    {
        BackgroundMusic.OnVolumeChangedEvent.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged()
    {
        AudioSource.volume = BackgroundMusic.Instance.Volume;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crystal"))
        {
            crystalsCollected++;
            Debug.Log("Crystal collected! Total: " + crystalsCollected);
            AudioSource.Play();
            Destroy(other.gameObject);

            //if (crystalsCollected >= crystalsToCollect)   -- еcли у игрока 3 кристала то загрузится сцена которая в кавычках в лоад сцене 
            //{
            //    Debug.Log("All crystals collected! Loading next scene...");
            //    SceneManager.LoadScene("NextSceneName");
            //}
        }
    }
}
