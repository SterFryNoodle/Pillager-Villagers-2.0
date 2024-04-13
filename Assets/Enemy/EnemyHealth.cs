using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] //Adds a script component requirement which includes both scripts when adding this script to an object.
public class EnemyHealth : MonoBehaviour
{
    [SerializeField][Range(0,5)] int maxHitPoints = 2;

    [Tooltip("Adds amount of hp when enemies die.")]
    [SerializeField] int difficultyLevel = 1;
    int currentHitPoints;
    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();  
    }

    void OnEnable() //Resets the function everytime the gameObject attached is re-enabled.
    {
        currentHitPoints = maxHitPoints;
    }
    
    void OnParticleCollision(GameObject other)
    {
        DamageEnemy();
    }

    void DamageEnemy()
    {
        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficultyLevel;
            enemy.RewardGold();
        }
    }
}
