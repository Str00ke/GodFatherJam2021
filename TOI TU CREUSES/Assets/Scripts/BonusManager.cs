using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public GameObject[] bonusPool;
    GridManager gridManager;
    public float minRandTime, maxRandTime;
    public float timeBeforeDepop;

    public GameObject bulletPrefab;
    int _speed;
    float _digSpeed;
    bool _ammoB;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        //TimerBonusSpawn();
    }

    void TimerBonusSpawn()
    {
        float timer = Random.Range(minRandTime, maxRandTime);
        StartCoroutine(Chrono(timer));
    }

    IEnumerator Chrono(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnBonus();
    }

    void SpawnBonus()
    {
        GameObject chooseRandBonus = bonusPool[Random.Range(0, bonusPool.Length)];
        Player1Controller digger = FindObjectOfType<Player1Controller>();
        Vector2 pos = GetRandPos(digger);
        GameObject go = Instantiate(chooseRandBonus, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
        //go.GetComponent<Bonus>().PrepareTimer(timeBeforeDepop);
        TimerBonusSpawn();
    }

    Vector2 GetRandPos(Player1Controller digger)
    {
        int X = Random.Range(0, gridManager.sizeX);
        int Y = Random.Range(0, gridManager.sizeY);
        if (gridManager.tileState[X, Y] != '#' || (digger.posX == X && digger.posY == Y)) return GetRandPos(digger);
        else return new Vector2(X, Y);
    }
    public void SpawnerBonus(Vector2 pos)
    {
        GameObject chooseRandBonus = bonusPool[Random.Range(0, bonusPool.Length)];
        Player1Controller digger = FindObjectOfType<Player1Controller>();
        Vector2 newPos;
        newPos.x = Mathf.Round(pos.x);
        newPos.y = Mathf.Round(pos.y);
        GameObject go = Instantiate(chooseRandBonus, newPos, transform.rotation, transform);
        //go.GetComponent<Bonus>().PrepareTimer(timeBeforeDepop);

    }
    public void GetValue(int sp, float dSp, bool ammo)
    {
        _speed = sp; _digSpeed = dSp; _ammoB = ammo;
    }
    public void RemoveBonus(int nbBonus, float counter, GameObject Bonus)
    {
        StartCoroutine(BonusDuration((int)counter, nbBonus));
        Destroy(Bonus);
        
    }
    IEnumerator BonusDuration(int timing, int nbBonus)
    {
        yield return new WaitForSeconds(timing);
        switch (nbBonus)
        {
            case 0:
                FindObjectOfType<Player1Controller>().speed = _speed;
                //Debug.Log("WANTS TO: " + _speed);
                //Debug.Log("IS TO: " + FindObjectOfType<Player1Controller>().speed);
                break;
            case 1:
                FindObjectOfType<DigManager>().bonusAmmo = false;
                break;
            case 2:
                FindObjectOfType<Player1Controller>().timeDigging = _digSpeed;
                //Debug.Log("WANTS TO: " + _digSpeed);
                //Debug.Log("IS TO: " + FindObjectOfType<Player1Controller>().timeDigging);
                break;
            case 3:
                FindObjectOfType<Player1Controller>().shootPrefab = bulletPrefab;
                break;
            default:
                break;
        }
    }
}
