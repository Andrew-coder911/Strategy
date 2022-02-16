using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour {
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Правильно ли реализовано статическое поле и свойство а так же последующее применение в классе Building?
	// И как сделать чтобы это поле отображалось в инспекторе?
	[SerializeField] private static float _cell = 1f;

	public static float CellSize {
		get
		{
			return _cell;
		}
		set
		{
			_cell = value;
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////

	[SerializeField] private Camera _camera;

	private Plane _plane;

	[SerializeField] private Building _currentBuilding;
	[SerializeField] private Dictionary<Vector2Int, Building> _buildingsDictionary = new Dictionary<Vector2Int, Building> ();

	void Start() {
		_plane = new Plane (Vector3.up, Vector3.zero);
	}

	void Update() {
		if (_currentBuilding == null) {
			return;
		}

		Ray ray = _camera.ScreenPointToRay (Input.mousePosition);

		float distance;
		_plane.Raycast (ray, out distance);
		Vector3 point = ray.GetPoint (distance) / CellSize;

		int x = Mathf.RoundToInt (point.x);
		int z = Mathf.RoundToInt (point.z);

		_currentBuilding.transform.position = new Vector3 (x, 0f, z) * CellSize;

		if(CheckAllow(x, z, _currentBuilding)) {
			_currentBuilding.DisplayAcceptablePosition();
			if (Input.GetMouseButtonDown (0)) {
				InstallBuilding (x, z, _currentBuilding);
				_currentBuilding = null;
			}
		}
		else {
			_currentBuilding.DisplayUnacceptablePosition();
		}		
	}

	bool CheckAllow( int xPosition, int zPosition, Building building ) {

		//Можно ли этот кусок кода заменить на цикл foreach?
		for (int x = 0; x < building.XSize; x++) {
			for (int z = 0; z < building.ZSize; z++) {
				Vector2Int coordinate = new Vector2Int (xPosition + x, zPosition + z);
				if (_buildingsDictionary.ContainsKey (coordinate)) {
					return false;
				}
			}
		}
		return true;
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	//Не совсем понял мы чтобы добавить координаты здания в качестве ключа пробегаем их в цикле задавая 1 шагом цикла,
	//а что же будет если мы изменим размер клетки например на 0.5?
	//И координата одного квадратика клеток это же получается конечная точка его диагонали или я совсем не правильно понял?(вот это (xPosition + x, zPosition + z)) 
	void InstallBuilding( int xPosition, int zPosition, Building building ) {
		for (int x = 0; x < building.XSize; x++) {
			for (int z = 0; z < building.ZSize; z++) {
				Vector2Int coordinate = new Vector2Int (xPosition + x, zPosition + z);
				_buildingsDictionary.Add (coordinate, _currentBuilding);
			}
		}
		foreach (var item in _buildingsDictionary) {
			Debug.Log(item);
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void CreateBuilding( GameObject buildingPrefab ) {
		GameObject newBuilding = Instantiate (buildingPrefab);
		_currentBuilding = newBuilding.GetComponent<Building> ();
	}
}