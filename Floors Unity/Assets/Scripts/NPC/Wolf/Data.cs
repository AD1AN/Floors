using UnityEngine;
using UnityEngine.Networking;

public class Data : NetworkBehaviour {

    [SerializeField] Interface Interface;

    public SpawnPointWolf SpawnPointWolf;
    public GameObject WolfDeathEffect;

    // ЗДОРОВЬЕ
    [SyncVar(hook = "onChangeHealth")] public int health = 25;
	[SyncVar] public int healthMax = 25;

	// УРОН
	public int attackDamage = 5;
	public int attackSpeed = 2;

	// УРОВЕНЬ И ОПЫТ
	public int level = 2;
	public int exp = 13;

    void onChangeHealth(int newHealth) {
        if (newHealth <= 0) {
            NetworkServer.Spawn(WolfDeathEffect = Instantiate(WolfDeathEffect, transform.position, transform.rotation));
            SpawnPointWolf.CurrentSpawn--;
            NetworkServer.Destroy(gameObject);
        } else {
            health = newHealth;
            Interface.healthNumbers.text = health + " / " + healthMax;
        }
    }
}