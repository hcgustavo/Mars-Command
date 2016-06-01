using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    public AudioClip collectCrystalSound;
    public AudioClip collectMedPack;

    private AudioSource audioSource;
    public static bool playerIsDead;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        playerIsDead = false;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void PlayCollectSound(string itemTag)
    {
        if(itemTag == "Krystal")
        {
            audioSource.clip = collectCrystalSound;
            audioSource.Play();
        }
        else if(itemTag == "MedPack")
        {
            audioSource.clip = collectMedPack;
            audioSource.Play();
        }
        
    }

    public void GameOver()
    {
        playerIsDead = true;
        GameObject.Find("UIController").SendMessage("ShowGameOverPanel");
    }
}
