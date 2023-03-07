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
        public abstract Vector2 Size { get; }
        public abstract GameObject[] Blocks { get; }
        protected abstract bool CanFollow { get; set; }
        
        private void Update()
        {
            if (CanFollow)
            {
                Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos.z = 0; 
                transform.position = targetPos;
            }
        }
        public void StartHolding()
        {
            CanFollow = true;
        }
    }
    public interface IBuilding
    {
        public void StartHolding();

    }
}
