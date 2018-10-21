using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : Gun {
	
	public Transform spawnPoint;
	public float fireRate;
	public GameObject projectile;
	public float projectileForce;

	float timer = 0;


	public override void Shooting(bool isPlayer)
	{
		timer += fireRate * Time.deltaTime;
		if(timer >= 1){
			Shoot(isPlayer);
			timer = 0;
		}
	}
	private void Shoot( bool isPlayer){
		GameObject go = Instantiate(projectile, spawnPoint.position,transform.rotation);
		if(isPlayer){
			go.layer = 12;
		}else{
			go.layer = 11;
		}
		go.GetComponent<ProjectileBehaviour>().StartCountdown();
	}
}
