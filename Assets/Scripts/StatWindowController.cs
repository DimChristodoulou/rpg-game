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
    private sampleStatView statManager;
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
        statManager = GameObject.Find("Stats Manager").GetComponent<sampleStatView>();
        playerInstance = GameObject.FindWithTag("Player").GetComponent<myPlayer>();
        
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
        adjustUnusedStatPointsText(true);
    }
    
    public void decreaseStrengthStat(){
        IStat pStrength = stats.Get("Main stats", "Strength");
        pStrength.SetCurrentValueRaw(pStrength.currentValue-1);
        statManager.updateStatsText(Localization.StatStrength, pStrength);
        adjustUnusedStatPointsText(false);
    }
    
    public void increaseDexterityStat(){
        IStat pDexterity = stats.Get("Main stats", "Dexterity");
        pDexterity.SetCurrentValueRaw(pDexterity.currentValue+1);
        statManager.updateStatsText(Localization.StatDexterity, pDexterity);
        adjustUnusedStatPointsText(true);
    }
    
    public void decreaseDexterityStat(){
        IStat pDexterity = stats.Get("Main stats", "Dexterity");
        pDexterity.SetCurrentValueRaw(pDexterity.currentValue-1);
        statManager.updateStatsText(Localization.StatDexterity, pDexterity);
        adjustUnusedStatPointsText(false);
    }
    
    public void increaseIntelligenceStat(){
        IStat pIntelligence = stats.Get("Main stats", "Intelligence");
        pIntelligence.SetCurrentValueRaw(pIntelligence.currentValue+1);
        statManager.updateStatsText(Localization.StatIntelligence, pIntelligence);
        adjustUnusedStatPointsText(true);
    }
    
    public void decreaseIntelligenceStat(){
        IStat pIntelligence = stats.Get("Main stats", "Intelligence");
        pIntelligence.SetCurrentValueRaw(pIntelligence.currentValue-1);
        statManager.updateStatsText(Localization.StatIntelligence, pIntelligence);
        adjustUnusedStatPointsText(false);
    }

    public void adjustUnusedStatPointsText(bool statIncreased)
    {
        if (statIncreased)
            playerInstance.unusedStatPoints--;
        else
            playerInstance.unusedStatPoints++;
        unusedStatPointsText.text = Localization.UnusedStatPoints + ": " + playerInstance.unusedStatPoints;
    }
    
}
