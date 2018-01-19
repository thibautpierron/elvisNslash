using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	private GameObject maya;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		maya = GameObject.Find("Maya");
		offset = transform.position - maya.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = maya.transform.position + offset;
	}
}
