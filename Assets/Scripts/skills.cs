using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class skills : MonoBehaviour
{
    private GameObject lineRenderer;

    public void OnPointerEnterDelegate(PointerEventData data, skillsScriptableObject currentSkill){
        skillInfoName.GetComponent<TextMeshProUGUI>().text = currentSkill.skill_name;
        skillInfoDescription.GetComponent<TextMeshProUGUI>().text = currentSkill.skill_description;
        skillInfoDamage.GetComponent<TextMeshProUGUI>().text = currentSkill.skill_minimum_damage + " - " + currentSkill.skill_maximum_damage;

        Vector3 mouse = new Vector3(Input.mousePosition.x, 0.0f, zAxisPos - Camera.main.transform.position.z);
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        skillInfoPanel.transform.position = mouse;
        skillInfoPanel.SetActive(true);
    }

    public void OnPointerExitDelegate(PointerEventData data, skillsScriptableObject currentSkill){
        Debug.Log("exit");
        skillInfoPanel.SetActive(false);
    }

    public static int zAxisPos = 0;
    public List<skillsScriptableObject> allSkills, learntSkills;
    public List<GameObject> uiIcons;
    public GameObject skillInfoPanel;
    public int maximumLevel;

    private GameObject skillInfoName;
    private GameObject skillInfoDescription;
    private GameObject skillInfoDamage;
    
    void Start(){
        skillInfoName = GameObject.Find("skillName");
        skillInfoDescription = GameObject.Find("skillDescription");
        skillInfoDamage = GameObject.Find("skillDamage");
    }

    /*
     * Builds a skill window from a list of skills, 
     * analyzing skills by minimum level and showing prerequisites
     */
    public void buildSkillWindow(){
        List<int> distinctMinimumLevels = new List<int>();
        List<int> frequencyOfSkillsByLevel = new List<int>(maximumLevel);
        for (int i = 0; i < maximumLevel; i++){
            frequencyOfSkillsByLevel.Add(0);
        }

        float windowWidth = gameObject.GetComponent<RectTransform>().rect.width;
        float windowHeight = gameObject.GetComponent<RectTransform>().rect.height - 100;
        //Debug.Log(gameObject.GetComponent<RectTransform>().rect.width);

        for (int i = 0; i < allSkills.Count; i++){
            // Get how many different minimum levels of skills there are. 
            // We will use this number to define vertical panels inside the main skill window.

            if( distinctMinimumLevels.IndexOf(allSkills[i].minimum_level) < 0 ){
                distinctMinimumLevels.Add(allSkills[i].minimum_level);
                frequencyOfSkillsByLevel[allSkills[i].minimum_level]=1;
            }
            else{
                frequencyOfSkillsByLevel[allSkills[i].minimum_level]++;
            }
            
        }

        float subWindowWidth = windowWidth/distinctMinimumLevels.Count;
        //float subWindowHeight = windowHeight/frequencyOfSkillsByLevel.Count;

        for (int i = 0; i < distinctMinimumLevels.Count; i++){

            List<skillsScriptableObject> currentMinimumLevelSkills = new List<skillsScriptableObject>();
            for (int j = 0; j < allSkills.Count; j++){
                if(allSkills[j].minimum_level == distinctMinimumLevels[i])
                    currentMinimumLevelSkills.Add(allSkills[j]);
            }

            //Debug.Log("Distinct i:" + distinctMinimumLevels[i] + " Count " + distinctMinimumLevels.Count);

            for (int j = 0; j < frequencyOfSkillsByLevel[distinctMinimumLevels[i]]; j++){

                float subWindowHeight = windowHeight/frequencyOfSkillsByLevel[distinctMinimumLevels[i]];

                //Debug.Log("Frequency i:" + frequencyOfSkillsByLevel[distinctMinimumLevels[i]]);

                //Current skill is currentMinimumLevelSkills[j]
                GameObject skill_i = new GameObject();
                skill_i.name = "skill_btn_id_" + currentMinimumLevelSkills[j].id;
                skill_i.transform.SetParent(gameObject.transform);
                skill_i.AddComponent<Image>();
                skill_i.GetComponent<Image>().sprite = currentMinimumLevelSkills[j].skill_image;
                Debug.Log(i + " - " + j + " - " + subWindowHeight + " - " + j*subWindowHeight);
                Vector2 skillPos;
                if( frequencyOfSkillsByLevel[distinctMinimumLevels[i]] > 1)
                    skillPos = new Vector2((i-1)*subWindowWidth + subWindowWidth/2, (j-1)*subWindowHeight);
                else
                    skillPos = new Vector2((i-1)*subWindowWidth + subWindowWidth/2, (j)*subWindowHeight);
                skill_i.transform.localPosition = skillPos;
                skill_i.transform.localScale = new Vector3(0.5f, 0.5f, 1);

            }            
        }

        //Debug.Log(allSkills.Count);
        for (int i = 0; i < allSkills.Count; i++){
            //Debug.Log(allSkills[i].name);
            //Debug.Log(allSkills[i].skill_prerequisites.Count);
            if(allSkills[i].skill_prerequisites.Count != 0){
                List<Vector2> points = new List<Vector2>();
                GameObject currentSkill = GameObject.Find("skill_btn_id_" + allSkills[i].id);
                points.Add(currentSkill.transform.position);

                foreach (skillsScriptableObject prerequisite in allSkills[i].skill_prerequisites){
                    //Debug.Log("1");

                    lineRenderer = new GameObject();
                    lineRenderer.name = "line_renderer_" + allSkills[i].id;
                    lineRenderer.AddComponent<UnityEngine.UI.Extensions.UILineRenderer>();
                    lineRenderer.transform.SetParent(gameObject.transform);

                    currentSkill = GameObject.Find("skill_btn_id_" + prerequisite.id);
                    points.Add(currentSkill.transform.position);
                    lineRenderer.GetComponent<UnityEngine.UI.Extensions.UILineRenderer>().Points = points.ToArray();
                }
            }
        }

        // for(int i=0; i<uiIcons.Count; i++){
        //     uiIcons[i].GetComponent<Image>().sprite = allSkills[i].skill_image;

        //     // Add event to display skill info to each icon
        //     EventTrigger eventTrigger = uiIcons[i].AddComponent<EventTrigger>();

        //     EventTrigger.Entry eventEntryEnter = new EventTrigger.Entry();
        //     eventEntryEnter.eventID = EventTriggerType.PointerEnter;
        //     eventEntryEnter.callback.AddListener((data) => {OnPointerEnterDelegate((PointerEventData)data, allSkills[i]); });
        //     eventTrigger.triggers.Add(eventEntryEnter);

        //     EventTrigger.Entry eventEntryExit = new EventTrigger.Entry();
        //     eventEntryExit.eventID = EventTriggerType.PointerExit;
        //     eventEntryExit.callback.AddListener((data) => {OnPointerExitDelegate((PointerEventData)data, allSkills[i]); });
        //     eventTrigger.triggers.Add(eventEntryExit);
        // }

    }
}
