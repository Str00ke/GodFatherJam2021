using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public float minRandTime, maxRandTime;
    GridManager gridManager;

    public int palier;

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
    public void checkPalier(int pal)
    {
        palier = pal;
    }
    void SpawnEnemy()
    {
        Player1Controller digger = FindObjectOfType<Player1Controller>();
        Vector2 pos = GetRandPos(digger);
        switch (palier)
        {
            case 0:
                Instantiate(enemy1, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
                TimerEnemySpawn();
                break;
            case 1:
                Instantiate(enemy1, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
                TimerEnemySpawn();
                break;
            case 2:
                Instantiate(enemy1, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
                TimerEnemySpawn();
                pos = GetRandPos(digger);
                Instantiate(enemy2, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
                TimerEnemySpawn();
                break;
            case 3:
                Instantiate(enemy1, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
                TimerEnemySpawn();
                pos = GetRandPos(digger);
                Instantiate(enemy3, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
                TimerEnemySpawn();
                break;
            case 4:
                Instantiate(enemy1, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
                TimerEnemySpawn();
                pos = GetRandPos(digger);
                Instantiate(enemy2, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
                TimerEnemySpawn();
                pos = GetRandPos(digger);
                Instantiate(enemy3, gridManager.tilePos[(int)pos.x, (int)pos.y], transform.rotation, transform);
                TimerEnemySpawn();
                break;
            default:
                break;
        }
    }

    Vector2 GetRandPos(Player1Controller digger)
    {
        int X = Random.Range(0, gridManager.sizeX);
        int Y = Random.Range(0, gridManager.sizeY);
        if (gridManager.tileState[X, Y] != '#' || (digger.posX == X && digger.posY == Y)) return GetRandPos(digger);
        else return new Vector2(X, Y);
    }
}
