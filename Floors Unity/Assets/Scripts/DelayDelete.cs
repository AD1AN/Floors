using UnityEngine.Networking;
using UnityEngine;

public class DelayDelete : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("DeleteParticle", 5f);
	}

	void DeleteParticle() {
		NetworkServer.Destroy(gameObject);
	}
}