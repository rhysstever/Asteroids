using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
	    
	}

	public static bool AABBCollision(GameObject gameObj1, GameObject gameObj2)
	{
		// If all 4 conditions are met, then the objects are colliding
		if((gameObj2.GetComponent<SpriteInfo>().MinX < gameObj1.GetComponent<SpriteInfo>().MaxX) &&
			(gameObj2.GetComponent<SpriteInfo>().MaxX > gameObj1.GetComponent<SpriteInfo>().MinX) &&
				(gameObj2.GetComponent<SpriteInfo>().MaxY > gameObj1.GetComponent<SpriteInfo>().MinY) &&
					(gameObj2.GetComponent<SpriteInfo>().MinY < gameObj1.GetComponent<SpriteInfo>().MaxY))
		{
			return true;
		}

		return false;
	}

	public static bool CircleCollision(GameObject gameObj1, GameObject gameObj2)
	{
		// Find center and radius of each game object
		Vector3 center1 = gameObj1.GetComponent<SpriteInfo>().Center;
		Vector3 center2 = gameObj2.GetComponent<SpriteInfo>().Center;
		float radius1 = gameObj1.GetComponent<SpriteInfo>().Size.x / 2f;
		float radius2 = gameObj2.GetComponent<SpriteInfo>().Size.x / 2f;

		// Calculate differences in both the x and y of the centers
		float xDiff = center1.x - center2.x;
		float yDiff = center1.y - center2.y;

		// Using the Pythagorean Theorem, if the squared distance is less than
		// or equal to the sum of the radii, then the circles are colliding
		if(Mathf.Pow(xDiff, 2) + Mathf.Pow(yDiff, 2) <= radius1 + radius2)
			return true;
		
		return false;
	}

	public static bool PointCollision(GameObject gameObj1, GameObject bullet)
	{
		// Finds the center and radius of the planet
		Vector3 center1 = gameObj1.GetComponent<SpriteInfo>().Center;
		float radius = gameObj1.GetComponent<SpriteInfo>().Size.x / 2f;

		// Gets the center of the bullet
		Vector3 centerBullet = bullet.GetComponent<SpriteInfo>().Center;

		// Finds the distance between the bullet and the center of the planet
		// If the center of the bullet is less or equal to the planet's
		// radius, there is a collision
		float xDist = center1.x - centerBullet.x;
		float yDist = center1.y - centerBullet.y;

		if(Mathf.Pow(xDist, 2) + Mathf.Pow(yDist, 2) <= Mathf.Pow(radius, 2))
			return true;

		return false;
	}
}
