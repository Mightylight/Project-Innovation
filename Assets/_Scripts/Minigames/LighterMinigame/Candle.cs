using Unity.Netcode;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace _Scripts.Minigames.LighterMinigame
{
    public class Candle : NetworkBehaviour
    {
        public bool _isLit;
        NetworkVariable<bool> isLitNetwork = new();

       // private Animator animator;
        private ParticleSystem particaleSystem;
        private Light light;


        public void Awake()
        {
            particaleSystem = GetComponentInChildren<ParticleSystem>();
            //animator = GetComponentInChildren<Animator>();
            light = GetComponentInChildren<Light>();
        }
        private void Start()
        {
            if (!_isLit) ResetCandle();
            else LightCandle();
            isLitNetwork.Value = _isLit;
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                isLitNetwork.Value = _isLit;
            }
            else
            {
                if (isLitNetwork.Value != _isLit)
                {
                    Debug.LogWarning($"NetworkVariable was {isLitNetwork.Value} upon being spawned" +
                        $" when it should have been {_isLit}");
                }
                else
                {
                    Debug.Log($"NetworkVariable is {isLitNetwork.Value} when spawned.");
                }
                isLitNetwork.OnValueChanged += ValueChanged;
            }
        }

        void ValueChanged(bool prev, bool current)
        {
            LightCandleVisual(current);
        }

        public void ResetCandle()
        {
            // Reset candle
            LightCandleClientRpc(false);
            LightCandleVisual(false);
        }
        
        public void LightCandle()
        {
            LightCandleClientRpc();
            LightCandleVisual();
        }

        public void LightCandleVisual(bool pIsLit = true)
        {
            //shiny flamy stuffy
            Debug.Log("Candle Lit");
            _isLit = pIsLit;
            isLitNetwork.Value = pIsLit;
            if (pIsLit)
            {
                particaleSystem.Play();
               // animator.enabled = true;
                //animator.Play("fireLight");
                light.enabled = true;
            }
            else
            {
                particaleSystem.Stop();
                //animator.enabled = false;
                light.enabled = false;
            }
        }

        [ClientRpc]
        public void LightCandleClientRpc(bool pIsLit = true)
        {
            LightCandleVisual(pIsLit);
        }

        public void OnTrigger()
        {
            LightCandle();
            if (NetworkManager.Singleton.IsClient) return;
            MinigameFSM.Instance.GetComponentInChildren<LighterMinigameLogic>().OnCandleLit(this);
        }
    }
}
