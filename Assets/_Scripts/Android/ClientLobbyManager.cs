using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClientLobbyManager : MonoBehaviour
{
    [SerializeField] string defaultIP = "192.168.1.100";
    [SerializeField] ushort defaultPort = 7777;

    [SerializeField] TMP_InputField ipInputField;
    [SerializeField] TextMeshProUGUI userFeedbackTextobj;
    [SerializeField] Button joinButton;

    [SerializeField] TextMeshProUGUI joiningText;

    public void Start()
    {
        ipInputField.text = defaultIP;
        NetworkManager.Singleton.OnClientConnectedCallback += OnServerJoined;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    public void JoinServer()
    {
        CanvasManager.Instance.GetComponent<MobileCanvasFSM>().LoadState(MobileState.JOINING);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
        ipInputField.text,  // The IP address is a string
        (ushort)defaultPort // The port number is an unsigned short
        );
        joiningText.text = $"TRYING TO JOIN SERVER ON\n{ipInputField.text}";
        NetworkManager.Singleton.StartClient();
        //joinButton.interactable = false;
        //userFeedbackTextobj.text = "Trying to join...";
    }

    public void StopJoin()
    {
        NetworkManager.Singleton.Shutdown();
        CanvasManager.Instance.GetComponent<MobileCanvasFSM>().LoadState(MobileState.MAIN_MENU);
    }

    public void OnServerJoined(ulong clientID)
    {
        CanvasManager.Instance.PhoneConsoleMessage($" Connected as client with id {clientID}!");
        CanvasManager.Instance.GetComponent<MobileCanvasFSM>().LoadState(MobileState.PLAYING);
    }

    public void OnClientDisconnected(ulong clientID)
    {
        CanvasManager.Instance.PhoneConsoleMessage($"Client with id {clientID} disconnected");
        CanvasManager.Instance.GetComponent<MobileCanvasFSM>().LoadState(MobileState.DISCONNECTED);
    }



}
