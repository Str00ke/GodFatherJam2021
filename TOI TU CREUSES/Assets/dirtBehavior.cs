using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirtBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13) Destroy(gameObject);
        if (collision.gameObject.layer == 14) Destroy(gameObject);
    }
}
