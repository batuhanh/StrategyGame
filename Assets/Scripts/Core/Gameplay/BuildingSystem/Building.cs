using StrategyGame.Core.Managers;
using StrategyGame.MVC;
using StrategyGame.MVC.Models;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.BuildingSystem
{

    public abstract class Building : MonoBehaviour, IBuilding
    {
        public abstract string Name { get; }
        public abstract Sprite Image { get; }
        public abstract int StartHealth { get; }
        public abstract int CurrentHealth { get; }
        public abstract bool IsPlaced { get; set; }
        public abstract Vector2 GridPosition { get; }
        public abstract Vector3Int Size { get; set; }
        protected Vector3[] vertices;
        public void Start()
        {
            GetColliderVertexPositionsLocal();
            CalculateSizeInCells();
        }
        public virtual void Place()
        {
            IsPlaced = true;
        }

        public void GetColliderVertexPositionsLocal()
        {
            BoxCollider2D b = GetComponent<BoxCollider2D>();
            vertices = new Vector3[4];
            Vector3 pos = b.offset;
            vertices[0] = pos + new Vector3(-b.size.x, -b.size.y, 0) * 0.5f;
            vertices[1] = pos + new Vector3(b.size.x, -b.size.y, 0) * 0.5f;
            vertices[2] = pos + new Vector3(-b.size.x, b.size.y, 0) * 0.5f;
            vertices[3] = pos + new Vector3(b.size.x, b.size.y, 0) * 0.5f;
            Debug.Log("b.offset " + b.offset + " vertices[1] " + vertices[0] + " vertices[2] " + vertices[1] + " vertices[3] " + vertices[2] + " vertices[4] " + vertices[3]);
        }
        public void CalculateSizeInCells()
        {
            Vector3Int[] verts = new Vector3Int[vertices.Length];
            for (int i = 0; i < verts.Length; i++)
            {
                Vector3 worldPos = transform.TransformPoint(vertices[i]);
                Debug.DrawRay(worldPos, worldPos + new Vector3(0, 0, -20), Color.yellow, 100f);
                verts[i] = GameGrid.Instance.GameGridView.GridLayout.WorldToCell(worldPos);
            }
            Size = new Vector3Int(Mathf.Abs((verts[0] - verts[1]).x),
                Mathf.Abs((verts[0] - verts[3]).y), 1);
        }
        public Vector3 GetStartPosition()
        {
            if (vertices == null)
            {
                GetColliderVertexPositionsLocal();
                CalculateSizeInCells();
            }
            return transform.TransformPoint(vertices[0]);
        }
    }
    public interface IBuilding
    {

    }
}
