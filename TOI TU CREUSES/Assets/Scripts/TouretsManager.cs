using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouretsManager : MonoBehaviour
{
    public GameObject[] turrets;
    public GameObject shooter;
    public Vector2 UPLEFT = new Vector2(2.5f, 1.25f);
    public Vector2 UPMIDDLE = new Vector2(2.5f, 1.25f);
    public Vector2 UPRIGHT = new Vector2(2.5f, 1.25f);
    public Vector2 MIDDLELEFT = new Vector2(2.5f, 0);
    public Vector2 MIDDLERIGHT = new Vector2(2.5f, 0);
    public Vector2 DOWNLEFT = new Vector2(2.5f, 1.25f);
    public Vector2 DOWNMIDDLE = new Vector2(0, 0);
    public Vector2 DOWNRIGHT = new Vector2(2.5f, 1.25f);

    GameObject tLTurret;

    int _sizeX, _sizeY;
    GameObject topLeftTile;

    public void GetGridSize(int sizeX, int sizeY)
    {
        _sizeX = sizeX;
        _sizeY = sizeY;
    }

    public void GetTile(GameObject tile)
    {
        topLeftTile = tile;
    }

    public void PlaceTurrets()
    {
        Camera cam = Camera.main;
        Rect camRect = cam.rect;

        Vector3 tileSize = topLeftTile.GetComponent<SpriteRenderer>().sprite.bounds.size;
        int gridSizeX = (int)tileSize.x * _sizeX;
        int gridSizeY = (int)tileSize.y * _sizeY;

        Vector2 turretSize = turrets[0].GetComponent<Collider2D>().bounds.size;
        tLTurret = turrets[0]; //Hardcode sorry
        for (int i = 0; i < turrets.Length; ++i)
        {
            turrets[i].GetComponent<tourretController>().SetRange(gridSizeX, gridSizeY, turretSize);
            turrets[i].transform.GetChild(0).gameObject.SetActive(true);

            //Instantiate(turrets[i], transform.position, transform.rotation);
            switch (i)
            {
                case 0: //UP-LEFT
                    turrets[i].transform.position = new Vector2((cam.transform.position.x - gridSizeX / 2) - turretSize.x * 3f, (cam.transform.position.x + gridSizeY / 2) + turretSize.y * 3f);
                    break;

                case 1: //UP-MIDDLE
                    turrets[i].transform.position = new Vector2((cam.transform.position.x), (cam.transform.position.x + gridSizeY / 2) + turretSize.y * 4f);
                    break;

                case 2: //UP-RIGHT
                    turrets[i].transform.position = new Vector2((cam.transform.position.x + gridSizeX / 2) + turretSize.x * 3f, (cam.transform.position.x + gridSizeY / 2) + turretSize.y * 3f);
                    break;

                case 3: //MIDDLE-LEFT
                    turrets[i].transform.position = new Vector2((cam.transform.position.x - gridSizeX / 2) - turretSize.x * 3f, (cam.transform.position.x));
                    break;

                case 4: //MIDDLEE-RIGHT
                    turrets[i].transform.position = new Vector2((cam.transform.position.x + gridSizeX / 2) + turretSize.x * 3f, (cam.transform.position.x));
                    break;

                case 5: //DOWN-LEFT
                    turrets[i].transform.position = new Vector2((cam.transform.position.x - gridSizeX / 2) - turretSize.x * 3f, (cam.transform.position.x - gridSizeY / 2) - turretSize.y * 3f);
                    break;

                case 6: //DOWN-MIDDLE
                    turrets[i].transform.position = new Vector2((cam.transform.position.x), (cam.transform.position.x - gridSizeY / 2) - turretSize.y * 3f);
                    break;

                case 7: //DOWN-RIGHT
                    turrets[i].transform.position = new Vector2((cam.transform.position.x + gridSizeX / 2) + turretSize.x * 3f, (cam.transform.position.x - gridSizeY / 2) - turretSize.y * 3f);
                    break;
            }
        }

        SpawnShooter();
    }

    void SpawnShooter()
    {
        Vector2 vec = new Vector2(tLTurret.transform.position.x + tLTurret.GetComponent<Collider2D>().bounds.size.x * 3, tLTurret.transform.position.y +1);
        Instantiate(shooter, vec, transform.rotation);
    }
}
