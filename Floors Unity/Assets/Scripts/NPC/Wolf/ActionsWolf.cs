using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ActionsWolf : NetworkBehaviour {

	[SerializeField] Data Data;
    [SerializeField] Interface Interface;

	public CharacterController controller;
	float stepSpeed = 4f; // Скорость ходьбы
	float runSpeed = 7f; // Скорость бега

	// ТОЧКА ВОСКРЕШЕНИЯ
	public GameObject WolfSpawnPoint;

    // СОСТОЯНИЕ
    public string state = "wander"; // wander - гуляет, может агриться если заметит || attack - атакует цель, преследует её || dyspnea - уставший, ходит пару секунд и не может агриться за это время

    // WANDER
    float directionChangeInterval = 1; // Через сколько новое вращение. Каждый раз задается рандомное число
	float stayInterval; // Сколько секунд стоит. Каждый раз задается рандомное число
    float moveMaxTime; // Сколько максимум секунд ходит. Каждый раз задается рандомное число
    float moveTime = 0; // Сколько уже ходит.
	bool startStay; // Остановиться
	float maxHeadingChange = 150; // На склолько градусов поворачивается
    float heading; // Поворот
	Quaternion newRotation; // Нововый поворот

	public float radiusFieldsOfView = 13; // Дальность агра перед ним
    public float attackDistance = 2; // Дальность атаки

    // ATTACK
    GameObject target; // Цель - игрок за которым он бежит
    bool attacked = false; // Атака
    float timeChaseMax = 5; // Как долго бежит за игроком
    float timeChase; // Сколько секунд бежит
    float timeChaseReactionMax = 0.35f; // Если игрок отошел от волка, через сколько будет он преследовать его
    float timeChaseReaction; // Сколько уже стоит

    // DYSPNEA
    float timeDyspneaMax = 3; // Сколько максимум секунд уставший
	float timeDyspnea; // Сколько уже уставший

	void Start() {
		Interface.healthNumbers.text = Data.health + " / " + Data.healthMax;
		heading = Random.Range(0, 360);
		moveMaxTime = Random.Range(0, 3);
		transform.eulerAngles = new Vector3(0, heading, 0);
		StartCoroutine(NewHeading());
	}
	
	void Update() {
		if (isServer) {
			if (state == "wander" || state == "dyspnea") {
				if (state != "dyspnea") {
					RaycastHit hit;
					//Debug.DrawRay(transform.position + Vector3.up, transform.forward * radiusFieldsOfView);
					//Debug.DrawRay(transform.position + Vector3.up, (transform.forward - transform.right).normalized * radiusFieldsOfView);
					//Debug.DrawRay(transform.position + Vector3.up, (transform.forward + transform.right).normalized * radiusFieldsOfView);
					if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, radiusFieldsOfView)) {
						if (hit.collider.transform.root.gameObject.tag == "Player") {
							state = "attack";
							timeChase = timeChaseMax;
							target = hit.collider.transform.root.gameObject;
						}
					}
					if (Physics.Raycast(transform.position + Vector3.up, (transform.forward - transform.right).normalized, out hit, radiusFieldsOfView)) {
                        if (hit.collider.transform.root.gameObject.tag == "Player") {
                            state = "attack";
                            timeChase = timeChaseMax;
                            target = hit.collider.transform.root.gameObject;
                        }
                    }
					if (Physics.Raycast(transform.position + Vector3.up, (transform.forward + transform.right).normalized, out hit, radiusFieldsOfView)) {
                        if (hit.collider.transform.root.gameObject.tag == "Player") {
                            state = "attack";
                            timeChase = timeChaseMax;
                            target = hit.collider.transform.root.gameObject;
                        }
                    }
				} else {
					timeDyspnea -= Time.deltaTime;
					if (timeDyspnea <= 0) {
						state = "wander";
						timeChase = timeChaseMax;
					}
				}
				transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * directionChangeInterval);
				if (moveTime <= moveMaxTime) {
					controller.SimpleMove(transform.TransformDirection(Vector3.forward) * stepSpeed);
					moveTime += Time.deltaTime;
					startStay = true;
				} else if (moveTime > moveMaxTime && startStay) {
					stayInterval = Random.Range(7, 13);
					startStay = false;
					Invoke("StartMove", stayInterval);
				}
			} else if (state == "attack") {
                transform.LookAt(target.transform);
                RaycastHit hit;
                //Debug.DrawRay(transform.position + Vector3.up, transform.forward * attackDistance, Color.red);
                if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, attackDistance)) {
                    if (hit.collider.transform.root.gameObject.tag == "Player" && hit.collider.transform.root.gameObject == target) {
                        timeChaseReaction = 0;
                        timeChase = timeChaseMax;
                        if (!attacked) {
                            attacked = true;
                            target.GetComponent<DataPlayer>().health -= Data.attackDamage;
                            Invoke("ReloadAttack", Data.attackSpeed);
                        }
                    }
                } else {
                    if (timeChaseReaction < timeChaseReactionMax) { // Если не будет задержки то он будет много дергаться если убегает противник (выглядит ужасно :/ лучше так)
                        timeChaseReaction += Time.deltaTime;
                    } else {
                        controller.SimpleMove(transform.TransformDirection(Vector3.forward) * runSpeed);
                        timeChase -= Time.deltaTime;
                        if (timeChase <= 0) {
                            timeDyspnea = timeDyspneaMax;
                            state = "dyspnea";
                        }
                    }
                }
            }
		}
	}

	void ReloadAttack() {
		attacked = false;
	}

	void StartMove() {
		moveMaxTime = Random.Range(3, 5);
		moveTime = 0;
	}

	IEnumerator NewHeading() {
		while (true) {
			NewHeadingRoutine();
			directionChangeInterval = Random.Range(1, 6);
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}

	void NewHeadingRoutine() {
		var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
		var ceil  = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
		heading = Random.Range(floor, ceil);
		newRotation = Quaternion.Euler(new Vector3(0, heading, 0));
	}
}
