using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesController : MonoBehaviour
{
    private Animator pAnimator;
    public Transform target;
    Rigidbody2D rb;
    public float speed;
    public bool isOut;
    public bool isRed;
    bool hasAttacked;
    float distAttack;
    public enum StateMove { MOVE, ATTACK }
    public StateMove currentState;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pAnimator = GetComponent<Animator>();
        currentState = StateMove.MOVE;
        isOut = false;
        if (isRed) distAttack = 1f;
        else distAttack = 0.2f;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player1Controller>().transform;
        }
        else
        {
            if (isOut)
            {
                switch (currentState)
                {
                    case StateMove.MOVE:
                        MoveToTarget();
                        break;
                    case StateMove.ATTACK:
                        Attack();
                        break;
                    default:
                        break;
                }
                MoveToTarget();
            }
        }
        pAnimator.SetFloat("XVel", rb.velocity.x);
        pAnimator.SetFloat("YVel", rb.velocity.y);
    }

    public void MoveToTarget()
    {
        hasAttacked = false;
        if(Vector2.Distance(target.position, transform.position) > distAttack)
        {
            rb.velocity = new Vector2(target.transform.position.x - rb.transform.position.x, target.transform.position.y - rb.transform.position.y).normalized * speed * 100 * Time.deltaTime;
        }else currentState = StateMove.ATTACK;

    }
    public void Attack()
    {
        if (!hasAttacked)
        {
            rb.velocity = Vector2.zero;
            if(isRed)
                StartCoroutine(ZoneToDestroy());
            else
                StartCoroutine(waitToMove());
        }
    }
    IEnumerator waitToMove()
    {
        hasAttacked = true;
        pAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f);
        currentState = StateMove.MOVE;
    }
    IEnumerator ZoneToDestroy()
    {
        hasAttacked = true;
        pAnimator.SetTrigger("AttackZone");
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        transform.GetChild(1).gameObject.SetActive(false);
        currentState = StateMove.MOVE;
    }
    private void OnEnable()
    {
        StartCoroutine(waitToRUN());
    }
    IEnumerator waitToRUN()
    {
        yield return new WaitForSeconds(0.6f);
        isOut = true;
    }
    bool contact;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = true;
            currentState = StateMove.ATTACK;
            StartCoroutine(waitToKill());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = false;
        }
    }

    IEnumerator waitToKill()
    {
        yield return new WaitForSeconds(0.2f);
        if(currentState == StateMove.ATTACK && contact) SceneManager.LoadScene(0);
    }
}
