using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkScript : Sort {

	private float				delay = 0.5f;
	private int					damage = 150;
	private List<Stats>			enemy;

	// Use this for initialization
	void Start () {
		enemy = new List<Stats>();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	public override IEnumerator Execute() {
		yield return new WaitForSeconds(delay);
		foreach(Stats stat in enemy)
		{
			stat.hp -= damage;
			if (stat.hp < 0)
				stat.hp = 0;
		}
		yield return new WaitForSeconds(5);
		Destroy(clone.gameObject);
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Zombie")
		{
			print("enter");
			enemy.Add(other.gameObject.GetComponent<Stats>());
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Zombie")
		{
			enemy.Remove(other.gameObject.GetComponent<Stats>());
		}
	}

	public override void Upgrade() {
		level++;
		if (level == 1)
			return ;
		damage = (int)((float)damage * 1.6f);
	}

	public override string Info() {
		return "Inflige de tres gros degats dans une petite zone donnee. Puissance de " + damage.ToString() + " actuellement.\nProchain niveau:\n - degats " + ((int)((float)damage * 1.6f)).ToString();
	}
}
