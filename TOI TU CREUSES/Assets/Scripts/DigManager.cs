using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigManager : MonoBehaviour
{

    public Text blocksToHaveTxt, blocksInDispTxt;
    int blocksToHave, blocksInDisp;
    public int blocksToHaveMax = 3;
    public GameObject dirtBlock;

    void Start()
    {
        blocksInDisp = 0;
        blocksToHave = 0;
        UpdateTxt();
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
        UpdateTxt();
    }

    public void OnGetBlock()
    {
        blocksInDisp++;
        UpdateTxt();
    }

    public void OnPlacingBlock()
    {
        blocksInDisp--;
        UpdateTxt();
    }

    public void UpdateTxt()
    {
        blocksToHaveTxt.text = blocksToHave.ToString() + "/" + blocksToHaveMax.ToString();
        blocksInDispTxt.text = blocksInDisp.ToString();
    }
}
