using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was created by Francisco

public class BoostFruit : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {

            controller.ChangeSpeed(0.5f);
            Destroy(gameObject);

            controller.PlaySound(collectedClip);
        }

    }
}
