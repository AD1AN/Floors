using UnityEngine;
using UnityEngine.UI;

public class ChangeHealthFullMarker : MonoBehaviour {

	public Slider healthBar;
	public Image healthFullMarker;

	void Update () {
		if (healthBar.value >= 0.99) {// Почему то не округляется до 1
			healthFullMarker.color = new Color32(151, 240, 79, 255);
		} else {
			healthFullMarker.color = new Color32(46, 46, 46, 255);
		}
	}
}
