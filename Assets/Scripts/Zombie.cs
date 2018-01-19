using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private NavMeshAgent nav;
	private Maya maya;
	private Vector3 destination;
	[HideInInspector]public Stats stats;
	public int health;

	public float aggroRange = 2;
	private bool attack;
	private bool move;
	private float distToMaya;
	private bool dead = false;
	public Spawner spawner;

	public float hitRate = 1;
	private float counter;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
		nav = gameObject.GetComponent<NavMeshAgent>();
		stats = gameObject.GetComponent<Stats>();
		destination = transform.position;
		maya = GameObject.Find("Maya").GetComponent<Maya>();
		move = false;
		attack = false;
		distToMaya = 100;
	}
	
	// Update is called once per frame
	void Update () {
		health = stats.hp;
		if (dead)
			return;
		distToMaya = Vector3.Distance(maya.transform.position, transform.position);
		aggro();
		manageAttack();
		updateAnimator();
		navigation();
		if (health <= 0) {
			nav.enabled = false;
			dead = true;
			StartCoroutine(deathAnimation());
		}
	}

	void aggro() {
		if (distToMaya < aggroRange) {
			destination = maya.transform.position;
			attack = true;
		}
	}

	void manageAttack() {
		if (!attack)
			return;
		if (distToMaya < 2) {
			transform.LookAt(maya.transform.position);
			move = false;
		} else
			move = true;
	}

	void updateAnimator() {

		if(attack && !move) {
			if (counter != 0) {
				counter = System.Math.Max(counter - Time.deltaTime, 0);
			} else if (counter == 0) {
				Stats mayaStat = maya.GetComponent<Stats>();
				int damage = stats.getDamage(mayaStat);
				// Debug.Log(damage);
				mayaStat.hp -= damage;
				if (mayaStat.hp <= 0) {
					attack = false;
					dead = true;
				}
				counter = hitRate;
			}
		} else {
			counter = 0.6f;
		}


		anim.SetInteger("health", health);
		anim.SetBool("isMoving", move);
		anim.SetBool("attack", attack);
		if (attack)
			anim.SetFloat("attackRange", distToMaya);
		else
			anim.SetFloat("attackRange", 1000);
	}

	void navigation() {
		if (destination == transform.position)
			return;

		nav.destination = destination;
	}

	IEnumerator	deathAnimation() {
		yield return new WaitForSeconds(5);
		for (float f = 0; f < 20; f++) {
			gameObject.transform.Translate(Vector3.down * 0.05f);
        	yield return new WaitForSeconds(.05f);
		}
		yield return new WaitForSeconds(spawner.frequency);
		spawner.spawnZombie();
		Destroy(gameObject);
    }
}
