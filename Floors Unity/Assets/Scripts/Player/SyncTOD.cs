using UnityEngine;
using UnityEngine.Networking;

public class SyncTOD : NetworkBehaviour {
	[SyncVar] public float syncHour;
	public float SyncHour {
		get {return syncHour;}
		set {
			if (isServer) {
				syncHour = value;
			}
		}
	}
}
