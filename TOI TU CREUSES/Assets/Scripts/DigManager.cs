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
    public bool bonusAmmo;
    public Vector2 ammo;

    void Start()
    {
        blocksInDisp = 0;
        blocksToHave = 0;
    }

    //updating bullets in HUD from playercontroller/digmanager (TBD)

    public void OnDig()
    {
        blocksToHave++;
        if (FindObjectOfType<GameManager>().ready)
        {
            Player1Controller _p1 = FindObjectOfType<GameManager>().P1.GetComponent<Player1Controller>();
            _hud.VarUpdatesrBlocks(blocksToHave, _p1.modeSwitch);

            //random bullet drop
            BonusAmmo(_p1, bonusAmmo);

            Vector2 shovelPos = _p1.transform.GetChild(1).GetChild(0).transform.position;
            //random Bonus drop
            int randBonus = Random.Range(0, 30);
            if (randBonus <= 3) FindObjectOfType<BonusManager>().GetComponent<BonusManager>().SpawnerBonus(shovelPos);
        }
         

        if (blocksToHave >= blocksToHaveMax)
        {
            OnGetBlock();
            blocksToHave = 0;
        }
    }
    public void BonusAmmo(Player1Controller p, bool adBonus)
    {
        int rand;
        if (adBonus)
            rand = Random.Range(4, 8);
        else
            rand = Random.Range(0, 4);

        _hud.VarUpdatesBullets(rand, p.modeSwitch);
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
