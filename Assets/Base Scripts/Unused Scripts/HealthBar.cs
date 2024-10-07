using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fillColour;
    public Gradient gradient; 
   

   public void SetHealth(int health)
    {
        slider.value = health;
        fillColour.color = gradient.Evaluate(slider.normalizedValue);
    }

  
}
