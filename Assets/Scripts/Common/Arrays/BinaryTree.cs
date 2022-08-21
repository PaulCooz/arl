using System.Collections.Generic;

namespace Common.Arrays
{
    public class BinaryTree<T>
    {
        private class Node
        {
            public readonly Node up;

            public T value;
            public Node left;
            public Node right;

            public Node(in Node up)
            {
                this.up = up;
            }
        }

        private readonly Node root;

        private Node current;

        public T CurrentValue => current.value;

        public BinaryTree(in T value)
        {
            current = new Node(null) {value = value};
            root = current;
        }

        public List<T> GetAllLeaves()
        {
            var res = new List<T>();
            Detour(root, ref res);

            return res;
        }

        private void Detour(in Node node, ref List<T> leaves)
        {
            var hasLeft = node.left != null;
            var hasRight = node.right != null;

            if (!hasLeft && !hasRight)
            {
                leaves.Add(node.value);
                return;
            }

            if (hasLeft) Detour(node.left, ref leaves);
            if (hasRight) Detour(node.right, ref leaves);
        }

        public void AddNode(in T value, in bool isLeft)
        {
            var newNode = new Node(current) {value = value};
            if (isLeft)
            {
                current.left = newNode;
            }
            else
            {
                current.right = newNode;
            }
        }

        public void Down(in bool isLeft)
        {
            current = isLeft ? current.left : current.right;
        }

        public void Up()
        {
            current = current.up;
        }
    }
}