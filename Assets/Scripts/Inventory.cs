using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public Weapon[] weapons = new Weapon[10];
	public Weapon[] prefabs; // 0:guitar, 1:snow, 2:laser
	public Weapon currentGuitar;
	public Weapon currentWeapon;
	public InventoryMenu ui;

	// private void Awake() {
		
	// }
	private void Awake() {
		currentGuitar = GameObject.Instantiate(prefabs[0], new Vector3(1000, 1000, 1000), Quaternion.identity);
		currentWeapon = GameObject.Instantiate(prefabs[1], new Vector3(1000, 1000, 1000), Quaternion.identity);

		weapons[0] = GameObject.Instantiate(prefabs[0], new Vector3(1000, 1000, 1000), Quaternion.identity);
		weapons[1] = GameObject.Instantiate(prefabs[1], new Vector3(1000, 1000, 1000), Quaternion.identity);
		weapons[2] = GameObject.Instantiate(prefabs[2], new Vector3(1000, 1000, 1000), Quaternion.identity);
		weapons[3] = GameObject.Instantiate(prefabs[1], new Vector3(1000, 1000, 1000), Quaternion.identity);
	}
	// Use this for initialization
	public void add(Weapon w) {
		for (int i = 0; i < weapons.Length; i++) {
			if (!weapons[i]) {
				weapons[i] = w;
				weapons[i].setOnInventory();
				return;
			}
		}
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
		return Instantiate(currentGuitar, prefabs[0].transform.position, prefabs[0].transform.rotation);
	}

	public Weapon getCurrentWeapon() {
		return Instantiate(currentWeapon, Vector3.zero, Quaternion.identity);
	}
	public Weapon getCurrentGuitarRef() {
		return currentGuitar;
	}

	public Weapon getCurrentWeaponRef() {
		return currentWeapon;
	}
}
