using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _instance = null;

    public Rigidbody2D rb;
    protected SpriteRenderer sr;
    public Animator pAnimator;

    public Transform movePoint;

    [Header("Movements")]
    public int speed;
    public int maxHealth;
    public int health;

    #region Unity callbacks
    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pAnimator = GetComponent<Animator>();
        health = maxHealth;

        movePoint.parent = null;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement();
    }
    #endregion
    protected virtual void Movement()
    {
        rb.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                
            }else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }
        }

    }
    protected virtual void DropBomb()
    {

    }
    protected virtual void Dig()
    {

    }

}
