using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Building : SelectableObject {

	[SerializeField] private int _price;
	[SerializeField] private int _xSize = 3;
	[SerializeField] private int _zSize = 3;

	[SerializeField] private NavMeshObstacle _navMeshObstacle;
	[SerializeField] private Canvas _buildingWindow;

	public int XSize {
		get
		{
			return _xSize;
		}
	}
	public int ZSize {
		get
		{
			return _zSize;
		}
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Правильно ли сделать переменную _cellSize полем для отображения в инспекторе?
	//[SerializeField] private float _cellSize = BuildingPlacer.CellSize;
	// Или лучше просто добавить множитель?
	//Сделал множитель как тренировочный вариант, но потом понял что размер клетки мы должны менять в классе BuildingPlacer сразу для всех
	[SerializeField] private float _cellMultiplier = 1f;
	///////////////////////////////////////////////////////////////////////////////////////////////////////////

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

	private Color _starColor;
	[SerializeField] private Renderer _renderer;

	private void Awake() {
		_starColor = _renderer.material.color;
	}

	private void OnDrawGizmos() {
		float cellSize = BuildingPlacer.CellSize * _cellMultiplier;

		for (int x = 0; x < _xSize; x++) {
			for (int z = 0; z < _zSize; z++) {
				Gizmos.DrawWireCube (transform.position + new Vector3 (x, 0, z) * cellSize, new Vector3 (1f, 0f, 1f) * cellSize);
			}
		}
		
	}

	public override void AnySelect() {
		base.AnySelect ();
		_buildingWindow.enabled = true;
	}

	public override void AnyUnselect() {
		base.AnyUnselect ();
		_buildingWindow.enabled = false;
	}

	public override void WhenClickOnGround( Vector3 point ) {
		base.WhenClickOnGround (point);
		_navMeshObstacle.enabled = true;
	}

	public void DisplayUnacceptablePosition() {
		_renderer.material.color = Color.red;
	}

	public void DisplayAcceptablePosition() {
		_renderer.material.color = _starColor;
	}

}
