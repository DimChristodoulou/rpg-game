using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnByNum : MonoBehaviour{

    public float startTime;
    public int totalNumOfEnemies;
    public List<enemyScriptableObject> enemiesToSpawn;

    // Start is called before the first frame update
    void Start(){
        InvokeRepeating("SpawnEnemy", totalNumOfEnemies, startTime);        
    }

    // Update is called once per frame
    void Update(){}

    void SpawnEnemy(){
        int listLen = enemiesToSpawn.Count;
        Vector3 randomPos;
        for (int i = 0; i < totalNumOfEnemies; i++){
            randomPos = Random.insideUnitCircle * 2;
            randomPos.z = randomPos.y;
            randomPos.y = 0;
            Debug.Log(Random.Range(0, listLen));
            GameObject.Instantiate(enemiesToSpawn[Random.Range(0, listLen)].prefab, gameObject.transform.position + randomPos, new Quaternion());
        }
    }
}