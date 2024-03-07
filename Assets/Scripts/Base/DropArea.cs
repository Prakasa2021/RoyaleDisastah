using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DropArea : MonoBehaviour
{
    public int boxCount;
    [SerializeField] Transform spawnPos;
    // public bool isTriggerArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Debug.Log("Trigger");
            Destroy(other.gameObject);
            boxCount += 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (Input.GetKey(KeyCode.R))
            ExchangeShop();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!other.CompareTag("Box")) return;
    }

    void ExchangeShop()
    {
        switch (boxCount)
        {
            case 1:
                // transform.position = new Vector3(transform.position.x, 2f, transform.position.z - 5f);
                Instantiate(Resources.Load<GameObject>("Pistol/Pistol1"), spawnPos.position, Quaternion.identity);
                boxCount -= 1;
                break;

            default:
                Debug.Log("No Box!");
                break;
        }
    }
}
