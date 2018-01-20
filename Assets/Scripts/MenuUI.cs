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
		MENU,
		NONE
	}
	public Selected selected;
	// Use this for initialization
	void Awake () {
		characPanel.gameObject.SetActive(false);
		inventoryPanel.gameObject.SetActive(false);
		talentPanel.gameObject.SetActive(false);
		menuPanel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void select(Selected s) {
		selected = s;
		switch(selected) {
			case Selected.CHARAC:
				characPanel.gameObject.SetActive(true);
				inventoryPanel.gameObject.SetActive(false);
				talentPanel.gameObject.SetActive(false);
				menuPanel.gameObject.SetActive(false);
				break;
			case Selected.TALENT:
				characPanel.gameObject.SetActive(false);
				inventoryPanel.gameObject.SetActive(false);
				talentPanel.gameObject.SetActive(true);
				menuPanel.gameObject.SetActive(false);
				break;
			case Selected.INVENTORY:
				characPanel.gameObject.SetActive(false);
				inventoryPanel.gameObject.SetActive(true);
				talentPanel.gameObject.SetActive(false);
				menuPanel.gameObject.SetActive(false);
				break;
			case Selected.MENU:
				characPanel.gameObject.SetActive(false);
				inventoryPanel.gameObject.SetActive(false);
				talentPanel.gameObject.SetActive(false);
				menuPanel.gameObject.SetActive(true);
				break;
			case Selected.NONE:
				characPanel.gameObject.SetActive(false);
				inventoryPanel.gameObject.SetActive(false);
				talentPanel.gameObject.SetActive(false);
				menuPanel.gameObject.SetActive(false);
				break;
		}
	}

	public void selectCharac() {
		selected = Selected.CHARAC;
		select(Selected.CHARAC);
	}

	public void selectInventory() {
		selected = Selected.INVENTORY;
		select(Selected.INVENTORY);
	}

	public void selectTalent() {
		selected = Selected.TALENT;
		select(Selected.TALENT);
	}

	public void selectMenu() {
		selected = Selected.MENU;
		select(Selected.MENU);
	}
}
