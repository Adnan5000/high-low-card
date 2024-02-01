﻿using Arch.Views.Mediation;
using HighLow.Scripts.Views.CardHand;
using Zenject;

namespace HighLow.Scripts.Views.Installers
{
    public class CardHandInstaller : Installer<CardHandInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindViewToMediator<CardHandView, CardHandMediator>();
        }
    }
}