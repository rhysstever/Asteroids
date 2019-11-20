using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	public Vector3 asteroidPosition;		// Local vector for movement calculation
	public Vector3 direction;			// Way the vehicle should move
	public Vector3 velocity;                // Change in X and Y

	// Start is called before the first frame update
	void Start()
	{
		asteroidPosition = new Vector3(transform.position.x, transform.position.y);
		direction = new Vector3(1, 0, 0);
		velocity = new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f));
	}
	
	// Update is called once per frame
	void Update()
	{
		Move();
	}

	public void Move()
	{
		asteroidPosition += velocity;
		transform.position = asteroidPosition;
	}
}
