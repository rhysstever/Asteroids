using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Vector3 direction;
	public Vector3 velocity;

	// Start is called before the first frame update
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update()
	{
		transform.position += velocity * 0.1f;
	}
}
