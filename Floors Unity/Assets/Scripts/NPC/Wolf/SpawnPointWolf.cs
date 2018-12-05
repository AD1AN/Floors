using UnityEngine;
using UnityEngine.Networking;

public class SpawnPointWolf : NetworkBehaviour {
	
	public GameObject PrefabWolf;
	public float SpawnTime = 30f;
	public int maxSpawns = 4;
	int currentSpawn;

	public int CurrentSpawn {
		get {
			return currentSpawn;
		}
		set {
			currentSpawn = value;
			if (currentSpawn < maxSpawns) {
				Invoke("Spawning", SpawnTime);
			}
		}
	}
    
	void Start () {
		for (currentSpawn = 0; currentSpawn < maxSpawns; currentSpawn++) {
            GameObject thisWolf;
            NetworkServer.Spawn(thisWolf = Instantiate(PrefabWolf, transform.position + new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15)), transform.rotation));
            thisWolf.GetComponent<Data>().SpawnPointWolf = this;
		}
	}

	void Spawning() {
        GameObject thisWolf;
        NetworkServer.Spawn(thisWolf = Instantiate(PrefabWolf, transform.position, transform.rotation));
        thisWolf.GetComponent<Data>().SpawnPointWolf = this;
        currentSpawn++; // Не должна прибавляться свойством, иначе будет хрень
	}
}
