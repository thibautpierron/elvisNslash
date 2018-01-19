using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;

	private GameObject remove;

	public int size = 10;
	public int frequency = 10;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < size; i++) {
			spawnZombie();
		}
	}
	
	// Update is called once per frame
	public void spawnZombie() {
		int i = Random.Range(0, 2);
		float x = Random.Range(transform.position.x - 5, transform.position.x + 5);
		float z = Random.Range(transform.position.z - 5, transform.position.z + 5);
		GameObject zom = GameObject.Instantiate(prefabs[i], new Vector3(x, transform.position.y, z), Quaternion.identity);
		zom.GetComponent<Zombie>().spawner = this;
	}
}
