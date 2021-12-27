using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

	public Transform Target;

	public float minSpeed = 1;
	public float averageSpeed = 15;
	public float maxSpeed = 30;

	private float initialHeight;

	// Use this for initialization
	public void Start()
	{

		initialHeight = transform.localPosition.y;

	}

	// Update is called once per frame
	public void Update()
	{

		float distance = Mathf.Abs(Target.position.x - transform.position.x);
		float newSpeed;

		float percent = (distance / 2);

		newSpeed = (averageSpeed * percent) + (minSpeed * percent);

		if (distance > 2)
		{
			newSpeed = maxSpeed;
		}

		Vector3 newPos = new Vector3(Target.position.x, transform.position.y + percent);

		transform.position = Vector3.MoveTowards(transform.position, newPos, newSpeed * Time.deltaTime);

		transform.localPosition = new Vector3(transform.localPosition.x,
			Mathf.Clamp(transform.localPosition.y, initialHeight, initialHeight + percent));

	}
}