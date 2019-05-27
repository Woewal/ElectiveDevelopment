using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager2 : MonoBehaviour
{
    public int maxCount = 3;
    public int count = 0;
    public GameObject[] PickupArray;
    public float timer = 30;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CreatPickup();
    }

    void CreatPickup()
    {
        if (count >= maxCount)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            count++;
            Instantiate(PickupArray[Random.Range(0, PickupArray.Length)], new Vector2(Random.Range(0, 20), Random.Range(0, 20)), Quaternion.identity);
            timer = 30;
        }
    }
}

