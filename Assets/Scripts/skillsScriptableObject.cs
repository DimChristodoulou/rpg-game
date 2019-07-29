using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="New Skill", menuName = "Skill")]
public class skillsScriptableObject : ScriptableObject
{
    public int id;
    public string skill_name, skill_description;
    public int minimum_level;
    public float skill_minimum_damage, skill_maximum_damage;
    public Sprite skill_image;
    public GameObject skill_effect;
    public List<skillsScriptableObject> skill_prerequisites;
}
