using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GridManager grid;
    public GameObject P1, P2;
    public GameObject bullet;
    public HUD _hud;
    public bool ready;
    public void setControlsCharacter(GameObject player, bool isFirst)
    {
        if(isFirst)
        P1 = player;
        else
        P2 = player;

        ready = true;
    }
    public void swapControlsCharacter()
    {
        P1.GetComponent<Player1Controller>().SwitchModeController();
        P2.GetComponent<Player2Controller>().SwitchModeController();
    }


    void Start()
    {
        _hud.HudTextUpdates();
    }
}
