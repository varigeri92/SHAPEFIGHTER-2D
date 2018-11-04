using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

	[SerializeField]
	float slowAmount;

	float _fixedDeltaTime;
	private void Start()
	{
		_fixedDeltaTime = Time.fixedDeltaTime;
	}


	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			Time.timeScale = slowAmount;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			Time.timeScale = 1;
			Time.fixedDeltaTime = _fixedDeltaTime;
		}
	}
}
