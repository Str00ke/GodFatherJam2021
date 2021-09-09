using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text blocksJ1;
    public Text blocksJ2;
    public Text bulletsJ1;
    public Text bulletsJ2;
    public Text requiredBlocks1;
    public Text requiredBlocks2;

    private float maxBlocks;
    private float blocks1;
    private float blocks2;
    private float bullets1;
    private float bullets2;
    private float rBlocks1;
    private float rBlocks2;

    public void VarUpdatesBlocks(float newValue, bool player)
    {
        if (player)
        {
            blocks1 = newValue;
        }
        else
        {
            blocks2 = newValue;
        }
        HudTextUpdates();
    }

    public void VarUpdatesrBlocks(float newValue, bool player)
    {
        if (player)
        {
            rBlocks1 = newValue;
        }
        else
        {
            rBlocks2 = newValue;
        }
        HudTextUpdates();
    }

    public void VarUpdatesBullets(float newValue, bool player)
    {
        if (player)
        {
            bullets1 = newValue;
        }
        else
        {
            bullets2 = newValue;
        }
        HudTextUpdates();
    }

    public void HudTextUpdates()
    {
        blocksJ1.text = blocks1 + "/" + FindObjectOfType<DigManager>().blocksInDispMax;
        blocksJ2.text = blocks2 + "/" + FindObjectOfType<DigManager>().blocksInDispMax;
        bulletsJ1.text = bullets1.ToString();
        bulletsJ2.text = bullets2.ToString();
        requiredBlocks1.text = rBlocks1.ToString() + "/" + FindObjectOfType<DigManager>().blocksToHaveMax.ToString();
        requiredBlocks2.text = rBlocks2.ToString() + "/" + FindObjectOfType<DigManager>().blocksToHaveMax.ToString();
    }
}


