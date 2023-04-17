using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CorrectPlateManager : MonoBehaviour
{
    [SerializeField] Material glowMat;
    [SerializeField] Renderer render;

    private void Start()
    {
        if(NetworkManager.Singleton != null)
            if(NetworkManager.Singleton.IsClient) render.GetComponent<Renderer>().material = glowMat;
    }
}
