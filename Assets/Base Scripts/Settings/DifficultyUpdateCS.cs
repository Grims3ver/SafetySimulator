using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using System.Linq; //for ToList()

public class DifficultyUpdateCS : MonoBehaviour
{
    private List<GameObject> enemies;
    private List<CharacterMovement> enemyMovement;
    private List<AIDecisionDetectTargetRadius2D> enemyDetectionRadius; 

    private CharacterMovement playerMovement;

    public DamageOnNearby proximityDamage;

    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

        enemies = new List<GameObject>();
        enemyMovement = new List<CharacterMovement>();
        enemyDetectionRadius = new List<AIDecisionDetectTargetRadius2D>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();

        for (int i = 0; i < enemies.Count; ++i)
        {
            enemyMovement.Add(enemies.ElementAt(i).GetComponent<CharacterMovement>());
            enemyDetectionRadius.Add(enemies.ElementAt(i).GetComponent<AIDecisionDetectTargetRadius2D>());
        }

        SetDifficultyParameters();

    }

    public void SetDifficultyParameters()
    {
        if (SelectDifficulty.difficultySelected.Equals("Easy"))
        {
            playerMovement.WalkSpeed = 6f;
            proximityDamage.damageTaken = 2; 

            for (int i = 0; i < enemies.Count; ++i)
            {
                enemyMovement[i].WalkSpeed = 3f;
                enemyDetectionRadius[i].Radius = 1f;
            }

        }
        else if (SelectDifficulty.difficultySelected.Equals("Medium"))
        {
            playerMovement.WalkSpeed = 5f;
            proximityDamage.damageTaken = 5; 

            for (int i = 0; i < enemies.Count; ++i)
            {
                enemyMovement[i].WalkSpeed = 4f;
                enemyDetectionRadius[i].Radius = 2f;
            }
        }
        else if (SelectDifficulty.difficultySelected.Equals("Hard"))
        {

            playerMovement.WalkSpeed = 4f;
            proximityDamage.damageTaken = 10; 

            for (int i = 0; i < enemies.Count; ++i)
            {
                enemyMovement.ElementAt(i).WalkSpeed = 5f;
                enemyDetectionRadius.ElementAt(i).Radius = 5f;
            }

        }
    }

}
