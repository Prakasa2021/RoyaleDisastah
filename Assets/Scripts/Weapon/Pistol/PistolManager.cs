using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class PistolManager : MonoBehaviour
{
    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;
    [SerializeField] GameObject bulletPrefabs;
    [SerializeField] Transform BulletSpawnPos;
    [SerializeField] float bulletSpeed = 40f;
    [SerializeField] int bulletPerShot;
    public AimState aim;

    void Start()
    {
        aim = GameObject.FindGameObjectWithTag("Player").GetComponent<AimState>();
        fireRateTimer = fireRate;
    }

    void Update()
    {
        if (transform.parent != null)
        {
            transform.LookAt(aim.aimPos);

            if (ShouldFire())
                // var bullet = Instantiate(bulletPrefabs, BulletSpawnPos.position, BulletSpawnPos.rotation);
                // bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPos.forward * bulletSpeed;
                Fire();
        }
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        BulletSpawnPos.LookAt(aim.aimPos);

        for (int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bulletPrefabs, BulletSpawnPos.position, BulletSpawnPos.rotation);
            currentBullet.GetComponent<Rigidbody>().AddForce(BulletSpawnPos.forward * bulletSpeed, ForceMode.Impulse);
        }
    }

    // public bool isFiring = false;
    // public ParticleSystem[] muzzleFlash;
    // public Transform raycastOrigin;

    // Ray ray;
    // RaycastHit hitInfo;

    // public void StartFiring()
    // {
    //     isFiring = true;

    //     foreach (var particle in muzzleFlash)
    //     {
    //         particle.Emit(1);
    //     }

    //     ray.origin = raycastOrigin.position;
    //     ray.direction = raycastOrigin.forward;

    //     if (Physics.Raycast(ray, out hitInfo) && Input.GetKeyDown(KeyCode.Mouse0))
    //     {
    //         Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1f);
    //     }
    // }

    // public void StopFiring()
    // {
    //     isFiring = false;
    // }
}
