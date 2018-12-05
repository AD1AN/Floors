using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataInterfacePlayer : NetworkBehaviour { // ХРАНИТ ВСЕ НУЖНЫЕ ДАННЫЕ И КОМПОНЕНТЫ ИНТЕРФЕЙСА.

	public DataPlayer DataPlayer;
	[SyncVar] public int playerId;

	public Canvas InterfaceCanvas;
	public GameObject MainMenu;
	public Animator MainMenuAnim;

	public Slider StaminaBar;
	public Slider HealthBar;
	public Slider ExpBar;

	public Text Health;
	public Text Nick;
	public Text Level;
}
