// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/16

using UnityEngine;
using System.Collections;

public class ToolBucketManager : MonoBehaviour {

	private int ToolCount;
	public int ToolMax;
	public float ToolRespawnTime;
	public GameObject ToolImage;

	// Use this for initialization
	void Start () 
	{
		ToolCount = ToolMax;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	/// <summary>
	/// Determines whether this instance can throw tool.
	/// </summary>
	/// <returns><c>true</c> if this instance can throw tool; otherwise, <c>false</c>.</returns>
	public bool CanThrowTool()
	{
		return true;
	}

	/// <summary>
	/// Gets the current tool count.
	/// </summary>
	/// <returns>The tool count.</returns>
	public int GetToolCount()
	{
		return ToolCount;
	}

	/// <summary>
	/// Throws the tool immediately.
	/// </summary>
	public void ThrowTool()
	{
		print("RZUCAM KURWA");
	}
}
