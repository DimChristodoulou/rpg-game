using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class onHoverOverSkillImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public skillListScriptableObject allSkills;
    private GameObject skillInfoPanel, skillNameText, skillDescriptionText, skillDamageText;
    private String[] temp;
    
    private void Start(){
        skillInfoPanel = GameObject.Find("skillInfoPanel");

        skillNameText = skillInfoPanel.transform.GetChild(0).gameObject;
        skillDescriptionText = skillInfoPanel.transform.GetChild(1).gameObject;
        skillDamageText = skillInfoPanel.transform.GetChild(2).gameObject;
        allSkills = Resources.Load("allSkills") as skillListScriptableObject;
        allSkills.skills.Sort((p1,p2)=>p1.id.CompareTo(p2.id));
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        //Debug.Log("Cursor Entering " + name + " GameObject");
        temp = name.Split('_');
        skillInfoPanel.SetActive(true);
        skillInfoPanel.transform.position = Input.mousePosition + new Vector3(-110,-100,110);

        skillNameText.GetComponent<TextMeshProUGUI>().text =
            allSkills.skills[int.Parse(temp[temp.Length - 1])].skill_name;
        
        skillDescriptionText.GetComponent<TextMeshProUGUI>().text =
            allSkills.skills[int.Parse(temp[temp.Length - 1])].skill_description;
        
        skillDamageText.GetComponent<TextMeshProUGUI>().text =
            allSkills.skills[int.Parse(temp[temp.Length - 1])].skill_minimum_damage.ToString() + " - " + 
            allSkills.skills[int.Parse(temp[temp.Length - 1])].skill_maximum_damage.ToString();
        
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        //Debug.Log("Cursor Exiting " + name + " GameObject");
        skillInfoPanel.SetActive(false);
        Debug.Log(allSkills.skills[int.Parse(temp[temp.Length - 1])].skill_name);
    }
}
