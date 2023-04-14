using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class PhonographManager : NetworkBehaviour
{
    [SerializeField] List<VynilPlate> vinylPlates;
    private Dictionary<VynilPlateName, VynilPlate> vinylPlatesDic = new();
    [SerializeField] XRLockSocketInteractor socket;
    AudioSource audioSource;


    [SerializeField] GameObject keyPrefab;
    [SerializeField] Transform shootFrom;
    [SerializeField] float shootStrenght = 5;
    [SerializeField] private AudioSource _audioSource;
    bool keyShot = false;

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

    public void ShootKey()
    {
        if (keyShot) return;
        MinigameFSM miniFSM = MinigameFSM.Instance;
        miniFSM.NextState();
        GameObject key = Instantiate(keyPrefab);
        miniFSM.CurrentState.hintObjects.Add(key);
        miniFSM.CurrentState.hintObjects.Add(this.gameObject);
        key.transform.position = shootFrom.position;
        key.transform.rotation = shootFrom.rotation;
        key.GetComponent<Rigidbody>().velocity = key.transform.forward * shootStrenght;
        key.GetComponent<NetworkObject>().Spawn();
        keyShot = true;
        if(_audioSource != null) _audioSource.Play();
    }

    public void OnSongEntered()
    {
        VynilPlateName plateName = socket.GetOldestInteractableSelected().transform.GetComponent<VinylPlateManager>().plateName;
        int plateNameInt = (int)plateName;
        PlaySong(plateNameInt);
        PlayerSongClientRpc(plateNameInt);
        if(plateName == VynilPlateName.THE_CORRECT_SONG) ShootKey();

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
