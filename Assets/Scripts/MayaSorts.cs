using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayaSorts : MonoBehaviour {

	public Sort[]		sorts;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("1"))
		{
			Sort clone = Instantiate(sorts[0]);
			clone.player = gameObject.GetComponent<Maya>();
		}
		if (Input.GetKeyDown("2"))
		{
			Sort clone = Instantiate(sorts[1]);
			clone.player = gameObject.GetComponent<Maya>();
		}
		if (Input.GetKeyDown("3"))
		{
			Sort clone = Instantiate(sorts[2]);
			clone.player = gameObject.GetComponent<Maya>();
		}
		if (Input.GetKeyDown("4"))
		{
			Sort clone = Instantiate(sorts[3]);
			clone.player = gameObject.GetComponent<Maya>();
		}
	}
}
