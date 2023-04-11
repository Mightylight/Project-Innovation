using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class PhonographManager : NetworkBehaviour
{
    [SerializeField] List<VynilPlate> vinylPlates;
    private Dictionary<VynilPlateName, VynilPlate> vinylPlatesDic = new();
    [SerializeField] XRLockSocketInteractor socket;
    AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (VynilPlate plate in vinylPlates)
        {
            vinylPlatesDic.Add(plate.vinylPlateName,plate);
        }
        if (NetworkManager.Singleton == null) return;
        if (NetworkManager.Singleton.IsClient) Destroy(socket);
    }

    [ClientRpc]
    public void PlayerSongClientRpc(int platenameInt)
    {
        PlaySong(platenameInt);
    }
    [ClientRpc]
    public void StopSongClientRpc()
    {
        StopSong();
    }

    public void OnSongEntered()
    {
        VynilPlateName plateName = socket.GetOldestInteractableSelected().transform.GetComponent<VinylPlateManager>().plateName;
        int plateNameInt = (int)plateName;
        PlaySong(plateNameInt);
        PlayerSongClientRpc(plateNameInt);
    }
    
    public void OnSongLeaving()
    {
        StopSong();
        StopSongClientRpc();
    }


    public void PlaySong(int platenameInt)
    {
        audioSource.clip = vinylPlatesDic[(VynilPlateName)platenameInt].music;
        audioSource.Play();
    }

    public void StopSong()
    {
        audioSource.Stop();
    }



}
