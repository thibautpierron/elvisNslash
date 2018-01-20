using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayaSorts : MonoBehaviour {

	public Sort[]		sorts;

	public float[]		LastSending;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("1") && (LastSending[0] + sorts[0].restorationDelay < Time.realtimeSinceStartup || LastSending[0] == 0.0f))
		{
			Sort clone = Instantiate(sorts[0]);
			clone.player = gameObject.GetComponent<Maya>();
			clone.positionInTab = 0;
		}
		if (Input.GetKeyDown("2") && (LastSending[1] + sorts[1].restorationDelay < Time.realtimeSinceStartup || LastSending[1] == 0.0f))
		{
			Sort clone = Instantiate(sorts[1]);
			clone.player = gameObject.GetComponent<Maya>();
			clone.positionInTab = 1;
		}
		if (Input.GetKeyDown("3") && (LastSending[2] + sorts[2].restorationDelay < Time.realtimeSinceStartup || LastSending[2] == 0.0f))
		{
			Sort clone = Instantiate(sorts[2]);
			clone.player = gameObject.GetComponent<Maya>();
			clone.positionInTab = 2;
		}
		if (Input.GetKeyDown("4") && (LastSending[3] + sorts[3].restorationDelay < Time.realtimeSinceStartup || LastSending[3] == 0.0f))
		{
			Sort clone = Instantiate(sorts[3]);
			clone.player = gameObject.GetComponent<Maya>();
			clone.positionInTab = 3;
		}
	}
}
