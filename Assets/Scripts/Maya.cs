using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Maya : MonoBehaviour {

	private Animator anim;
	private GameObject map;
	private NavMeshAgent nav;
	public Weapon weaponPrefab;
	public Weapon weapon;
	[HideInInspector]public Stats stats;
	private Vector3 destination;
	public GameObject targetEnemy;
	private float distToEnemy;
	private int health;
	private bool isAttacking;
	private bool move;
	private bool dead = false;
	private Coroutine routineAttack = null;
	private Transform rightHandPlace;
	// Use this for initialization
	void Start () {
		rightHandPlace = GameObject.Find("WeaponRightHandPlace").GetComponent<Transform>();
		anim = gameObject.GetComponent<Animator>();
		nav = gameObject.GetComponent<NavMeshAgent>();
		stats = gameObject.GetComponent<Stats>();
		map = GameObject.Find("Terrain");
		weapon = GameObject.Instantiate(weaponPrefab);
		weapon.gameObject.transform.parent = rightHandPlace.transform;
		destination = transform.position;
		move = false;
		isAttacking = false;
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
					isAttacking = true;
					return;
				}
			}
			if (map.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
				destination = hit.point;
				targetEnemy = null;
				isAttacking = false;
				move = true;
				if (routineAttack != null) {
					StopCoroutine(attack());
					routineAttack = null;
				}
				return;
			}
		}
	}

	void manageAttack() {
		if (!isAttacking)
			return;
		distToEnemy = Vector3.Distance(targetEnemy.transform.position, transform.position);

		if (distToEnemy < 2) {
			transform.LookAt(targetEnemy.transform.position);
			move = false;
			if (routineAttack == null)
				routineAttack = StartCoroutine(attack());
		} else {
			StopCoroutine(attack());
			routineAttack = null;
			move = true;
		}
	}

	void updateAnimator() {
		anim.SetInteger("health", health);
		anim.SetBool("isMoving", move);
		anim.SetBool("attack", isAttacking);
		if (isAttacking)
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
    }
	IEnumerator	attack() {
		yield return new WaitForSeconds(weapon.getFirstHitTiming());
		while(isAttacking) {
			applyDamage();
        	yield return new WaitForSeconds(weapon.getRegularHitTiming());
		}
    }

	void applyDamage() {
		Stats enemy = targetEnemy.GetComponent<Stats>();
		int damage = stats.getDamage(enemy);
		Debug.Log(damage);
		enemy.hp -= damage;
		if (enemy.hp <= 0) {
			StopCoroutine(attack());
			routineAttack = null;
			stats.xp += enemy.xp;
			stats.money += enemy.money;
			isAttacking = false;
			targetEnemy = null;
			if (stats.xp > stats.levelUpXp)
				levelUp();
		}
	}

	void levelUp() {
		stats.xp = 0;
		stats.level++;
		int newXPn = Mathf.RoundToInt(stats.levelUpXp * 1.5f);
		stats.levelUpXp = newXPn;
	}
}
