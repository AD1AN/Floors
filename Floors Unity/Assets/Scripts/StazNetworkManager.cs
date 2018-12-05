using UnityEngine;
using UnityEngine.Networking;

public class StazNetworkManager : NetworkManager {

    public class NetworkMessage : MessageBase {
        public string nick;
    }
                                              
    public static string tempNick; 

	[SerializeField] GameObject[] InstantiateNetworkObjects;

	public override void OnStartClient(NetworkClient client) {
		base.OnStartClient(client);
        
		foreach (var item in InstantiateNetworkObjects) {
			ClientScene.RegisterPrefab(item);
		}
	}
    
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader) {
        NetworkMessage clientData = extraMessageReader.ReadMessage<NetworkMessage>();
        GameObject player = Instantiate(playerPrefab, GetStartPosition().position, Quaternion.identity) as GameObject;
        player.GetComponent<DataPlayer>().playerNick = clientData.nick;
        player.GetComponent<DataPlayer>().playerId = conn.connectionId;    

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnClientSceneChanged(NetworkConnection conn) {
        NetworkMessage clientData = new NetworkMessage();

        clientData.nick = tempNick;

        ClientScene.AddPlayer(conn, 0, clientData);
    }

    public override void OnServerDisconnect(NetworkConnection conn) {
        base.OnServerDisconnect(conn); 
        Destroy(GameObject.Find("InterfacePlayer " + conn.connectionId));
    }
}
