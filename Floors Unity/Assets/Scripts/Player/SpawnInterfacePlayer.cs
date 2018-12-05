using UnityEngine;
using UnityEngine.Networking;

public class SpawnInterfacePlayer : NetworkBehaviour { // СОЗДАЕТ ИНТЕРФЕЙС ИГРОКА У ВСЕХ КЛИЕНТОВ.

	[SerializeField] DataPlayer DataPlayer;

	[SerializeField] GameObject InterfacePlayerPrefab;

	void Start() {
		CmdInstall(DataPlayer.playerId);
	}
	[Command]
	void CmdInstall(int id) {
        InterfacePlayerPrefab.GetComponent<DataInterfacePlayer>().playerId = id; // ИЗМЕНЯЕТ ЗНАЧЕНИЕ В ПРЕФАБЕ
        NetworkServer.Spawn(InterfacePlayerPrefab = Instantiate(InterfacePlayerPrefab, transform.position, transform.rotation));
    }
}
