﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp {

	PowerUpType type = PowerUpType.Shield;

	public int shield;


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player"){
			// other.gameObject.GetComponent<Player>().PickUpShield(shield);
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			other.gameObject.GetComponent<Player>().PickUpShield(shield);
			base.PickedUp();
		}
	}
}
