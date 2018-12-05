using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessing : MonoBehaviour {

	public PostProcessingProfile MainPP;

	void Update() {
		DamagedFadeout();
	}

	public void Damaged() {
		var vignette = MainPP.vignette.settings;
		vignette.smoothness += 0.3f;
		MainPP.vignette.settings = vignette;
	}

	void DamagedFadeout() {
		var vignette = MainPP.vignette.settings;
		vignette.smoothness = Mathf.Lerp(vignette.smoothness, 0.01f, Time.deltaTime);
		MainPP.vignette.settings = vignette;
	}
}
