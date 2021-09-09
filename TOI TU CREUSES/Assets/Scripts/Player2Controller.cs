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
        modeSwitch = false;
        currentAmunitionBullet = 30;
    }

    // Update is called once per frame
    protected override void Update()
    {
        switch (inTurretMode)
        {
            case true:
                Shoot();
                OnTurret();
                break;
            case false:
                Movement();
                break;
            default:
                break;
        }
    }

    
}
