using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;

    private DigManager p1DigManager;
    private DigManager p2DigManager;

    public Text blocksJ1;
    public Text blocksJ2;
    public Text bulletsJ1;
    public Text bulletsJ2;
    public Text requiredBlocks1;
    public Text requiredBlocks2;

    private float maxBullets = 8;
    private float maxBlocks = 3;

    private float blocks1;
    private float blocks2;
    private float bullets1 = 0;
    private float bullets2 = 0;
    private float rBlocks1;
    private float rBlocks2;

    void Start()
    {
        player1 = FindObjectOfType<Player1Controller>().gameObject;
        player2 = FindObjectOfType<Player2Controller>().gameObject;
        p1DigManager = player1.GetComponentInChildren<DigManager>();
        p2DigManager = player2.GetComponentInChildren<DigManager>();
        blocks1 = p1DigManager.blocksInDisp;
        blocks2 = p2DigManager.blocksInDisp;
        rBlocks1 = p1DigManager.blocksToHave;
        rBlocks2 = p2DigManager.blocksToHave;
        blocksJ1.text = string.Format("{0:0}/{1:0}", blocks1, maxBlocks);
        blocksJ2.text = string.Format("{0:0}/{1:0}", blocks2, maxBlocks);
        bulletsJ1.text = string.Format("{0:0}/{1:0}", bullets1, maxBullets);
        bulletsJ2.text = string.Format("{0:0}/{1:0}", bullets2, maxBullets);
        requiredBlocks1.text = string.Format("Mine ", blocks1, " more to get a block.");
        requiredBlocks2.text = string.Format("Mine ", blocks2, " more to get a block.");
    }

    void Update()
    {
        blocksJ1.text = string.Format("{0:0}/{1:0}", blocks1, maxBlocks);
        blocksJ2.text = string.Format("{0:0}/{1:0}", blocks2, maxBlocks);
        bulletsJ1.text = string.Format("{0:0}/{1:0}", bullets1, maxBullets);
        bulletsJ2.text = string.Format("{0:0}/{1:0}", bullets2, maxBullets);
    }
}


