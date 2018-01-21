using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;

	private GameObject remove;

	public int size = 10;
	public int frequency = 10;
	public int zoneSize;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < size; i++) {
			spawnZombie();
		}
	}
	
	// Update is called once per frame
	public void spawnZombie() {
		int i = Random.Range(0, prefabs.Length);
		float x = Random.Range(transform.position.x - zoneSize, transform.position.x + zoneSize);
		float z = Random.Range(transform.position.z - zoneSize, transform.position.z + zoneSize);
		GameObject zom = GameObject.Instantiate(prefabs[i], new Vector3(x, transform.position.y, z), Quaternion.identity);
		zom.GetComponent<Zombie>().spawner = this;
	}
}
