using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour {

	public GameObject characPanel;
	public GameObject inventoryPanel;
	public GameObject talentPanel;
	public GameObject menuPanel;

	public enum Selected {
		CHARAC,
		INVENTORY,
		TALENT,
		MENU
	}
	public Selected selected;
	// Use this for initialization
	void Awake () {
		// characPanel = GameObject.Find("Characteristic");
		// inventoryPanel = GameObject.Find("Inventory");
		// talentPanel = GameObject.Find("Talent");
		// menuPanel = GameObject.Find("Menu");
		characPanel.gameObject.SetActive(false);
		inventoryPanel.gameObject.SetActive(false);
		talentPanel.gameObject.SetActive(false);
		menuPanel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void selectCharac() {
		Debug.Log("ButtonPressed");
		selected = Selected.CHARAC;
		characPanel.gameObject.SetActive(true);
	}

	public void selectInventory() {
		Debug.Log("ButtonPressed");
		selected = Selected.INVENTORY;
		inventoryPanel.gameObject.SetActive(true);
	}

	public void selectTalent() {
		Debug.Log("ButtonPressed");
		selected = Selected.TALENT;
		talentPanel.gameObject.SetActive(true);
	}

	public void selectMenu() {
		Debug.Log("ButtonPressed");
		selected = Selected.MENU;
		menuPanel.gameObject.SetActive(true);
	}
}
