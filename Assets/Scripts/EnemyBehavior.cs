using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
	public float health = 150f;
	public float projectileSpeed = 20f;
	public float firingRate = 0.2f;
	public float shotsPerSecond = 0.5f;
	public GameObject enemyProjectile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float probability = Time.deltaTime * shotsPerSecond;

		if (Random.value < probability) {
			Fire ();	
		}
	}

	void Fire () 
	{
		GameObject beam = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
	}


	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();

		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit();

			if (health <= 0) {
				Destroy(gameObject);
			} 

			Debug.Log("Enemy hit by projectile");

		}
	}
}

