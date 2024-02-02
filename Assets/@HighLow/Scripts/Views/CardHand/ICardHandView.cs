using System;
using System.Collections.Generic;
using Arch.Views.Mediation;
using HighLow.Scripts.Views.Card;
using UnityEngine;

namespace HighLow.Scripts.Views.CardHand
{
    public interface ICardHandView: IView
    {
        public Action HandOutCards { get; set; }
        public ushort MaxCount { get; }
        public List<Transform> CardPositions { get; }
        public List<CardView> CardViews { get; }
    }
}