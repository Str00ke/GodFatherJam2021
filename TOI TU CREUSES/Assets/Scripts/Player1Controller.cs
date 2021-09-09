using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : PlayerController
{
    // Digger
    public List<GameObject> dirt;
    float tileUp, tileDown, tileLeft, tileRight;
    public GameObject test;
    GameObject test2;
    [SerializeField]
    public int posX, posY;
    public bool canPlaceBlock;
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetStartTile();
        SetStartPos();
        test2 = Instantiate(test, transform.position, transform.rotation);
        player = 0;
        modeSwitch = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (canDig) Dig();
        //Debug.Log(posX + "  " + posY + "   " + FindObjectOfType<GridManager>().tilePos[posX, posY]);
    }

    protected override void Movement()
    {
        base.Movement();

        float pX = transform.position.x;
        float pY = transform.position.y;
        if (pY > tileUp && pX < tileRight && pX > tileLeft) //up
        {
            UpdateOnTile(1);
        }
        else if (pY < tileDown && pX < tileRight && pX > tileLeft) //down
        {
            UpdateOnTile(2);
        }
        else if (pX < tileLeft && pY < tileUp && pY > tileDown) //left
        {
            UpdateOnTile(3);
        }
        else if (pX > tileRight && pY < tileUp && pY > tileDown) //right
        {
            UpdateOnTile(4);
        }
                /*else if (pX < tileLeft && pY > tileUp && pX < tileRight && pY > tileDown) //ul
        {
            UpdateOnTile(5);
        }
        else if (pX > tileRight && pY > tileUp) //ur
        {
            UpdateOnTile(6);
        }
        else if (pX < tileLeft && pY < tileDown) //dl
        {
            UpdateOnTile(7);
        }
        else if (pX > tileRight && pY < tileDown) //dr
        {
            UpdateOnTile(8);
        }*/

    }

    public void SetStartTile()
    {
        Vector2 pos = transform.position;
        tileUp = pos.y + 0.5f;
        tileDown = pos.y - 0.5f;
        tileLeft = pos.x - 0.5f;
        tileRight = pos.x + 0.5f;
    }

    public void SetStartPos()
    {
        posX = FindObjectOfType<GridManager>().tmpDigPosX;
        posY = FindObjectOfType<GridManager>().tmpDigPosY;
    }


    void UpdateOnTile(int dir)
    {
            switch (dir)  //in order: up down left right ul ur dl dr
            {
            case 0:
                break;
    
                case 1:
                    tileUp += 1.0f;
                    tileDown += 1.0f;
                    posY--;
                    test2.transform.position = new Vector2(test2.transform.position.x, test2.transform.position.y + 1);
                break;
    
                case 2:
                    tileUp -= 1.0f;
                    tileDown -= 1.0f;
                    posY++;
                    test2.transform.position = new Vector2(test2.transform.position.x, test2.transform.position.y - 1);
                break;
    
                case 3:
                    tileLeft -= 1.0f;
                    tileRight -= 1.0f;
                    posX--;
                    test2.transform.position = new Vector2(test2.transform.position.x - 1, test2.transform.position.y);
                break;
    
                case 4:
                    tileLeft += 1.0f;
                    tileRight += 1.0f;
                    posX++;
                    test2.transform.position = new Vector2(test2.transform.position.x + 1, test2.transform.position.y);
                break;
    
                /*case 5:
                tileUp += 1.0f;
                tileDown += 1.0f;
                tileLeft -= 1.0f;
                tileRight -= 1.0f;
                test2.transform.position = new Vector2(test2.transform.position.x - 1, test2.transform.position.y + 1);
                Debug.Log("UPLEFT");
                break;

            case 6:
                tileUp += 1.0f;
                tileDown += 1.0f;
                tileLeft += 1.0f;
                tileRight += 1.0f;
                test2.transform.position = new Vector2(test2.transform.position.x + 1, test2.transform.position.y + 1);
                Debug.Log("UPRIGHT");
                break;

            case 7:
                tileUp -= 1.0f;
                tileDown -= 1.0f;
                tileLeft -= 1.0f;
                tileRight -= 1.0f;
                test2.transform.position = new Vector2(test2.transform.position.x - 1, test2.transform.position.y - 1);
                Debug.Log("DOWNLEFT");
                break;

            case 8:
                tileUp -= 1.0f;
                tileDown -= 1.0f;
                tileLeft += 1.0f;
                tileRight += 1.0f;
                test2.transform.position = new Vector2(test2.transform.position.x + 1, test2.transform.position.y - 1);
                Debug.Log("DOWNRIGHT");
                break;*/
            }
        Debug.Log(posX + "  " + posY);
        //Debug.Log(tileUp + " " + tileDown + " " + tileLeft + " " + tileRight);
    }

    protected override void DropDirt()
    {
        base.DropDirt();
        if (!canPlaceBlock) return;
        digManager.OnPlacingBlock();
        switch (transform.localRotation.eulerAngles.z)
        {
            case 90: //up
                Instantiate(digManager.dirtBlock, FindObjectOfType<GridManager>().tilePos[posX, posY - 1], transform.rotation);
                break;

            case 180: //left
                Instantiate(digManager.dirtBlock, FindObjectOfType<GridManager>().tilePos[posX - 1, posY], transform.rotation);
                break;

            case 270: //down
                Instantiate(digManager.dirtBlock, FindObjectOfType<GridManager>().tilePos[posX, posY + 1], transform.rotation);
                break;

            case 0: //right
                Instantiate(digManager.dirtBlock, FindObjectOfType<GridManager>().tilePos[posX + 1, posY], transform.rotation);
                break;
        }
    }
}
