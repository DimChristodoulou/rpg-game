using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawns one time the enemies passed
public class simpleSpawner : MonoBehaviour
{
    public float startTime;
    public int totalNumOfEnemies;
    public List<enemyScriptableObject> enemiesToSpawn;

    // Start is called before the first frame update
    void Start(){
        int listLen = enemiesToSpawn.Count;
        Vector3 randomPos;
        for (int i = 0; i < totalNumOfEnemies; i++){
            randomPos = Random.insideUnitCircle * 2;
            randomPos.z = randomPos.y;
            randomPos.y = 0;
            GameObject.Instantiate(enemiesToSpawn[Random.Range(0, listLen)].prefab, gameObject.transform.position + randomPos, new Quaternion());
        }
    }

    // Update is called once per frame
    void Update(){}

}
