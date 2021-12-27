using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{

	public Text amountText;

	private int amount;

	private Player player;
	private float nextTime;

	private Color initialColor;

	private MeshRenderer meshRenderer;
	public void Awake()
	{
		meshRenderer =gameObject.GetComponent<MeshRenderer>();
	}

	// Use this for initialization
	public void Start()
	{



	}

	// Update is called once per frame
	public void Update()
	{

		if (player != null && nextTime < Time.time)
		{
			PlayerDamage();
		}

	}

	public void SetAmount()
	{
		gameObject.SetActive(true);
		amount = Random.Range(0, LevelController.instance.obstaclesAmount);
		if (amount <= 0)
		{
			gameObject.SetActive(false);
		}

		SetAmountText();
		SetColor();
	}

	public void SetAmountText()
	{
		amountText.text = amount.ToString();
	}

	public void SetColor()
	{
		int playerLives = FindObjectOfType<Player>().transform.childCount;
		Color newColor;

		if (amount > playerLives)
		{
			newColor = LevelController.instance.hardColor;
		}
		else if (amount > playerLives / 2)
		{
			newColor = LevelController.instance.mediumColor;
		}
		else
		{
			newColor = LevelController.instance.easyColor;
		}
		meshRenderer.material.color = newColor;
		initialColor = newColor;
	}

	void PlayerDamage()
	{
		if (LevelController.instance.gameOver)
			return;

		
		nextTime = Time.time + LevelController.instance.damageTime;
		player.TakeDamage();
		amount--;
		SetAmountText();
		if (amount <= 0)
		{
			gameObject.SetActive(false);
			player = null;
		}
		else
		{
			StopAllCoroutines();
			StartCoroutine(DamageColor());
		}
	}

	IEnumerator DamageColor()
	{
		float timer = 0;
		float t = 0;

		meshRenderer.material.color = initialColor;

		while (timer < LevelController.instance.damageTime)
		{
			meshRenderer.material.color = Color.Lerp(initialColor, Color.white, t);
			timer += Time.deltaTime;
			t += Time.deltaTime / LevelController.instance.damageTime;
			yield return null;
		}

		meshRenderer.material.color = initialColor;
	}

	public void OnCollisionEnter(Collision other)
	{
		Player otherPlayer = other.gameObject.GetComponent<Player>();
		if (otherPlayer != null)
		{
			player = otherPlayer;
		}
	}

	public void OnCollisionExit(Collision other)
	{
		Player otherPlayer = other.gameObject.GetComponent<Player>();
		if (otherPlayer != null)
		{
			player = null;
		}
	}
}