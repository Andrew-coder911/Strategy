using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableCollaider : MonoBehaviour {

	[SerializeField] private SelectableObject SelectableObject;

	public SelectableObject Select {
		get
		{
			return SelectableObject;
		}
	}


}
