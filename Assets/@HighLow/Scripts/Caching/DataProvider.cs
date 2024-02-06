using System.Collections.Generic;
using HighLow.Scripts.Common;
using HighLow.Scripts.Views.Card;
using UnityEngine;

namespace HighLow.Scripts.Caching
{
    public class DataProvider : MonoSingleton<DataProvider>
    {
        [SerializeField] private bool showCardsForDebug;
        [SerializeField] private Deck _deck;
        [SerializeField] private float _choiceTime = 5f;
        public Deck deck => _deck;
        public bool ShowCardsForDebug => showCardsForDebug;
        public float ChoiceTime => _choiceTime;
        private void Awake()
        {
            InitializeSingleton();
            DontDestroyOnLoad(gameObject);
        }
    }

    [System.Serializable]
    public class Deck
    {
        public int counter = 0;
        public List<GameObject> cards;
        public CardView GetCard()
        {
            CardView card = cards[counter].GetComponent<CardView>();
            counter++;
            return card;
        }
        public void ShuffleDeck()
        {
            counter = 0;
            
            System.Random rng = new System.Random();
            ushort n = (ushort)cards.Count;
            while (n > 1)
            {
                n--;
                ushort k = (ushort)rng.Next(n + 1);
                (cards[k], cards[n]) = (cards[n], cards[k]);
            }
        }
    }
}
