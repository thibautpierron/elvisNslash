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
	private GameObject inventoryUI;
	// Use this for initialization
	void Start () {
		maya = GameObject.Find("Maya").GetComponent<Stats>();
		deathPanel = GameObject.Find("DeathPanel");
		deathPanel.gameObject.SetActive(false);
		inGameUI = GameObject.Find("InGameUICanvas");
		inventoryUI = GameObject.Find("InventoryUICanvas");
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
				if (Input.GetKeyDown("c")) {
					Camera.main.GetComponent<Cam>().switchInventoryView();
					state = State.INVENTORY;
				}
				break;
			case State.INVENTORY:
				maya.GetComponent<Maya>().inMenu = true;
				inGameUI.gameObject.SetActive(false);
				inventoryUI.gameObject.SetActive(true);
				if (Input.GetKeyDown("c")) {
					Camera.main.GetComponent<Cam>().switchInventoryView();
					state = State.PLAY;
				}
				break;
			case State.DEATH:
				maya.GetComponent<Maya>().inMenu = true;
				if (Input.GetKey("space"))
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				break;
		}
		// if (death) {
		// 	return;
		// }
	}
}
