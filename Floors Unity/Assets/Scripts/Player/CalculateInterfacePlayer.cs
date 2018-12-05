using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CalculateInterfacePlayer : NetworkBehaviour {

	[SerializeField] DataInterfacePlayer DataInterfacePlayer;

	void Update() {
		CalculateStaminaBar();
		CalculateHealthBar();
		CalculateExpBar();
	}

	void CalculateStaminaBar() {
        DataInterfacePlayer.StaminaBar.value = Mathf.Lerp(DataInterfacePlayer.StaminaBar.value, Map(DataInterfacePlayer.DataPlayer.stamina, 0, DataInterfacePlayer.DataPlayer.staminaMax, 0, 1), Time.deltaTime*8f); // Почему то не округляется до 1
	}

	void CalculateHealthBar() {
		DataInterfacePlayer.HealthBar.value = Mathf.Lerp(DataInterfacePlayer.HealthBar.value, Map(DataInterfacePlayer.DataPlayer.health, 0, DataInterfacePlayer.DataPlayer.healthMax, 0, 1), Time.deltaTime*8f); // Почему то не округляется до 1
	}

	void CalculateExpBar() {
		DataInterfacePlayer.ExpBar.value = Mathf.Lerp(DataInterfacePlayer.ExpBar.value, Map(DataInterfacePlayer.DataPlayer.exp, 0f, DataInterfacePlayer.DataPlayer.expMax, 0f, 1f), Time.deltaTime*8f); // Почему то не округляется до 1
	}

	float Map(float value, float inMin, float inMax, float outMin, float outMax) {
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}