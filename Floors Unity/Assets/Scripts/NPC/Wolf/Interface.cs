using UnityEngine.UI;
using UnityEngine;

public class Interface : MonoBehaviour {

    [SerializeField] Data Data;

    public Slider healthBar;
    public Text healthNumbers;

    void Update() {
        CalculateHealthBar();
    }

    void CalculateHealthBar() {
        healthBar.value = Mathf.Lerp(healthBar.value, Map(Data.health, 0, Data.healthMax, 0, 1), Time.deltaTime * 8f); // Почему то не округляется до 1
    }

    float Map(float value, float inMin, float inMax, float outMin, float outMax) {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
