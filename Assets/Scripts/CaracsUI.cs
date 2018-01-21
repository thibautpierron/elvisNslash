using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaracsUI : MonoBehaviour {

	public Text			strengh;
	public Text			agility;
	public Text			constitution;
	public Button		strenghButton;
	public Button		agilityButton;
	public Button		constitutionButton;
	public Stats		maya;

	public Text			armor;
	public Text			maxDamage;
	public Text			minDamage;
	public Text			maxHP;
	public Text			HP;

	public Text			points;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (maya.caracPoints == 0)
		{
			strenghButton.interactable = false;
			agilityButton.interactable = false;
			constitutionButton.interactable = false;
		}
		else
		{
			strenghButton.interactable = true;
			agilityButton.interactable = true;
			constitutionButton.interactable = true;
		}
		strengh.text = "  Strengh: " + maya.strengh.ToString();
		agility.text = "  Agility: " + maya.agility.ToString();
		constitution.text = "  Constitution: " + maya.constitution.ToString();

		armor.text = "armor: " + maya.armor.ToString();
		maxDamage.text = "max damage: " + maya.maxDamage.ToString();
		minDamage.text = "min damage: " + maya.minDamage.ToString();
		maxHP.text = "max HP: " + maya.hpMax.ToString();
		HP.text = "HP: " + maya.hp.ToString();
		points.text = "point(s): " + maya.caracPoints.ToString();
	}

	public void StrenghUpgrade() {
		maya.strengh++;
		maya.caracPoints--;
		maya.RefreshStats();
	}

	public void AgilityUpgrade() {
		maya.agility++;
		maya.caracPoints--;
		maya.RefreshStats();
	}

	public void ConstitutionUpgrade() {
		maya.constitution++;
		maya.caracPoints--;
		maya.RefreshStats();
	}
}
