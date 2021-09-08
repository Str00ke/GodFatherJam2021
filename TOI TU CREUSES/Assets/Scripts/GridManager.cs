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

    // Start is called before the first frame update
    void Start()
    {
        size = tile.GetComponent<Renderer>().bounds.size.x;
        Gen();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Gen()
    {
        GameObject farestObj = null;

        LevelData level = SaveSystem.Load();

        sizeX = level.tileStatesArr.GetLength(0);
        sizeY = level.tileStatesArr.GetLength(1);
        FindObjectOfType<TouretsManager>().GetGridSize(sizeX, sizeY); //ui c caca je sai

        X = (sizeX / 2) * size;
        Y = (sizeY / 2) * size;
        float y = Y;
        for (int i = 0; i < sizeY; ++i)
        {
            float x = -X;
            for (int j = 0; j < sizeX; ++j)
            {
                GameObject go = Instantiate(tile, new Vector2(x, y), transform.rotation);
                if (i == 0 && j == 0)
                    farestObj = go;
                if (level.tileStatesArr[j, i] == 'X')
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
                            level.tileStatesArr[j, i] = 'D';
                            currDirt++;
                        }
                        else if (diggerSpawnPos == Vector2.zero)
                            diggerSpawnPos = go.transform.position;
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
    }
}
