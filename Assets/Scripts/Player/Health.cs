using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Visual management
    VisualsManager visuals;

    public Slider Slider;
	public float CurrentHP;
	public float MaxHP = 100;
	private bool isInvulnerable;
	// Start is called before the first frame update
	void Start()
	{
        visuals = GetComponent<VisualsManager>();
        CurrentHP = MaxHP;
	}

	public void takeDamage(float damage)
	{
		if (isInvulnerable)
			return;

        visuals.StartEffect(4);

		CurrentHP -= damage;
		ShowHPSlider();
		if (CurrentHP <= 0)
		{
			gameObject.SetActive(false);
			var robot = GetComponent<Robot>();
			Spawner.Instance.Respawn(robot);
		}
	}

	public void ShowHPSlider()
	{
		Slider.value = CurrentHP / (float)MaxHP;
	}

	public void GainInvulnerability(float duration)
	{
		StopAllCoroutines();
		StartCoroutine(InvulnerabilityCoroutine(duration));
        visuals.StartEffect(3, duration);
	}

	private IEnumerator InvulnerabilityCoroutine(float duration)
	{
		float currentTime = 0;
		isInvulnerable = true;

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			yield return null;
		}

		isInvulnerable = false;
	}

	public void GainHealth(float amount)
	{
        visuals.StartEffect(5);
		if(CurrentHP + amount > MaxHP)
		{
			CurrentHP = MaxHP;
		}
		else
		{
			CurrentHP += amount;
		}
	}

	void Update()
	{

	}
}
