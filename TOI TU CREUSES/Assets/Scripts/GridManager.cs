using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

    public float minRandTime, maxRandTime;

    List<GameObject> testAppear = new List<GameObject>();

    [HideInInspector]
    public Vector2[,] tilePos;
    Vector2 diggerPos;
    public char[,] tileState;
    [HideInInspector]
    public int tmpDigPosX, tmpDigPosY;

    public GameObject HautH, BasH, GaucheV, DroiteV, HautGauche, HautDroite, BasGauche, BasDroite;


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
        Debug.Log(Application.dataPath);
    }

    void Update()
    {

    }
    [SerializeField]
    public FileStream file;
    void Gen()
    {
        GameObject farestObj = null;

        LevelData level = SaveSystem.Load(file);

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
                    testAppear.Add(pillarGo);
                    pillarGo.SetActive(false);
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
                            testAppear.Add(dirtGo);
                            dirtGo.SetActive(false);
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
                testAppear.Add(go);
                go.SetActive(false);
            }
            y -= size;
        }

        innerCol.transform.localScale = new Vector3(sizeX + 3.5f, sizeY + 5.5f, 0);
        outerCol.transform.localScale = new Vector3(sizeX + 7.5f, sizeY + 8.5f, 0);

        CreateBorder(farestObj);

        FindObjectOfType<TouretsManager>().GetTile(farestObj);
        //TODO: Organize
        FindObjectOfType<CamAdapt>().CamAdaptToTerrain(farestObj, sizeX, sizeY);

    }

    void CreateBorder(GameObject upLeftTile)
    {
        float tileSizeX = upLeftTile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float tileSizeY = upLeftTile.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        for (int i = 0; i < sizeX; i++)
        {
            Vector2 vec = tilePos[i, 0];
            vec.y += tileSizeY;
            Instantiate(HautH, vec, transform.rotation);

            vec = tilePos[i, sizeY-1];
            vec.y -= tileSizeY * 1.5f;
            Instantiate(BasH, vec, transform.rotation);

        }
        
        for (int i = 0; i < sizeY; i++)
        {
            Vector2 vec = tilePos[0, i];
            vec.x -= tileSizeX;
            Instantiate(GaucheV, vec, transform.rotation);

            vec = tilePos[sizeX - 1, i];
            vec.x += tileSizeX;
            Instantiate(DroiteV, vec, transform.rotation);
        }

        Vector2 vec2 = tilePos[0, 0];
        vec2.x -= tileSizeX;
        vec2.y += tileSizeY;
        Instantiate(HautGauche, vec2, transform.rotation);

        vec2 = tilePos[sizeX-1, 0];
        vec2.x += tileSizeX;
        vec2.y += tileSizeY;
        Instantiate(HautDroite, vec2, transform.rotation);

        vec2 = tilePos[0, sizeY-1];
        vec2.x -= tileSizeX;
        vec2.y -= tileSizeY;
        Instantiate(BasGauche, vec2, transform.rotation);

        vec2 = tilePos[sizeX-1, sizeY-1];
        vec2.x += tileSizeX;
        vec2.y -= tileSizeY;
        Instantiate(BasDroite, vec2, transform.rotation);
    }

    IEnumerator TilesAppearCo()
    {
        for (int i = 0; i < testAppear.Count; i++)
        {
            testAppear[i].SetActive(true);
            yield return new WaitForSeconds(0.001f);
        }
    }
    /*void TilesAppear()
    {
        for (int i = 0; i < testAppear.Count; i++)
        {
            testAppear[i].SetActive(true);
        }
    }*/

    public void SpawnDigger()
    {
        Instantiate(digger, diggerSpawnPos, transform.rotation);
        StartCoroutine(TilesAppearCo());
        //digger.GetComponent<Player1Controller>().SetStartPos(tmpDigPosX, tmpDigPosY);
        TimerDirtSpawn();
    }

    void TimerDirtSpawn()
    {
        float timer = Random.Range(minRandTime, maxRandTime);
        StartCoroutine(Chrono(timer));
    }

    IEnumerator Chrono(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnDirt();
    }

    void SpawnDirt()
    {
        Player1Controller digger = FindObjectOfType<Player1Controller>();
        Vector2 pos = GetRandPos(digger);
        Instantiate(dirt, tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform.GetChild(1));
        tileState[(int)pos.x, (int)pos.y] = 'D';
        TimerDirtSpawn();
    }

    Vector2 GetRandPos(Player1Controller digger)
    {
        int X = Random.Range(0, sizeX);
        int Y = Random.Range(0, sizeY);
        if (tileState[X, Y] != '#' || (digger.posX == X && digger.posY == Y)) return GetRandPos(digger);
        else return new Vector2(X, Y);
    }
}
