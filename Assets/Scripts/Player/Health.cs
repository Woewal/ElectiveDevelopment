using AI;
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

	private void takeDamage(float damage)
	{
		if (isInvisible)
			return;

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

	public void GainInvisisbility(float duration)
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

	// Update is called once per frame
	void Update()
	{

	}
}
