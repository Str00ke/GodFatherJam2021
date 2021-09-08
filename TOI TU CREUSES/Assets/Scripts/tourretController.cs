using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tourretController : MonoBehaviour
{
    private Vector2 mousePos;
    public GameObject shootPrefab;
    GameObject circle;
    float range;

    private void Start()
    {
        circle = transform.GetChild(1).gameObject;
    }

    public void SetRange(float gridSizeX, float gridSizeY, Vector2 turretSize)
    {
        if (gridSizeY > gridSizeX)
        {
            range = gridSizeY;
        } else
        {
            range = gridSizeX;
        }

        circle.transform.localScale = new Vector3(range + turretSize.x, range + turretSize.x, 0);
    }

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
