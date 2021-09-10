using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootController : MonoBehaviour
{
    //[SerializeField]
    //private bool isBomb;
    //private bool canExplose;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (!isBomb)
        //{
            Destroy(gameObject);
            if (collision.gameObject.layer == 14) Destroy(collision.gameObject);
            if (collision.gameObject.CompareTag("Player")) FindObjectOfType<GameManager>().swapControlsCharacter();
        //}

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (isBomb)
    //    {
    //        StartCoroutine(WaitToExplode());
    //    }
    //}
    //IEnumerator WaitToExplode()
    //{
    //    yield return new WaitForSeconds(1);
    //    canExplose = true;
    //    StartCoroutine(Explode());
    //}
    //IEnumerator Explode()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    Destroy(gameObject);
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (isBomb)
    //    {
    //        if (collision.gameObject.layer == 14 && canExplose) Destroy(collision.gameObject);
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            Destroy(gameObject);
            //if (isBomb)
            //{
            //    StartCoroutine(WaitToExplode());
            //}
            //else
            //{
                
            //}
        }
    }
}
