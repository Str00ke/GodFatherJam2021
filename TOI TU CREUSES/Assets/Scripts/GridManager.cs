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
    public int sizeX, sizeY;
    float X, Y;
    static GridManager instance;

    [HideInInspector]
    public Vector2[,] tilePos;
    Vector2 diggerPos;
    public char[,] tileState;
    public int tmpDigPosX, tmpDigPosY;


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
        TimerDirtSpawn();
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
                GameObject go = Instantiate(tile, new Vector2(x, y), transform.rotation, transform.GetChild(0));
                if (i == 0 && j == 0)
                    farestObj = go;

                tilePos[j, i] = go.transform.position;

                if (tileState[j, i] == 'X')
                {
                    GameObject pillarGo = Instantiate(pillar, new Vector2(x, y), transform.rotation, transform.GetChild(2));
                    pillarGo.GetComponent<SpriteRenderer>().color = Color.red;
                } else
                {
                    if (currDirt < maxDirtCountOnTerrain)
                    {
                        float rand = Random.Range(0, 100);
                        if (rand <= dirtSpawnRate)
                        {
                            GameObject dirtGo = Instantiate(dirt, new Vector2(x, y), transform.rotation, transform.GetChild(1));
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
        //digger.GetComponent<Player1Controller>().SetStartPos(tmpDigPosX, tmpDigPosY);
    }

    void TimerDirtSpawn()
    {
        float timer = Random.Range(5, 10);
        Debug.Log(timer);
        StartCoroutine(Chrono(timer));
    }

    IEnumerator Chrono(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnDirt();
    }

    void SpawnDirt()
    {
        Vector2 pos = GetRandPos();
        Debug.Log("SPAWN");
        Instantiate(dirt, tilePos[(int)pos.x, (int)pos.y], transform.rotation);
        TimerDirtSpawn();
    }

    Vector2 GetRandPos()
    {
        int X = Random.Range(0, sizeX);
        int Y = Random.Range(0, sizeY);
        Debug.Log(tileState[X, Y]);
        if (tileState[X, Y] != '#') return GetRandPos();
        else return new Vector2(X, Y);
    }
}
