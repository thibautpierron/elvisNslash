using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : Sort {

	private int					armor = 50;
	private int					time = 5;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	public override IEnumerator Execute() {
		clone.transform.Translate(0,1,0);
		player.GetComponent<Stats>().armor += armor;
		yield return new WaitForSeconds(time);
		player.GetComponent<Stats>().armor -= armor;
		yield return new WaitForSeconds(2);
		Destroy(clone.gameObject);
		Destroy(gameObject);
	}

	public override void Upgrade() {
		level++;
		if (level == 1)
			return ;
		armor = (int)((float)armor * 1.5f);
		time += 2;
	}

	public override string Info() {
		return "Fait une bulle de protection autour de vous. Augmente votre armure de " + armor.ToString() + " pendant " + time.ToString()
		+ " secondes.\nProchain niveau:\n - Armor " + ((int)((float)armor * 1.5f)).ToString() + "\n - durée " + (time + 2).ToString() + " sec";
	}
}
