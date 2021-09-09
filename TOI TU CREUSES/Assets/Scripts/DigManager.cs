using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigManager : MonoBehaviour
{

    public int blocksToHave, blocksInDisp;
    public int blocksToHaveMax = 3;
    public int blocksInDispMax = 2;
    
    public GameObject dirtBlock;

    void Start()
    {
        blocksInDisp = 0;
        blocksToHave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDig()
    {
        blocksToHave++;
        if (blocksToHave >= blocksToHaveMax)
        {
            OnGetBlock();
            blocksToHave = 0;
        }
    }

    public void OnGetBlock()
    {
        if (blocksInDisp < blocksInDispMax)
        {
            blocksInDisp++;
            if (FindObjectOfType<Player1Controller>().canPlaceBlock == false)
            {
                FindObjectOfType<Player1Controller>().canPlaceBlock = true;
            }
        }
    }

    public void OnPlacingBlock()
    {
        blocksInDisp--;
        if (blocksInDisp <= 0)
        {
            FindObjectOfType<Player1Controller>().canPlaceBlock = false;
        }
    }

}
