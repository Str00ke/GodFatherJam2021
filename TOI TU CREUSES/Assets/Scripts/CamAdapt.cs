using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAdapt : MonoBehaviour
{
    GameObject farestObjTmp;
    public GameObject turret;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void CamAdaptToTerrain(GameObject farestObj, float sizeX, float sizeY)
    {
        GameObject helper = new GameObject();
        helper.AddComponent<SpriteRenderer>();
        helper.transform.position = new Vector2(farestObj.GetComponent<Renderer>().bounds.min.x - turret.GetComponent<Collider2D>().bounds.size.x * 2.5f, farestObj.GetComponent<Renderer>().bounds.max.y + turret.GetComponent<Collider2D>().bounds.size.y * 2.5f);
        farestObjTmp = helper;
        if (sizeY > sizeX)
        {
            RecAdapt(helper, sizeY);
        } else
        {
            RecAdapt(helper, sizeX);
        }
        
    }


    void RecAdapt(GameObject obj, float size)
    {
        if (!obj.GetComponent<Renderer>().isVisible)
        {
            GetComponent<Camera>().orthographicSize += Time.deltaTime * (size / 6);
            StartCoroutine(AdaptTimer(size));
        }
        else 
        {
            FindObjectOfType<TouretsManager>().PlaceTurrets();
            FindObjectOfType<GridManager>().SpawnDigger();
        }
            
    }

    IEnumerator AdaptTimer(float size)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        RecAdapt(farestObjTmp, size);
    }
    
}
