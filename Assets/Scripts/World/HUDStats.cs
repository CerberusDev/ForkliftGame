// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/14
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDStats : MonoBehaviour 
{
	public Slider ArmorBar;

	public Slider LevelProgressBar;

	public Image DamageImage;
	private float DamageImageTime;
	public Color DamageImageColor;
	public Text temp;
	// Use this for initialization
	void Start () 
	{
		DamageImageTime = 0.05f;
		ArmorBar.value = ArmorBar.maxValue;
		LevelProgressBar.value = LevelProgressBar.minValue;
	}
	
	// Update is called once per frame
	void Update (){}

	//////////////////////
	////  ARMOR  BAR  ////
	//////////////////////
	public void playerDamaged( int inDamage )
	{
		ArmorBar.value -= inDamage;
	}

	//////////////////////
	//// DAMAGE IMAGE ////
	//////////////////////
	public void PlayDamageEffect()
	{
		DamageImage.color = DamageImageColor;
		Invoke("RestoreDamageImage", DamageImageTime);
	}

	public void RestoreDamageImage()
	{
		DamageImage.color = Color.clear;
	}

	////////////////////////
	//// LEVEL PROGRESS ////
	////////////////////////
	public void SetLevelProgress( float inProgress )
	{
		LevelProgressBar.value = inProgress;
	}

	///////////////////////
	//// BUCKET STATUS ////
	///////////////////////
	public void SetToolCount( int inCount )
	{
		print("UI updated");
	}
}

