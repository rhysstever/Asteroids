using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip ambiance;
	public AudioClip laser;
	public AudioClip boosters;
	public AudioClip asteroidExplosion;
	public AudioClip shipExplosion;

	// Start is called before the first frame update
	void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();

		audioSource.volume = gameObject.GetComponent<AudioSource>().volume / 2;

		audioSource.clip = ambiance;
		audioSource.loop = true;
		audioSource.Play();
	}

	// Update is called once per frame
	void Update()
	{
		if(gameObject.GetComponent<GameManager>().currentState == GameStates.Game)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				audioSource.PlayOneShot(laser);
			}

			if(Input.GetKeyDown(KeyCode.W))
			{
				audioSource.PlayOneShot(boosters);
			}
		}
	}
}
