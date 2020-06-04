using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public void LoadGameScene() 
	{
		SceneManager.LoadScene("Game");
	}

	public void LoadGameOver()
	{
		SceneManager.LoadScene(SceneManager.sceneCount - 1);
	}

	public void LoadStartScene()
	{
		SceneManager.LoadScene(0);
	}

	public void QuitGame() 
	{
		Application.Quit();
	}
}
