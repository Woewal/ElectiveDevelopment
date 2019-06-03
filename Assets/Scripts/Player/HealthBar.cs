using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float health = 100.0f;
    public float maxHealth = 100.0f;
    public AnimationCurve animCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public Image healthBarImage = null;
    public Gradient gradient;
    public float timeVar;

    void Start(){}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            DecreaseHealth(10.0f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            IncreaseHealth(10.0f);
        }
    }

    void DecreaseHealth(float amount) {
        float newHealth = health - amount;
        StartCoroutine(Flash());
        StartCoroutine(Shake());
        if (newHealth < 0.0f) {
            newHealth = 0.0f;
        }
        health = newHealth;

        if (healthBarImage != null) {
            healthBarImage.rectTransform.sizeDelta = new Vector2(health / maxHealth, healthBarImage.rectTransform.sizeDelta.y);
        }
    }

    void IncreaseHealth(float amount) {
        float newHealth = health + amount;
        StartCoroutine(Flash());
        StartCoroutine(Shake());
        if (newHealth > maxHealth) {
            newHealth = maxHealth;
        }
        health = newHealth;

        if (healthBarImage != null) {
            healthBarImage.rectTransform.sizeDelta = new Vector2(health / maxHealth, healthBarImage.rectTransform.sizeDelta.y);
        }
    }

    IEnumerator Flash()
    {
        // flash
        healthBarImage.color = Color.HSVToRGB(0, 0, 1);
        // camera1.backgroundColor = Color.HSVToRGB(0, 1, 1);
        yield return new WaitForSeconds(.1f);
        healthBarImage.color = gradient.Evaluate(health/maxHealth);
        // camera1.backgroundColor = Color.HSVToRGB(0, 0, 0);
    }

    IEnumerator Shake()
    {
        timeVar = 0;
        while(timeVar <= 1)
        {
            yield return new WaitForSeconds(.01f);
            timeVar += 0.1f;
            transform.localScale = new Vector3(1 + animCurve.Evaluate(timeVar), 1 + animCurve.Evaluate(timeVar), 1);
        }        
    }
}
