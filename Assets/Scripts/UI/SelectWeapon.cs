using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour {

	public GameObject InventoryPanel;

	public Transform marker;

	public Transform[] slotPositions;
	int selected = 0;


	public void OnCollectablepickedUp(){
		
	}

	void SelectElement(){
		if (Input.GetKeyDown(KeyCode.E)) {
			selected++;
			if (selected > slotPositions.Length - 1) {
				selected = 0;
			}
			marker.transform.position = slotPositions[selected].position;
			// StartCoroutine(MoveMarker(slotPositions[selected]));
		}
		else if(Input.GetKeyDown(KeyCode.Q))
		{
			selected--;
			if (selected < 0) {
				selected = slotPositions.Length -1;
			}
			marker.transform.position = slotPositions[selected].position;
			// StartCoroutine(MoveMarker(slotPositions[selected]));
		}
	}

	void TriggerChange(){
		
	}

	// Use this for initialization
	void Start()
	{
		// slotPositions = InventoryPanel.GetComponentsInChildren<Transform>();
		marker.position = slotPositions[selected].position;
	}
	// Update is called once per frame
	void Update () {
		SelectElement();
	}

	IEnumerator MoveMarker(Transform target){
		float distance = 0;
		float duration = 0.25f;
		Vector2 markerPos = marker.transform.position;
		while(distance < 1){
			distance += Time.deltaTime / duration;
			marker.transform.position = Vector2.Lerp(markerPos,target.position,distance);
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}
}
