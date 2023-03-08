using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.SoldierSystem
{
    public abstract class Soldier : MonoBehaviour, ISoldier
    {
        public abstract string Name { get; }
        public abstract Sprite Image { get; }
        public abstract int StartHealth { get; }
        public abstract int CurrentHealth { get; }
        public abstract int AttackPower { get; }
        public abstract bool IsPlaced { get; set; }
        public abstract void Attack();
        public abstract void TakeDamage(int amount);
    }
    public interface ISoldier
    {

    }
}
