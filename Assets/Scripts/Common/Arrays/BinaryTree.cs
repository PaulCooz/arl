using System.Collections.Generic;

namespace Common.Arrays
{
    public class BinaryTree<T>
    {
        private class Node
        {
            public readonly Node Up;

            public T Value;
            public Node Left;
            public Node Right;

            public Node(in Node up)
            {
                Up = up;
            }
        }

        private readonly Node _root;

        private Node _current;

        public int Count { get; private set; }
        public T CurrentValue => _current.Value;

        public BinaryTree(in T value)
        {
            _current = new Node(null) {Value = value};
            _root = _current;
            Count = 1;
        }

        public List<T> GetAllLeaves()
        {
            var res = new List<T>();
            Detour(_root, ref res);

            return res;
        }

        private void Detour(in Node node, ref List<T> leaves)
        {
            var hasLeft = node.Left != null;
            var hasRight = node.Right != null;

            if (!hasLeft && !hasRight)
            {
                leaves.Add(node.Value);
                return;
            }

            if (hasLeft) Detour(node.Left, ref leaves);
            if (hasRight) Detour(node.Right, ref leaves);
        }

        public void AddNode(in T value, in bool isLeft)
        {
            var newNode = new Node(_current) {Value = value};
            if (isLeft)
            {
                _current.Left = newNode;
            }
            else
            {
                _current.Right = newNode;
            }

            Count++;
        }

        public void Down(in bool isLeft)
        {
            _current = isLeft ? _current.Left : _current.Right;
        }

        public void Up()
        {
            _current = _current.Up;
        }
    }
}