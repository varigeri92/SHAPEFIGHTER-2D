using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUpType
{
	Gun,
	Shield
}

public class Player : MonoBehaviour
{
	public delegate void PlayerDead();
	public static event PlayerDead OnPlayerDeath;

	public List<SelectorBehaviour> selectorBahaviours = new List<SelectorBehaviour>();

	public TMPro.TMP_Text enemyCounterText;
	int enemyCounter = 0;

	public float turbo;
	public Image turboBar;
	public TMPro.TMP_Text shieldText;
	public bool allowTurbo;
	public GameObject dieText;
	public float speed = 3f;
	float _moveSpeed;
	public List<Transform> attachPoints = new List<Transform>();
	public List<Gun> activeGuns = new List<Gun>();

	public int health = 10;
	float horizontal = 0f;
	float vertical = 0f;

	void Start()
	{
		SetBasicGun();
		EnemyFollow.onEnemyDead += countEnemyes;
	}
	void countEnemyes(EnemyFollow enemy){
		enemyCounter++;
		enemyCounterText.text = enemyCounter.ToString();

	}

	void Die()
	{
		dieText.SetActive(true);
		if( OnPlayerDeath != null ){
			OnPlayerDeath();
		}
		Destroy(gameObject);
	}
	void Update()
	{
		LookAtMouse();

		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		if (Input.GetKey(KeyCode.Space) && allowTurbo) {
			_moveSpeed = speed * 2;
			CountTurbo(-0.5f);
		} else if (Input.GetKeyUp(KeyCode.Space)) {
			_moveSpeed = speed;
		}else{
			_moveSpeed = speed;
			CountTurbo(0.2f);
		}

		Vector3 direction = new Vector3(horizontal, vertical, 0);
		Moove(direction);

		Shoot();
	}
	void CountTurbo(float mult){
		if (turbo < 1 | turbo > 0f) {
			turbo += mult * Time.deltaTime;
			turboBar.fillAmount = turbo;
			if (turbo > 0.5f)
				allowTurbo = true;
		}

		if (turbo > 1)
		{
			turbo = 1;
			turboBar.fillAmount = turbo;
		}
		else if(turbo < 0){
			turbo = 0;
			turboBar.fillAmount = turbo;
			allowTurbo = false;
		}

	}

	public void TakeDmg(int dmg)
	{
		health -= dmg;
		if (health < 0) {
			Die();
		}
		if(health >= 0){
			shieldText.text = health.ToString();
		}
		else
		{
			shieldText.text = "0";
		}
	}
	void Shoot()
	{
		if (Input.GetAxis("Fire1") > 0.1) {
			foreach (Gun gun in activeGuns) {
				gun.Shooting(true);
			}
		}
	}

	public void OnGunChanged()
	{
		activeGuns = new List<Gun>();
		foreach (Transform t in attachPoints) {
			if (t.GetComponentInChildren<Gun>() != null) {
				activeGuns.Add(t.GetComponentInChildren<Gun>());
			}

		}
	}

	private void Moove(Vector3 direction)
	{
		float dist = Vector3.Distance(Input.mousePosition, Camera.main.WorldToScreenPoint(transform.position));
		float _speed;

		if (dist < 20) {
			_speed = 0;
		} else {
			_speed = _moveSpeed;
		}
		transform.position = Vector3.Lerp(transform.position, transform.position + direction * _speed, Time.deltaTime);
		// transform.Translate(direction * _speed * Time.deltaTime);
	}

	void LookAtMouse()
	{
		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
	}

	public void PickUpShield(int shield){
		health += shield;
		shieldText.text = health.ToString();
	}
	public void PickupGun(GameObject gun, GameObject icon){
		bool alreadyGot = false;
		Debug.Log("PickupCalled");

		if (!alreadyGot) {
			foreach (SelectorBehaviour selector in selectorBahaviours) {
				if (selector.haschild == false) {
					Debug.Log("SHOW IMAGE!");
					selector.gun = gun;
					selector.haschild = true;
					Instantiate(icon, selector.gameObject.transform);
					break;
				} else if (selector.gun.name == gun.name) {
					// add ammo
				}
			}
		}
	}
	void SetBasicGun(){
		activeGuns = new List<Gun>();
		foreach (Transform t in attachPoints) {
			if (t.GetComponentInChildren<Gun>() != null) {
				Gun gun = t.GetComponentInChildren<Gun>();
				activeGuns.Add(gun);
				PickupGun(t.gameObject, gun.icon);
			}
		}
	}
}
