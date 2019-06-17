using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnerPerTime : MonoBehaviour{

    public float timeInterval, startTime;
    public List<enemyScriptableObject> enemiesToSpawn;

    void Start(){
        InvokeRepeating("SpawnEnemy", startTime, timeInterval);
    }

    public void startSpawner(int start, int time){
        InvokeRepeating("SpawnEnemy", start, time);
    }

    public void SpawnEnemy(){
        Vector3 randomPos;
        foreach (enemyScriptableObject enemy in enemiesToSpawn){
            randomPos = Random.insideUnitCircle * 2;
            randomPos.z = randomPos.y;
            randomPos.y = 0;
            GameObject.Instantiate(enemy.prefab, gameObject.transform.position + randomPos, new Quaternion());
        }
    }
}
