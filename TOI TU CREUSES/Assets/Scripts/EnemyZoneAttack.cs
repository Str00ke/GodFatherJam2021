using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZoneAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            StartCoroutine(timeToDestroy(collision));
        }
    }
    IEnumerator timeToDestroy(Collider2D coll)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(coll.gameObject);
    }
}
