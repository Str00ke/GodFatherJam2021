using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.layer == 14) Destroy(collision.gameObject);
        if (collision.gameObject.CompareTag("Player")) FindObjectOfType<GameManager>().swapControlsCharacter();

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 12)
        Destroy(gameObject);
    }
}
