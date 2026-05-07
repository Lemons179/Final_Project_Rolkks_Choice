using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot2D : MonoBehaviour
{
    //audio 
    [SerializeField] private AudioClip narwhalsound;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

    public float shootCooldown = 0.3f;
    private float nextShootTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextShootTime)
        {
            //play coin sound
            soundFXManager.instance.PlaySoundFXClip(narwhalsound, transform, 1f);

            Shoot();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void Shoot()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector2 dir = (mouse - firePoint.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.velocity = dir * projectileSpeed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        proj.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
