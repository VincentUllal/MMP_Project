using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillHealthBar : MonoBehaviour
{
    public Health playerHealth; //reference to the Health Script in Char1
    public Image fillImage; //the image of the healthbar
    private Slider slider; //reference to the Slider to set the value of it
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue){
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled){
            fillImage.enabled = true;
        }
        float fillValue = playerHealth.currentHealth / playerHealth.maxHealth;
        /*
        if (fillValue >= slider.maxValue/3){
            fillImage.color = new Vector4(0.7f, 0.3f, 0.3f);
        } else if (fillValue > slider.maxValue/3)
            fillImage.color = new Vector4(0.9f, 0.3f, 0.3f);
            */
        slider.value = fillValue;
    }
}
