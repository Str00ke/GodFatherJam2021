using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public GameObject enemy;
    public float minRandTime, maxRandTime;
    GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        TimerEnemySpawn();
    }

    void TimerEnemySpawn()
    {
        float timer = Random.Range(minRandTime, maxRandTime);
        StartCoroutine(Chrono(timer));
    }

    IEnumerator Chrono(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        Player1Controller digger = FindObjectOfType<Player1Controller>();
        Vector2 pos = GetRandPos(digger);
        Instantiate(enemy, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
        TimerEnemySpawn();
    }

    Vector2 GetRandPos(Player1Controller digger)
    {
        int X = Random.Range(0, gridManager.sizeX);
        int Y = Random.Range(0, gridManager.sizeY);
        if (gridManager.tileState[X, Y] != '#' || (digger.posX == X && digger.posY == Y)) return GetRandPos(digger);
        else return new Vector2(X, Y);
    }
}
