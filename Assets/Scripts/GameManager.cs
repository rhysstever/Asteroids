using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameStates
{
	MainMenu,
	Controls,
	Game,
	Pause,
	Lose
}

public class GameManager : MonoBehaviour
{
	public Canvas canvas;
	Text textStart;
	Text textControlsHeader;
	Text textControls;
	Text textGame;
	Text textPause;
	Text textLose;

	public int points = 0;
	public int lives = 3;
	public GameStates currentState;

	// Start is called before the first frame update
	void Start()
	{
		// Gets each text opject from the canvas
		textStart = canvas.transform.GetChild(0).gameObject.GetComponent<Text>();
		textControlsHeader = canvas.transform.GetChild(1).gameObject.GetComponent<Text>();
		textControls = canvas.transform.GetChild(2).gameObject.GetComponent<Text>();
		textGame = canvas.transform.GetChild(3).gameObject.GetComponent<Text>();
		textPause = canvas.transform.GetChild(4).gameObject.GetComponent<Text>();
		textLose = canvas.transform.GetChild(5).gameObject.GetComponent<Text>();

		// Initial game state
		ChangeGameState(GameStates.MainMenu);
	}

	// Update is called once per frame
	void Update()
	{
		points = gameObject.GetComponent<CollisionManager>().points;
		lives = gameObject.GetComponent<CollisionManager>().lives;

		// Cases for changing the game state during while the scene is running
		switch(currentState)
		{
			case GameStates.MainMenu:
				// If the player presses either "Enter" key, it goes to the Instructions Menu
				if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
					ChangeGameState(GameStates.Controls);

				// Closes the game is the escape key is pressed on the main menu screen
				if(Input.GetKeyDown(KeyCode.Escape))
					Application.Quit();
				break;

			case GameStates.Controls:
				// If the player presses either "Enter" key, it goes to the Game
				if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
					ChangeGameState(GameStates.Game);
				break;

			case GameStates.Game:
				// Score and Lives counter for gameplay
				textGame.text = "Lives: " + lives +
					"\nPoints: " + points;
				if(lives <= 0)
				{ 
					gameObject.GetComponent<AudioPlayer>().audioSource.PlayOneShot(
						gameObject.GetComponent<AudioPlayer>().shipExplosion);
					ChangeGameState(GameStates.Lose);
				}
				else if(Input.GetKeyDown(KeyCode.P))
				{
					ChangeGameState(GameStates.Pause);
				}
				break;

			case GameStates.Pause:
				if(Input.GetKeyDown(KeyCode.P))
				{
					ChangeGameState(GameStates.Game);
				}
				else if(Input.GetKeyDown(KeyCode.Escape))
				{
					ChangeGameState(GameStates.MainMenu);
				}
				break;

			case GameStates.Lose:
				// If the player presses either "Enter" key, it goes to the Main Menu
				if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
					ChangeGameState(GameStates.MainMenu);
				break;
		}
	}

	// Changes that are made to the scene exactly once
	// Ex: setting text properties, setting the active-ness 
	// of the player object, etc
	public void ChangeGameState(GameStates gameState)
	{
		switch(gameState)
		{
			case GameStates.MainMenu:
				currentState = GameStates.MainMenu;

				// Sets up text for each gameState
				// Main menu title
				textStart.text = "No Man's Asteroid";
				textStart.alignment = TextAnchor.MiddleCenter;
				textStart.fontSize = 60;
				textStart.color = Color.white;

				textStart.enabled = true;
				textControlsHeader.enabled = false;
				textControls.enabled = false;
				textGame.enabled = false;
				textPause.enabled = false;
				textLose.enabled = false;

				gameObject.GetComponent<CollisionManager>().spaceship.SetActive(false);

				// The game is "reset": the ship's position is set to the origin, the player's
				// points and lives are set to their default values: 0 and 3 respectively
				gameObject.GetComponent<CollisionManager>().spaceship.transform.position = Vector3.zero;
				gameObject.GetComponent<CollisionManager>().points = 0;
				gameObject.GetComponent<CollisionManager>().lives = 3;
				break;

			case GameStates.Controls:
				currentState = GameStates.Controls;

				// Instructions menu
				textControlsHeader.text = "Controls";
				textControlsHeader.alignment = TextAnchor.MiddleCenter;
				textControlsHeader.fontSize = 40;
				textControlsHeader.color = Color.white;

				textControls.text =
					"Forward - W or Up Arrow" +
					"\nTurn Left - A or Left Arrow" +
					"\nTurn Right - D or Right Arrow" +
					"\nShoot - Spacebar" +
					"\nP - Toggle Pause" + 
					"\nEsc (in Pause Menu) - Quit to Main Menu";
				textControls.alignment = TextAnchor.MiddleLeft;
				textControls.fontSize = 30;
				textControls.color = Color.white;

				textStart.enabled = false;
				textControlsHeader.enabled = true;
				textControls.enabled = true;
				textGame.enabled = false;
				textPause.enabled = false;
				textLose.enabled = false;

				gameObject.GetComponent<CollisionManager>().spaceship.SetActive(false);
				break;

			case GameStates.Game:
				currentState = GameStates.Game;
				
				textGame.alignment = TextAnchor.MiddleLeft;
				textGame.fontSize = 25;
				textGame.color = Color.white;

				textStart.enabled = false;
				textControlsHeader.enabled = false;
				textControls.enabled = false;
				textGame.enabled = true;
				textPause.enabled = false;
				textLose.enabled = false;

				// When the game is started, the player object is activated
				gameObject.GetComponent<CollisionManager>().spaceship.SetActive(true);
				break;

			case GameStates.Pause:
				currentState = GameStates.Pause;

				textPause.text = "Game Paused";
				textPause.alignment = TextAnchor.MiddleCenter;
				textPause.fontSize = 40;
				textPause.color = Color.white;

				textStart.enabled = false;
				textControlsHeader.enabled = false;
				textControls.enabled = false;
				textGame.enabled = true;
				textPause.enabled = true;
				textLose.enabled = false;

				gameObject.GetComponent<CollisionManager>().spaceship.SetActive(false);
				break;

			case GameStates.Lose:
				currentState = GameStates.Lose;

				// GameOver Text
				textLose.text = "Game Over!" +
					"\nScore: " + points;
				textLose.alignment = TextAnchor.MiddleCenter;
				textLose.fontSize = 40;
				textLose.color = Color.white;

				textStart.enabled = false;
				textControlsHeader.enabled = false;
				textControls.enabled = false;
				textGame.enabled = false;
				textPause.enabled = false;
				textLose.enabled = true;

				gameObject.GetComponent<CollisionManager>().spaceship.SetActive(false);
				break;
		}
	}
}
