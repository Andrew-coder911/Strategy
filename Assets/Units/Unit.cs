using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject {
	[SerializeField] private int _price = 3;
	public int Price {
		get
		{
			return _price;
		}
		set
		{
			_price = value;
		}
	}

	[SerializeField] private NavMeshAgent _navMeshAgent;

	public override void WhenClickOnGround( Vector3 point ) {
		base.WhenClickOnGround (point);

		_navMeshAgent.SetDestination (point);
	}
}
