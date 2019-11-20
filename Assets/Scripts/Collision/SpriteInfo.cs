using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
	SpriteRenderer spriteRenderer;
	
	// Start is called before the first frame update
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	public float MinX { get { return spriteRenderer.bounds.min.x; } }
	public float MinY { get { return spriteRenderer.bounds.min.y; } }
	public float MaxX { get { return spriteRenderer.bounds.max.x; } }
	public float MaxY { get { return spriteRenderer.bounds.max.y; } }
	public Vector3 Size { get { return spriteRenderer.bounds.size; } }
	public Vector3 Center { get { return spriteRenderer.bounds.center; } }
}
