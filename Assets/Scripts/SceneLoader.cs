using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	[SerializeField] float delayBeforeGameOver = 3f;

	public void LoadGameScene() 
	{
		SceneManager.LoadScene("Game");
	}

	public void LoadGameOver()
	{
		StartCoroutine("GameOverWithDelay");
	}

	public void LoadStartScene()
	{
		SceneManager.LoadScene(0);
	}

	public void QuitGame() 
	{
		Application.Quit();
	}

	private IEnumerator GameOverWithDelay()
	{
		yield return new WaitForSeconds(delayBeforeGameOver);

		SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
	}
}
