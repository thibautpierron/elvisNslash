using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyBallScript : Sort {

	private float				lifeTime = 0.5f;
	private int					damage = 20;
	private RaycastHit			hit;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		clone.gameObject.transform.position = Vector3.MoveTowards(transform.position, hit.point + new Vector3(0,1,0), Time.deltaTime * 15);
		gameObject.transform.position = clone.gameObject.transform.position;
	}

	public override IEnumerator Execute() {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, Mathf.Infinity) && AZ) {
			AZ.transform.position = hit.point + new Vector3(0.0f, 6.0f, 0.0f);
			hit.point -= player.transform.position;
			hit.point *= 1000;
		}

		yield return new WaitForSeconds(lifeTime);
		Destroy(clone.gameObject);
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Zombie")
		{
			other.gameObject.GetComponent<Stats>().hp -= damage;
			if (other.gameObject.GetComponent<Stats>().hp < 0)
				other.gameObject.GetComponent<Stats>().hp = 0;
			Destroy(clone.gameObject);
			Destroy(gameObject);
		}
	}

	public override void Upgrade() {
		level++;
		if (level == 1)
			return ;
		damage = (int)((float)damage * 1.5f);
		lifeTime += 0.1f;
	}

	public override string Info() {
		return "Petite balle d'energie. Fait " + damage.ToString() + " de degats au contact.\nPorchain niveau:\n - degat " + ((int)((float)damage * 1.5f)).ToString();
	}
}
