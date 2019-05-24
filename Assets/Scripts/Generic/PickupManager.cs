using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance { get; private set; }

    public List<Pickup> allPickups { get; private set; } = new List<Pickup>();

    private int currentId;

    public void Awake()
    {
        Instance = this;
    }

    public void Register(Pickup pickup)
    {
        // pickup.id = currentId++;
        allPickups.Add(pickup);
    }
}
