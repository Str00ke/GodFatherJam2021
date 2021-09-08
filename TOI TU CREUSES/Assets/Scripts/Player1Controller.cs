using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : PlayerController
{
    // Digger
    public List<GameObject> dirt;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = 0;
        modeSwitch = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (canDig) Dig();
    }

    protected override void Movement()
    {
        base.Movement();
    }

}
