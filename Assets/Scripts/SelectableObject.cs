using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour {

	[SerializeField] private GameObject _selectionIndicator;

	private void Start() {
		_selectionIndicator.SetActive (false);
	}

	public virtual void Onhover() {
		transform.localScale = Vector3.one * 1.1f;
	}

	public virtual void OnUnhower() {
		transform.localScale = Vector3.one;
	}

	public virtual void AnySelect() {
		_selectionIndicator.SetActive (true);
	}

	public virtual void AnyUnselect() {
		_selectionIndicator.SetActive (false);
	}

	public virtual void WhenClickOnGround( Vector3 point ) {
	}
}
