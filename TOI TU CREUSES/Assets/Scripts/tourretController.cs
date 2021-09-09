using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tourretController : MonoBehaviour
{
    public GameObject shootPrefab;
    GameObject circle;
    float range;
    public float rotationA;

    private void Start()
    {
        circle = transform.GetChild(1).gameObject;
    }

    public void LookDirection(float AngleDeg, GameObject Player)
    {
        if (Player.GetComponent<Player2Controller>().inTurretMode)
        {
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, AngleDeg-180f);
        }
        else transform.GetChild(0).rotation = Quaternion.AngleAxis(rotationA, Vector3.forward);
    }
    public void ShootAnim()
    {
        GetComponent<Animator>().SetTrigger("shoot");
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
