using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : NetworkBehaviour {

    void Start () {
        Debug.Log("testing started lol");
        if (SceneManager.GetActiveScene().buildIndex != 0 && !isServer && !isClient) {
            Debug.Log("scene changed");
            SceneManager.LoadScene(0);
        } else if (!isServer) {
            Debug.Log("server started");
            StazNetworkManager.singleton.networkPort = 5300;
            StazNetworkManager.singleton.StartHost();
            StazNetworkManager.tempNick = "Staz";
        }
    }
}
