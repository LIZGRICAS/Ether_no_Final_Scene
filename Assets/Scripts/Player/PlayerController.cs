using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        
        [Header("Propiedades")]
        public string name;

        public int vidas;
        public int mana;
        
        #region vida

        public void QuitarMana(int cuantoManaQuita)
        {
            mana -= cuantoManaQuita;
        }

        public void RecibirMana(int cuantoManaTiene)
        {
            mana += cuantoManaTiene;
        }

        public int InfoVida()
        {
            return vidas;
        }
        
        public int InfoMana()
        {
            return mana;
        }
        #endregion
        
        

    }
}

