using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static GameManager _instance;

	public static GameManager Instance {
		get {
			if( _instance == null ) {
				GameObject gameManager = new GameObject( "GameManager" );
				_instance = gameManager.AddComponent<GameManager>();
			}

			return _instance;
		}
	}

	public float pointsPerUnitTravelled = 1.0f;
	public float gameSpeed = 10.0f;
	public string titleScreenName = "TitleScreen";

	[HideInInspector]
	public int previousScore = 0;

	private float score = 0.0f;
	private static float highScore = 0.0f;
	private static bool saved = false;
	private bool gameOver = false;

	void Start() {
		if( _instance != this ) {
			if( _instance == null ) {
				_instance = this;
			} else {
				Destroy( gameObject );
			}
		}

		LoadHighScore();
		DontDestroyOnLoad( gameObject );
	}
	
	void Update() {
		if( Application.loadedLevelName == titleScreenName ) {
			ResetGame();
			return;
		}

		if( GameObject.FindGameObjectWithTag( "Player" ) == null ) {
			gameOver = true;
		}

		if( gameOver ) {
			if( ! saved ) {
				SaveHighScore();
				previousScore = ( int ) score;
				saved = true;
			}
			if( Input.anyKeyDown ) {
				Application.LoadLevel( titleScreenName );
			}
		}

		if( ! gameOver ) {
			score += pointsPerUnitTravelled * gameSpeed * Time.deltaTime;
			if( score > highScore ) {
				highScore = score;
				saved = false;
			}
		}
	}

	void ResetGame() {
		score = 0.0f;
		gameOver = false;
		saved = false;
	}

	void SaveHighScore() {
		PlayerPrefs.SetInt( "Highscore", ( int ) highScore );
		PlayerPrefs.Save();
	}

	void LoadHighScore() {
		highScore = PlayerPrefs.GetInt( "Highscore" );
	}

	void OnGUI() {
		if( Application.loadedLevelName == titleScreenName ) {
			return;
		}

		string currentScore = ( ( int ) score ).ToString();
		string currentHighScore = ( ( int ) highScore ).ToString();
		GUILayout.Label( "Score: " + currentScore );
		GUILayout.Label ( "Highscore: " + currentHighScore );
		if( gameOver == true ) {
			GUILayout.Label ( "Game Over! Press any key to quit!" );
		}
	}
}
