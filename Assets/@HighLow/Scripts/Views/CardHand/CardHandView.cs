using System;
using System.Collections.Generic;
using Arch.Views.Mediation;
using HighLow.Scripts.Views.Card;
using Unity.VisualScripting;
using UnityEngine;

namespace HighLow.Scripts.Views.CardHand
{
    [System.Serializable]
    public class CardHandView: View, ICardHandView
    {
        [SerializeField] private bool showCardsForDebug;
        public bool ShowCardsForDebug => showCardsForDebug;
        public Action HandOutCards { get; set; }
        
        [SerializeField] private ushort maxCount;
        [SerializeField] private List<Transform> cardPositions;
        [SerializeField] private List<CardView> cardViews;
        
        public ushort MaxCount => maxCount;
        public List<Transform> CardPositions => cardPositions;
        public List<CardView> CardViews => cardViews;

        protected void Start()
        {
            HandOutCards?.Invoke();
        }
        
    }
}