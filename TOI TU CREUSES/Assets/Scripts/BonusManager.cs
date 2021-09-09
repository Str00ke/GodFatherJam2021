using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public GameObject[] bonusPool;
    GridManager gridManager;
    public float minRandTime, maxRandTime;
    public float timeBeforeDepop;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        TimerBonusSpawn();
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
        go.GetComponent<Bonus>().PrepareTimer(timeBeforeDepop);
        TimerBonusSpawn();
    }

    Vector2 GetRandPos(Player1Controller digger)
    {
        int X = Random.Range(0, gridManager.sizeX);
        int Y = Random.Range(0, gridManager.sizeY);
        if (gridManager.tileState[X, Y] != '#' || (digger.posX == X && digger.posY == Y)) return GetRandPos(digger);
        else return new Vector2(X, Y);
    }
}
