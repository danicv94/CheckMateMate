using Controllers;
using UnityEngine;

namespace Model
{
    public class Cell : IMapItem<Cell>
    {
        public CellController CellController;
        public Vector2 Position;
        public Color CellColor;
        public Node<Cell> Node;

        public Cell(Vector2 position,Color c)
        {
            Position = position;
            CellColor = c;
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public void SetBindedNode(Node<Cell> node)
        {
            Node = node;
        }
    }
}