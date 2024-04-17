using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMeows : MonoBehaviour
{
    //This whole script was created by Kole
    public AudioClip collectedClip;


    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.PlaySound(collectedClip);
            // Reset happiness on collision
            CatHappinessManager happinessManager = GetComponent<CatHappinessManager>();
            if (happinessManager != null)
            {
                happinessManager.ResetHappiness();
            }
        }

    }
}