using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : PlayerController
{
    //Bomber
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = 1;
        modeSwitch = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Shoot();
    }
    protected override void Movement()
    {
        base.Movement();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            turret = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            turret = null;
        }
    }
}
