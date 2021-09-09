using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBonusType
{
    DSpeed,
    DPickUpAmmo,
    DDigSpeed,
    SBulletsSizes
}
public class Bonus : MonoBehaviour
{
    public EBonusType bonusType;
    public float durationTime;
    public int effectMultiplier;
    Player1Controller digger;
    Player2Controller shooter;
    Coroutine depopCoroutine = null;

    private void Start()
    {
        digger = FindObjectOfType<Player1Controller>();
        shooter = FindObjectOfType<Player2Controller>();
        //Debug.Log("APPEAR" + gameObject.name);
    }


    public void PrepareTimer(float value) 
    {
        depopCoroutine = StartCoroutine(TimeBeforeDepop(value));
    }

    public IEnumerator TimeBeforeDepop(float value)
    {
        yield return new WaitForSeconds(value);
        //Debug.Log("DEPOP" + gameObject.name);
        DestroyBonus();
    }

    public void ApplyEffect()
    {
        //Debug.Log("APPLY" + gameObject.name);
        StopCoroutine(depopCoroutine);
        switch (bonusType)
        {
            case EBonusType.DSpeed:
                digger.speed *= effectMultiplier;
                break;

            case EBonusType.DPickUpAmmo:
                digger.nbrAmmoPickedAtOnce *= effectMultiplier;
                break;

            case EBonusType.DDigSpeed:
                digger.timeDigging /= effectMultiplier;
                break;

            case EBonusType.SBulletsSizes:
                FindObjectOfType<GameManager>().bullet.transform.localScale *= effectMultiplier;
                break;
        }
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        StartCoroutine(Timer());
    }


    IEnumerator Timer() {
        yield return new WaitForSeconds(durationTime);
        RemoveEffect();
    }

    public void RemoveEffect()
    {
        switch (bonusType)
        {
            case EBonusType.DSpeed:
                digger.speed /= effectMultiplier;
                break;

            case EBonusType.DPickUpAmmo:
                digger.nbrAmmoPickedAtOnce /= effectMultiplier;
                break;

            case EBonusType.DDigSpeed:
                digger.timeDigging *= effectMultiplier;
                break;

            case EBonusType.SBulletsSizes:
                FindObjectOfType<GameManager>().bullet.transform.localScale /= effectMultiplier;
                break;
        }

        DestroyBonus();
    }

    void DestroyBonus()
    {
        StopAllCoroutines();
        //Debug.Log("DESTROY" + gameObject.name);
        Destroy(transform.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyEffect();
    }

}
