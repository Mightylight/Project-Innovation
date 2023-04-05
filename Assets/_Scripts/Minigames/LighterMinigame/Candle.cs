using Unity.Netcode;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace _Scripts.Minigames.LighterMinigame
{
    public class Candle : NetworkBehaviour
    {
        public bool _isLit;

        private Animator animator;
        private ParticleSystem particaleSystem;
        private Light light;


        public void Awake()
        {
            particaleSystem= GetComponentInChildren<ParticleSystem>();
            animator = GetComponentInChildren<Animator>();
            light = GetComponentInChildren<Light>();
            ResetCandle();
            
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
            if (pIsLit)
            {
                particaleSystem.Play();
                animator.enabled = true;
                animator.Play("fireLight");
                light.enabled = true;
            }
            else
            {
                particaleSystem.Stop();
                animator.enabled = false;
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

            if (NetworkManager.Singleton.IsClient) return;
            if (MinigameFSM.Instance.CurrentState is LighterMinigameState state)
            {
                state.GetComponent<LighterMinigameLogic>().OnCandleLit(this);
            }
        }

    }
}
