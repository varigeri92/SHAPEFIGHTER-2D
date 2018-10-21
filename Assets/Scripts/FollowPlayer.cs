using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform player;
	float speed;

	Vector3 prevpos;

	// Use this for initialization
	void Start () {
		speed = player.GetComponent<Player>().speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			speed = player.GetComponent<Player>().speed * 2;
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			speed = player.GetComponent<Player>().speed;
		}
	}
	private void LateUpdate()
	{

		Vector3 targetPos = player.transform.position;
		targetPos.z = -10f;

		// stransform.position = targetPos;


		Vector3 playerPos = Camera.main.WorldToScreenPoint(player.position);

		if (playerPos.y >= (Screen.height - Screen.height * 0.2f)) {
			MooveCamera(Vector3.up);

		} else if (playerPos.y <= (Screen.height * 0.2f)) {
			MooveCamera(Vector3.down);

		}
		if (playerPos.x <= (Screen.width * 0.2f)) {
			MooveCamera(Vector3.left);

		} else if (playerPos.x >= (Screen.width - Screen.width * 0.2f)) {
			MooveCamera(Vector3.right);
		}

		/*
		else
		{
			if (Vector2.Distance((Vector2)transform.position, (Vector2)playerPos) > )
			{
				transform.position = Vector2.Lerp((Vector2)transform.position, (Vector2)playerPos, Time.deltaTime);
			}
		}
		*/
	}

	private void MooveCamera(Vector3 dir){
		transform.position = Vector3.Lerp(transform.position, transform.position + dir * speed, Time.deltaTime);
	}
}
