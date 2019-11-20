using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
	public List<GameObject> asteroids;
	public List<GameObject> bullets;
	public GameObject spaceship;

	public int points;
	public int lives;
	bool hurt;
	float hurtTime;
	float currentHurtTime;

	// Start is called before the first frame update
	void Start()
	{
		lives = 3;
		hurt = false;
		hurtTime = 2f;
		currentHurtTime = hurtTime;
	}
	
	// Update is called once per frame
	void Update()
	{
		// Gets an updated list of the asteroids and bullets in the scene
		asteroids = gameObject.GetComponent<AsteroidManager>().asteroids;
		bullets = gameObject.GetComponent<BulletManager>().bullets;

		// Checks each planet against the spaceship
		foreach(GameObject asteroid in asteroids)
		{
			// If the spaceship collides a planet, the player loses a life
			// and is "hurt" or invulernable
			if(CollisionDetection.CircleCollision(spaceship, asteroid))
			{
				if(!hurt)
				{
					lives--;
					hurt = true;
				}
			}
			
			// Checks each bullet against each planet
			foreach(GameObject bullet in bullets)
			{
				// If a bullet collides with a planet
				if(CollisionDetection.PointCollision(asteroid, bullet))
				{
					// Plays the asteroid explosion sound when an asteroid is shot
					gameObject.GetComponent<AudioPlayer>().audioSource.PlayOneShot(
						gameObject.GetComponent<AudioPlayer>().asteroidExplosion);

					// Removes the planet from the list and deletes it from the scene
					asteroids.Remove(asteroid);
					Destroy(asteroid);

					// Awards the player with points for destroying the asteroid
					// Stage 1 - 20 points
					// Stage 2 - 50 points
					if(asteroid.CompareTag("Asteroid_S1"))
					{
						points += 20;

						// If the asteroid is a Stage 1 asteroid, 
						// it breaks into 2 Stage 2 asteroids
						gameObject.GetComponent<AsteroidManager>().CreateAsteroid(
							2, 
							asteroid.GetComponent<Asteroid>().asteroidPosition.x,
							asteroid.GetComponent<Asteroid>().asteroidPosition.y);
						gameObject.GetComponent<AsteroidManager>().CreateAsteroid(
							2,
							asteroid.GetComponent<Asteroid>().asteroidPosition.x,
							asteroid.GetComponent<Asteroid>().asteroidPosition.y);
					}
					else if(asteroid.CompareTag("Asteroid_S2"))
					{
						points += 50;
					}
						
					// Removes the bullet from the list and deletes it from the scene
					bullets.Remove(bullet);
					Destroy(bullet);
				}
			}
		}

		IsHurt();
	}

	void IsHurt()
	{
		if(hurt)
		{
			currentHurtTime -= Time.deltaTime;
			spaceship.GetComponent<Renderer>().material.color = Color.red;

			// When the hurt time hits 0, the player
			// is no longer invulernable
			if(currentHurtTime <= 0)
			{
				hurt = false;
				currentHurtTime = hurtTime;
			}
		}
		else
		{
			spaceship.GetComponent<Renderer>().material.color = Color.white;
		}
	}

	// A helper method that checks all four bounds 
	// to see if the given game object is off the screen
	public bool IsOutOfBounds(GameObject gameObj)
	{
		if((gameObj.transform.position.x < spaceship.GetComponent<Vehicle>().leftBound) ||
			(gameObj.transform.position.x > spaceship.GetComponent<Vehicle>().rightBound) ||
			(gameObj.transform.position.y < spaceship.GetComponent<Vehicle>().botBound) ||
			(gameObj.transform.position.y > spaceship.GetComponent<Vehicle>().topBound))
		{
			return true;
		}

		return false;
	}
}
