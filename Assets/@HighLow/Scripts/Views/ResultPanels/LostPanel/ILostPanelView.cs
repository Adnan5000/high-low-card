using System;
using Arch.Views.Mediation;

namespace HighLow.Scripts.Views.ResultPanels.LostPanel
{
    public interface ILostPanelView: IView
    {
        public Action PlayAgainButtonClicked { get; set; }
    }
}