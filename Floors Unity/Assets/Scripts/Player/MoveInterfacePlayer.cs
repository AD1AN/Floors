using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MoveInterfacePlayer : NetworkBehaviour {

	[SerializeField] DataInterfacePlayer DataInterfacePlayer;

	void Update() {
		transform.position = Vector3.Lerp(transform.position, DataInterfacePlayer.DataPlayer.transform.position, Time.deltaTime*8f);
        transform.rotation = Quaternion.Lerp(transform.rotation, DataInterfacePlayer.DataPlayer.transform.rotation, Time.deltaTime * 13f);
	}
}
