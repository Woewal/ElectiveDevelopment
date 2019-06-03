using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public static Spawner Instance;

	[Header("Spawn locations")]
	public List<Transform> Team1Spawns = new List<Transform>();
	public List<Transform> Team2Spawns = new List<Transform>();
	public List<Transform> PickupSpawns = new List<Transform>();
	public Transform BallSpawn;

	[Header("Prefabs")]
	public List<Robot> RobotPrefabs;
	public List<Pickup> PickupPrefabs;
	public Ball BallPrefab;

	[Header("Settings")]
	public float RespawnDuration;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		SpawnBall();
		SpawnPlayers();
		SpawnPickups();

	}

	void SpawnPlayers()
	{
		for (int i = 0; i < RobotPrefabs.Count; i++)
		{
			var robotPrefab = RobotPrefabs[i];
			var robot = Instantiate(robotPrefab);
			robot.transform.position = Team1Spawns[i].position;
			robot.team = 1;
		}

		for (int i = 0; i < RobotPrefabs.Count; i++)
		{
			var robotPrefab = RobotPrefabs[i];
			var robot = Instantiate(robotPrefab);
			robot.transform.position = Team2Spawns[i].position;
			robot.team = 2;
		}
	}
	void SpawnBall()
	{
		var ball = Instantiate(BallPrefab);
		ball.transform.position = BallSpawn.transform.position;
	}
	void SpawnPickups()
	{
		for(int i = 0; i < PickupSpawns.Count; i++)
		{
			int randomNumber = Random.Range(0, PickupPrefabs.Count);
			var pickupPrefab = PickupPrefabs[randomNumber];
			var pickup = Instantiate(pickupPrefab);
			pickup.transform.position = PickupSpawns[i].transform.position;
		}
	}

	public void Respawn(Robot robot)
	{
		StartCoroutine(RespawnRobotCoroutine(2, robot));
	}

	public void Respawn(GameObject target)
	{
		StartCoroutine(RespawnCoroutine(RespawnDuration, target));
	}

	IEnumerator RespawnRobotCoroutine(float duration, Robot robot)
	{
		float currentTime = 0;

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			yield return null;
		}

		robot.transform.position = robot.respawnLocation;
		robot.gameObject.SetActive(true);
	}

	IEnumerator RespawnCoroutine(float duration, GameObject target)
	{
		float currentTime = 0; 

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			yield return null;
		}

		target.SetActive(true);
	}
}
