using UnityEngine;
using System.Collections;

public class Tavern
{
	#region Fields

	private string _name;
	private string _owner;
	private int _gold;
	private int _moodSafe;
	private int _moodRich;
	private int _fame;

	#endregion

	#region Setters and Getters

	public string Name
	{
		get { return _name; }
	}

	public string Owner
	{
		get { return _owner; }
	}

	public int Gold
	{
		get { return _gold; }
		set { _gold = Mathf.Min(0, value); }
	}

	public int SecurityAtmosphere
	{
		get { return _moodSafe; }
		set { _moodSafe = value; }
	}

	public bool IsSafe
	{
		get { return _moodSafe > 0; }
	}

	public bool IsDangerous
	{
		get { return _moodSafe < 0; }
	}

	public int CostAtmosphere
	{
		get { return _moodRich; }
		set { _moodRich = value; }
	}

	public bool IsRich
	{
		get { return _moodRich > 0; }
	}

	public bool IsHomely
	{
		get { return _moodRich < 0; }
	}

	#endregion

	#region Constructor

	public Tavern(string name, string owner)
	{
		_name = name;
		_owner = owner;
		_gold = 100;
		_fame = 0;
		_moodRich = 0;
		_moodSafe = 0;
	}

	#endregion
}
