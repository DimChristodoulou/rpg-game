using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Devdog.General;
using Devdog.InventoryPro;
using Random = UnityEngine.Random;

public class enemyController : MonoBehaviour
{

    public enemyScriptableObject currentEnemy;
    public float lookRadius = 10f;
    NavMeshAgent agent;
    [SerializeField] private GameObject target;
    public Animator myAnimation;
    public int level;
    public int maxHitPoints;
    public int currHitPoints;
    public string name;
    [SerializeField] private GameObject enemyPanel;
    private GameObject playerRef;
    private IStat pStrength;
    private IStat pMinAttack;
    private IStat pMaxAttack;
    private bool isInRange = false;

    private void Awake()
    {
        enemyPanel = GameObject.Find("EnemyWindow");
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myAnimation.SetBool("isWalking",false);
        myAnimation.SetBool("isAttacking",false);
        enemyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if(distance <= lookRadius){
            agent.SetDestination(target.transform.position);
            myAnimation.SetBool("isWalking",true);
            if(distance <= agent.stoppingDistance && !isInRange){
                InvokeRepeating("AttackTarget", 0, 3);
                isInRange = true;
                FaceTarget();
            }
            else if(distance > agent.stoppingDistance){
                CancelInvoke("AttackTarget");
                isInRange = false;
            }
        }

        // If HP below zero destroy enemy
        if(this.currHitPoints <= 0f){
            EnemyKilled();
        }
    }

    private void EnemyKilled()
    {
        var stats = PlayerManager.instance.currentPlayer.inventoryPlayer.stats;
        Destroy(gameObject);            
        enemyPanel.GetComponentInChildren<TextMeshProUGUI>().text = "";
        enemyPanel.SetActive(false);
        IStat xp = stats.Get("Default", "Experience");
        xp.SetCurrentValueRaw(xp.currentValueRaw + currentEnemy.xpOnDeath); 
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void OnMouseOver(){
        enemyPanel.SetActive(true);
        enemyPanel.GetComponentInChildren<TextMeshProUGUI>().text = name + "\n" + currHitPoints + "/" + maxHitPoints;
    }

    void OnMouseExit(){
        enemyPanel.SetActive(false);
    }

    void OnMouseDown(){
        playerRef = GameObject.FindGameObjectWithTag("Player");
        float currDist = Vector3.Distance(playerRef.transform.position, this.transform.position);
        Debug.Log(currDist);

        var stats = PlayerManager.instance.currentPlayer.inventoryPlayer.stats;
        pStrength = stats.Get("Main stats", "Strength");
        pMinAttack = stats.Get("Default", "minAttack");
        pMaxAttack = stats.Get("Default", "maxAttack");

        if(currDist<=2f){
            float damage = Random.Range(pMinAttack.currentValue, pMaxAttack.currentValue) + pStrength.currentValue;
            playerRef.GetComponent<myPlayer>().myAnimation.SetBool("isAttacking",true);
            Debug.Log(damage);
            currHitPoints -= Mathf.RoundToInt(damage);            
            enemyPanel.GetComponentInChildren<TextMeshProUGUI>().text = name + "\n" + currHitPoints + "/" + maxHitPoints;            
        }
        else
            playerRef.GetComponent<myPlayer>().myAnimation.SetBool("isAttacking",false);
    }

    void FaceTarget(){
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
    }

    void AttackTarget(){
        myAnimation.SetBool("isAttacking",true);
        target.GetComponent<myPlayer>().takeDamage(currentEnemy.damage);
    }
}
