using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyBase enemy;

    private void Start()
    {
        print("MAKING an ENEMY of type " + enemy.enemyType);
    }
}
   

