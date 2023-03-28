using System.Collections;
using System.Collections.Generic;
using _Scripts.Networking.Core.protocol;
using Networking.Core;
using UnityEngine;


public class TCPClientSide : MonoBehaviour
{

    [SerializeField] private TcpMessageChannel _channel;
    [SerializeField] private string _IPAdress;
    [SerializeField] private int _serverPort;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _channel = new TcpMessageChannel();
        _channel.Connect(_IPAdress, _serverPort);
        if (_channel.Connected)
        {
            SimpleMessage message = new SimpleMessage();
            message.message = "Hello from client";
            _channel.SendMessage(message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
