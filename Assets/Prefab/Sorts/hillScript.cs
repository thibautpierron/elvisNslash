using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hillScript : Sort {

	private float				delay = 2.0f;
	private int					damage = 5;
	private int					time = 15;
	private List<Stats>			players;

	// Use this for initialization
	void Start () {
		players = new List<Stats>();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	public override IEnumerator Execute() {
		for (int i = 0 ; i < time / delay ; i++)
		{
			foreach(Stats stat in players)
			{
				stat.hp += damage;
				if (stat.hp > stat.hpMax)
					stat.hp = stat.hpMax;
			}
			yield return new WaitForSeconds(delay);
		}
		yield return new WaitForSeconds(5);
		Destroy(clone.gameObject);
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
		{
			players.Add(other.gameObject.GetComponent<Stats>());
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player")
		{
			players.Remove(other.gameObject.GetComponent<Stats>());
		}
	}

	public override void Upgrade() {
		level++;
		if (level == 1)
			return ;
		damage = (int)((float)damage * 1.5f);
		time += 2;
		delay -= 0.2f;
	}

	public override string Info() {
		return "Soigne " + damage.ToString() + " toutes les " + delay.ToString() + " sec pendant " + time.ToString() + " sec sur une zone donnee.\nProchain niveau:\n - soin "
			+ ((int)((float)damage * 1.5f)).ToString() + "\n - delay " + (delay - 0.2f).ToString() + "\n - duree " + (time + 2).ToString();
	}
}
