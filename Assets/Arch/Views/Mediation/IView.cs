using System;
using UnityEngine;

namespace Arch.Views.Mediation
{
    public interface IView
    {
        GameObject GetGameObject { get; }
        void Remove(Action callback);
    }
}