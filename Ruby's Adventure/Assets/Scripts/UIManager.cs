using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created by Kole

public class UIManager : MonoBehaviour
{
    public Slider cat1HappinessBar;
    public Slider cat2HappinessBar;

    public CatHappinessManager cat1Manager;
    public CatHappinessManager cat2Manager;

    void Update()
    {
        // Update the slider values to reflect the cat's happiness
        if (cat1Manager != null)
            cat1HappinessBar.value = cat1Manager.happiness;

        if (cat2Manager != null)
            cat2HappinessBar.value = cat2Manager.happiness;
    }
}
