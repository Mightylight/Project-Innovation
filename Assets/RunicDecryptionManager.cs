using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RunicDecryptionManager : NetworkBehaviour
{

    [SerializeField] GameObject clientText;
    [SerializeField] GameObject serverText;



    public void ShowText()
    {
        serverText.SetActive(true);
        ShowTextClientRpc(true);
    }

    public void HideText()
    {
        serverText.SetActive(false);
        ShowTextClientRpc(false);
    }


    [ClientRpc]
    public void ShowTextClientRpc(bool doShow)
    {
        clientText.SetActive(doShow);
    }



}
