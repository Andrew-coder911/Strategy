using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButton : MonoBehaviour {
	[SerializeField] private UnitSpawner _unitSpawner;
	[SerializeField] private GameObject _unitPrefab;

	public void TryBuy() {
		int price = _unitPrefab.GetComponent<Unit> ().Price;
		int money = FindObjectOfType<Resources> ().Money;
		if (money >= price) {
			money -= price;
			_unitSpawner.SpawnUnit (_unitPrefab);
		}
		else {
			Debug.Log ("Недостаточно золота");
		}
	}
}
