using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DigitalRuby.LaserSword;

public class Maya : MonoBehaviour {

	private Animator anim;
	private GameObject map;
	private NavMeshAgent nav;
	public Weapon weaponPrefab;
	public Weapon weapon;
	public Weapon weaponUnused;
	public Weapon guitar;
	[HideInInspector]public Stats stats;
	private Vector3 destination;
	public GameObject targetEnemy;
	private float distToEnemy;
	private int health;
	private bool isAttacking;
	private bool move;
	private bool dead = false;
	public bool inMenu = false;
	private Coroutine routineAttack = null;
	private Transform rightHandPlace;
	private Transform leftHandPlace;
	private Transform weaponBackPlace;
	private Transform guitarBackPlace;
	private Transform beltPlace;

	// private Transform originPos;
	// Use this for initialization
	void Start () {
		rightHandPlace = GameObject.Find("WeaponRightHandPlace").GetComponent<Transform>();
		leftHandPlace = GameObject.Find("WeaponLeftHandPlace").GetComponent<Transform>();
		weaponBackPlace = GameObject.Find("WeaponBackPlace").GetComponent<Transform>();
		guitarBackPlace = GameObject.Find("GuitarBackPlace").GetComponent<Transform>();
		beltPlace = GameObject.Find("BeltPlace").GetComponent<Transform>();

		anim = gameObject.GetComponent<Animator>();
		nav = gameObject.GetComponent<NavMeshAgent>();
		stats = gameObject.GetComponent<Stats>();
		map = GameObject.Find("Terrain");

		weapon = GameObject.Instantiate(weaponPrefab);
		weaponUnused = GameObject.Instantiate(weaponPrefab);
		weapon.gameObject.transform.parent = rightHandPlace.transform;

		guitar = GameObject.Instantiate(guitar);
		guitar.transform.position = guitarBackPlace.transform.position;
		guitar.transform.rotation = guitarBackPlace.transform.rotation;
		guitar.gameObject.transform.parent = guitarBackPlace.transform;

		switch (weapon.type) {
			case Weapon.Type.ONE_HAND:
				weaponUnused.transform.position = beltPlace.transform.position;
				weaponUnused.transform.rotation = beltPlace.transform.rotation;
				weaponUnused.gameObject.transform.parent = beltPlace.transform; break;
			case Weapon.Type.TWO_HAND:
				weaponUnused.transform.position = weaponBackPlace.transform.position;
				weaponUnused.transform.rotation = weaponBackPlace.transform.rotation;
				weaponUnused.gameObject.transform.parent = weaponBackPlace.transform; break;
			case Weapon.Type.DOUBLE:
				weaponUnused.transform.position = beltPlace.transform.position;
				weaponUnused.transform.rotation = beltPlace.transform.rotation;
				weaponUnused.gameObject.transform.parent = beltPlace.transform; break;
		}

		weapon.gameObject.SetActive(false);
		weaponUnused.gameObject.SetActive(true);

		destination = transform.position;
		move = false;
		isAttacking = false;
		distToEnemy = 100;
	}
	
	// Update is called once per frame
	void Update () {
		health = stats.hp;
		if (dead || inMenu)
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
		if (!isAttacking) {
			manageWeaponPlace(false);
			return;
		}
		distToEnemy = Vector3.Distance(targetEnemy.transform.position, transform.position);

		if (distToEnemy < 2) {
			manageWeaponPlace(true);
			transform.LookAt(targetEnemy.transform.position);
			move = false;
			if (routineAttack == null)
				routineAttack = StartCoroutine(attack());
		} else {
			manageWeaponPlace(false);
			StopCoroutine(attack());
			routineAttack = null;
			move = true;
		}
	}

	void updateAnimator() {
		anim.SetInteger("health", health);
		anim.SetInteger("attackType", (int)weapon.type);
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

	void manageWeaponPlace(bool attacking) {
		if (attacking) {
			weapon.gameObject.SetActive(true);
			weaponUnused.gameObject.SetActive(false);
			if (weapon.gameObject.tag == "LaserSaber") {
				if (weapon.GetComponentInChildren<LaserSwordScript>().state == 0)
					weapon.GetComponentInChildren<LaserSwordScript>().Activate();
				// weaponUnused.GetComponentInChildren<LaserSwordScript>().Deactivate();
			}
		} else {
			weapon.gameObject.SetActive(false);
			weaponUnused.gameObject.SetActive(true);
			// if (weapon.GetComponentInChildren<LaserSwordScript>().state == 1)
			// 	weapon.GetComponentInChildren<LaserSwordScript>().Deactivate();
		}
	}
}
