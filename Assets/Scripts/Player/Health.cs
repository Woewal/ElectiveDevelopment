using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
  public Slider Slider;
    public float CurrentHP;
    private float MaxHP = 100;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        
    }

    private void takeDamage(float damage)
    {
        CurrentHP -= damage;
        ShowHPSlider();
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;

            CurrentHP = 100;

        }
    }

    public void ShowHPSlider()
    {
        Slider.value = CurrentHP / (float)MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
