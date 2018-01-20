using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentUpgrade : MonoBehaviour {

	public int		talentLevel;
	public Sort		sort;
	public RawImage	image;
	public Button	button;
	public Stats	maya;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (maya.level < talentLevel || maya.talentPoints == 0)
			button.interactable = false;
		else
			button.interactable = true;
	}
}
