using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutsceneexit : MonoBehaviour
{
    void OnEnable()

    {
        GameManager.LoadLevel(2);
    }
}