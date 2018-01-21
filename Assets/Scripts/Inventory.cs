using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public Weapon[] weapons = new Weapon[10];
	public Weapon[] prefabs;
	private Weapon[] clones;
	public Weapon currentGuitar;
	public Weapon currentWeapon;

	private void Start() {
		clones = new Weapon[prefabs.Length];
		for(int i = 0; i < prefabs.Length; i++) {
			clones[i] = GameObject.Instantiate(prefabs[i], new Vector3(1000, 1000, 1000), Quaternion.identity);
		}
		currentGuitar = GameObject.Instantiate(prefabs[0], new Vector3(1000, 1000, 1000), Quaternion.identity);
		currentWeapon = GameObject.Instantiate(prefabs[1], new Vector3(1000, 1000, 1000), Quaternion.identity);
	}
	// Use this for initialization
	public void add(Weapon w) {

	}

	public void drop(Weapon w) {
		
	}

	public void equipWeapon(Weapon w) {

	}

	public void equipeGuitar(Weapon w) {

	}
	public void	refreshMaya() {
		gameObject.GetComponent<Maya>().refreshInventory();
	}
	public Weapon getCurrentGuitar() {
		return currentGuitar;
	}

	public Weapon getCurrentWeapon() {
		return currentWeapon;
	}
}
