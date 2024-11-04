using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{
    public int crystalsCollected = 0;
    public int crystalsToCollect = 3;



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crystal"))
        {
            crystalsCollected++;
            Debug.Log("Crystal collected! Total: " + crystalsCollected);

            Destroy(other.gameObject);

            //if (crystalsCollected >= crystalsToCollect)
            //{
            //    Debug.Log("All crystals collected! Loading next scene...");
            //    SceneManager.LoadScene("NextSceneName");
            //}
        }
    }
}
