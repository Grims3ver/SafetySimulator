using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Default")]
public class DefaultEnemy : EnemyBase
{

    public void Awake()
    {
        enemyType = EnemyType.Default;
    }
}
