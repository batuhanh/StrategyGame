using StrategyGame.MVC;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
        public override bool CanMove { get { return _canMove; } set { _canMove = value; } }
        public override SoldierState State { get { return _state; } set { _state = value; } }
        public Vector3[] Path { get { return _path; } set { _path = value; } }

        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;
        [SerializeField] private int _startHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private int _attackPower;
        [SerializeField] private bool _isPlaced;
        [SerializeField] private bool _canMove;
        [SerializeField] private SoldierState _state;
        private Vector3[] _path;
        private IDamageable _target;
        private float attackTimer = 1f;
        private float attackDelay = 1f;
        private void Update()
        {
            if (_state == SoldierState.Attacking)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer > attackDelay)
                {
                    attackTimer = 0f;
                    Attack(_target);
                }
            }
        }
        public override void CallInformationPanel()
        {
            string desc = "Health: " + StartHealth + "\n" + " Attack Power: " + AttackPower;
            InformationPanel.Instance.InformationPanelController.SetInformation(this, Name, desc, Image, new GameObject[0]);
        }
        public override void DecideMovement(Vector3 target)
        {
            Move(target);
        }
        public override void DecideMovement(IDamageable target)
        {
            _target = target;
            Move(target.GetObjectPos());
        }
        public override void Move(Vector3 target)
        {
            Vector3Int cellPos = GameGrid.Instance.GameGridView.GridLayout.WorldToCell(target);
            Vector3 actTarget = GameGrid.Instance.GameGridView.Grid.GetCellCenterWorld(cellPos);
            State = SoldierState.Moving;

            Vector3[] _path = new Vector3[] { actTarget };//pathfindg ile doldur bu pathi
            StartCoroutine(MoveCaroutine(_path));
        }
        public override void Attack(IDamageable target)
        {
            if (target != null)
            {
                target.TakeDamage(AttackPower);
            }
        }
        public override void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0 && _state != SoldierState.Dead)
            {
                _state = SoldierState.Dead;
                Dead();
            }
        }
        private void Dead()
        {
            Destroy(gameObject);
        }
        private void CheckForAttack()
        {
            if (_target != null)
            {
                State = SoldierState.Attacking;
            }

        }
        private IEnumerator MoveCaroutine(Vector3[] path)
        {
            float moveSpeed = 10f;
            for (int i = 0; i < path.Length; i++)
            {
                Vector3 direction = (path[i] - transform.position).normalized;
                while (true)
                {
                    transform.position += direction * moveSpeed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, path[i]) < 0.05f)
                    {
                        transform.position = path[i];
                        break;
                    }
                    yield return null;
                }
            }
            CheckForAttack();
        }
    }
}
