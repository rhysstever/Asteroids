using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
	public GameObject vehicle;
	public List<GameObject> bullets;

	float leftBound;
	float rightBound;
	float botBound;
	float topBound;

	// Start is called before the first frame update
	void Start()
	{
		leftBound = gameObject.GetComponent<CollisionManager>().spaceship.GetComponent<Vehicle>().leftBound;
		rightBound = gameObject.GetComponent<CollisionManager>().spaceship.GetComponent<Vehicle>().rightBound;
		botBound = gameObject.GetComponent<CollisionManager>().spaceship.GetComponent<Vehicle>().botBound;
		topBound = gameObject.GetComponent<CollisionManager>().spaceship.GetComponent<Vehicle>().topBound;
	}
	
	// Update is called once per frame
	void Update()
	{
		bullets = vehicle.GetComponent<Vehicle>().bullets;

		// Checks each bullet to see if it is off-
		// screen, if so, it is removed from the list
		if(bullets.Count != 0)
		{
			foreach(GameObject bullet in bullets)
			{
				if(gameObject.GetComponent<CollisionManager>().IsOutOfBounds(bullet))
				{
					bullets.Remove(bullet);
					Destroy(bullet);
				}
			}
		}
	}
}
