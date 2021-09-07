using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public int[,] pos;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(pos.GetLength(0) + "  " + pos.GetLength(1));
        if (GetComponent<SpriteRenderer>().color == Color.white)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            FindObjectOfType<LevelEditorManager>().SetTile(pos, 'X'); //Non-Walkable
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            FindObjectOfType<LevelEditorManager>().SetTile(pos, '#'); //Walkable
        }
            
    }

    public int FindPos(GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; ++i)
        {
            if (arr[i] == this)
                return i;
        }
        return 0;
    }
}
