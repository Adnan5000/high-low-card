using HighLow.Scripts.Common;
using UnityEngine;

namespace HighLow.Scripts.Caching
{
    public class DataProvider : MonoSingleton<DataProvider>
    {
        [SerializeField] private Deck _deck;


        private void Awake()
        {
            InitializeSingleton();
            DontDestroyOnLoad(gameObject);
        }
        
        
    }

    internal class Deck
    {
        
    }
}
