using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : PlayerController
{
    //Shooter
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = 1;
        modeSwitch = true;
        Init();
        FindObjectOfType<GameManager>().setControlsCharacter(gameObject, modeSwitch);
        currentAmunitionBullet = 2;
        FindObjectOfType<HUD>().VarUpdatesBullets(currentAmunitionBullet, modeSwitch);
    }
    // Update is called once per frame
    protected override void Update()
    {
        switch (inTurretMode)
        {
            case true:
                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                Shoot();
                OnTurret();
                break;
            case false:
                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
                Movement();
                break;
            default:
                break;
        }
        if (inTurretRange)
        {
            if (Input.GetButtonDown(subD)) inTurretMode = true;
            if (Input.GetButtonDown(cancD)) inTurretMode = false;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            turret = collision.gameObject;
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            inTurretRange = true;
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            inTurretRange = false;
            turret = null;
        }
    }

   
}
