using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	public Slider Slider;
	public float CurrentHP;
	public float MaxHP = 100;
	private bool isInvisible;
	// Start is called before the first frame update
	void Start()
	{
		CurrentHP = MaxHP;
	}

	public void takeDamage(float damage)
	{
		if (isInvisible)
			return;

		CurrentHP -= damage;
		ShowHPSlider();
		if (CurrentHP <= 0)
		{
			CurrentHP = 0;

			CurrentHP = 100;

		}
	}

	public void GainHealth(float amount)
	{
		CurrentHP += amount;
		ShowHPSlider();
		
		if(CurrentHP > 100)
		{
			CurrentHP = 100;
		}
	}

	public void ShowHPSlider()
	{
		if(Slider != null)
			Slider.value = CurrentHP / (float)MaxHP;
	}

	public void GainInvulnerability(float duration)
	{
		StopAllCoroutines();
		StartCoroutine(InvisibilityCoroutine(duration));
	}

	private IEnumerator InvisibilityCoroutine(float duration)
	{
		float currentTime = 0;
		isInvisible = true;

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			yield return null;
		}

		isInvisible = false;
	}
    public void AddBlood(float amount, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(AddBloodCoroutine(amount, duration));
    }

    IEnumerator AddBloodCoroutine(float amount, float duration)
    {
        CurrentHP += 5 * Time.deltaTime;
        if (CurrentHP >= 100)
        {
            CurrentHP = 100;
        }
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

      
    }

    // Update is called once per frame
    void Update()
	{

	}
}
