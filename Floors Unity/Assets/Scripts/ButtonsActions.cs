using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonsActions : MonoBehaviour
{

    [Header("Элементы Меню и прочее")]
    public GameObject StartMenu;
    public GameObject CreateServerMenu;
    public GameObject JoinServerMenu;

    public InputField Nick;

    public Button BtnToCreateServerMenu;
    public Button BtnToJoinServerMenu;
    public Button BtnStartServer;
    public Button BtnStartConnect;
    [Header("Настройки создание сервера")]
    public InputField PortServer;
    public Toggle UseNAT;
    [Header("Настройки подключение к серверу")]
    public InputField IP;
    public InputField PortJoin;

    void Start()
    {
        StazNetworkManager.singleton.networkPort = 5300;
        StazNetworkManager.singleton.networkAddress = "127.0.0.1";
        PortServer.text = "5300";
        PortJoin.text = "5300";
    }

    public void onChangeNick()
    {

        if (Nick.text == "")
        {
            StazNetworkManager.tempNick = "";
            BtnToCreateServerMenu.GetComponent<ChangeTextColorOnButton>().enabled = false;
            BtnToCreateServerMenu.interactable = false;
            BtnToJoinServerMenu.GetComponent<ChangeTextColorOnButton>().enabled = false;
            BtnToJoinServerMenu.interactable = false;
        } else
        {
            StazNetworkManager.tempNick = Nick.text;
            BtnToCreateServerMenu.GetComponent<ChangeTextColorOnButton>().enabled = true;
            BtnToCreateServerMenu.interactable = true;
            BtnToJoinServerMenu.GetComponent<ChangeTextColorOnButton>().enabled = true;
            BtnToJoinServerMenu.interactable = true;
        }
    }

    public void toCreateServerMenu()
    {
        if (Nick.text != "")
        {
            StartMenu.SetActive(false);
            CreateServerMenu.SetActive(true);
        }
    }

    public void onChangePort_Server()
    {
        PortJoin.text = PortServer.text;
        if (PortServer.text == "")
        {
            BtnStartServer.GetComponent<ChangeTextColorOnButton>().enabled = false;
            BtnStartServer.interactable = false;
            BtnStartConnect.GetComponent<ChangeTextColorOnButton>().enabled = false;
            BtnStartConnect.interactable = false;
        } else
        {
            StazNetworkManager.singleton.networkPort = Convert.ToInt32(PortServer.text);
            BtnStartServer.GetComponent<ChangeTextColorOnButton>().enabled = true;
            BtnStartServer.interactable = true;
            BtnStartConnect.GetComponent<ChangeTextColorOnButton>().enabled = true;
            BtnStartConnect.interactable = true;
        }
    }

    public void onChangePort_Join()
    {
        PortServer.text = PortJoin.text;
        if (PortServer.text == "")
        {
            BtnStartServer.GetComponent<ChangeTextColorOnButton>().enabled = false;
            BtnStartServer.interactable = false;
            BtnStartConnect.GetComponent<ChangeTextColorOnButton>().enabled = false;
            BtnStartConnect.interactable = false;
        } else
        {
            StazNetworkManager.singleton.networkPort = Convert.ToInt32(PortJoin.text);
            BtnStartServer.GetComponent<ChangeTextColorOnButton>().enabled = true;
            BtnStartServer.interactable = true;
            BtnStartConnect.GetComponent<ChangeTextColorOnButton>().enabled = true;
            BtnStartConnect.interactable = true;
        }
    }

    public void onChangeIpJoin()
    {
        if (IP.text == "")
        {
            StazNetworkManager.singleton.networkAddress = "127.0.0.1";
        } else
        {
            StazNetworkManager.singleton.networkAddress = IP.text;
        }
    }

    public void toJoinServerMenu()
    {
        if (Nick.text != "")
        {
            StartMenu.SetActive(false);
            JoinServerMenu.SetActive(true);
        }
    }

    public void toMainMenu()
    {
        CreateServerMenu.SetActive(false);
        JoinServerMenu.SetActive(false);
        StartMenu.SetActive(true);
    }

    public void StartHosting()
    {
        StazNetworkManager.singleton.StartHost();
    }

    public void StartJoining()
    {
        StazNetworkManager.singleton.StartClient();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
