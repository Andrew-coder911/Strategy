using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
	public void SpawnUnit (GameObject unitPrefab ) {
		Instantiate(unitPrefab, transform);
	}
}
