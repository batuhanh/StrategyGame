using StrategyGame.Core.Managers;
using StrategyGame.MVC;
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
                Vector3 targetPos = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePostition);
                targetPos.z = 0;
                Vector3 diff = GameGrid.Instance.GameGridController.CheckBuildingPosSnapable(this, targetPos - transform.position);
                if (diff != Vector3.zero)
                {
                    Debug.Log("Snappable");
                    transform.position = targetPos + diff;
                }

            }
        }
        public void StartHolding()
        {
            CanFollow = true;
        }
        public void StopHolding()
        {
            CanFollow = false;
        }
    }
    public interface IBuilding
    {
        public void StartHolding();
        public void StopHolding();

    }
}
