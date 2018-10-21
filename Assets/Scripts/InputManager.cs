using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	Vector2 direction;
	Vector2 movement;

	void Start () {
		
	}
	

	void Update () {

		// direction = new Vector2(Input.GetAxis(""), Input.GetAxis(""));
		// movement = new Vector2(Input.GetAxis(""),Input.GetAxis(""));

	}
	Vector2 GetMovement()
	{
		return movement;
	}
	Vector2 GetDirection()
	{
		return direction;
	}
}
