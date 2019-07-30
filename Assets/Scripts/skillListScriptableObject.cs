using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Skill List", menuName = "Skill List")]
public class skillListScriptableObject : ScriptableObject
{
    public List<skillsScriptableObject> skills;
}
