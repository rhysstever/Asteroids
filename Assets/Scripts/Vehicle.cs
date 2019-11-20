using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
	public Camera cam;
	public float leftBound;
	public float rightBound;
	public float botBound;
	public float topBound;

	public float accelRate;                     // Small, constant rate of acceleration
	public Vector3 vehiclePosition;             // Local vector for movement calculation
	public Vector3 direction;                   // Way the vehicle should move
	public Vector3 velocity;                    // Change in X and Y
	public Vector3 acceleration;                // Small accel vector that's added to velocity
	public float angleOfRotation;               // 0 
	public float maxSpeed;                      // 0.5 per frame, limits mag of velocity
	
	public GameObject bullet;
	public List<GameObject> bullets;
	GameObject newBullet;

	// Use this for initialization
	void Start ()
     {
		// Setups up screen bounds for screen wrapping
		leftBound = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0)).x;
		rightBound = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0)).x;
		botBound = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0)).y;
		topBound = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0)).y;

		// Starts the vehicle in the center of the scene
		vehiclePosition = Vector3.zero;  
		// Facing right
		direction = new Vector3(1, 0, 0);
		velocity = new Vector3(0, 0, 0);

		bullets = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Shoot();
		RotateVehicle();
		Drive();
		SetTransform();
		ScreenWrap();
	}

	public void Shoot()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			// Creates the bullet, positioning and rotating it according to the ship
			newBullet = Instantiate(bullet, transform.position, transform.rotation);
			newBullet.AddComponent<Bullet>();
			newBullet.GetComponent<Bullet>().velocity = direction;
			bullets.Add(newBullet);

			// Plays laser sound

		}
	}

	/// <summary>
	/// Changes / Sets the transform component
	/// </summary>
	public void SetTransform()
	{
		// Rotate vehicle sprite
		transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
		
		// Set the transform position
		transform.position = vehiclePosition;
	}

	/// <summary>
	/// 
	/// </summary>
	public void Drive()
	{
		// Accelerate
		// Small vector that's added to velocity every frame
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			acceleration = accelRate * direction;
		}
		else
		{
			velocity *= 0.97f;
			acceleration = Vector3.zero;
		}

		// We used to use this, but acceleration will now increase the vehicle's "speed"
		// Velocity will remain intact from one frame to the next
		velocity += acceleration;
		
		// Limit velocity so it doesn't become too large
		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
		
		// Add velocity to vehicle's position
		vehiclePosition += velocity;
	}

	public void RotateVehicle()
	{
		// Player can control direction
		// A key = rotate left by 2 degrees
		// D key = rotate right by 2 degrees
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
		    angleOfRotation += 2;
		    direction = Quaternion.Euler(0, 0, 2) * direction;
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
		    angleOfRotation -= 2;
		    direction = Quaternion.Euler(0, 0, -2) * direction;
		}
	}

	public void ScreenWrap()
	{
		if(transform.position.x < leftBound)
			transform.position = new Vector3(rightBound - 1f, transform.position.y, transform.position.z);
		
		if(transform.position.x > rightBound)
			transform.position = new Vector3(leftBound + 1f, transform.position.y, transform.position.z);
		
		if(transform.position.y < botBound)
			transform.position = new Vector3(transform.position.x, topBound - 1f, transform.position.z);
		
		if(transform.position.y > topBound)
			transform.position = new Vector3(transform.position.x, botBound + 1f, transform.position.z);
	
		vehiclePosition = transform.position;
	}
}
