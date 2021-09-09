using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public Transform target;
    Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player1Controller>().transform;
        }
        else
        {
            MoveToTarget();
        }

    }

    public void MoveToTarget()
    {
        Vector3 lookAt = target.position;
        float AngleDeg = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x) * Mathf.Rad2Deg - 90;
        rb.rotation = AngleDeg;

        rb.velocity = new Vector2(target.transform.position.x - rb.transform.position.x, target.transform.position.y - rb.transform.position.y).normalized * speed;
    }
}
