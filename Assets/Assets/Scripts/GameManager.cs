using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public float pointsPerUnitTravelled = 1.0f;
	public float gameSpeed = 10.0f;

	private float score = 0.0f;
	private static float highScore = 0.0f;
	private static bool saved = false;
	private bool gameOver = false;

	// Use this for initialization
	void Start () {
		instance = this;
		LoadHighScore();
	}
	
	// Update is called once per frame
	void Update() {
		if( GameObject.FindGameObjectWithTag( "Player" ) == null ) {
			gameOver = true;
		}

		if( gameOver ) {
			if( ! saved ) {
				SaveHighScore();
				saved = true;
			}
			if( Input.anyKeyDown ) {
				Application.LoadLevel( Application.loadedLevel );
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

	void SaveHighScore() {
		PlayerPrefs.SetInt( "Highscore", ( int ) highScore );
		PlayerPrefs.Save();
	}
	void LoadHighScore() {
		highScore = PlayerPrefs.GetInt( "Highscore" );
	}

	void OnGUI() {
		string currentScore = ( ( int ) score ).ToString();
		string currentHighScore = ( ( int ) highScore ).ToString();
		GUILayout.Label( "Score: " + currentScore );
		GUILayout.Label ( "Highscore: " + currentHighScore );
		if( gameOver == true ) {
			GUILayout.Label ( "Game Over! Press any key to reset!" );
		}
	}
}
