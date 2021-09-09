using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    private Animator pAnimator;
    public Transform target;
    Rigidbody2D rb;
    public float speed;
    public bool isOut;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pAnimator = GetComponent<Animator>();
        isOut = false;
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
            if(isOut)MoveToTarget();
        }
        pAnimator.SetFloat("XVel", rb.velocity.x);
        pAnimator.SetFloat("YVel", rb.velocity.y);
    }

    public void MoveToTarget()
    {
        rb.velocity = new Vector2(target.transform.position.x - rb.transform.position.x, target.transform.position.y - rb.transform.position.y).normalized * speed * 100 * Time.deltaTime;
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
}
