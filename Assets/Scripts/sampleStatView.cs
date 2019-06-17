using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General;
using Devdog.InventoryPro;
using TMPro;

public class sampleStatView : MonoBehaviour
{

    private IStat pStrength;
    private IStat pIntelligence;
    private IStat pDexterity;
    public TextMeshProUGUI strText;
    public TextMeshProUGUI dexText;
    public TextMeshProUGUI intText;

    // Start is called before the first frame update
    void Start()
    {
        var stats = PlayerManager.instance.currentPlayer.inventoryPlayer.stats;
        pStrength = stats.Get("Main stats", "Strength");
        pIntelligence = stats.Get("Default", "Intelligence");
        pDexterity = stats.Get("Default", "Dexterity");
        pStrength.OnValueChanged += listenerStr;
        pIntelligence.OnValueChanged += listenerInt;
        pDexterity.OnValueChanged += listenerDex;
    }

    private void listenerStr(IStat stat){
        // Debug.Log(stat.currentValue);
        strText.text = "Strength: " + stat;
    }

    private void listenerInt(IStat stat){
        // Debug.Log(stat.currentValue);
        intText.text = "Intelligence: " + stat;
    }

    private void listenerDex(IStat stat){
        // Debug.Log(stat.currentValue);
        dexText.text = "Dexterity: " + stat;
    }

}
