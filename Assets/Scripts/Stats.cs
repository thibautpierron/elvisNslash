using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

	public int strengh;
	public int agility;
	public int constitution;
	public int armor;
	public int hp;
	public int hpMax;
	[HideInInspector]public int minDamage;
	[HideInInspector]public int maxDamage;
	public int level;
	public int xp;
	public int money;
	public int levelUpXp;

	public int	talentPoints;
	public int	caracPoints;

	public ParticleSystem	levelUpPS;

	// Use this for initialization
	void Start () {
		hpMax = 5 * constitution;
		hp = hpMax;
		minDamage = strengh / 2;
		maxDamage = minDamage + 4;
		armor = strengh + constitution;
	}

	void Update() {
		if (levelUpXp != 0 && xp >= levelUpXp)
			LevelUp();
	}

	public void LevelUp() {
		xp -= levelUpXp;
		levelUpXp = (int)((float)levelUpXp * 1.5f);
		talentPoints++;
		caracPoints += 5;
		ParticleSystem clone = Instantiate(levelUpPS);
		clone.transform.position = transform.position;
		clone.transform.SetParent(gameObject.transform);
		Destroy(clone, 2);
		RefreshStats();
		hp = hpMax;
	}
	
	public int getDamage(Stats target) {
		if (!missHit(target.agility))
			return 0;
		
		return finalDamage(target.armor);
	}
	// Update is called once per frame
	bool	missHit(int targetAgi) {
		int chance = 75 + agility - targetAgi;
		int r = Random.Range(0, 100);

		return r < chance ? true : false;
	}

	int baseDamage() {
		return Random.Range(minDamage, maxDamage + 1);
	}

	int finalDamage(int targetArmor) {
		return baseDamage() * (1 - targetArmor / 200);
	}

	public void RefreshStats() {
		hpMax = 5 * constitution;
		hp += 5;
		minDamage = strengh / 2;
		maxDamage = minDamage + 4;
		armor = strengh + constitution;
	}
}
