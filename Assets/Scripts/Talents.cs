using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talents : MonoBehaviour {

	public Text			description;
	public Text			points;
	private Sort		actif;
	public MayaSorts	player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("1") && actif && actif.level != 0)
			SetPosition(actif, 0);
		if (Input.GetKeyDown("2") && actif && actif.level != 0)
			SetPosition(actif, 1);
		if (Input.GetKeyDown("3") && actif && actif.level != 0)
			SetPosition(actif, 2);
		if (Input.GetKeyDown("4") && actif && actif.level != 0)
			SetPosition(actif, 3);

		points.text = "point(s): " + player.GetComponent<Stats>().talentPoints;
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

	public void UpgradeSort() {
		player.GetComponent<Stats>().talentPoints--;
	}
}
