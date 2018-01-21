using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadeScript : Sort {

	private float				delay = 0.5f;
	public int					damage = 5;
	private int					time = 7;
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
		for (int i = 0 ; i < time / delay ; i++)
		{
			foreach(Stats stat in enemy)
			{
				stat.hp -= damage;
				if (stat.hp < 0)
					stat.hp = 0;
			}
			yield return new WaitForSeconds(delay);
		}
		yield return new WaitForSeconds(5);
		Destroy(clone.gameObject);
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Zombie")
		{
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
		damage = (int)((float)damage * 1.5f);
		time += 2;
	}

	public override string Info() {
		return "Inflige " + damage.ToString() + " de degat autour de toi toutes les " + delay.ToString() + " sec pendant " + time.ToString() + " sec\nProchain niveau:\n - degat "
			+ ((int)((float)damage * 1.5f)).ToString() + "\n - duree " + (time + 2).ToString();
	}
}
