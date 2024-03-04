using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject crossHair;
    [SerializeField] GameObject weaponContainer;

    void Start()
    {

    }

    void Update()
    {
        if (weaponContainer.transform.childCount > 0)
        {
            crossHair.SetActive(true);
        }
        else
        {
            crossHair.SetActive(false);
        }
    }
}
