﻿using System.Collections;
using System.Collections.Generic;
using Devdog.General;
using Devdog.InventoryPro;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 *  Class to attach on the player GO
 *  Requirements: LayerMask for the clickables, the Animator for the animations
 */
public class myPlayer : MonoBehaviour
{

    public LayerMask clickables;
    private int uilayermask = 1 << 5;
    private NavMeshAgent myAgent;
    public Animator myAnimation;
    public Image healthBar;
    public Text healthBarText;
    public GameObject skillsController;

    public GameObject ClassAndLevelText;

    public float currentHealth, maximumHealth;
    public int level { get; set; }
    public string playerClass { get; set; }
    public string playerRace { get; set; }

    public int totalSkillPoints { get; set; }
    public int unusedSkillPoints { get; set; }
    
    public int currentXP { get; set; }
    public int totalXP { get; set; }
    
    public int totalStatPoints { get; set; }
    public int unusedStatPoints { get; set; }
    
    private StatsCollection stats;
    private IStat pStrength;
    private IStat pIntelligence;
    private IStat pDexterity;
    private IStat pExperience;

    private StatManager statManager;
    private StatWindowController statWindowController;

    // Start is called before the first frame update
    void Start(){
        myAgent = GetComponent<NavMeshAgent>();
        
        skillsController.GetComponent<skills>().buildSkillWindow();
        skillsController.SetActive(false);
        
        //Initialize player variables
        level = 1;
        maximumHealth = 100.0f;
        currentHealth = maximumHealth;
        totalSkillPoints = 0;
        unusedSkillPoints = 0;
        totalStatPoints = 0;
        unusedStatPoints = 0;
        
        stats = PlayerManager.instance.currentPlayer.inventoryPlayer.stats;
        pStrength = stats.Get("Main stats", "Strength");
        pIntelligence = stats.Get("Main stats", "Intelligence");
        pDexterity = stats.Get("Main stats", "Dexterity");

        pStrength.SetCurrentValueRaw(10);
        pIntelligence.SetCurrentValueRaw(10);
        pDexterity.SetCurrentValueRaw(10);
        
        statManager = GameObject.Find("Stats Manager").GetComponent<StatManager>();
        statWindowController = GameObject.Find("Stats Manager").GetComponent<StatWindowController>();

        //TODO: CHANGE THIS AS SOON AS CHARACTER CREATION IS IMPLEMENTED
        playerClass = Localization.ClassFighter;
        playerRace = Localization.RaceHuman;
        
        setClassAndLevelText();
        
        myAnimation.SetBool("isWalking",false);
        myAnimation.SetBool("isRunning",false);
        myAnimation.SetBool("isAttacking",false);
        
        updateHealthBarText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            toggleSkillWindow();

        //Debug.Log(myAnimation.GetBool("isWalking"));
        
        if (Input.GetMouseButton(0))
        {
            Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            uilayermask = ~uilayermask;
            if (Physics.Raycast(tempRay, out hitInfo, 100, uilayermask))
            {
                myAgent.SetDestination(hitInfo.point);
                //FaceTarget(hitInfo.transform);
                myAnimation.SetBool("isWalking", true);
                // Debug.Log(myAgent.remainingDistance);
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    myAnimation.SetBool("isWalking", false);
                    myAnimation.SetBool("isRunning", true);
                    myAgent.speed = 5.0f;
                }
                else
                {
                    myAnimation.SetBool("isWalking", true);
                    myAnimation.SetBool("isRunning", false);
                    myAgent.speed = 3.5f;
                }
            }
        }

        if (myAgent.remainingDistance <= 1f)
        {
            myAnimation.SetBool("isWalking", false);
            myAnimation.SetBool("isRunning", false);
            if (!myAgent.hasPath || myAgent.velocity.sqrMagnitude == 0f)
            {
                myAnimation.SetBool("isWalking", false);
                myAnimation.SetBool("isRunning", false);
            }
        }
    }

    public void takeDamage(float damage){
        currentHealth -= damage;
        healthBar.fillAmount = (float)(currentHealth)/100;
        updateHealthBarText();
    }

    private void updateHealthBarText()
    {
        healthBarText.text = currentHealth + "/" + maximumHealth;
    }

    void FaceTarget(Transform target){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
    }

    private void toggleSkillWindow(){
        if(!skillsController.activeSelf)
            skillsController.SetActive(true);
        else
            skillsController.SetActive(false);
    }

    //When Touching UI
    private bool IsPointerOverUIObject(){
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    
    /*
     * LEVELING AND CHARACTER FUNCTIONS
     */

    protected void setClassAndLevelText(){
        ClassAndLevelText.GetComponent<TextMeshProUGUI>().text = Localization.LevelText + " " + 
                                                                 level + " " + 
                                                                 playerRace + " " + playerClass;
    }

    /**
     * Levels 10,20,30,40 and 50 are milestone levels
     */
    public bool isMilestoneLevel(){
        return (level % 10 == 0) ? true : false;
    }
    
    public void levelUp(){
        //Increase level
        int currentPlayerLevel = level;
        level = ++currentPlayerLevel;
        
        //Increase skill points
        if (isMilestoneLevel()){
            totalSkillPoints+=2;
            unusedSkillPoints+=2;
        }
        else{
            totalSkillPoints++;
            unusedSkillPoints++;
        }
        
        //Increase stat points
        if (isMilestoneLevel()){
            totalStatPoints+=10;
            unusedStatPoints+=10;
        }
        else{
            totalStatPoints += 5;
            unusedStatPoints += 5;
        }
        statWindowController.adjustUnusedStatPointsText();
        //popup level up message
        StartCoroutine(PopupMessage.setPopupText(Localization.LevelUpText));

        setClassAndLevelText();
    }
    

}
