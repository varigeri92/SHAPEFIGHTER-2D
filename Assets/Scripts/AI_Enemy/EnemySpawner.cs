using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {



	public List<GameObject> enemyes = new List<GameObject>();
	public List<Transform> points = new List<Transform>();
	public List<EnemyFollow> spawnedEnemyes = new List<EnemyFollow>();


	public int count;
	public float rate;
	public float time = 0;

	int lastrandomindex;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < transform.childCount; i++){
			Transform t = transform.GetChild(i);
			points.Add(t);
		}
		EnemyFollow.onEnemyDead += RemoveEnemyFromList;
	}

	void RemoveEnemyFromList(EnemyFollow enemy){
		spawnedEnemyes.Remove(enemy);
		Debug.Log("Enemy Shot down!");
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime * rate;
		if(time >= 1){
			for (int i = 0; i < count; i++) {
				Spawn();
			}
			time = 0;
		}
	}

	void Spawn(){
		int rand = Random.Range(0,points.Count);
		if(rand == lastrandomindex){
			if(rand == 0){
				rand++;
			}else if (rand == points.Count -1){
				rand--;
			}else{
				rand++;
			}
		}
		lastrandomindex = rand;
		GameObject go = Instantiate(enemyes[(int)Random.Range(0,enemyes.Count)], points[rand].position, Quaternion.identity);
		spawnedEnemyes.Add(go.GetComponent<EnemyFollow>());
	}
}
