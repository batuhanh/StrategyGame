using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.SoldierSystem
{
    public class Captain : Soldier
    {
        public override string Name { get { return _name; } }
        public override Sprite Image { get { return _image; } }
        public override int StartHealth { get { return _startHealth; } }
        public override int CurrentHealth { get { return _currentHealth; } }
        public override int AttackPower { get { return _attackPower; } }
        public override bool IsPlaced { get { return _isPlaced; } set { _isPlaced = value; } }

        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;
        [SerializeField] private int _startHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private int _attackPower;
        [SerializeField] private bool _isPlaced;
        public override void Attack()
        {

        }
        public override void TakeDamage(int amount)
        {

        }
    }
}
