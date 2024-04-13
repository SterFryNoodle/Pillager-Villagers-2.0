using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float towerRange = 15f;
    [SerializeField] ParticleSystem boltParticles;
    [SerializeField] Transform weapon;
        
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestEnemy = null;
        float maxDistance = Mathf.Infinity; //Sets distance and assigns to max value possible.

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position); //Checks the distance between the position of towers and position of enemy it targets.

            if (targetDistance < maxDistance)
            {
                closestEnemy = enemy.transform; //Add safeguards to find closest enemy after current target dies or goes out of range.
                maxDistance = targetDistance; //adjusts max distance to difference of distance value of current enemy target.
            }
            
        }
        target = closestEnemy;
    }
    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position); //Another distance check.

        if(targetDistance < towerRange)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }

        weapon.LookAt(target);
    }

    void Attack(bool isActive)
    {
        var enableEmission = boltParticles.emission;
        enableEmission.enabled = isActive; 
    }
}
