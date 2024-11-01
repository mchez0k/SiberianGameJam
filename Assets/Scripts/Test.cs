using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    private void Start()
    {
        Instantiate(cube, Vector3.zero, Quaternion.identity);
    }
}
