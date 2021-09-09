using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GridManager grid;
    public GameObject P1, P2;
    public void setControlsCharacter(GameObject player, bool isFirst)
    {
        if(isFirst)
        P1 = player;
        else
        P2 = player;
    }
    public void swapControlsCharacter()
    {
        P1.GetComponent<Player1Controller>().SwitchModeController();
        P2.GetComponent<Player2Controller>().SwitchModeController();
    }
}
