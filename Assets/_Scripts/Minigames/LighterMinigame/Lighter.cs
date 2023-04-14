using UnityEngine;

namespace _Scripts.Minigames.LighterMinigame
{
    public class Lighter : MonoBehaviour
    {
        public bool _isLit = true;
        [SerializeField] float _threshold = 0.1f;
        [SerializeField] GameObject candleFlame;
        [SerializeField] ParticleSystem particlesystem;
        //[SerializeField] Animator animator;


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
