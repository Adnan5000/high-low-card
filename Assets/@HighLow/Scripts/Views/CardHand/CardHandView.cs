using System;
using System.Collections.Generic;
using Arch.Views.Mediation;
using UnityEngine;

namespace HighLow.Scripts.Views.CardHand
{
    public class CardHandView: View, ICardHandView
    {
        public Action HandOutCards { get; set; }
        public ushort MaxCount { get; set; }
        public List<Transform> CardPositions { get; set; }

        private void Start()
        {
            HandOutCards?.Invoke();
        }
    }
}