using UnityEngine;

namespace _Scripts.Minigames.LighterMinigame
{
    public class Lighter : MonoBehaviour
    {
        public bool _isLit = true;
        [SerializeField] float _threshold = 0.1f;
        [SerializeField] GameObject candleFlame;


        private void Light()
        {
            candleFlame.SetActive(true);
            GetComponentInChildren<ParticleSystem>().Play();
            GetComponentInChildren<Animator>().enabled = true;
            GetComponentInChildren<Animator>().Play("fireLight");
        }

        public void MatchBoxExit()
        {
            Debug.Log("Hello");
            if (GetComponent<Rigidbody>().velocity.magnitude > _threshold)
            {
                Light();
            }
        }
    
        public void UnLight()
        {
            candleFlame.SetActive(false);
            GetComponentInChildren<ParticleSystem>().Stop();
            GetComponentInChildren<Animator>().enabled = false;
        }
    }
}
