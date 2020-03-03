using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Devdog.General;
using Devdog.InventoryPro;

public class StatWindowController : MonoBehaviour
{
    private StatsCollection stats;
    private StatManager statManager;
    private myPlayer playerInstance;
    private TextMeshProUGUI unusedStatPointsText;
    private GameObject[] statButtons;

    private void Awake()
    {
        unusedStatPointsText = GameObject.Find("UnusedStatPointsText").GetComponent<TextMeshProUGUI>();
        /**
         * STAT BUTTONS
         */
        statButtons = GameObject.FindGameObjectsWithTag("StatButtons");
    }

    public void Start()
    {
        stats = PlayerManager.instance.currentPlayer.inventoryPlayer.stats;
        statManager = GameObject.Find("Stats Manager").GetComponent<StatManager>();
        playerInstance = GameObject.FindWithTag("Player").GetComponent<myPlayer>();
        Debug.Log(playerInstance.unusedStatPoints);
        adjustUnusedStatPointsText();
        foreach (GameObject statButton in statButtons){
            statButton.SetActive(false);
        }
    }

    public void Update()
    {
        //TODO: CHECK IF I CAN FIND BETTER SOLUTION INSTEAD OF CHECKING EVERY FRAME
        showStatButtonsWhenAppropriate();
    }

    public void showStatButtonsWhenAppropriate(){
        if (playerInstance.unusedStatPoints != 0){
            foreach (GameObject statButton in statButtons){
                statButton.SetActive(true);
            }
        }
        else{
            foreach (GameObject statButton in statButtons){
                statButton.SetActive(false);
            }
        }
    }

    public void increaseStrengthStat(){
        IStat pStrength = stats.Get("Main stats", "Strength");
        pStrength.SetCurrentValueRaw(pStrength.currentValue+1);
        statManager.updateStatsText(Localization.StatStrength, pStrength);
        adjustUnusedStatPointsText(true, true);
    }
    
    public void decreaseStrengthStat(){
        IStat pStrength = stats.Get("Main stats", "Strength");
        if (pStrength.currentValue > 0)
        {
            pStrength.SetCurrentValueRaw(pStrength.currentValue - 1);
            statManager.updateStatsText(Localization.StatStrength, pStrength);
            adjustUnusedStatPointsText(false, true);
        }
        else
        {    
            Debug.Log("NOPE");
        }
    }
    
    public void increaseDexterityStat(){
        IStat pDexterity = stats.Get("Main stats", "Dexterity");
        pDexterity.SetCurrentValueRaw(pDexterity.currentValue+1);
        statManager.updateStatsText(Localization.StatDexterity, pDexterity);
        adjustUnusedStatPointsText(true, true);
    }
    
    public void decreaseDexterityStat(){
        IStat pDexterity = stats.Get("Main stats", "Dexterity");
        if (pDexterity.currentValue > 0){
            pDexterity.SetCurrentValueRaw(pDexterity.currentValue - 1);
            statManager.updateStatsText(Localization.StatDexterity, pDexterity);
            adjustUnusedStatPointsText(false, true);
        }
        else{
            Debug.Log("NOPE");
        }
    }
    
    public void increaseIntelligenceStat(){
        IStat pIntelligence = stats.Get("Main stats", "Intelligence");
        pIntelligence.SetCurrentValueRaw(pIntelligence.currentValue+1);
        statManager.updateStatsText(Localization.StatIntelligence, pIntelligence);
        adjustUnusedStatPointsText(true, true);
    }
    
    public void decreaseIntelligenceStat(){
        IStat pIntelligence = stats.Get("Main stats", "Intelligence");
        if (pIntelligence.currentValue > 0){
            pIntelligence.SetCurrentValueRaw(pIntelligence.currentValue - 1);
            statManager.updateStatsText(Localization.StatIntelligence, pIntelligence);
            adjustUnusedStatPointsText(false, true);
        }
        else{
            Debug.Log("NOPE");
        }
    }

    public void adjustUnusedStatPointsText(bool statIncreased = false, bool statChanged = false)
    {
        if (statChanged)
        {
            if (statIncreased)
                playerInstance.unusedStatPoints--;
            else
                playerInstance.unusedStatPoints++;
        }

        if (playerInstance.unusedStatPoints == 0)
            unusedStatPointsText.text = "";
        else
            unusedStatPointsText.text = Localization.UnusedStatPoints + ": " + playerInstance.unusedStatPoints;
    }
    
}
