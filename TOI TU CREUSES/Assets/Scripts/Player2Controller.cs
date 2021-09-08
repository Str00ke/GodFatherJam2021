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
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Shoot();
    }
    protected override void Movement()
    {
        float h;
        if (Input.GetAxisRaw("Vertical2") == 0f)
            h = Input.GetAxisRaw("Horizontal2");
        else h = 0f;

        float v;
        if (Input.GetAxisRaw("Horizontal2") == 0f)
            v = Input.GetAxisRaw("Vertical2");
        else v = 0f;

        rb.velocity = new Vector2(h, v) * speed;

        //rb.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        //if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        //{
        //    if (Mathf.Abs(Input.GetAxisRaw("Horizontal2")) == 1f)
        //    {
        //        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal2"), 0f, 0f);

        //    }
        //    else if (Mathf.Abs(Input.GetAxisRaw("Vertical2")) == 1f)
        //    {
        //        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical2"), 0f);
        //    }
        //}
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
