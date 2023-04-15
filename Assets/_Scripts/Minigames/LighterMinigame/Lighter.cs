using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Minigames.LighterMinigame
{
    public class Lighter : NetworkBehaviour
    {
        public bool _isLit = true;
        NetworkVariable<bool> isLitNetwork = new();

        [SerializeField] float _threshold = 0.1f;
        [SerializeField] GameObject candleFlame;
        [SerializeField] ParticleSystem particlesystem;
        //[SerializeField] Animator animator;


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
            LightVisual(current);
        }

        public void LightVisual(bool pIsLit = true)
        {
            //shiny flamy stuffy
            _isLit = pIsLit;
            isLitNetwork.Value = pIsLit;
            if (pIsLit)
            {
                candleFlame.SetActive(true);
                particlesystem.Play();
            }
            else
            {
                candleFlame.SetActive(false);
                particlesystem.Stop();
            }
        }


        public void Light()
        {
            _isLit = true;
            candleFlame.SetActive(true);
            particlesystem.Play();
            //animator.enabled = true;
            //animator.Play("fireLight");
        }

        public void MatchBoxExit()
        {
            Debug.Log("Hello");
            Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
            if (GetComponent<Rigidbody>().velocity.magnitude > _threshold)
            {
                Light();
            }
        }
    
        public void UnLight()
        {
            if (!_isLit) return;
            _isLit = false;
            candleFlame.SetActive(false);
            particlesystem.Stop();
            //animator.enabled = false;
        }
    }
}
