using UnityEngine;

public class MouseClic : MonoBehaviour {

	void OnMouseUp () {
		if (RollOver.itemHit == renderer) // ensure mouse click (up+down) is done on the same building
			LinksController.MouseClickOn(name);
	}
	
}
