using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//D
public class LevelGenerator : MonoBehaviour
{
	[SerializeField] GameObject[] _walls;
	[SerializeField] float _spawnChance;


	void Start()
	{
		foreach(var wall in _walls)
		{
			var randomValue = Random.value;

			if (randomValue < _spawnChance)
				wall.SetActive(false);
		}
	}

}
