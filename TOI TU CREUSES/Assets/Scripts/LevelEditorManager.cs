using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelEditorManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject walkableTile;
    float size;
    float X, Y;
    public Slider sliderX, sliderY;
    public Button saveBtn;
    public Text valX, valY;
    char[,] tileState;
    void Start()
    {
        size = walkableTile.GetComponent<Renderer>().bounds.size.x;
        saveBtn.gameObject.SetActive(false);
        
    }


    void Update()
    {
        valX.text = sliderX.value.ToString();
        valY.text = sliderY.value.ToString();
    }

    public void GenGrid()
    {
        tileState = new char[(int)sliderX.value, (int)sliderY.value];
        saveBtn.gameObject.SetActive(true);
        panel.SetActive(false);
        X = (sliderX.value / 2) * size;
        Y = (sliderY.value / 2) * size;

        float y = Y;
        for (int i = 0; i < sliderY.value; ++i)
        {
            float x = -X;
            for (int j = 0; j < sliderX.value; ++j)
            {
                GameObject tile = Instantiate(walkableTile, new Vector2(x, y), transform.rotation);
                SetTile(new int[j, i], '#');
                x += size;
                tile.GetComponent<Tile>().pos = new int[j, i];
            }
            y -= size;
        }
    }

    public void SetTile(int[,] pos, char state)
    {
        tileState[pos.GetLength(0), pos.GetLength(1)] = state;
    }

    public void Save()
    {
        LevelData levelData = new LevelData(tileState);
        SaveSystem.Save(levelData);
    }
}

[System.Serializable]
public class LevelData
{
    public char[,] tileStatesArr;
    public LevelData(char[,] arr)
    {
        tileStatesArr = arr;
    }
}
