using System.Collections.Generic;
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

    [System.Serializable]
    public class Deck
    {
        public List<GameObject> cards;
    }
}
