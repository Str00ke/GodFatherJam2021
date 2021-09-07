using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject bomb;
    public Transform firePoint;
    private Rigidbody2D rb;
    public Camera cam;

    public float bulletSpeed = 20f;

    private Vector2 mousepos;
    private Vector2 lookDir;
    private float shootAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);

        lookDir = mousepos - rb.position;
        shootAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        rb.rotation = shootAngle;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bomb, firePoint.position, firePoint.rotation);
        Rigidbody2D rb2 = bullet.GetComponent<Rigidbody2D>();
        rb2.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
