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
    private void Start()
    {
        _hud.HudTextUpdates();
        Time.timeScale = 0;
    }
    public void setControlsCharacter(GameObject player, bool isFirst)
    {
        if (isFirst)
            P1 = player;
        else
            P2 = player;

        ready = true;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
    }
    public void swapControlsCharacter()
    {

        FindObjectOfType<Player1Controller>().SwitchModeController();
        FindObjectOfType<Player1Controller>().pAnimator.SetTrigger("TP");
        FindObjectOfType<Player2Controller>().SwitchModeController();
        FindObjectOfType<Player2Controller>().pAnimator.SetTrigger("TP");
        _hud.SwapValues();
        _hud.swapIcon();
    }


}
