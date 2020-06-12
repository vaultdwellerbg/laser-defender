using UnityEngine;

public class GameSession : MonoBehaviour
{
	private int score = 0;

	private void Awake()
	{
		int gameSessionCount = FindObjectsOfType(GetType()).Length;
		if (gameSessionCount > 1)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	public int GetScore()
	{
		return score;
	}

	public void AddToScore(int value)
	{
		score += value;
	}

	public void Reset()
	{
		Destroy(gameObject);
	}
}
