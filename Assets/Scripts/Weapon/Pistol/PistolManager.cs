using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class PistolManager : MonoBehaviour
{
    [SerializeField] float fireRate; //15f new script
    [SerializeField] bool semiAuto;
    float fireRateTimer;
    [SerializeField] GameObject bulletPrefabs;
    [SerializeField] Transform BulletSpawnPos;
    [SerializeField] float bulletSpeed = 40f;
    [SerializeField] int bulletPerShot;
    [SerializeField] int rotationSpeed;
    public AimState aim;

    // // Raycast Shoot
    // public float damage = 10f;
    // public float range = 100f;
    // public Camera cam;
    // public float nextTimeToFire = 0f;

    void Start()
    {
        aim = GameObject.FindGameObjectWithTag("Player").GetComponent<AimState>();
        // cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        fireRateTimer = fireRate;
    }

    void Update()
    {
        if (transform.parent != null)
        {
            // transform.LookAt(aim.aimPos);

            // // Calculate the direction to the target
            // Vector3 direction = aim.targetPosition - transform.position;
            // // direction.y = 0f; // Optional: Keep the direction on the XZ plane

            // // Calculate the rotation to look at the target
            // Quaternion targetRotation = Quaternion.LookRotation(direction);
            // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (ShouldFire())
                // var bullet = Instantiate(bulletPrefabs, BulletSpawnPos.position, BulletSpawnPos.rotation);
                // bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPos.forward * bulletSpeed;
                Fire();
        }

        // if (transform.parent != null && Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        // {
        //     nextTimeToFire = Time.deltaTime + 1f / fireRate;
        //     Shoot();
        // }
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (semiAuto && Input.GetMouseButtonDown(0)) return true;
        if (!semiAuto && Input.GetMouseButton(0)) return true;
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        // BulletSpawnPos.LookAt(aim.aimPos);

        // // Calculate the direction to the target
        // Vector3 direction = aim.targetPosition - transform.position;
        // // direction.y = 0f; // Optional: Keep the direction on the XZ plane

        // // Calculate the rotation to look at the target
        // Quaternion targetRotation = Quaternion.LookRotation(direction);
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        for (int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bulletPrefabs, BulletSpawnPos.position, BulletSpawnPos.rotation);
            currentBullet.GetComponent<Rigidbody>().AddForce(BulletSpawnPos.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}
