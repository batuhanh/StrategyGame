using StrategyGame.MVC.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.BuildingSystem
{

    public abstract class Building : MonoBehaviour, IBuilding
    {
        public abstract string Name { get; }
        public abstract Sprite Image { get; }
        public abstract int StartHealth { get; }
        public abstract int CurrentHealth { get; }
        public abstract bool IsPlaced { get; }
        public abstract Vector2 GridPosition { get; }
    }
    public interface IBuilding // Just for marking
    {

    }
}
