using StrategyGame.Core.Managers;
using StrategyGame.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace StrategyGame.Core.Gameplay.SoldierSystem
{
    public abstract class Soldier : MonoBehaviour, ISoldier, IShowable, IAttackable, IDamageable
    {
        public abstract string Name { get; }
        public abstract Sprite Image { get; }
        public abstract int StartHealth { get; }
        public abstract int CurrentHealth { get; }
        public abstract int AttackPower { get; }
        public abstract bool IsPlaced { get; set; }
        public abstract bool CanMove { get; set; }
        public abstract SoldierState State { get; set; }
        public abstract void Attack(IDamageable target);
        public abstract void TakeDamage(int amount);
        public abstract void Move(Vector3 targetPos);
        public abstract void DecideMovement(Vector3 target);
        public abstract void DecideMovement(IDamageable target);
        public abstract void CallInformationPanel();
        public Vector3 GetObjectPos()
        {
            return transform.position;
        }
      
    }
    public interface ISoldier
    {

    }
    public enum SoldierState
    {
        Idle,
        Moving,
        Attacking,
        Dead
    }
}
