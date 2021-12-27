using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

	public Obstacle[] allObstacles;
	public GameObject[] barriers;

	public Vector3 positionRange;
	public GameObject ObstaclesGroup;

	private Transform player;

	// Use this for initialization
	void Start()
	{

		player = FindObjectOfType<Player>().transform;
		SetObstacles();
	}

	void SetObstacles()
	{
		for (int i = 0; i < allObstacles.Length; i++)
		{
			allObstacles[i].SetAmount();
		}

		for (int i = 0; i < barriers.Length; i++)
		{
			bool randomBool = Random.value > 0.5f;
			barriers[i].SetActive(randomBool);
		}
	}

	void Reposition()
	{
		int obstaclesAmount = FindObjectsOfType<Obstacles>().Length;

		transform.position = new Vector3(0, player.position.y + (LevelController.instance.obstaclesDistance * (obstaclesAmount - 1)));

		ObstaclesGroup.transform.localPosition = new Vector3(0, Random.Range(positionRange.x, positionRange.y));
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Reposition();

			SetObstacles();
		}
	}
}