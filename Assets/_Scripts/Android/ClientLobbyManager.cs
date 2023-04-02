using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ClientLobbyManager : MonoBehaviour
{
    [SerializeField] string defaultIP = "192.168.1.100";
    [SerializeField] ushort defaultPort = 7777;

    [SerializeField] TMP_InputField ipInputField;
    [SerializeField] TextMeshProUGUI userFeedbackTextobj;
    [SerializeField] Button joinButton;

    public void Start()
    {
        ipInputField.text = defaultIP;
        NetworkManager.Singleton.OnClientConnectedCallback += OnServerJoined;
    }

    public void JoinServer()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
        ipInputField.text,  // The IP address is a string
        (ushort)defaultPort // The port number is an unsigned short
        );
        NetworkManager.Singleton.StartClient();
        joinButton.interactable = false;
        userFeedbackTextobj.text = "Trying to join...";
    }

    public void OnServerJoined(ulong clientID)
    {
        Debug.Log("Succesfully connected with the server!");
        this.gameObject.SetActive(false);
    }
}
