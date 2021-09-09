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

    private float maxBullets = 8;
    private float maxBlocks = 3;

    private float blocks1 = 0;
    private float blocks2 = 0;
    private float bullets1 = 0;
    private float bullets2 = 0;

    void Start()
    {
        blocksJ1.text = string.Format("{0:0}/{1:0}", blocks1, maxBlocks);
        blocksJ2.text = string.Format("{0:0}/{1:0}", blocks2, maxBlocks);
        bulletsJ1.text = string.Format("{0:0}/{1:0}", bullets1, maxBullets);
        bulletsJ2.text = string.Format("{0:0}/{1:0}", bullets2, maxBullets);
    }

    void Update()
    {
        blocksJ1.text = string.Format("{0:0}/{1:0}", blocks1, maxBlocks);
        blocksJ2.text = string.Format("{0:0}/{1:0}", blocks2, maxBlocks);
        bulletsJ1.text = string.Format("{0:0}/{1:0}", bullets1, maxBullets);
        bulletsJ2.text = string.Format("{0:0}/{1:0}", bullets2, maxBullets);
    }
}


