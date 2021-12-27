using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

	public float speed = 5;

	public Text livesText;

	private Rigidbody rb;

	private float lastYPos;

	public bool sliding;
	public int dir;

	public void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	public void Start()
	{

		lastYPos = transform.position.y;

	}

    // Update is called once per frame
    public void Update()
	{
		float x = Input.GetAxis("Vertical");
		float y = Input.GetAxis("Horizontal");

		rb.MovePosition(transform.position + new Vector3(-x, 0, y) * speed * Time.deltaTime);

		if (transform.position.y > lastYPos + 5)
		{
			lastYPos = transform.position.y;
		}

	}

	public void FixedUpdate()
	{

		if (LevelController.instance.gameOver)
			return;

	}

	public void SetText(int amount)
	{
		livesText.text = amount.ToString();
	}

	public void TakeDamage()
	{
		if (LevelController.instance.gameOver)
			return;

		int children = transform.childCount;
		if (children <= 1)
		{
			LevelController.instance.GameOver();
		}
		else
		{
			Destroy(transform.GetChild(children - 1).gameObject);
		}

		SetText(children - 1);
	}

	public void Slide(int direction)
	{
		sliding = true;
		dir = direction;
		Invoke("SetSlideToFalse", 0.25f);
	}

	public void SetSlideToFalse()
	{
		sliding = false;
	}

}