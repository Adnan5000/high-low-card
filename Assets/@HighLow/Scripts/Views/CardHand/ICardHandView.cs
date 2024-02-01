using System;
using System.Collections.Generic;
using Arch.Views.Mediation;
using UnityEngine;

namespace HighLow.Scripts.Views.CardHand
{
    public interface ICardHandView: IView
    {
        public Action HandOutCards { get; set; }

        public ushort MaxCount { get; set; }
        public List<Transform> CardPositions { get; set; }
    }
}