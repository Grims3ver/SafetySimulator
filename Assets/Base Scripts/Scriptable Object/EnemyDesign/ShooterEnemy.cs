using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Shooter")]
public class ShooterEnemy : EnemyBase
{
    public void Awake()
    {
        enemyType = EnemyType.Shooter;
    }
}
