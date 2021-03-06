﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {

	public Slider healthSlider;
	public Slider xpSlider;
	public Text health;
	public Text xp;
	public Text level;
	public RawImage[] sortsImages;

	public Image enemyStatsPanel;
	public Slider enemySlider;
	public Text enemyHealth;
	public Text enemyName;
	public Text enemyLevel;
	private Stats maya;
	private GameObject mayaTarget;

	// Use this for initialization
	void Start () {
		maya = GameObject.Find("Maya").GetComponent<Stats>();
	}
	
	// Update is called once per frame
	void Update () {
		enemyStats();
		mayaStats();
		mayaSorts();
	}

	void mayaSorts() {
		Sort[] sorts = maya.GetComponent<MayaSorts>().sorts;
		float[] lastSend = maya.GetComponent<MayaSorts>().LastSending;

		for (int i = 0 ; i < 4 ; i++)
		{
			if (!sorts[i])
			{
				sortsImages[i].transform.GetChild(0).GetComponent<Text>().text = "";
				sortsImages[i].color = new Color(0,0,0,0.5f);
				continue;
			}
			sortsImages[i].color = Color.white;
			if (lastSend[i] + sorts[i].restorationDelay < Time.realtimeSinceStartup)
				sortsImages[i].transform.GetChild(0).GetComponent<Text>().text = "";
			else
				sortsImages[i].transform.GetChild(0).GetComponent<Text>().text = ((int)(lastSend[i] + sorts[i].restorationDelay - Time.realtimeSinceStartup)).ToString();
			sortsImages[i].texture = sorts[i].image;
		}
	}

	void mayaStats() {
		healthSlider.maxValue = maya.hpMax;
		xpSlider.maxValue = maya.levelUpXp;
		healthSlider.value = maya.hp;
		xpSlider.value = maya.xp;
		health.text = maya.hp.ToString() + "  I  " + maya.hpMax.ToString();
		xp.text = maya.xp.ToString() + "  I  " + maya.levelUpXp.ToString();
		level.text = maya.level.ToString();
	}

	void enemyStats() {
		mayaTarget = maya.gameObject.GetComponent<Maya>().targetEnemy;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		GameObject target = null;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
			if (hit.collider.gameObject.tag == "Zombie") {
				target = hit.collider.gameObject;
			}
		}

		if (mayaTarget) {
			enemyStatsPanel.gameObject.SetActive(true);
			Stats s = mayaTarget.GetComponent<Stats>();
			enemySlider.maxValue = s.hpMax;
			enemySlider.value = s.hp;
			enemyLevel.text = s.level.ToString();
			enemyName.text = "ZOMBIE";/* /////////////////////////////////////////// */
			enemyHealth.text = s.hp.ToString() + "  I  " + s.hpMax.ToString();
		}
		else if (target) {
			Stats s = target.GetComponent<Stats>();
			enemySlider.maxValue = s.hpMax;
			enemySlider.value = s.hp;
			enemyLevel.text = s.level.ToString();
			enemyName.text = "ZOMBIE";/* /////////////////////////////////////////// */
			enemyHealth.text = s.hp.ToString() + "  I  " + s.hpMax.ToString();

			enemyStatsPanel.gameObject.SetActive(true);
		}
		else
			enemyStatsPanel.gameObject.SetActive(false);
	}
}
