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
	public Canvas Menu_win;

	public Canvas Background;

	public AudioSource MainMusic;
	public Text distanceLose, distanceWin; //yes it should be the same.

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
		Menu_win.enabled = false;
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

	public void Menu_WinPressed( int action )
	{
		switch (action)
		{
		case 0:
			print ("level completed, restarting");
			
			MainCharacterScript.ReloadLevel();
			break;
		}
	}

	public void ShowStatMenu( float inDistance, bool bWin )
	{
		SetEnable(true);

		if( bWin )
		{
			Menu_win.enabled = true;
			distanceWin.text = ((int)(inDistance * 100)).ToString();
		}
		else
		{
			Menu_stats.enabled = true;
			distanceLose.text = ((int)(inDistance * 100)).ToString();
		}
	}
}
