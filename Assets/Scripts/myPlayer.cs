using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

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
    public GameObject skillsController;

    public float currentHealth, maximumHealth;
    public int level;


    // Start is called before the first frame update
    void Start(){        

        myAgent = GetComponent<NavMeshAgent>();
        
        skillsController.GetComponent<skills>().buildSkillWindow();
        skillsController.SetActive(false);
        
        //Initialize player variables
        level = 1;
        maximumHealth = 100.0f;
        currentHealth = maximumHealth;
        
        myAnimation.SetBool("isWalking",false);
        myAnimation.SetBool("isRunning",false);
        myAnimation.SetBool("isAttacking",false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
            toggleSkillWindow();

        //Debug.Log(myAnimation.GetBool("isWalking"));        
        if(Input.GetMouseButton(0)){
            Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(tempRay, out hitInfo, 100, clickables)){                
                myAgent.SetDestination(hitInfo.point);
                //FaceTarget(hitInfo.transform);
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

}
