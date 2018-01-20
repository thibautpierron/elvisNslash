using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort : MonoBehaviour {

	public float			distance;
	public float			rayon;
	public ZoneEffect		AZ;
	public Maya				player;
	public ParticleSystem	PS;
	public ParticleSystem	clone;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (AZ == null && distance == 0.0f)
		{
			clone.transform.position = player.transform.position;
			return ;
		}

		if (distance == 0.0f && AZ)
		{
			clone = Instantiate(PS, player.transform.position, new Quaternion(0,0,0,0));
			Destroy(AZ.gameObject);
			return ;
		}


		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, Mathf.Infinity) && AZ) {
			AZ.transform.position = hit.point + new Vector3(0.0f, 6.0f, 0.0f);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			Destroy(gameObject);
		if (Input.GetMouseButton(0) && AZ)
		{
			clone = Instantiate(PS, hit.point, new Quaternion(0,0,0,0));
			Destroy(AZ.gameObject);
		}
	}
}
