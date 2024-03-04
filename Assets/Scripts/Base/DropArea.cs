using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DropArea : MonoBehaviour
{
    public int boxCount;
    // public bool isTriggerArea;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Destroy(other.gameObject);
            boxCount += 1;
        }

        if (other.CompareTag("Player"))
        {
            // isTriggerArea = true;
            ExchangeShop();
        }
    }

    // void OnTriggerExit(Collider other)
    // {
    //     isTriggerArea = false;
    // }

    void ExchangeShop()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            switch (boxCount)
            {
                case 1:
                    transform.position = new Vector3(transform.position.x, 2f, transform.position.z - 5f);
                    Instantiate(Resources.Load("Pistol/Pistol1"), transform.position, Quaternion.identity);
                    boxCount = 0;
                    break;
            }
        }
    }
}
