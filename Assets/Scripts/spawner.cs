using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(enemySpawnByNum))]
[RequireComponent(typeof(simpleSpawner))]
[RequireComponent(typeof(enemySpawnerPerTime))]
public class spawner : MonoBehaviour
{

    public bool spawnPerTime, spawnPerCount, spawnOneAndDone;
    public List<enemyScriptableObject> enemies;
    public int initialTime, startTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[CustomEditor(typeof(spawner))]
public class SpawnerEditor : Editor
{
    private simpleSpawner simpleSpawnerRef;
    private enemySpawnByNum perNumSpawnerRef;
    private enemySpawnerPerTime perTimeSpawnerRef;
    spawner thisScript;
    GameObject scriptObject;

    void OnEnable() {
        thisScript = (spawner) target;
        scriptObject = thisScript.gameObject;
    }

    override public void OnInspectorGUI()
    {

        var spawnScript = target as spawner;

        spawnScript.spawnPerTime = GUILayout.Toggle(spawnScript.spawnPerTime, "Spawn every x seconds");
        spawnScript.spawnPerCount = GUILayout.Toggle(spawnScript.spawnPerCount, "Spawn x units every y seconds");
        spawnScript.spawnOneAndDone = GUILayout.Toggle(spawnScript.spawnOneAndDone, "Spawn x units once");
        
        if(spawnScript.spawnPerTime){            
            spawnScript.initialTime = EditorGUILayout.IntField("Time interval:", spawnScript.initialTime);
            spawnScript.startTime = EditorGUILayout.IntField("Start time:", spawnScript.startTime);            
            scriptObject.GetComponent<enemySpawnerPerTime>().enabled = true;
            scriptObject.GetComponent<enemySpawnerPerTime>().startSpawner(spawnScript.startTime, spawnScript.initialTime);
        }
        else{
            scriptObject.GetComponent<enemySpawnerPerTime>().enabled = false;
        }

        if(spawnScript.spawnPerCount){            
            spawnScript.initialTime = EditorGUILayout.IntField("Time interval:", spawnScript.initialTime);
            enemySpawnByNum newSpawner = new enemySpawnByNum();
            scriptObject.GetComponent<enemySpawnByNum>().enabled = true;
        }
        else{
            scriptObject.GetComponent<enemySpawnByNum>().enabled = false;
        }

        if(spawnScript.spawnOneAndDone){            
            //spawnScript.initialTime = EditorGUILayout.IntField("Time interval:", spawnScript.initialTime);
            simpleSpawner newSpawner = new simpleSpawner();
            scriptObject.GetComponent<simpleSpawner>().enabled = true;
        }
        else{
            scriptObject.GetComponent<simpleSpawner>().enabled = false;
        }

    }
}