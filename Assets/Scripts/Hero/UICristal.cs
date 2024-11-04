using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICristal : MonoBehaviour
{
    public PickUp pickUpScript;
    public GameObject crystalObject1;
    public GameObject crystalObject2;
    public GameObject crystalObject3;

    void Update()
    {
        if (pickUpScript.crystalsCollected == 1)
        {
            crystalObject1.SetActive(true);
            crystalObject2.SetActive(false);
            crystalObject3.SetActive(false);
        }
        else if (pickUpScript.crystalsCollected == 2)
        {
            crystalObject1.SetActive(true);
            crystalObject2.SetActive(true);
            crystalObject3.SetActive(false);
        }
        else if (pickUpScript.crystalsCollected == 3)
        {
            crystalObject1.SetActive(true);
            crystalObject2.SetActive(true);
            crystalObject3.SetActive(true);
        }
    }
}
