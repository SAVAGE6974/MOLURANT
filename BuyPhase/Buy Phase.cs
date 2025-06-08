using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPhase : MonoBehaviour
{
    public GameObject buyPhase;

    private void Awake()
    {
        buyPhase.SetActive(true);
    }

    public void HideBuyPhase()
    {
        buyPhase.SetActive(false);
    }

    public void ShowBuyPhaseText()
    {
        buyPhase.SetActive(true);
    }
}
