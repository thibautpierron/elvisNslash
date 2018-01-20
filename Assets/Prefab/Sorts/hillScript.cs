using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hillScript : Sort {

	private float				delay = 2.0f;
	private int					damage = 5;
	private int					time = 15;
	private List<Stats>			player;

	// Use this for initialization
	void Start () {
		player = new List<Stats>();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	public override IEnumerator Execute() {
		for (int i = 0 ; i < time / delay ; i++)
		{
			foreach(Stats stat in player)
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
			player.Add(other.gameObject.GetComponent<Stats>());
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player")
		{
			player.Remove(other.gameObject.GetComponent<Stats>());
		}
	}
}
