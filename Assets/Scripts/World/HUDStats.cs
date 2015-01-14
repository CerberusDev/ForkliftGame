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
	public Image DamageImage;
	private float DamageImageTime;
	public Color DamageImageColor;

	// Use this for initialization
	void Start () 
	{
		DamageImageTime = 0.05f;
	}
	
	// Update is called once per frame
	void Update (){}

	public void playerDamaged( int inDamage )
	{
		ArmorBar.value -= inDamage;
	}

	public void PlayDamageEffect()
	{
		DamageImage.color = DamageImageColor;
		Invoke("RestoreDamageImage", DamageImageTime);
	}

	public void RestoreDamageImage()
	{
		DamageImage.color = Color.clear;
	}
}
