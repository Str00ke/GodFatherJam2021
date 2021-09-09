using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    public Animator pAnimator;

    Vector2 joyPos;

    public bool modeSwitch;
    protected int player;

    //Inputs
    protected string hrzD, vrtD, actD, subD, cancD, starD;

    [Header("Movements")]
    public int speed;
    public int maxHealth;
    public int health;

    [Header("Shooter")]
    public GameObject shootPrefab;
    public GameObject turret;
    public int currentAmunitionBullet;
    public bool inTurretRange;
    public bool inTurretMode;
    public int nbrAmmoPickedAtOnce;

    [Header("Digger")]
    public pickAxe tool;
    public bool canDig;
    public bool digging;
    protected float angle;
    protected float lastAngle;
    [SerializeField]
    public float timeDigging;

    char[,] tilesStates;
    protected DigManager digManager;

    #region Unity callbacks
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        digManager = FindObjectOfType<DigManager>();
        pAnimator = GetComponent<Animator>();
        health = maxHealth;
    }
    protected void Init()
    {
        SwitchModeController();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        Movement();
    }
    #endregion

    protected virtual void Movement()
    {
        rb.velocity = new Vector2(Input.GetAxis(hrzD), Input.GetAxis(vrtD))* speed * 100 * Time.deltaTime;

        if (rb.velocity != Vector2.zero) pAnimator.SetBool("isWalking", true);
        else pAnimator.SetBool("isWalking", false);

        pAnimator.SetFloat("X", rb.velocity.x);
        pAnimator.SetFloat("Y", rb.velocity.y);
    }
    protected virtual void OnTurret()
    {
        rb.velocity = Vector2.zero;
        joyPos = new Vector2(Input.GetAxis(hrzD), Input.GetAxis(vrtD));
    }

    public virtual void SwitchModeController()
    {
        modeSwitch = !modeSwitch;
        Debug.Log("p : " + player + " modesw : " + modeSwitch);

        if (modeSwitch)
        {
            hrzD = "Horizontal";
            vrtD = "Vertical";
            actD = "Action1";
            subD = "Submit";
            cancD = "Cancel";
            starD = "Start";
        }
        else
        {
            hrzD = "Horizontal2";
            vrtD = "Vertical2";
            actD = "Action2";
            subD = "Submit2";
            cancD = "Cancel2";
            starD = "Start2";
        }
    }

    protected virtual void Shoot()
    {
        Vector2 lookDir = joyPos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        turret.GetComponent<tourretController>().LookDirection(angle, gameObject);

        if (Input.GetButtonDown(actD) && turret != null && currentAmunitionBullet > 0)
        {
            GameObject bullet = Instantiate(shootPrefab, turret.transform.GetChild(0).GetChild(0).GetChild(0).position, Quaternion.AngleAxis(angle - 135f, Vector3.forward));
            turret.GetComponent<tourretController>().ShootAnim();
            bullet.GetComponent<Rigidbody2D>().AddForce(lookDir.normalized * 20f, ForceMode2D.Impulse);
            currentAmunitionBullet--;
            FindObjectOfType<HUD>().VarUpdatesBullets(currentAmunitionBullet, modeSwitch);
        }
    }
    protected virtual void Dig()
    {
        rb.velocity = Vector2.zero;
        digging = true;
        if (joyPos != Vector2.zero) digging = false;

        StartCoroutine(waitToDestroy());
    }
    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(timeDigging);
        if (digging)
        {
            tool.DestroyBloc();
            digManager.OnDig();
        }
        digging = false;
        canDig = false;
    }
    protected virtual void DropDirt()
    {

    }

}
