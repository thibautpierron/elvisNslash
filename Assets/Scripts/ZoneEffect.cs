using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneEffect : MonoBehaviour {

	public float	rayon;
	public Light[]	spots;

	// Use this for initialization
	void Start () {
		spots[0].transform.position = transform.position + new Vector3(0.0f,1.0f,rayon);
		spots[1].transform.position = transform.position + new Vector3(0.0f,1.0f,-rayon);
		spots[2].transform.position = transform.position + new Vector3(rayon,1.0f,0.0f);
		spots[3].transform.position = transform.position + new Vector3(-rayon,1.0f,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0.0f, 40.0f, 0.0f);
	}
}
