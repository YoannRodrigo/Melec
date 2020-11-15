using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{

    public GameObject hpUI;

    public Sprite hpEmpty;

    public Sprite hpFull;

    public void UpdateHpUI()
    {
        float remainingHp = GetComponent<PlayerManager>().life;
        int hpDiff = 4 - Mathf.CeilToInt((remainingHp / 25));
        print(hpDiff);
        if (hpDiff > 0)
        {
            for (int i = 0; i < hpDiff; i++)
            {
                hpUI.transform.Find("Links").GetChild(i).GetComponent<Image>().DOFade(0, .3f);
                hpUI.transform.Find("Hits").GetChild(i).GetComponent<Image>().sprite = hpEmpty;
                hpUI.transform.Find("Hits").GetChild(i).GetComponent<Image>().DOFade(0, .2f);
            }
        }


    }

}
