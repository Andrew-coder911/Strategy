using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionState {
	UnitsSelected,
	Frame,
	Other
}

public class Managment : MonoBehaviour {
	[SerializeField] private Camera _camera;
	[SerializeField] private SelectableObject _howered;

	[SerializeField] List<SelectableObject> _listOfSelected = new List<SelectableObject> ();

	[SerializeField] private Image _frameImage;
	[SerializeField] private Vector2 _frameStart;
	[SerializeField] private Vector2 _frameEnd;

	[SerializeField] private SelectionState _currentSelectionState;

	void Update() {

		Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay (ray.origin, ray.direction * 10f, Color.red);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.GetComponent<SelectableCollaider> ()) {
				SelectableObject hitSelectable = hit.collider.GetComponent<SelectableCollaider> ().Select;

				if (_howered != null) {
					if (_howered != hitSelectable) {
						_howered.OnUnhower ();
						_howered = hitSelectable;
						_howered.Onhover ();
					}
				}
				else {
					_howered = hitSelectable;
					_howered.Onhover ();
				}
			}
			else {
				UnhoverCurrent ();
			}
		}
		else {
			UnhoverCurrent ();
		}

		if (Input.GetMouseButtonUp (0)) {
			if (_howered != null) {
				if (Input.GetKey (KeyCode.LeftControl) == false) {
					UnselectAll ();
				}
				_currentSelectionState = SelectionState.UnitsSelected;
				Select (_howered);
			}
		}

		if(_currentSelectionState == SelectionState.UnitsSelected) {
			if (Input.GetMouseButtonUp (0)) {
				if (hit.collider.tag == "Ground") {
					for (int i = 0; i < _listOfSelected.Count; i++) {
						_listOfSelected[i].WhenClickOnGround (hit.point);
					}
				}
			}
		}
		

		if (Input.GetMouseButtonDown (1)) {
			UnselectAll ();
		}

		//Выделене рамкой
		if (Input.GetMouseButtonDown (0)) {
			_frameStart = Input.mousePosition;
		}

		if (Input.GetMouseButton (0)) {

			_frameEnd = Input.mousePosition;

			Vector2 min = Vector2.Min (_frameStart, _frameEnd);
			Vector2 max = Vector2.Max (_frameStart, _frameEnd);

			Vector2 size = max - min;

			if (size.magnitude > 10) {				

				_frameImage.enabled = true;
				_frameImage.rectTransform.anchoredPosition = min;

				_frameImage.rectTransform.sizeDelta = size;

				Rect rect = new Rect (min, size);

				UnselectAll ();
				Unit[] allUnits = FindObjectsOfType<Unit> ();
				for (int i = 0; i < allUnits.Length; i++) {
					Vector2 screenPosition = _camera.WorldToScreenPoint (allUnits[i].transform.position);
					if (rect.Contains (screenPosition)) {
						Select (allUnits[i]);
					}
				}

				_currentSelectionState = SelectionState.Frame;
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			_frameImage.enabled = false;
			if (_listOfSelected.Count > 0) {
				_currentSelectionState = SelectionState.UnitsSelected;
			}
			else {
				_currentSelectionState = SelectionState.Other;
			}
		}

	}

	void Select( SelectableObject selectableObject ) {
		if (_listOfSelected.Contains (selectableObject) == false) {
			_listOfSelected.Add (selectableObject);
			selectableObject.AnySelect ();
		}
	}

	void UnselectAll() {
		for (int i = 0; i < _listOfSelected.Count; i++) {
			_listOfSelected[i].AnyUnselect ();
		}
		_listOfSelected.Clear ();
		_currentSelectionState = SelectionState.Other;
	}

	void UnhoverCurrent() {
		if (_howered != null) {
			_howered.OnUnhower ();
			_howered = null;
		}
	}
}
