using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public static PlayerController _instance = null;

    public Rigidbody2D rb;
    protected SpriteRenderer sr;
    public Animator pAnimator;

    //public Transform movePoint;
    Vector2 mousePos;

    public bool canDig;

    protected bool modeSwitch;
    protected int player;

    [Header("Movements")]
    public int speed;
    public int maxHealth;
    public int health;

    [Header("Bomber")]
    public GameObject bombPrefab;

    protected GameObject turret;


    #region Unity callbacks
    protected virtual void Awake()
    {
        //if (_instance != null && _instance != this)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    _instance = this;
        //}
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pAnimator = GetComponent<Animator>();
        health = maxHealth;
        //movePoint.parent = null;
    }

    protected virtual void SwitchModeController()
    {
        modeSwitch = !modeSwitch;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.G)) SwitchModeController();
    }
    #endregion
    float angle;
    float lastAngle;
    Quaternion lastRotation;
    protected virtual void Movement()
    {
        //float h;
        //float v;
        //if (modeSwitch)
        //{
        //    if (Input.GetAxis("Vertical") == 0f)
        //        h = Input.GetAxis("Horizontal");
        //    else h = 0f;

        //    if (Input.GetAxis("Horizontal") == 0f)
        //        v = Input.GetAxis("Vertical");
        //    else v = 0f;
        //}else
        //{
        //    if (Input.GetAxis("Vertical") == 0f)
        //        h = Input.GetAxis("Horizontal");
        //    else h = 0f;

        //    if (Input.GetAxis("Horizontal") == 0f)
        //        v = Input.GetAxis("Vertical");
        //    else v = 0f;
        //}
        if (modeSwitch)
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * 100 * Time.deltaTime;
        else rb.velocity = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")) * speed * 100 * Time.deltaTime;

        if (rb.velocity == Vector2.zero) angle = lastAngle;
        else
        {
            angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            lastAngle = angle;
        }
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //rb.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        //if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        //{
        //    if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        //    {
        //        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

        //    }
        //    else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        //    {
        //        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
        //    }
        //}

    }
    protected virtual void Shoot()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;

        if (Input.GetKeyDown(KeyCode.Space) && turret != null)
        {
            //Vector2 pos = transform.position;
            //pos.x = Mathf.Round(pos.x);
            //pos.y = Mathf.Round(pos.y);

            GameObject bullet = Instantiate(bombPrefab, transform.position, Quaternion.AngleAxis(angle + 90f, Vector3.forward));
            bullet.GetComponent<Rigidbody2D>().AddForce(lookDir.normalized * 20f, ForceMode2D.Impulse);
        }
        
    }
    protected virtual void Dig()
    {
        if (Input.GetMouseButtonDown(0) && canDig == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            try
            {
                if (hit.transform.gameObject.layer == 9)
                {

                    Destroy(hit.transform.gameObject);

                }
            }
            catch { }
        }

    }
}
