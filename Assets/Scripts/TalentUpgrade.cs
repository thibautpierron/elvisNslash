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
	public Text		key;
	public Text		level;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (maya.level < talentLevel || maya.talentPoints == 0)
			button.interactable = false;
		else
			button.interactable = true;
		if (sort.level == 0)
			image.color = Color.grey;
		else
			image.color = Color.white;

		level.text = " lvl: " + sort.level.ToString();

		SetKeyText();
	}

	void SetKeyText() {
		MayaSorts player = maya.GetComponent<MayaSorts>();

		for (int i = 0 ; i < 4 ; i++)
		{
			if (player.sorts[i] == sort)
			{
				key.text = " key: " + (i + 1).ToString();
				return ;
			}
		}
		key.text = " key: -";
	}
}
