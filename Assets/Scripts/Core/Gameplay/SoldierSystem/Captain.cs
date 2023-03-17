using StrategyGame.Core.Gameplay.PathFinding;
using StrategyGame.Core.Managers;
using StrategyGame.MVC;
using StrategyGame.MVC.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
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
        [SerializeField] private SpriteRenderer _renderer;
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
                    if (_target == null)
                    {
                        _state = SoldierState.Idle;
                    }
                    else
                    {
                        Attack(_target);
                    }

                }
            }
        }
        public override void CallInformationPanel()
        {
            string desc = "Health: " + CurrentHealth + "\n" + " Attack Power: " + AttackPower;
            InformationPanel.Instance.InformationPanelController.SetInformation(this, Name, desc, Image, new GameObject[0]);
        }
        public override void DecideMovement(Vector3 target)
        {
            _target = null;
            Move(target);
        }
        public override void DecideMovement(IDamageable target)
        {
            _target = target;
            Move(target.GetObjectPos());
        }
        public override void Move(Vector3 target)
        {
            GameGrid.Instance.GameGridController.ChangeGridTileState(transform.position, Vector3Int.zero, GameGrid.Instance.GameGridView.EmptyTile);

            State = SoldierState.Moving;

            PathFinder.Instance.FindPath(GetObjectPos(), target);
            List<WorldTile> tilePath = PathFinder.Instance.lastPath;
            List<Vector3> _path = new List<Vector3>();
            for (int i = 0; i < tilePath.Count; i++)
            {
                Vector3 worldPos = GameGrid.Instance.GameGridView.GridLayout.CellToWorld(tilePath[i].GetGridPos());
                _path.Add(worldPos);
            }
            StartCoroutine(MoveCaroutine(_path));
        }
        public override void Attack(IDamageable target)
        {
            if (target != null)
            {
                EffectsManager.Instance.SpawnPopUpObject(GetObjectPos(), AttackPower.ToString());
                target.TakeDamage(AttackPower);
            }
        }
        public override void TakeDamage(int amount)
        {
            if (_state != SoldierState.Moving)
            {
                _currentHealth -= amount;
                if (_currentHealth <= 0 && _state != SoldierState.Dead)
                {
                    _state = SoldierState.Dead;
                    Dead();
                }
            }
        }
        private void Dead()
        {
            GameGrid.Instance.GameGridController.ChangeGridTileState(transform.position, Vector3Int.zero, GameGrid.Instance.GameGridView.EmptyTile);
            BattleHandler.Instance.InvokeItemDestroyed(this);
            Destroy(gameObject);
        }
        private void CheckForAttack()
        {
            if (_target != null)
            {
                State = SoldierState.Attacking;
            }
            else
            {
                State = SoldierState.Idle;
            }

        }
        private IEnumerator MoveCaroutine(List<Vector3> path)
        {
            float moveSpeed = 10f;
            Vector3 cellSize = new Vector3(0.4f, 0.4f, 0.4f);
            for (int i = path.Count - 1; i >= 0; i--)
            {
                TileBase curTileBase = GameGrid.Instance.GameGridController.GetTileBase(path[i]);
                if (curTileBase == GameGrid.Instance.GameGridView.BuildingTile
                    || curTileBase == GameGrid.Instance.GameGridView.SoldierTile)//Checeking is target tile is not avaialble
                {
                    path.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < path.Count; i++)
            {

                Vector3 direction = ((path[i] + cellSize / 2f) - transform.position).normalized;
                while (true)
                {
                    transform.position += direction * moveSpeed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, (path[i] + cellSize / 2f)) < 0.05f)
                    {
                        transform.position = (path[i] + cellSize / 2f);
                        break;
                    }
                    yield return null;
                }
            }
            GameGrid.Instance.GameGridController.ChangeGridTileState(transform.position, Vector3Int.zero, GameGrid.Instance.GameGridView.SoldierTile);
            CheckForAttack();
        }
        private void CheckMyTarget(IDamageable destroyedTarget)
        {
            if (_target == destroyedTarget)
            {

                _state = SoldierState.Idle;
                _target = null;
            }
        }
        public override void SetClickedStatus(bool isClicked)
        {
            if (isClicked)
            {
                _renderer.color = Color.green;
            }
            else
            {
                _renderer.color = Color.white;
            }
        }
        private void OnEnable()
        {
            BattleHandler.ItemDestroyed += CheckMyTarget;
            BattleHandler.SoldierStartedMove += CheckMyTarget;
        }
        private void OnDisable()
        {
            BattleHandler.ItemDestroyed -= CheckMyTarget;
            BattleHandler.SoldierStartedMove -= CheckMyTarget;
        }
    }
}
