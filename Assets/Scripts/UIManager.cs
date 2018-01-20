using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public enum State {
		PLAY,
		INVENTORY,
		DEATH
	}
	private Stats maya;
	private State state;

	private GameObject deathPanel;
	private GameObject inGameUI;
	public GameObject inventoryUI;

	private MenuUI.Selected currentMenu = MenuUI.Selected.NONE;
	// Use this for initialization
	void Start () {
		maya = GameObject.Find("Maya").GetComponent<Stats>();
		deathPanel = GameObject.Find("DeathPanel");
		deathPanel.gameObject.SetActive(false);
		inGameUI = GameObject.Find("InGameUICanvas");
		// inventoryUI = GameObject.Find("InventoryUICanvas");
		state = State.PLAY;
	}
	
	// Update is called once per frame
	void Update () {
		switch(state) {
			case State.PLAY:
				inGameUI.gameObject.SetActive(true);
				inventoryUI.gameObject.SetActive(false);
				maya.GetComponent<Maya>().inMenu = false;
				if (maya.hp <= 0) {
					deathPanel.gameObject.SetActive(true);
					state = State.DEATH;
					return;
				}
				if (Input.GetKeyDown("space"))
					Camera.main.GetComponent<Cam>().switchDistanceView();
				getInput();
				break;
			case State.INVENTORY:
				maya.GetComponent<Maya>().inMenu = true;
				inGameUI.gameObject.SetActive(false);
				inventoryUI.gameObject.SetActive(true);
				getInput();
				break;
			case State.DEATH:
				maya.GetComponent<Maya>().inMenu = true;
				if (Input.GetKey("space"))
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				break;
		}
	}

	void getInput() {
		if (Input.GetKeyDown("c")) {
			if (currentMenu == MenuUI.Selected.CHARAC) {
				inventoryUI.GetComponent<MenuUI>().select(MenuUI.Selected.NONE);
				Camera.main.GetComponent<Cam>().closeInventory();
				state = State.PLAY;
				currentMenu = MenuUI.Selected.NONE;
			} else {
				inventoryUI.GetComponent<MenuUI>().select(MenuUI.Selected.CHARAC);
				Camera.main.GetComponent<Cam>().openInventory();
				state = State.INVENTORY;
				currentMenu = MenuUI.Selected.CHARAC;
			}
		}
		else if (Input.GetKeyDown("i")) {
			if (currentMenu == MenuUI.Selected.INVENTORY) {
				inventoryUI.GetComponent<MenuUI>().select(MenuUI.Selected.NONE);
				Camera.main.GetComponent<Cam>().closeInventory();
				state = State.PLAY;
				currentMenu = MenuUI.Selected.NONE;
			} else {
				inventoryUI.GetComponent<MenuUI>().select(MenuUI.Selected.INVENTORY);
				Camera.main.GetComponent<Cam>().openInventory();
				state = State.INVENTORY;
				currentMenu = MenuUI.Selected.INVENTORY;
			}
		}
		else if (Input.GetKeyDown("n")) {
			if (currentMenu == MenuUI.Selected.TALENT) {
				inventoryUI.GetComponent<MenuUI>().select(MenuUI.Selected.NONE);
				Camera.main.GetComponent<Cam>().closeInventory();
				state = State.PLAY;
				currentMenu = MenuUI.Selected.NONE;
			} else {
				inventoryUI.GetComponent<MenuUI>().select(MenuUI.Selected.TALENT);
				Camera.main.GetComponent<Cam>().openInventory();
				state = State.INVENTORY;
				currentMenu = MenuUI.Selected.TALENT;
			}
		}
		else if (Input.GetKeyDown(KeyCode.Escape)) {
			if (currentMenu == MenuUI.Selected.MENU) {
				inventoryUI.GetComponent<MenuUI>().select(MenuUI.Selected.NONE);
				Camera.main.GetComponent<Cam>().closeInventory();
				state = State.PLAY;
				currentMenu = MenuUI.Selected.NONE;
			} else {
				inventoryUI.GetComponent<MenuUI>().select(MenuUI.Selected.MENU);
				Camera.main.GetComponent<Cam>().openInventory();
				state = State.INVENTORY;
				currentMenu = MenuUI.Selected.MENU;
			}
		}
	}

	public void mainMenu() {
		SceneManager.LoadScene(0);
	}

	public void quit() {
		Application.Quit();
	}
}
