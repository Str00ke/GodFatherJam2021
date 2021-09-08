using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickAxe : MonoBehaviour
{
    GameObject bloc;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            GetComponentInParent<PlayerController>().canDig = true;
            bloc = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            GetComponentInParent<PlayerController>().canDig = false;
            bloc = null;
        }
    }

    public void DestroyBloc(GameObject bloc)
    {
        Destroy(bloc);

    }

}
