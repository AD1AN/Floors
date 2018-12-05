using UnityEngine;
using UnityEngine.Networking;

public class SetupPlayer : NetworkBehaviour {

    [SerializeField] DataPlayer DataPlayer;

    [SerializeField] Behaviour[] componentsToDisable;
    
    void Start () {
        transform.name = "Player " + DataPlayer.playerId;

        if (!isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable[i].enabled = false;
			}
		}
	}
}
