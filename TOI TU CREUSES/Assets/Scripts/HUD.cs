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
    private PlayerController p1Controller;
    private PlayerController p2Controller;

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

    void Start()
    {
        player1 = FindObjectOfType<Player1Controller>().gameObject;
        player2 = FindObjectOfType<Player2Controller>().gameObject;
        p1DigManager = player1.GetComponentInChildren<DigManager>();
        p2DigManager = player2.GetComponentInChildren<DigManager>();
        p1Controller = player1.GetComponentInChildren<PlayerController>();
        p2Controller = player2.GetComponentInChildren<PlayerController>();
        blocks1 = p1DigManager.blocksInDisp;
        blocks2 = p2DigManager.blocksInDisp;
        maxBlocks = p1DigManager.blocksInDispMax;
        bullets1 = p1Controller.currentAmunitionBullet;
        bullets2 = p1Controller.currentAmunitionBullet;
        rBlocks1 = p1DigManager.blocksToHave;
        rBlocks2 = p2DigManager.blocksToHave;
        blocksJ1.text = string.Format("{0:0}/{1:0}", blocks1, maxBlocks);
        blocksJ2.text = string.Format("{0:0}/{1:0}", blocks2, maxBlocks);
        bulletsJ1.text = (bullets1.ToString());
        bulletsJ2.text = (bullets2.ToString());
        requiredBlocks1.text = "Dig " + rBlocks1.ToString() + " more to get a block.";
        requiredBlocks2.text = "Dig " + rBlocks2.ToString() + " more to get a block.";
    }

    void Update()
    {
        blocks1 = p1DigManager.blocksInDisp;
        blocks2 = p2DigManager.blocksInDisp;
        rBlocks1 = p1DigManager.blocksToHaveMax - p1DigManager.blocksToHave;
        rBlocks2 = p2DigManager.blocksToHaveMax - p1DigManager.blocksToHave;
        bullets1 = p1Controller.currentAmunitionBullet;
        bullets2 = p1Controller.currentAmunitionBullet;
        blocksJ1.text = string.Format("{0:0}/{1:0}", blocks1, maxBlocks);
        blocksJ2.text = string.Format("{0:0}/{1:0}", blocks2, maxBlocks);
        bulletsJ1.text = (bullets1.ToString());
        bulletsJ2.text = (bullets2.ToString());
        requiredBlocks1.text = "Dig "+ rBlocks1.ToString()+ " more to get a block.";
        requiredBlocks2.text = "Dig "+ rBlocks2.ToString()+ " more to get a block.";
    }
}


