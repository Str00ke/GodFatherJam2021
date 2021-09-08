using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tourretController : MonoBehaviour
{
    private Vector2 mousePos;
    public GameObject shootPrefab;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

   
}
