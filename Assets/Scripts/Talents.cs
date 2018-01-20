using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talents : MonoBehaviour {

	public Text		description;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetDescription(Sort sort) {
		description.text = sort.Info();
	}
}
