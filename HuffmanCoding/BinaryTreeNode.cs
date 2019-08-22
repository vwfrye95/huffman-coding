using System;

namespace HuffmanCoding
{
    class BinaryTreeNode<T>
    {
        public T Data { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public Boolean IsLeaf() { return (Left == null && Right == null); }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
