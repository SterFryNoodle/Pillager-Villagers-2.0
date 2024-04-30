using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] //Adds a script component requirement which includes both scripts when adding this script to an object.
public class EnemyHealth : MonoBehaviour
{
    [SerializeField][Range(0,10)] int maxHitPoints = 2;
    [Tooltip("Adds amount of hp when enemies die.")]
    [SerializeField] int difficultyLevel = 1;
    [SerializeField] AudioClip zombieGrunt;
    
    int currentHitPoints;
    Enemy enemy;
    AudioSource audioSource;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() //Resets the function everytime the gameObject attached is re-enabled.
    {
        currentHitPoints = maxHitPoints;
    }
    
    void OnParticleCollision(GameObject other)
    {
        PlayEnemyDamageSFX();
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

    void PlayEnemyDamageSFX()
    {
        if(!audioSource.isPlaying && gameObject.tag == "Zombie")
        {
            audioSource.PlayOneShot(zombieGrunt);
        }
    }
}
