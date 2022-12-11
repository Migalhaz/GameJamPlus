using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudDeVida : MonoBehaviour
{
    [SerializeField] FollowTarget follow;
    [SerializeField] PlayerScript playerScript;
    [SerializeField] UnityEngine.UI.Image healthSlider;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float healthPerc = (float)playerScript.healthSystem.GetLife / playerScript.healthSystem.GetMaxHealth;
        Vector4 color = Color.HSVToRGB(0.33f * healthPerc, 1f, 1f);
        color.w = healthSlider.color.a;
        healthSlider.color = color;
        healthSlider.fillAmount = healthPerc;
        follow.Follow(gameObject.transform);
    }
}
