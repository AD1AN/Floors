using UnityEngine;

public class RotateInterface : MonoBehaviour {

    [SerializeField] DataPlayer DataPlayer;
    
    float rotX;
    
    void Update () {
        //Debug.Log(transform.eulerAngles.x);
        rotX = transform.localEulerAngles.x;

        //rotX = Mathf.Clamp(rotX, -0.3f, 0.2f);
        //DataPlayer.DataInterfacePlayer.transform.localEulerAngles = new Vector3(rotX, DataPlayer.DataInterfacePlayer.transform.localEulerAngles.y);
    }
}
