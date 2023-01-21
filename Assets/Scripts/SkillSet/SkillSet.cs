using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create SkillSet")]
public class SkillSet : ScriptableObject
{
    public string charaName;
    public int ID;
    public float damage;
    public float KBG;
    public float BKB;
    public float rigidtyTime;
    public Vector3 vector;
    public GameObject HitColider;
}
