using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public enum Type {
		ONE_HAND,
		TWO_HAND,
		DOUBLE
	}

	public enum Quality {
		NORMAL,
		RARE,
		EPIQUE,
		LEGENDARY
	}
	// public GameObject skin;
	public Type type;

	public float hitRate;

	public int damage;

	public bool onGround;
	public ParticleSystem shine;
	public ParticleSystem shineInstance = null;
	public Texture icon;
	public Quality quality = Quality.NORMAL;

	private BoxCollider coll;
	// private Vector3 handPosition;
	// Use this for initialization
	void Awake () {
		coll = gameObject.GetComponent<BoxCollider>();
		coll.enabled = false;
	}

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

	public int getDamage() {
		switch(quality) {
			case Quality.NORMAL: return damage;
			case Quality.RARE: return damage * 2;
			case Quality.EPIQUE: return damage * 3;
			case Quality.LEGENDARY: return damage * 5;
		}
		return damage;
	}

	public string getText() {
		string t = "";
		string q = "";

		switch(type) {
			case Type.ONE_HAND: t = "One Hand"; break;
			case Type.TWO_HAND: t = "Two Hand"; break;
			case Type.DOUBLE: t = "Double"; break;
		}

		switch(quality) {
			case Quality.NORMAL: q = "Normal"; break;
			case Quality.RARE: q = "Rare"; break;
			case Quality.EPIQUE: q = "Epique"; break;
			case Quality.LEGENDARY: q = "Legendary"; break;
		}

		return t + "\n" + damage + "\n" + q;
	}

	public void setOnGround() {
		shineInstance = GameObject.Instantiate(shine, transform.position, Quaternion.identity);
		coll.enabled = true;
	}

	public void setOnInventory() {
		coll.enabled = false;
		Destroy(shineInstance.gameObject);
		shineInstance = null;
		transform.position = new Vector3(1000, 1000, 1000);
	}
}
