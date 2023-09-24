using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelManager : MonoBehaviour
{
    public AnimationCurve enemyDamageScalingCurve; // Add a reference to your animation curve here
    public AnimationCurve sorcererHealthScalingCurve; // Add a reference to your animation curve here
    public AnimationCurve orcHealthScalingCurve; // Add a reference to your animation curve here
    public AnimationCurve flyingEyeHealthScalingCurve; // Add a reference to your animation curve here
    public StatsScriptableObject playerStats;
    public StatsScriptableObject[] enemyStats;
    public WeaponScriptableObject enemyWeaponStats;
    // Start is called before the first frame update
    void Start()
    {
        foreach (StatsScriptableObject enemy in enemyStats)
        {
            enemy.level = playerStats.level;
            enemy.damage = 10f;
            if (enemy.characterName == "Sorcerer")
            {
                enemy.maxHealth = 20f;
            }
            else if (enemy.characterName == "Orc")
            {
                enemy.maxHealth = 50f;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (StatsScriptableObject enemy in enemyStats)
        {
            enemy.level = playerStats.level;
            float damageScale = enemyDamageScalingCurve.Evaluate(enemy.level);
            enemyWeaponStats.damage = damageScale;
            enemy.damage =damageScale;
            if (enemy.characterName == "Sorcerer")
            {
                float healthScale = sorcererHealthScalingCurve.Evaluate(enemy.level);
                enemy.maxHealth = healthScale;
            }
            if (enemy.characterName == "Orc")
            {
                float healthScale = orcHealthScalingCurve.Evaluate(enemy.level);
                enemy.maxHealth = healthScale;
            }
            if (enemy.characterName == "Flying Eye")
            {
                float healthScale = flyingEyeHealthScalingCurve.Evaluate(enemy.level);
                enemy.maxHealth = healthScale;
            }
        }
    }
    
   
}
