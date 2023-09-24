using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName ="WeaponScriptableObject", menuName ="ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public GameObject prefab;
    public string header;
    public Sprite image;
    // base stats for weapons
    public float level;
    public float damage;
    public string hitSound;
    public float speed;
    public float cooldownDuration;
    public bool pierce;
    public string pierceSound;
    public float range; 
    public float radius;
    public bool destroy;
    public float destroyAfter;
    public float homingSpeed;
    public bool homing;
    public bool knockBack;
    public float ricochet;


}
