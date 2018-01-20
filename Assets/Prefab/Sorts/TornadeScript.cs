using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadeScript : Sort {

	private float				delay = 0.5f;
	private int					damage = 10;
	private int					time = 7;
	private List<GameObject>	enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	public override IEnumerator Execute() {
		for (int i = 0 ; i < time / delay ; i++)
		{
			//foreach()
			yield return new WaitForSeconds(delay);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Zombie")
			enemy.Add(other.gameObject);
	}
}
