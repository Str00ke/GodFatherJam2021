using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    public Animator pAnimator;

    Vector2 joyPos;

    protected bool modeSwitch;
    protected int player;

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

    protected virtual void SwitchModeController()
    {
        modeSwitch = !modeSwitch;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.G)) SwitchModeController();
    }
    #endregion

    protected virtual void Movement()
    {
        if (modeSwitch)
            rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * 100 * Time.deltaTime;
        else rb.velocity = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2")) * speed * 100 * Time.deltaTime;

        if (rb.velocity != Vector2.zero) pAnimator.SetBool("isWalking", true);
        else pAnimator.SetBool("isWalking", false);

        pAnimator.SetFloat("X", rb.velocity.x);
        pAnimator.SetFloat("Y", rb.velocity.y);
    }
    protected virtual void OnTurret()
    {
        rb.velocity = Vector2.zero;

        if (modeSwitch)
            joyPos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        else joyPos = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
    }
    protected virtual void Shoot()
    {
        Vector2 lookDir = joyPos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        turret.GetComponent<tourretController>().LookDirection(angle, gameObject);

        if (Input.GetButtonDown("Action2") && turret != null && currentAmunitionBullet > 0)
        {
            GameObject bullet = Instantiate(shootPrefab, turret.transform.position, Quaternion.AngleAxis(angle - 135f, Vector3.forward));
            turret.GetComponent<tourretController>().ShootAnim();
            bullet.GetComponent<Rigidbody2D>().AddForce(lookDir.normalized * 20f, ForceMode2D.Impulse);
            currentAmunitionBullet--;
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
