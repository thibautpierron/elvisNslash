using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	public enum Mode {
		NORMAL,
		CLOSE,
		INVENTORY
	}
	private GameObject maya;
	private Vector3 offset;
	public GameObject inventorySpot;
	public GameObject mixTable;

	public Mode mode = Mode.NORMAL;
	// Use this for initialization
	void Start () {
		maya = GameObject.Find("Maya");
		offset = transform.position - maya.transform.position;
		inventorySpot.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		switch (mode) {
			case Mode.NORMAL:
				inventorySpot.SetActive(false);
				transform.position = maya.transform.position + offset;
				break;
			case Mode.CLOSE: 
				inventorySpot.SetActive(false);
				transform.position = maya.transform.position + offset / 3;
				break;
			case Mode.INVENTORY:
				inventorySpot.SetActive(true);
				break;
		}
	}

	public void switchDistanceView() {
		switch (mode) {
			case Mode.NORMAL:
				mode = Mode.CLOSE;
				break;
			case Mode.CLOSE:
				mode = Mode.NORMAL;
				break;
			case Mode.INVENTORY:
				mode = Mode.INVENTORY;
				break;
		}
	}

	public void switchInventoryView() {
		switch (mode) {
			case Mode.NORMAL:
				mode = Mode.INVENTORY;
				break;
			case Mode.CLOSE:
				mode = Mode.INVENTORY;
				break;
			case Mode.INVENTORY:
				mode = Mode.NORMAL;
				break;
		}
	}
}
