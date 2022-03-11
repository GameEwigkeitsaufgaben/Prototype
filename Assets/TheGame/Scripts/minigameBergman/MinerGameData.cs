using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinerGameData : MonoBehaviour
{
    private int nbrItemsMiner = 3;
    public AudioSource snapAudio;

    public Text minerItemText;
    public Text numberItemsOnMinerText;
    [SerializeField] private GameObject miner, suit, helmet, lamp, card;

    [SerializeField] private Vector2 posSuitOnMiner, posLampOnMiner, posHelmetOnMiner, posCardOnMiner;


    public Vector2 GetPosSuitOnMiner()
    {
        return posSuitOnMiner;
    }
    public Vector2 GetPosCardOnMiner()
    {
        return posCardOnMiner;
    }

    public Vector2 GetPosHelmetOnMiner()
    {
        return posHelmetOnMiner;
    }

    public Vector2 GetPosLampOnMiner()
    {
        return posLampOnMiner;
    }

    public void IncrementMinerItems()
    {
        nbrItemsMiner++;
    }

    public void DecrementMinerItems()
    {
        nbrItemsMiner--;
    }

    public int GetNbrMinerItems()
    {
        return nbrItemsMiner;
    }

    public void SetInfoText(string info)
    {
        minerItemText.text = info;
    }

    public string GetInfoCard()
    {
        return "Die Stechkarte wird benötigt, zur Abrechnung aber auch damit im Ernsfall klar ist wer sich unter Tage befindet. ";
    }

    public string GetInfoLamp()
    {
        return "Die Lampe ist ein verlässlicher Begleiter falls es zu einem Stromausfall kommt. ";

    }

    public string GetInfoHelmet()
    {
        return "Der Helm schützt vor Steinschlag und anderen Gefahren in Kopfhöhe";
    }

    public void PlaySnapSound()
    {
        if (!snapAudio.isPlaying)
        {
            snapAudio.Play();
            Debug.Log("Paly sound");

        }

    }

    public Transform GetMinerTransform()
    {
        return miner.transform;
    }

    public Transform GetSuitTransform()
    {
        return suit.transform;
    }

    public Transform GetLampTransform()
    {
        return lamp.transform;
    }

    public Transform GetHelmetTransform()
    {
        return helmet.transform;
    }

    public Transform GetCardTransform()
    {
        return card.transform;
    }

    public void UpdateMinNumberItemText()
    {
        if (nbrItemsMiner < 0)
        {
            numberItemsOnMinerText.text = 0.ToString();
        }
        else
        {
            numberItemsOnMinerText.text = nbrItemsMiner.ToString();
        }
    }
}

   
