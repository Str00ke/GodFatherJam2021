using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tile;
    public GameObject pillar;
    public GameObject dirt;
    public GameObject digger;
    public GameObject innerCol;
    public GameObject outerCol;
    Vector2 diggerSpawnPos = Vector2.zero;
    public float dirtSpawnRate = 33f;
    public int maxDirtCountOnTerrain;
    int currDirt;
    float size;
    int sizeX, sizeY;
    float X, Y;
    static GridManager instance;
    Vector2[,] tilePos;
    Vector2 diggerPos;
    public char[,] tileState;
    int tmpDigPosX, tmpDigPosY;



    private void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }

    static public GridManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        size = tile.GetComponent<Renderer>().bounds.size.x;
        Gen();
    }

    void Update()
    {

    }

    void Gen()
    {
        GameObject farestObj = null;

        LevelData level = SaveSystem.Load();

        sizeX = level.tileStatesArr.GetLength(0);
        sizeY = level.tileStatesArr.GetLength(1);
        FindObjectOfType<TouretsManager>().GetGridSize(sizeX, sizeY);

        X = (sizeX / 2) * size;
        Y = (sizeY / 2) * size;

        tilePos = new Vector2[sizeX, sizeY];
        tileState = level.tileStatesArr;

        float y = Y;
        for (int i = 0; i < sizeY; ++i)
        {
            float x = -X;
            for (int j = 0; j < sizeX; ++j)
            {
                GameObject go = Instantiate(tile, new Vector2(x, y), transform.rotation);
                if (i == 0 && j == 0)
                    farestObj = go;

                tilePos[j, i] = go.transform.position;

                if (tileState[j, i] == 'X')
                {
                    GameObject pillarGo = Instantiate(pillar, new Vector2(x, y), transform.rotation);
                    pillarGo.GetComponent<SpriteRenderer>().color = Color.red;
                } else
                {
                    if (currDirt < maxDirtCountOnTerrain)
                    {
                        float rand = Random.Range(0, 100);
                        if (rand <= dirtSpawnRate)
                        {
                            GameObject dirtGo = Instantiate(dirt, new Vector2(x, y), transform.rotation);
                            tileState[j, i] = 'D';
                            currDirt++;
                        }
                        else if (diggerSpawnPos == Vector2.zero)
                        {
                            diggerSpawnPos = go.transform.position;
                            tmpDigPosX = j;
                            tmpDigPosY = i;
                        }
                            
                    }
                    
                }
                x += size;
            }
            y -= size;
        }

        innerCol.transform.localScale = new Vector3(sizeX + 0.5f, sizeY + 0.5f, 0);
        outerCol.transform.localScale = new Vector3(sizeX + 3.5f, sizeY + 3.5f, 0);

        FindObjectOfType<TouretsManager>().GetTile(farestObj);
        //TODO: Organize
        FindObjectOfType<CamAdapt>().CamAdaptToTerrain(farestObj, sizeX, sizeY);


    }

    public void SpawnDigger()
    {
        Instantiate(digger, diggerSpawnPos, transform.rotation);
        digger.GetComponent<Player1Controller>().SetStartPos(tmpDigPosX, tmpDigPosY);
    }
}
