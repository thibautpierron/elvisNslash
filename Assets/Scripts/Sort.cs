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
	public int				level = 0;
	public float			restorationDelay;
	public int				positionInTab;
	public Texture			image;

	public AudioSource		audio;
	public AudioClip		sound;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	public void Update () {
		if (AZ == null && distance == 0.0f)
		{
			clone.transform.position = player.transform.position;
			transform.position = player.transform.position;
			return ;
		}


		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, Mathf.Infinity) && AZ) {
			AZ.transform.position = hit.point + new Vector3(0.0f, 6.0f, 0.0f);
		}

		if ((distance == 0.0f || rayon == 0.0f) && AZ)
		{
			if (rayon == 0.0f)
			{
				clone = Instantiate(PS, player.transform.position + new Vector3(0,1,0), Quaternion.LookRotation(-hit.point - new Vector3(0,1,0) + player.transform.position));
				transform.position = player.transform.position + new Vector3(0,1,0);
			}
			else
			{
				clone = Instantiate(PS, player.transform.position, new Quaternion(0,0,0,0));
			}
			player.GetComponent<MayaSorts>().LastSending[positionInTab] = Time.realtimeSinceStartup;
			StartCoroutine(Execute());
			Destroy(AZ.gameObject);
			return ;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			Destroy(gameObject);
		if (Input.GetMouseButton(0) && AZ)
		{
			clone = Instantiate(PS, hit.point, new Quaternion(0,0,0,0));
			player.GetComponent<MayaSorts>().LastSending[positionInTab] = Time.realtimeSinceStartup;
			transform.position = hit.point;
			StartCoroutine(Execute());
			Destroy(AZ.gameObject);
		}
	}

	public virtual IEnumerator Execute() {
		yield return new WaitForSeconds(0);
	}

	public virtual void Upgrade() {
		return ;
	}

	public virtual string Info() {
		return "";
	}
}
