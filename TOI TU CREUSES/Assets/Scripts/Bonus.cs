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
    public float effectMultiplier;
    Player1Controller digger;
    Player2Controller shooter;

    private void Start()
    {
        digger = FindObjectOfType<Player1Controller>();
        shooter = FindObjectOfType<Player2Controller>();
    }

    public void ApplyEffect()
    {
        switch (bonusType)
        {
            case EBonusType.DSpeed:
                digger.speed *= 2;
                break;

            case EBonusType.DPickUpAmmo:
                digger.nbrAmmoPickedAtOnce *= 2;
                break;

            case EBonusType.DDigSpeed:
                //digger.
                break;

            case EBonusType.SBulletsSizes:
                break;
        }

        Timer();
    }


    IEnumerator Timer() {
        yield return new WaitForSeconds(durationTime);
        RemoveEffect();
    }

    public void RemoveEffect()
    {

    }
}
