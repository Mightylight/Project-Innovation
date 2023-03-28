using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using _Scripts.Networking.Core.protocol;
using Networking.Core;
using UnityEditor;
using UnityEngine;

public class TCPServerSide : MonoBehaviour
{
    private List<TcpMessageChannel> _channels;
    private TcpListener _listener;



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
        if (!_listener.Pending()) return;
        TcpMessageChannel channel = new TcpMessageChannel(_listener.AcceptTcpClient());
        _channels.Add(channel);
        Debug.Log("New client connected");
    }
}
