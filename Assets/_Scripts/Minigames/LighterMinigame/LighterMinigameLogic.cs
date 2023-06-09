using _Scripts.Minigames.WindowCleanMinigame;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Minigames.LighterMinigame
{
    public class LighterMinigameLogic : MonoBehaviour
    {
        [SerializeField] private List<Candle> _candles = new();
        private List<Candle> _litCandles = new();
        
        


        public void OnCandleLit(Candle pCandle)
        {
            if (NetworkManager.Singleton.IsClient) return;
            if (!_candles.Contains(pCandle)) return;
            if (_litCandles.Contains(pCandle)) return;
            
            _litCandles.Add(pCandle);
            //pCandle.LightCandle();

            if (_litCandles.Count == _candles.Count)
            {
                CheckCandles();
            }
        }

        private void CheckCandles()
        {
            if (_candles.Where((pCandle, i) => _litCandles[i] != pCandle).Any())
            {
                ResetCandles();
                return;
            }
            //Catching up to the correct state!
            if (MinigameFSM.Instance.CurrentState is ReadStartPaperMinigameState) MinigameFSM.Instance.NextState();
            if (MinigameFSM.Instance.CurrentState is WindowCleanMinigameState) MinigameFSM.Instance.NextState();
            MinigameFSM.Instance.NextState();
            Debug.Log("Correct!");
        }

        private void ResetCandles()
        {
            foreach (Candle candle in _candles)
            {
                candle.ResetCandle();
            }
            GetComponent<AudioSource>().Play();
            _litCandles.Clear();
        }
    }
}
