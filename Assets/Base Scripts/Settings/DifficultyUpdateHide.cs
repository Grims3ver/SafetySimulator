using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class DifficultyUpdateHide : MonoBehaviour
{
    private CharacterMovement playerMovement;

    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

        SetDifficultyParameters();

    }

    public void SetDifficultyParameters()
    {
        if (SelectDifficulty.difficultySelected.Equals("Easy"))
        {
            playerMovement.WalkSpeed = 6f;
        }
        else if (SelectDifficulty.difficultySelected.Equals("Medium"))
        {
            playerMovement.WalkSpeed = 5f;
        }
        else if (SelectDifficulty.difficultySelected.Equals("Hard"))
        {
            playerMovement.WalkSpeed = 4f;

        } else
        {
            playerMovement.WalkSpeed = 5f; //default to medium if not working correctly
        }
    }
}
