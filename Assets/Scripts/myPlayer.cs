using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Class to attach on the player GO
 *  Requirements: LayerMask for the clickables, the Animator for the animations
 */
public class myPlayer : MonoBehaviour
{

    public LayerMask clickables;
    private NavMeshAgent myAgent;
    public Animator myAnimation;
    public SimpleHealthBar healthBar;

    public float currentHealth, maximumHealth;
    public int level;


    // Start is called before the first frame update
    void Start(){
        
        //Initialize player variables
        level = 1;
        maximumHealth = 100.0f;
        currentHealth = maximumHealth;

        myAgent = GetComponent<NavMeshAgent>();
        myAnimation.SetBool("isWalking",false);
        myAnimation.SetBool("isRunning",false);
        takeDamage(10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(myAnimation.GetBool("isWalking"));        
        if(Input.GetMouseButton(0)){
            Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(tempRay, out hitInfo, 100, clickables)){
                myAgent.SetDestination(hitInfo.point);
                myAnimation.SetBool("isWalking",true);
                // Debug.Log(myAgent.remainingDistance);
                if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
                    myAnimation.SetBool("isWalking",false);
                    myAnimation.SetBool("isRunning",true);
                    myAgent.speed = 5.0f;
                }
                else{
                    myAnimation.SetBool("isWalking",true);
                    myAnimation.SetBool("isRunning",false);
                    myAgent.speed = 3.5f;
                }
            }               
        }
        if (myAgent.remainingDistance <= 1f){
            myAnimation.SetBool("isWalking",false);
            myAnimation.SetBool("isRunning",false);
            if (!myAgent.hasPath || myAgent.velocity.sqrMagnitude == 0f){
                myAnimation.SetBool("isWalking",false);
                myAnimation.SetBool("isRunning",false);
            }
        }
    }

    public void levelUp(int stat1Up, int stat2Up){
        level++;
    }

    public void takeDamage(float damage){
        currentHealth -= damage;
        healthBar.UpdateBar(currentHealth, maximumHealth);
    }
}
