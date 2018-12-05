using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInterfacePlayer : MonoBehaviour {

	[SerializeField] DataInterfacePlayer DataInterfacePlayer;

	bool SaveMenu = false;
	List<string> SavedMenu = new List<string>();

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			bool ismm = DataInterfacePlayer.MainMenuAnim.GetBool("MainMenu");
            DataInterfacePlayer.MainMenuAnim.SetBool("MainMenu", ismm = !ismm);
			if (SaveMenu == true) {
				if (ismm == true) {
					for (int i = 0; i < SavedMenu.Count; i++) {
                        DataInterfacePlayer.MainMenuAnim.SetBool(SavedMenu[i], true);
					}
				} else {
					for (int i = 0; i < SavedMenu.Count; i++) {
                        DataInterfacePlayer.MainMenuAnim.SetBool(SavedMenu[i], true);
					}
				}
			} else {
				if (ismm == false) {
					if (SavedMenu.Count != 0) {
                        DataInterfacePlayer.MainMenuAnim.SetBool(SavedMenu[SavedMenu.Count-1], false);
						SavedMenu.Clear();
					}
				}
			}
		}
	}

	public void SettingsPanel() {
		if (SavedMenu.Contains("SettingsPanel")) {
            DataInterfacePlayer.MainMenuAnim.SetBool("SettingsPanel", false);
			SavedMenu.Remove("SettingsPanel");
		} else {
            DataInterfacePlayer.MainMenuAnim.SetBool("SettingsPanel", true);
			SavedMenu.Add("SettingsPanel");
		}
	}

	public void DisconnectToMainMenu() {
        if (DataInterfacePlayer.DataPlayer.isServer) {
            StazNetworkManager.singleton.StopHost();
        } else {
            StazNetworkManager.singleton.StopClient();
        }
	}

	public void QuitGame() {
		Application.Quit();
	}
}
