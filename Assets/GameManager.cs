using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private VRPlayerManager playerManager;
    [SerializeField] Transform vrPlayerSpawn;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameManager is null!");
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance= this;
    }

    public void Start()
    {
        playerManager = VRPlayerManager.Instance;
    }

    public void StartGame()
    {
        playerManager.ui.loadingUI.SetActive(false);
        playerManager.fadeEffect.FadeToTransparent();
        playerManager.transform.position = vrPlayerSpawn.position;
        playerManager.transform.rotation = vrPlayerSpawn.rotation;
        //TODO: Start timer on clock!
    }

    public void WinGame()
    {
        playerManager.OnGameWin();
        NerworkProtocolManager.Instance.WinGameClientRpc();
        Debug.Log("Won the game!");
    }

    public void LoseGame()
    {
        playerManager.OnGameLost();
        NerworkProtocolManager.Instance.LoseGameClientRpc();
        Debug.Log("Lost the game!");
    }
}
