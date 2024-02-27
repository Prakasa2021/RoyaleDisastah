using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickNDrop : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, boxContainer, boxHandler;
    public float pickUpRange, dropRange;
    public float dropForwardForce, dropUpwardForce;
    public bool equipped;
    public static bool slotFull;

    Collider colliderBox;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boxContainer = GameObject.FindGameObjectWithTag("BoxContainer").GetComponent<Transform>();
        boxHandler = GameObject.FindGameObjectWithTag("BoxHandler").GetComponent<Transform>();
        colliderBox = GetComponent<Collider>();

        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.G) && !slotFull)
        {
            PickUp();
        }
        else if (equipped && Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(boxContainer);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;
        colliderBox.enabled = false;

        // foreach (GameObject go in GameObject.FindGameObjectsWithTag("Weapon"))
        // {
        //     go.GetComponent<PickNDropWeapon>().enabled = false;
        // }
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;
        colliderBox.enabled = true;

        rb.AddForce(boxHandler.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(boxHandler.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        // foreach (GameObject go in GameObject.FindGameObjectsWithTag("Weapon"))
        // {
        //     go.GetComponent<PickNDropWeapon>().enabled = true;
        // }
    }
}
