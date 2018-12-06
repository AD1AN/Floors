using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine.Networking;

public class ActionsPlayer : NetworkBehaviour {

	[SerializeField] DataPlayer DataPlayer;

	[SerializeField] GameObject playerCamera; // Голова персонажа
	[SerializeField] CharacterController controller; // Контроллер

	float currentSpeed; // Текущая скорость. Переменная, зависит от нажатия шифта
	float stepSpeed = 4f; // Скорость ходьбы
	float shiftSpeed = 8f; // Скорость бега
	float verticalSpeed = 0f; // Вертикальная скорость
	float jumpSpeed = 10f; // Прыжок
	float gravity = 25f; // Гравитация
	
	bool dyspnea;

	float sensitivity = 2f; // Скорость мыши

	float rotX; // Вращение камеры и тела по горизонтали
	float rotY; // Вращение камеры по вертикали

	// Переменный для рывка
	public float maxDashTime = 2f;
	public float dashSpeed = 150f;
	float currentDashSpeed;
	float currentDashTime;

	void Update () {
		if (Input.GetKeyDown(KeyCode.BackQuote) && Cursor.lockState == CursorLockMode.Locked) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else if (Input.GetKeyDown(KeyCode.BackQuote) && Cursor.lockState == CursorLockMode.None) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 2);
		if (Input.GetButtonDown("Fire1")) {
			RaycastHit hit;
            GameObject target;
			if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 2)) {
				if (hit.collider.transform.root.tag == "NPC_Enemy") {
                    target = hit.collider.transform.root.gameObject;
					if (DataPlayer.attackDamage >= target.GetComponent<Data>().health) {
                        CmdGiveExp(target, gameObject);
					}
                    CmdDamage(target, DataPlayer.attackDamage);
				}
			}
		}

		if (controller.isGrounded) {
			verticalSpeed = -1f; // Без этого, персонаж быстро падает на землю когда сходит(не прыгает) с платформы/высоты
			if (Input.GetAxis("LeftShift") != 0 && dyspnea == false && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)) {
				if (Input.GetButtonDown("Jump")) {
					verticalSpeed = jumpSpeed;
					CmdDecreaseStamina(1.2f);
				}
				currentSpeed = Input.GetAxis("LeftShift") * shiftSpeed;
				if (DataPlayer.stamina > 0f) {
					CmdDecreaseStamina(0.02f);
				} else {
					CmdSetStamina(0f);
					dyspnea = true;
				}
			} else {
				if (Input.GetButtonDown("Jump") && DataPlayer.stamina > 0.4f) {
					verticalSpeed = jumpSpeed;
					CmdDecreaseStamina(0.4f);
				}
				currentSpeed = stepSpeed;
				if (DataPlayer.stamina < 6) {
					dyspnea = true;
				} else {
					dyspnea = false;
				}
				if (DataPlayer.stamina < DataPlayer.staminaMax) {
					CmdIncreaseStamina(0.02f);
				}
			}
			if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 && DataPlayer.stamina < DataPlayer.staminaMax) {
				CmdIncreaseStamina(0.04f);
			}
		}
		if (Input.GetKeyDown(KeyCode.Tab)) {
			currentDashTime = 0.0f;
		}
		if (currentDashTime < maxDashTime) {
			currentDashSpeed = dashSpeed;
			currentDashTime += Time.deltaTime;
		} else {
			currentDashSpeed = 0.0f;
		}
		Vector3 moveDir = Vector3.zero;
		if (Cursor.lockState == CursorLockMode.Locked) {
			moveDir = new Vector3(Input.GetAxis("Horizontal") * currentSpeed, 0, Input.GetAxis("Vertical") * currentSpeed + currentDashSpeed);

			rotX = Input.GetAxis("Mouse X") * sensitivity;

			transform.Rotate(0, rotX, 0);

			rotY -= Input.GetAxis("Mouse Y") * sensitivity;

			rotY = Mathf.Clamp(rotY, -85, 80); // Не позволяем смотреть слишком высоко и слишком низко

			playerCamera.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
		}

		verticalSpeed -= gravity * Time.deltaTime;
		moveDir.y = verticalSpeed;

		moveDir = transform.rotation * moveDir;
		controller.Move(moveDir * Time.deltaTime);
	}

	[Command]
	void CmdSetStamina(float st) {
		DataPlayer.stamina = st;
	}
	[Command]
	void CmdIncreaseStamina(float st) {
		DataPlayer.stamina += st;
	}
	[Command]
	void CmdDecreaseStamina(float st) {
		DataPlayer.stamina -= st;
	}
    [Command]
	void CmdDamage(GameObject target, int damage) {
        target.GetComponent<Data>().health -= damage;
    }
    [Command]
    void CmdGiveExp(GameObject target, GameObject me) {
        me.GetComponent<DataPlayer>().globalExp += target.GetComponent<Data>().exp;
    }
}