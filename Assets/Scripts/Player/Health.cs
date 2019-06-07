using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Visual management
    VisualsManager visuals;

    public HealthBar Slider;
	public float CurrentHP;
	public float MaxHP = 100;
	private bool isInvisible;
	// Start is called before the first frame update
	void Start()
	{
        visuals = GetComponent<VisualsManager>();
        CurrentHP = MaxHP;
        Slider.health = CurrentHP;
        Slider.maxHealth = MaxHP;
	}

	public void takeDamage(float damage)
	{
		if (isInvisible)
			return;

		CurrentHP -= damage;

        visuals.StartEffect(4);
        Slider.DecreaseHealth(damage);

		if (CurrentHP <= 0)
		{
			gameObject.SetActive(false);
			CurrentHP = MaxHP;
			Spawner.Instance.Respawn(GetComponent<Robot>());
		}
	}

	public void GainHealth(float amount)
	{
		CurrentHP += amount;

        visuals.StartEffect(5);
        Slider.IncreaseHealth(amount);
		
		if(CurrentHP > 100)
		{
			CurrentHP = 100;
		}
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
