using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName ="StatsScriptableObject", menuName ="ScriptableObjects/Stats")]
public class StatsScriptableObject : ScriptableObject
{
    public GameObject prefab;
    public string characterName;
    // base stats for weapons
    public float maxHealth;
    public float health;
    public float damage;
    public float speed;
    public float level;
    public float maxWine;
    public float wine;
    public float dashAmount;
    public float dashCooldown;
    public float dashPower;
    public float range;
    public string deathSound;
    
}
