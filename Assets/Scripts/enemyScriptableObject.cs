using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Enemy", menuName = "Enemy")]
public class enemyScriptableObject : ScriptableObject
{
    public string name;
    public int level;
    public float damage;
    public GameObject prefab;
}
