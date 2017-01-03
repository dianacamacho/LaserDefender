using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 15.0f;
	public float projectileSpeed = 20f;
	public float firingRate = 0.2f;
	public float padding = 0.5f;
	public float health = 150f;
	public GameObject projectile;

	float xMin;
	float xMax;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3 (1,0,distance));
		xMin = leftmost.x + padding;
		xMax = rightmost.x - padding;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.LeftArrow)) {
//			transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
//			transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);
		} 

		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}

		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		// restrict player to the gamespace
	}

	void Fire () 
	{
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		EnemyProjectile missile = collider.gameObject.GetComponent<EnemyProjectile> ();

		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit();

			if (health <= 0) {
				Destroy(gameObject);
			} 

			Debug.Log("Player hit by projectile");

		}
	}



}
