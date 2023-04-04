using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Minigames.LighterMinigame
{
    public class Candle : NetworkBehaviour
    {
        
        public bool _isLit;

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
        }

        [ClientRpc]
        public void LightCandleClientRpc(bool pIsLit = true)
        {
            LightCandleVisual(pIsLit);
        }
        
    }
}
