using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataPlayer : NetworkBehaviour { // ХРАНИТ ВСЕ ДАННЫЕ ОБ ИГРОКЕ. РАБОТАЕТ У ВСЕХ КЛИЕНТОВ.

    [SerializeField] PostProcessing PostProcessing;
    public DataInterfacePlayer DataInterfacePlayer;

    //СЕТЬ
    [SyncVar] public int playerId;
    [SyncVar] public string playerNick;

    // ЗДОРОВЬЕ
    [SyncVar(hook = "onChangeHealth")] public int health = 100;
    [SyncVar] public int healthMax = 100;

    // УРОВЕНЬ И ОПЫТ
    [SyncVar(hook = "onChangeLevel")] public int level = 1;
    [SyncVar(hook = "onChangeGlobalExp")] public int globalExp = 0;
    [SyncVar] public int exp = 0;
    [SyncVar] public int expMax = 100;

    // УРОН
    [SyncVar] public int attackDamage = 8;

    // РЕГЕНЕРАЦИЯ ВНЕ БОЯ
    [SyncVar] public int healthReg = 3;

    // РЕГЕНЕРАЦИЯ В БОЮ
    [SyncVar] public int healthBattleReg = 0;

    // ВЫНОСЛИВОСТЬ
    [SyncVar] public float stamina = 20f;
    [SyncVar] public float staminaMax = 20f;

    void onChangeHealth(int newHealth) {
        health = newHealth;
        if (health <= 0) {
            Destroy(DataInterfacePlayer.gameObject);
            Destroy(gameObject);
        }
        DataInterfacePlayer.Health.text = health + " / " + healthMax;
        if (isLocalPlayer) {
            PostProcessing.Damaged();
        }
    }

    void onChangeGlobalExp(int newGlobalExp) {
        int newExp = exp + (newGlobalExp - globalExp);
        globalExp = newGlobalExp;
        exp = newExp;
        if (isServer && exp >= expMax) {
            exp = exp - expMax;
            level++; // Не срабатывает хук когда вызывается в другом хуке(- у хоста), поэтому внизу я принудительно вызвал хук
            onChangeLevel(level);
        }
    }

    void onChangeLevel(int newLevel) {
        level = newLevel;
        DataInterfacePlayer.Level.text = "Level: " + level;
    }
}