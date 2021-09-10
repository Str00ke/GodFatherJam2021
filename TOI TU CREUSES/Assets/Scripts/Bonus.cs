using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBonusType
{
    DSpeed,
    DPickUpAmmo,
    DDigSpeed,
    SBulletsSizes,
    SwapCorps
}
public class Bonus : MonoBehaviour
{
    public EBonusType bonusType;
    public float durationTime;
    public int effectMultiplier;
    Player1Controller digger;
    Player2Controller shooter;
    BonusManager BonusMgr;
    public GameObject bombPrefab;
    Coroutine depopCoroutine = null;
    int _speed;
    float _digSpeed;
    bool _ammoB;
    Vector2 _bulletSize;

    private void Start()
    {
        digger = FindObjectOfType<Player1Controller>();
        shooter = FindObjectOfType<Player2Controller>();
        BonusMgr = FindObjectOfType<BonusManager>();
        Init();
        //Debug.Log("APPEAR" + gameObject.name);
    }
    private void Init()
    {
        _speed = digger.speed;
        _digSpeed = digger.timeDigging;
        _ammoB = false;
        _bulletSize = Vector2.one;
        BonusMgr.GetValue(_speed, _digSpeed, _ammoB);
    }

    //public void PrepareTimer(float value)
    //{
    //    depopCoroutine = StartCoroutine(TimeBeforeDepop(value));
    //}

    //public IEnumerator TimeBeforeDepop(float value)
    //{
    //    yield return new WaitForSeconds(value);
    //    //Debug.Log("DEPOP" + gameObject.name);
    //    DestroyBonus();
    //}

    public void ApplyEffect()
    {
        //Debug.Log("APPLY" + gameObject.name);
        //StopCoroutine(depopCoroutine);
        switch (bonusType)
        {
            case EBonusType.DSpeed:
                digger.speed += 4;
                BonusMgr.RemoveBonus(0, durationTime, gameObject);
                break;
            case EBonusType.DPickUpAmmo:
                //digger.nbrAmmoPickedAtOnce *= effectMultiplier;
                FindObjectOfType<DigManager>().bonusAmmo = true;
                BonusMgr.RemoveBonus(1, durationTime, gameObject);
                break;
            case EBonusType.DDigSpeed:
                digger.timeDigging /= effectMultiplier;
                BonusMgr.RemoveBonus(2, durationTime, gameObject);
                break;
            case EBonusType.SBulletsSizes:
                digger.shootPrefab = bombPrefab;
                BonusMgr.RemoveBonus(3, durationTime, gameObject);
                break;
            case EBonusType.SwapCorps:
                FindObjectOfType<GameManager>().swapControlsCharacter();
                //Debug.Log("BonusSWAP!");
                Destroy(gameObject);
                break;

        }
        //transform.GetChild(0).gameObject.SetActive(false);
        //StartCoroutine(Timer());
    }


    //IEnumerator Timer()
    //{
    //    yield return new WaitForSeconds(durationTime);
    //    //RemoveEffect();
    //}

    //public void RemoveEffect()
    //{
    //    //Debug.Log("REMOVE" + gameObject.name);
    //    switch (bonusType)
    //    {
    //        case EBonusType.DSpeed:
    //            digger.speed /= effectMultiplier;
    //            break;

    //        case EBonusType.DPickUpAmmo:
    //            digger.nbrAmmoPickedAtOnce /= effectMultiplier;
    //            break;

    //        case EBonusType.DDigSpeed:
    //            digger.timeDigging *= effectMultiplier;
    //            break;

    //        case EBonusType.SBulletsSizes:
    //            digger.shootPrefab = bulletPrefab;
    //            break;
    //    }

    //    DestroyBonus();
    //}

    //void DestroyBonus()
    //{
    //    //StopAllCoroutines();
    //    //Debug.Log("DESTROY" + gameObject.name);
    //    //Destroy(transform.gameObject);
    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            ApplyEffect();
    }

}
