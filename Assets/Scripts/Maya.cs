using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Maya : MonoBehaviour {

	private Animator anim;
	private GameObject map;
	private NavMeshAgent nav;
	private GameObject runSword;
	private GameObject attackSword;

	[HideInInspector]public Stats stats;

	private Vector3 destination;
	public GameObject targetEnemy;
	private float distToEnemy;
	public int health;
	private bool attack;
	private bool move;
	public float hitRate = 1;
	private float counter;
	// Use this for initialization
	private bool dead = false;
	void Start () {
		anim = gameObject.GetComponent<Animator>();
		nav = gameObject.GetComponent<NavMeshAgent>();
		stats = gameObject.GetComponent<Stats>();
		map = GameObject.Find("Terrain");
		runSword = GameObject.Find("RunSword");
		attackSword = GameObject.Find("AttackSword");
		destination = transform.position;
		attackSword.SetActive(false);
		move = false;
		attack = false;
		distToEnemy = 100;
	}
	
	// Update is called once per frame
	void Update () {
		health = stats.hp;
		if (dead)
			return;
		clickHandler();
		manageAttack();
		updateAnimator();
		navigation();
		if (health <= 0) {
			nav.enabled = false;
			dead = true;
			StartCoroutine(deathAnimation());
		}
	}

	void clickHandler() {
		if (Input.GetMouseButton(0)) {
			// Debug.Log("CLICK");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				if (hit.collider.gameObject.tag == "Zombie") {
					// Debug.Log("HIT ZOMBIE");
					destination = hit.collider.gameObject.transform.position;
					targetEnemy = hit.collider.gameObject;
					attack = true;
					return;
				}
			}
			if (map.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
				destination = hit.point;
				targetEnemy = null;
				attack = false;
				move = true;
				return;
			}

		}
	}

	void manageAttack() {
		if (!attack)
			return;
		distToEnemy = Vector3.Distance(targetEnemy.transform.position, transform.position);

		if (distToEnemy < 2) {
			transform.LookAt(targetEnemy.transform.position);
			move = false;
		} else
			move = true;
	}

	void updateAnimator() {
		if(attack && !move) {
			attackSword.SetActive(true);
			runSword.SetActive(false);
			if (counter != 0) {
				counter = System.Math.Max(counter - Time.deltaTime, 0);
			} else if (counter == 0) {
				Stats enemy = targetEnemy.GetComponent<Stats>();
				int damage = stats.getDamage(enemy);
				// Debug.Log(damage);
				enemy.hp -= damage;
				if (enemy.hp <= 0) {
					stats.xp += enemy.xp;
					stats.money += enemy.money;
					if (stats.xp > stats.levelUpXp) {
						stats.xp = 0;
						stats.level++;
						int newXPn = Mathf.RoundToInt(stats.levelUpXp * 1.5f);
						stats.levelUpXp = newXPn;
					}
					attack = false;
					targetEnemy = null;
				}
				counter = hitRate;
			}
		} else {
			counter = 0.6f;
			attackSword.SetActive(false);
			runSword.SetActive(true);
		}
		anim.SetInteger("health", health);
		anim.SetBool("isMoving", move);
		anim.SetBool("attack", attack);
		if (attack)
			anim.SetFloat("attackRange", distToEnemy);
		else
			anim.SetFloat("attackRange", 1000);
	}

	void navigation() {
		if (destination == transform.position || Vector3.Distance(transform.position, destination) < 2) {
			move = false;
			return;
		}
		nav.destination = destination;
	}

	IEnumerator	deathAnimation() {
		yield return new WaitForSeconds(5);
		for (float f = 0; f < 20; f++) {
			gameObject.transform.Translate(Vector3.down * 0.05f);
        	yield return new WaitForSeconds(.05f);
		}
		// Destroy(gameObject);
    }
}
