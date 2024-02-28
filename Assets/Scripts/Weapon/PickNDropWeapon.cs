using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PickNDropWeapon : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, weaponContainer, weaponHandler;
    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;
    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        weaponContainer = GameObject.FindGameObjectWithTag("WeaponContainer").GetComponent<Transform>();
        weaponHandler = GameObject.FindGameObjectWithTag("WeaponHandler").GetComponent<Transform>();

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

        transform.SetParent(weaponContainer);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;

        // GetComponent<BulletSpawn>().enabled = true;
        // foreach(GameObject go in GameObject.FindGameObjectsWithTag("Box"))
        // {
        //     go.GetComponent<PickNDropBox>().enabled = false;
        // }        
        // gunScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.AddForce(weaponHandler.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(weaponHandler.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        // GetComponent<BulletSpawn>().enabled = false;
        // foreach(GameObject go in GameObject.FindGameObjectsWithTag("Box"))
        // {
        //     go.GetComponent<PickNDropBox>().enabled = true;
        // }        
        // gunScript.enabled = false;
    }
}
