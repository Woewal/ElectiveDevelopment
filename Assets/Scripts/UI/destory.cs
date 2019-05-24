using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DeatroyPickup();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void DeatroyPickup()
    {

        Destroy(transform.gameObject, 60);

    }
}
