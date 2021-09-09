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
    public HUD _hud;

    void Start()
    {
        blocksInDisp = 0;
        blocksToHave = 0;
    }

    //updating bullets in HUD from playercontroller/digmanager (TBD)

    public void OnDig()
    {
        blocksToHave++;
        if(FindObjectOfType<GameManager>().ready)
            _hud.VarUpdatesrBlocks(blocksToHave, FindObjectOfType<GameManager>().P1.GetComponent<PlayerController>().modeSwitch);
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
            if (FindObjectOfType<GameManager>().ready)
                _hud.VarUpdatesBlocks(blocksInDisp, FindObjectOfType<GameManager>().P1.GetComponent<PlayerController>().modeSwitch);
            Debug.Log( "block : " + blocksInDisp);
            if (FindObjectOfType<Player1Controller>().canPlaceBlock == false)
            {
                FindObjectOfType<Player1Controller>().canPlaceBlock = true;
            }
        }
    }

    public void OnPlacingBlock()
    {
        blocksInDisp--;
        if (FindObjectOfType<GameManager>().ready)
            _hud.VarUpdatesBlocks(blocksInDisp, FindObjectOfType<GameManager>().P1.GetComponent<PlayerController>().modeSwitch);
        if (blocksInDisp <= 0)
        {
            FindObjectOfType<Player1Controller>().canPlaceBlock = false;
            blocksInDisp = 0;
        }
    }

}
