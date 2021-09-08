using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tile;
    public GameObject pillar;
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
        LevelData level = SaveSystem.Load();
        sizeX = level.tileStatesArr.GetLength(0);
        sizeY = level.tileStatesArr.GetLength(1);
        X = (sizeX / 2) * size;
        Y = (sizeY / 2) * size;
        float y = Y;
        for (int i = 0; i < sizeY; ++i)
        {
            float x = -X;
            for (int j = 0; j < sizeX; ++j)
            {
                GameObject go = Instantiate(tile, new Vector2(x, y), transform.rotation);
                if (level.tileStatesArr[j, i] == 'X')
                {
                    GameObject pillarGo = Instantiate(pillar, new Vector2(x, y), transform.rotation);
                    pillarGo.GetComponent<SpriteRenderer>().color = Color.red;
                }
                x += size;
            }
            y -= size;
        }
    }
}
