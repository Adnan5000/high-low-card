using System;
using Arch.Views.Mediation;

namespace HighLow.Scripts.Views.ResultPanels.WinPanel
{
    public interface IWinPanelView: IView
    {
        public Action PlayAgainButtonClicked { get; set; }
    }
}