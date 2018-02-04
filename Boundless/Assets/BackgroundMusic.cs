using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	public static BackgroundMusic Instance;

	public AudioSource AudioSource;

	public void Start () {
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this);
		}
	}

	public void Play(AudioClip music)
	{
		AudioSource.clip = music;
		AudioSource.Play();
	}
}
