using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talents : MonoBehaviour {

	public Text			description;
	private Sort		actif;
	public MayaSorts	player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("1") && actif)
			SetPosition(actif, 0);
		if (Input.GetKeyDown("2") && actif)
			SetPosition(actif, 1);
		if (Input.GetKeyDown("3") && actif)
			SetPosition(actif, 2);
		if (Input.GetKeyDown("4") && actif)
			SetPosition(actif, 3);
	}

	public void SetDescription(Sort sort) {
		description.text = sort.Info();
		SetActif(sort);
	}

	public void SetActif(Sort s) {
		actif = s;
	}

	public void SetPosition(Sort sort, int position) {
		for (int i = 0 ; i < 4 ; i++)
		{
			if (i == position)
				player.sorts[i] = sort;
			else if (player.sorts[i] == sort)
				player.sorts[i] = null;
		}
	}
}
