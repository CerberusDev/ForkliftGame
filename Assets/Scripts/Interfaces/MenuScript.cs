// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/23
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour 
{
	public Canvas Menu_start;
	public Canvas Menu_instructions;
	public Canvas Menu_stats;
	public Canvas Background;

	public AudioSource MainMusic;
	public Text distance;

	//only for setting game flow, we don't have SceneManager, main char does it.
	public GameObject MainCharacter;
	PlayerMovement MainCharacterScript;

	// Use this for initialization
	void Start () 
	{
		MainCharacterScript = MainCharacter.GetComponent<PlayerMovement>();
		SetEnable(true);
		MainMusic.Stop();
		Menu_instructions.enabled = false;
		Menu_stats.enabled = false;

	}

	public void SetEnable( bool inState )
	{
		enabled = inState;
		Background.enabled = inState;
	}

	// Update is called once per frame
	void Update () 
	{
	}

	bool IsStartMenuVisible()
	{
		return Menu_start.enabled;
	}

	bool IsInstructionsMenuVisible()
	{
		return Menu_instructions.enabled;
	}

	bool IsStatsMenuVisible()
	{
		return Menu_stats.enabled;
	}

	public void Menu_startPressed( int action )
	{
		switch (action)
		{
		case 0:
			print ("Start");

			SetEnable(false);
			Menu_start.enabled = false;

			MainCharacterScript.StartGame();
			MainMusic.Play();

			break;
		case 1:
			print ("Instructions");
			Menu_start.enabled = false;

			Menu_instructions.enabled = true;

			break;
		case 2:
			print ("QUIT");

			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit();
			#endif

			//Application.Quit();
			break;
		}
	}

	public void Menu_instructionsPressed( int action )
	{
		switch (action)
		{
		case 0:
			print ("back from instructions");

			Menu_instructions.enabled = false;
			Menu_start.enabled = true;
			break;
		}
	}

	public void Menu_statsPressed( int action )
	{
		switch (action)
		{
		case 0:

			MainCharacterScript.ReloadLevel();
			break;
		}
	}
	
	public void ShowStatMenu( float inDistance )
	{
		SetEnable(true);

		Menu_stats.enabled = true;
		distance.text = ((int)(inDistance * 100)).ToString();
	}
}
