using System;
using Arch.Views.Mediation;

namespace HighLow.Scripts.Views.GameplayPanel
{
    public interface IGameplayPanel: IView
    {
        public Action HighButtonClicked { get; set; }
        public Action LowButtonClicked { get; set; }
        public Action EqualButtonClicked { get; set; }
    }
}