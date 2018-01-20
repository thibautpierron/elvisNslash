using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public enum Type {
		ONE_HAND,
		TWO_HAND,
		DOUBLE
	}
	// public GameObject skin;
	public Type type;

	public float hitRate;

	public int damage;

	// private Vector3 handPosition;
	// Use this for initialization
	// void Awake () {
	// 	handPosition = GameObject.Find("HandPlace").transform.position;
	// }

	public float getFirstHitTiming() {
		switch(type) {
			case Type.ONE_HAND: return 0.5f;
			case Type.TWO_HAND: return 0.5f;
			case Type.DOUBLE: return 0.5f;
		}
		return 0;
	}

	public float getRegularHitTiming() {
		switch(type) {
			case Type.ONE_HAND: return 1.7f;
			case Type.TWO_HAND: return 1.7f;
			case Type.DOUBLE: return 1.7f;
		}
		return 0;
	}
}
