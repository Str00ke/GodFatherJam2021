using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text blocksJ1;
    public Text blocksJ2;
    public Text bulletsJ1;
    public Text bulletsJ2;
    public Text requiredBlocks1;
    public Text requiredBlocks2;

    private float maxBlocks;
    private float blocks1;
    private float blocks2;
    private float bullets1;
    private float bullets2;
    private float rBlocks1;
    private float rBlocks2;

    public Image barJ1;
    public Image barJ2;
    public Image shovelJ1;
    public Image shovelJ2;
    public GameObject Icon1;
    public GameObject Icon2;
    public float counter;

    [HideInInspector]
    public bool isDigging;

    public void VarUpdatesBlocks(float newValue, bool player)
    {
        if (player)
        {
            blocks1 = newValue;
        }
        else
        {
            blocks2 = newValue;
        }
        HudTextUpdates();
    }

    public void VarUpdatesrBlocks(float newValue, bool player)
    {
        if (player)
        {
            rBlocks1 = newValue;
        }
        else
        {
            rBlocks2 = newValue;
        }
        HudTextUpdates();
    }

    public void VarUpdatesBullets(float newValue, bool player)
    {
        if (player)
        {
            bullets1 += newValue;
        }
        else
        {
            bullets2 += newValue;
        }
        HudTextUpdates();
    }

    private void Update()
    {
        if (FindObjectOfType<GameManager>().ready)
        {
            switch (isDigging)
            {
                case true:
                    StartAnimDig();
                    break;
                case false:
                    ResetAnimDig();
                    break;
                default:
                    break;
            }
        }


    }
    public void StartAnimDig()
    {
        counter += Time.deltaTime;
        if (FindObjectOfType<GameManager>().P1.GetComponent<PlayerController>().modeSwitch)
        {
            barJ1.GetComponent<Image>().fillAmount = Mathf.Lerp(1, 0, counter / 0.8f);
            shovelJ1.rectTransform.anchoredPosition = Vector3.Lerp(new Vector3(20, shovelJ1.rectTransform.anchoredPosition.y), new Vector3(115, shovelJ1.rectTransform.anchoredPosition.y), counter / 0.8f);
            if (barJ1.GetComponent<Image>().fillAmount == 0) isDigging = false;
        }
        else
        {
            barJ2.GetComponent<Image>().fillAmount = Mathf.Lerp(1, 0, counter / 0.8f);
            shovelJ2.rectTransform.anchoredPosition = Vector3.Lerp(new Vector3(110, shovelJ2.rectTransform.anchoredPosition.y), new Vector3(13, shovelJ2.rectTransform.anchoredPosition.y), counter / 0.8f);
            if (barJ2.GetComponent<Image>().fillAmount == 0) isDigging = false;
        }
    }
    public void ResetAnimDig()
    {
        counter = 0;
        if (FindObjectOfType<GameManager>().P1.GetComponent<PlayerController>().modeSwitch)
        {
            barJ1.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, 1);
            shovelJ1.rectTransform.anchoredPosition = Vector3.Lerp(new Vector3(115, shovelJ1.rectTransform.anchoredPosition.y), new Vector3(20, shovelJ1.rectTransform.anchoredPosition.y), 1);
        }
        else
        {
            barJ2.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, 1);
            shovelJ2.rectTransform.anchoredPosition = Vector3.Lerp(new Vector3(13, shovelJ2.rectTransform.anchoredPosition.y), new Vector3(110, shovelJ2.rectTransform.anchoredPosition.y), 1);
        }
    }
    public void swapIcon()
    {
        if (FindObjectOfType<Player1Controller>().modeSwitch)
        {
            Icon1.transform.GetChild(0).gameObject.SetActive(false);
            Icon1.transform.GetChild(1).gameObject.SetActive(true);
            Icon2.transform.GetChild(0).gameObject.SetActive(true);
            Icon2.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            Icon1.transform.GetChild(0).gameObject.SetActive(true);
            Icon1.transform.GetChild(1).gameObject.SetActive(false);
            Icon2.transform.GetChild(0).gameObject.SetActive(false);
            Icon2.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void HudTextUpdates()
    {
        blocksJ1.text = blocks1 + "/" + FindObjectOfType<DigManager>().blocksInDispMax;
        blocksJ2.text = blocks2 + "/" + FindObjectOfType<DigManager>().blocksInDispMax;
        bulletsJ1.text = bullets1.ToString();
        bulletsJ2.text = bullets2.ToString();
        requiredBlocks1.text = rBlocks1.ToString() + "/" + FindObjectOfType<DigManager>().blocksToHaveMax.ToString();
        requiredBlocks2.text = rBlocks2.ToString() + "/" + FindObjectOfType<DigManager>().blocksToHaveMax.ToString();
    }
}


