using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public GameObject gameOverUI;
	public GameObject gameWinUI;
	bool gameIsOver;

	// Use this for initialization
	void Start () {
		Guard.OnGuardHasSpottedPlayer += ShowGameOverUI;
		FindObjectOfType<Player>().OnReachedEndOfLevel += ShowGameWinUI;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameIsOver)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene(0);
			}
		}
	}

	void ShowGameWinUI()
	{
		OnGameOver(gameWinUI);
	}

	void ShowGameOverUI()
	{
		OnGameOver(gameOverUI);
	}

	void OnGameOver (GameObject showUI)
	{
		showUI.SetActive(true);
		gameIsOver = true;
		Guard.OnGuardHasSpottedPlayer -= ShowGameOverUI;
		FindObjectOfType<Player>().OnReachedEndOfLevel -= ShowGameWinUI;
	}
}
