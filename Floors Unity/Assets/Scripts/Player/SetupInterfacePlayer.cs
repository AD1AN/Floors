using UnityEngine;

public class SetupInterfacePlayer : MonoBehaviour {

	[SerializeField] DataInterfacePlayer DataInterfacePlayer;

	[SerializeField] Behaviour[] componentsToDisable;

	void Start () {
        GameObject Player = GameObject.Find("Player " + DataInterfacePlayer.playerId);
        DataInterfacePlayer.DataPlayer = Player.GetComponent<DataPlayer>();
        Player.GetComponent<DataPlayer>().DataInterfacePlayer = DataInterfacePlayer;

        transform.name = "InterfacePlayer " + DataInterfacePlayer.playerId;
        DataInterfacePlayer.Nick.text = DataInterfacePlayer.DataPlayer.playerNick;
        DataInterfacePlayer.Level.text = "Level: " + DataInterfacePlayer.DataPlayer.level.ToString();

		if (!DataInterfacePlayer.DataPlayer.isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable[i].enabled = false;
			}
		}
	}
}
