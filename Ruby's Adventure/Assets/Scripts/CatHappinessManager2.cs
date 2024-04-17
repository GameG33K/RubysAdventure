using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kole Created this script

public class CatHappinessManager2 : MonoBehaviour
{
    public float happiness = 20.0f;
    public float decreaseRate = 1.0f;

    private RubyController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<RubyController>(); // Finds the RubyController in the scene.
    }

    private void Update()
    {
        happiness -= decreaseRate * Time.deltaTime;
        happiness = Mathf.Max(happiness, 0);

        if (happiness == 0)
        {
            if (gameController != null)
            {
                gameController.GameOver();
            }
        }
    }

    // Method to reset happiness when the player interacts with the cat
    public void ResetHappiness()
    {
        happiness = 20.0f;
    }
}

