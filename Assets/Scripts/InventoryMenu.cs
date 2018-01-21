using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour {

	private RawImage[] stashIcons = new RawImage[10];
	public RawImage guitarIcon;
	public Text guitarText;
	public RawImage weaponIcon;
	public Text weaponText;

	private Inventory inventory;
	// Use this for initialization
	void Start () {
		GameObject[] icons = GameObject.FindGameObjectsWithTag("ItemInventory");
		for (int i = 0; i < 10; i++) {
			stashIcons[i] = icons[i].GetComponent<RawImage>();
		}
		inventory = GameObject.Find("Maya").GetComponent<Inventory>();
		refresh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void refresh() {
		Weapon[] weapons = inventory.weapons;
		for (int i = 0; i < 10; i++) {
			if (weapons[i]) {
				stashIcons[i].color = new Color(1, 1, 1);
				stashIcons[i].texture = weapons[i].icon;
			} else {
				stashIcons[i].color = new Color(0.3f, 0.3f, 0.3f);
			}
		}
		Weapon w = inventory.getCurrentWeapon();
		Weapon g = inventory.getCurrentGuitar();

		if (w) {
			weaponIcon.color = new Color(1, 1, 1);
			weaponIcon.texture = w.icon;
			weaponText.text = w.getText();
		}
		else
			weaponIcon.color = new Color(0.3f, 0.3f, 0.3f);

		if (g) {
			guitarIcon.color = new Color(1, 1, 1);
			guitarIcon.texture = g.icon;
			guitarText.text = g.getText();
		}
		else
			guitarIcon.color = new Color(0.3f, 0.3f, 0.3f);
	}

	public void clickOnItem(int n) {
		Weapon[] weapons = inventory.weapons;
		if (!weapons[n])
			return;

		if (weapons[n].tag == "Guitar") {
			Weapon tmp = inventory.getCurrentGuitar();
			inventory.currentGuitar = weapons[n];
			weapons[n] = tmp; 
		} else {
			Weapon tmp = inventory.getCurrentWeapon();
			inventory.currentWeapon = weapons[n];
			weapons[n] = tmp; 
		}
		refresh();
		inventory.refreshMaya();
	}
}
