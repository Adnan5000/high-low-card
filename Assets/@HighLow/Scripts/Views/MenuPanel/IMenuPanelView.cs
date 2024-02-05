using System;
using Arch.Views.Mediation;

namespace HighLow.Scripts.Views.MenuPanel
{
    public interface IMenuPanelView: IView
    {
        public Action PlayButtonClicked { get; set; }
        public Action ResetButtonClicked { get; set; }
    }
}