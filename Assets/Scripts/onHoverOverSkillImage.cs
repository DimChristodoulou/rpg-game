using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onHoverOverSkillImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public skillListScriptableObject allSkills;
    private String[] temp;
    private void Start(){
        allSkills = Resources.Load("allSkills") as skillListScriptableObject;
        allSkills.skills.Sort((p1,p2)=>p1.id.CompareTo(p2.id));
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        //Debug.Log("Cursor Entering " + name + " GameObject");
        temp = name.Split('_');
        Debug.Log(allSkills.skills[int.Parse(temp[temp.Length - 1])].skill_name);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        //Debug.Log("Cursor Exiting " + name + " GameObject");
        Debug.Log(allSkills.skills[int.Parse(temp[temp.Length - 1])].skill_name);
    }
}
