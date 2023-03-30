using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using _Scripts.Networking.Core.protocol;
using Networking.Core;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TCPServerSide : MonoBehaviour
{
    [SerializeField] private int _playerCount = 1;
    private List<TcpMessageChannel> _channels;
    private TcpListener _listener;

    private static TCPServerSide instance;
    public static TCPServerSide Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("TCPServerSide is null!");
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _listener = new TcpListener(IPAddress.Any, 55555);
        _listener.Start(50);
        _channels = new List<TcpMessageChannel>();
        Debug.Log("Server started");
    }

    // Update is called once per frame
    void Update()
    {
        ProcessNewClients();
        UpdateExistingClients();
    }

    private void UpdateExistingClients()
    {
        foreach (TcpMessageChannel channel in _channels)
        {
            if (!channel.HasMessage()) continue;
            
            ASerializable message = channel.ReceiveMessage();
            if (message is SimpleMessage)
            {
                HandleSimpleMessage((SimpleMessage) message, channel);
            }
        }
    }

    private void HandleSimpleMessage(SimpleMessage pMessage, TcpMessageChannel pChannel)
    {
        Debug.Log($"Received message: {pMessage.message}");
    }

    private void ProcessNewClients()
    {
        if (_listener.Pending())
        {
            TcpMessageChannel channel = new TcpMessageChannel(_listener.AcceptTcpClient());
            _channels.Add(channel);
            Debug.Log("New client connected");
        }
    }

    public void SendMessage(ASerializable aSerializable, int player)
    {
        _channels[player].SendMessage(aSerializable);
    }
    public void SendMessageToAll(ASerializable aSerializable)
    {
        foreach (TcpMessageChannel channel in _channels) channel.SendMessage(aSerializable);
    }
}
