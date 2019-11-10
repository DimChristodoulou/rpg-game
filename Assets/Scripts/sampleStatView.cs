using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General;
using Devdog.InventoryPro;
using TMPro;

public class sampleStatView : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
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
        updateStatsText(Localization.StatXP, stat);
    }

    public void updateStatsText(string statText, IStat stat){
        switch (statText) {
            case Localization.StatStrength:
                strText.text = Localization.StatStrength + ": " + stat;
                break;
            case Localization.StatDexterity:
                dexText.text = Localization.StatDexterity + ": " + stat;
                break;
            case Localization.StatIntelligence:
                intText.text = Localization.StatIntelligence + ": " + stat;
                break;
            case Localization.StatXP:
                xpText.text = Localization.StatXP + ": " + stat;
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
