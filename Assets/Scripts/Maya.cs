using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DigitalRuby.LaserSword;

public class Maya : MonoBehaviour {

	private Animator anim;
	private GameObject map;
	private NavMeshAgent nav;
	private Inventory inventory;
	[HideInInspector]public Stats stats;

	// public Weapon weaponPrefab;
	public Weapon weapon;
	public Weapon weaponUnused;
	public Weapon guitar;
	public Weapon guitarUnused;
	public Weapon weaponUsed;
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
		inventory = gameObject.GetComponent<Inventory>();
		map = GameObject.Find("Terrain");

		// weapon = GameObject.Instantiate(inventory.getCurrentWeapon());
		// weaponUnused = GameObject.Instantiate(inventory.getCurrentWeapon());
		// weapon.gameObject.transform.parent = rightHandPlace.transform;

		// guitar = GameObject.Instantiate(inventory.getCurrentGuitar());
		// guitar.transform.position = rightHandPlace.transform.position;
		// guitar.transform.rotation = rightHandPlace.transform.rotation;
		// guitar.gameObject.transform.parent = rightHandPlace.transform;
		// guitarUnused = GameObject.Instantiate(inventory.getCurrentGuitar());
		// guitarUnused.transform.position = guitarBackPlace.transform.position;
		// guitarUnused.transform.rotation = guitarBackPlace.transform.rotation;
		// guitarUnused.gameObject.transform.parent = guitarBackPlace.transform;

		// switch (weapon.type) {
		// 	case Weapon.Type.ONE_HAND:
		// 		weaponUnused.transform.position = beltPlace.transform.position;
		// 		weaponUnused.transform.rotation = beltPlace.transform.rotation;
		// 		weaponUnused.gameObject.transform.parent = beltPlace.transform; break;
		// 	case Weapon.Type.TWO_HAND:
		// 		weaponUnused.transform.position = weaponBackPlace.transform.position;
		// 		weaponUnused.transform.rotation = weaponBackPlace.transform.rotation;
		// 		weaponUnused.gameObject.transform.parent = weaponBackPlace.transform; break;
		// 	case Weapon.Type.DOUBLE:
		// 		weaponUnused.transform.position = beltPlace.transform.position;
		// 		weaponUnused.transform.rotation = beltPlace.transform.rotation;
		// 		weaponUnused.gameObject.transform.parent = beltPlace.transform; break;
		// }

		// weapon.gameObject.SetActive(false);
		// weaponUnused.gameObject.SetActive(true);
		// guitar.gameObject.SetActive(false);
		// guitarUnused.gameObject.SetActive(true);
		// weaponUsed = weapon;

		refreshInventory();

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
		if (Input.GetKeyDown("w") && !isAttacking)
			switchWeapon();
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
				string tag = hit.collider.gameObject.tag;
				if (tag == "Zombie") {
					// Debug.Log("HIT ZOMBIE");
					destination = hit.collider.gameObject.transform.position;
					targetEnemy = hit.collider.gameObject;
					isAttacking = true;
					return;
				} else if (tag == "SnowBoard" || tag == "LaserSaber" || tag == "Guitar") {
					Debug.Log("CLICK ON WEAPON");
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
		anim.SetInteger("attackType", (int)weaponUsed.type);
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
		yield return new WaitForSeconds(weaponUsed.getFirstHitTiming());
		while(isAttacking) {
			applyDamage();
        	yield return new WaitForSeconds(weaponUsed.getRegularHitTiming());
		}
    }

	void applyDamage() {
		Stats enemy = targetEnemy.GetComponent<Stats>();
		int damage = stats.getDamage(enemy);
		Debug.Log(damage);
		enemy.hp -= damage + weaponUsed.getDamage();
		if (enemy.hp <= 0) {
			StopCoroutine(attack());
			routineAttack = null;
			stats.xp += enemy.xp;
			stats.money += enemy.money;
			isAttacking = false;
			targetEnemy = null;
			if (stats.xp > stats.levelUpXp)
				stats.LevelUp();
		}
	}

	public void levelUp() {
		stats.xp = 0;
		stats.level++;
		int newXPn = Mathf.RoundToInt(stats.levelUpXp * 1.5f);
		stats.levelUpXp = newXPn;
	}

	void manageWeaponPlace(bool attacking) {
		if (attacking) {
			if (weaponUsed == guitar) {
				guitar.gameObject.SetActive(true);
				guitarUnused.gameObject.SetActive(false);
				weapon.gameObject.SetActive(false);
				weaponUnused.gameObject.SetActive(true);
			} else {
				weapon.gameObject.SetActive(true);
				weaponUnused.gameObject.SetActive(false);
				if (weapon.gameObject.tag == "LaserSaber") {
					if (weapon.GetComponentInChildren<LaserSwordScript>().state == 0)
						weapon.GetComponentInChildren<LaserSwordScript>().Activate();
				}
				guitar.gameObject.SetActive(false);
				guitarUnused.gameObject.SetActive(true);
			}
		} else {
			weapon.gameObject.SetActive(false);
			weaponUnused.gameObject.SetActive(true);
			guitar.gameObject.SetActive(false);
			guitarUnused.gameObject.SetActive(true);
		}
	}

	void switchWeapon() {
		if (weaponUsed == weapon)
			weaponUsed = guitar;
		else
			weaponUsed = weapon;
	}

	public void refreshInventory() {

		if (weapon)
			Destroy(weapon);
		if (weaponUnused)
			Destroy(weaponUnused);
		if (guitar)
			Destroy(guitar);
		if(guitarUnused)
			Destroy(guitarUnused);

		weapon = GameObject.Instantiate(inventory.getCurrentWeapon());
		weaponUnused = GameObject.Instantiate(inventory.getCurrentWeapon());
		weapon.gameObject.transform.parent = rightHandPlace.transform;

		guitar = GameObject.Instantiate(inventory.getCurrentGuitar());
		guitar.transform.position = rightHandPlace.transform.position;
		guitar.transform.rotation = rightHandPlace.transform.rotation;
		guitar.gameObject.transform.parent = rightHandPlace.transform;
		guitarUnused = GameObject.Instantiate(inventory.getCurrentGuitar());
		guitarUnused.transform.position = guitarBackPlace.transform.position;
		guitarUnused.transform.rotation = guitarBackPlace.transform.rotation;
		guitarUnused.gameObject.transform.parent = guitarBackPlace.transform;

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
		guitar.gameObject.SetActive(false);
		guitarUnused.gameObject.SetActive(true);
		weaponUsed = weapon;
	}
}
