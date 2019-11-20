using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
	public GameObject asteroid_S1;
	public GameObject asteroid_S2;
	public List<GameObject> asteroids;
	GameObject newAsteroid;
	float spawnTime;
	float counter;

	float leftBound;
	float rightBound;
	float botBound;
	float topBound;

	// Start is called before the first frame update
	void Start()
	{
		asteroids = new List<GameObject>();
		spawnTime = 3f;
		counter = spawnTime;

		// Screen Bounds
		leftBound = gameObject.GetComponent<CollisionManager>().spaceship.GetComponent<Vehicle>().leftBound;
		rightBound = gameObject.GetComponent<CollisionManager>().spaceship.GetComponent<Vehicle>().rightBound;
		botBound = gameObject.GetComponent<CollisionManager>().spaceship.GetComponent<Vehicle>().botBound;
		topBound = gameObject.GetComponent<CollisionManager>().spaceship.GetComponent<Vehicle>().topBound;

		// Starts the game with 3 Stage 1 asteroids spawned
		for(int i = 0; i < 3; i++)
		{
			newAsteroid = CreateAsteroid(
				1,
				Random.Range(leftBound, rightBound),
				Random.Range(botBound, topBound));
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		counter -= Time.deltaTime;
		if(counter <= 0)
		{
			newAsteroid = CreateAsteroid(1, 
				Random.Range(leftBound, rightBound), 
				Random.Range(botBound, topBound));
			counter = spawnTime;
		}
		
		// Loops through each planet, if the planet 
		// exits the screen, it is removed from the list
		// and destroyed from the scene
		foreach(GameObject asteroid in asteroids)
		{
			if(gameObject.GetComponent<CollisionManager>().IsOutOfBounds(asteroid))
			{
				asteroids.Remove(asteroid);
				Destroy(asteroid);
			}
		}
	}

	public GameObject CreateAsteroid(int stage, float x, float y)
	{
		if(stage == 1)
		{
			newAsteroid = Instantiate(
					asteroid_S1,
					new Vector3(x, y),
					Quaternion.identity);
		}
		else if(stage == 2)
		{
			newAsteroid = Instantiate(
					asteroid_S2,
					new Vector3(x, y),
					Quaternion.identity);
		}

		asteroids.Add(newAsteroid);
		return newAsteroid;
	}
}
