﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleGatling : Gun{


	public Transform[] SpawnPoints;

	public GameObject projectile;
	float timer = 0;
	public float fireRate;


	public override void Shooting(bool isPlayer)
	{
		timer += fireRate * Time.deltaTime;
		if (timer >= 1) {
			Shoot(isPlayer);
			timer = 0;
		}
	}

	void Shoot(bool isPlayer)
	{
		for (int i = 0; i < SpawnPoints.Length; i++){
			GameObject go = Instantiate(projectile, SpawnPoints[i].position, transform.rotation);
			if (isPlayer) {
				go.layer = 12;
			} else {
				go.layer = 11;
			}
			go.GetComponent<ProjectileBehaviour>().StartCountdown();
		}
	}
}
