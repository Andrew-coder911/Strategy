using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
	[SerializeField] private int _money;

    public int Money {
		get
		{
			return _money;
		}
		set
		{
			_money = value;
		}
	}
}
