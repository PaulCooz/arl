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

        private Node _current;

        public BinaryTree(in T value)
        {
            _current = new Node(null) {value = value};
        }

        public void AddNode(in T value, in bool isLeft)
        {
            var newNode = new Node(_current) {value = value};
            if (isLeft)
            {
                _current.left = newNode;
            }
            else
            {
                _current.right = newNode;
            }
        }

        public void Down(in bool isLeft)
        {
            _current = isLeft ? _current.left : _current.right;
        }

        public void Up()
        {
            _current = _current.up;
        }
    }
}