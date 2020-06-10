using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	private void Awake()
	{
		int musicPlayersCount = FindObjectsOfType(GetType()).Length;
		if (musicPlayersCount > 1)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
