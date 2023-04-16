using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Netcode;
using UnityEngine;

public class VRPlayerManager : MonoBehaviour
{
    public UIFollowCamera ui;
    public FadeEffect fadeEffect;

    [SerializeField] List<GameObject> controllerRays;

    bool isWaitingForClient = false;

    private static VRPlayerManager instance;
    public static VRPlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("VRPlayerManager is null!");
                return null;
            }
            return instance;
        }
    }


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    public void LoadLevelScene()
    {
        isWaitingForClient = true;
        ui.mainMenuUI.SetActive(false);
        ui.loadingUI.SetActive(true);
        ui.SetLoadingText("Loading...");
        fadeEffect.FadeToBlack(true);
    }

    public void OnVRSceneLoaded()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        string text = $"Waiting for client to connect on:\n {GetIPv4Address()}";
        ui.SetLoadingText(text);
        ToggleRays(false);
    }

    public void OnClientConnected(ulong client)
    {
        if (!isWaitingForClient) return;
        isWaitingForClient= false;
        fadeEffect.FadeToTransparent();
        ui.gameObject.SetActive(false);
        GameManager.Instance.StartGame();
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
    }

    string GetIPv4Address()
    {
        // Get the hostname of the local machine
        string hostName = Dns.GetHostName();

        // Get the IP addresses associated with the hostname
        IPAddress[] addresses = Dns.GetHostAddresses(hostName);

        // Find the first IPv4 address in the list
        foreach (IPAddress address in addresses)
        {
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
               return address.ToString();
            }
        }

        // If no IPv4 address found, return an empty string
        return string.Empty;
    }


    public void ToggleRays(bool toggle)
    {
        foreach(GameObject obj in controllerRays)
        {
            obj.SetActive(toggle);
        }
    }

    public void OnGameWin()
    {
        fadeEffect.FadeToBlackAndWin();
    }
    public void OnGameLost()
    {
        fadeEffect.FadeToBlackAndLose();
    }

}
