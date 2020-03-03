using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General;
using Devdog.InventoryPro;
using TMPro;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    private myPlayer playerInstance;
    private IStat pStrength;
    private IStat pIntelligence;
    private IStat pDexterity;
    private IStat pExperience;
    public TextMeshProUGUI strText;
    public TextMeshProUGUI dexText;
    public TextMeshProUGUI intText;
    public TextMeshProUGUI xpText;
    //XP Progress bar used to show how much XP left until next level.
    private Image progressBar;
    private TextMeshProUGUI xpProgressText;

    // Start is called before the first frame update
    void Start(){
        progressBar = GameObject.Find("xpProgress").GetComponent<Image>();
        xpProgressText = GameObject.Find("xpProgressText").GetComponent<TextMeshProUGUI>();
        playerInstance = GameObject.FindWithTag("Player").GetComponent<myPlayer>();
        var stats = PlayerManager.instance.currentPlayer.inventoryPlayer.stats;
        pStrength = stats.Get("Main stats", "Strength");
        pIntelligence = stats.Get("Main stats", "Intelligence");
        pDexterity = stats.Get("Main stats", "Dexterity");
        pExperience = stats.Get("Default", "Experience");
        pStrength.OnValueChanged += listenerStr;
        pIntelligence.OnValueChanged += listenerInt;
        pDexterity.OnValueChanged += listenerDex;
        pExperience.OnValueChanged += listenerXP;
    }

    private void listenerStr(IStat stat){
        updateStatsText(Localization.StatStrength, stat);
    }

    private void listenerInt(IStat stat){
        updateStatsText(Localization.StatIntelligence, stat);
    }

    private void listenerDex(IStat stat){
        updateStatsText(Localization.StatDexterity, stat);
    }
    
    private void listenerXP(IStat stat)
    {
        int xpDifference = (int)stat.currentValueRaw - globals.XPPerLevel[playerInstance.level];
        
        if (xpDifference >= 0){
            stat.SetCurrentValueRaw(xpDifference);
            playerInstance.levelUp();
        }

        progressBar.fillAmount = (float)(stat.currentValueRaw / globals.XPPerLevel[playerInstance.level]);
        xpProgressText.text = Localization.StatXP + ": " + stat.currentValueRaw + " / " +
                              globals.XPPerLevel[playerInstance.level];

        updateStatsText(Localization.StatXP, stat);
    }

    public void updateStatsText(string statText, IStat stat){
        switch (statText) {
            case Localization.StatStrength:
                strText.text = "Strength: " + stat.ToString();
                break;
            case Localization.StatDexterity:
                dexText.text = "Dexterity: " + stat.ToString();
                break;
            case Localization.StatIntelligence:
                intText.text = "Intelligence: " + stat.ToString();
                break;
            case Localization.StatXP:
                xpText.text = "XP: " + stat.ToString();
                break;
        }
    }

    public int getStrength(){
        return (int)pStrength.currentValue;
    }

    public int getDexterity(){
        return (int)pDexterity.currentValue;
    }
    
    public int getIntelligence(){
        return (int)pIntelligence.currentValue;
    }
    
}
