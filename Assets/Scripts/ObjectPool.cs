using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

	public GameObject prefab;
	public int amount;

	public GameObject[] prefabs;

	private int index;

	// Use this for initialization
	public void Awake()
	{

		prefabs = new GameObject[amount];

		for (int i = 0; i < amount; i++)
		{
			prefabs[i] = Instantiate(prefab, new Vector3(0, 15,2), Quaternion.identity);
			prefabs[i].SetActive(false);
		}

	}

	public GameObject GetObject()
	{

		index++;
		if (index >= amount)
		{
			index = 0;
		}

		prefabs[index].SetActive(true);

		return prefabs[index];
	}
}