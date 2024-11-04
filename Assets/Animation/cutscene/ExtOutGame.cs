using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtOutGame : MonoBehaviour
{
    void OnEnable()

    {
        GameManager.LoadLevel(0);
    }
}
