using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
   Shooter,
   Default
};
public abstract class EnemyBase : ScriptableObject
{
    public float health;
    public float speed;
    public EnemyType enemyType;
    public GameObject enemyPrefab; 

}
