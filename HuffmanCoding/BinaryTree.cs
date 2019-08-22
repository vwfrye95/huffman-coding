using System;
using System.Collections.Generic;

namespace HuffmanCoding
{
    class BinaryTree<T>
    {
        public BinaryTreeNode<T> Root { get; set; }
        public BinaryTreeNode<T> Current { get; set; }
        private int size;

        private string[] pathTable = new string[256];
        private string encoding;

        public int Size { get { return size; } }

        public enum Relative : int { LeftChild, RightChild, Parent, Root };

        public BinaryTree()
        {
            Root = null;
            Current = null;
            size = 0;
        }

        public BinaryTree(T data)
        {
            Root = new BinaryTreeNode<T>(data);
            Current = null;
            size = 0;
        }

        public string[] PathTable { get { return pathTable; } }

        public void Destroy(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                Destroy(node.Left);
                Destroy(node.Right);
                node = null;
                size--;
            }
        }

        public Boolean IsEmpty() { return Root == null; }

        public void Encode(BinaryTreeNode<T> node)
        {

            if (node != null)
            {
                encoding += "1";
                Encode(node.Left);

                if (node.IsLeaf())
                {
                    pathTable[node.Data.GetHashCode()] = encoding;
                }

                encoding += "0";
                Encode(node.Right);
            }

            if (encoding.Length > 0)
                encoding = encoding.Remove(encoding.Length - 1);
        }

        public void InOrder(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrder(node.Left);
                if (node.IsLeaf())
                    encoding = pathTable[node.GetHashCode()];
                InOrder(node.Right);
            }
        }

        public void PreOrder(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                if (node.IsLeaf())
                    encoding = pathTable[node.GetHashCode()];
                PreOrder(node.Left);
                PreOrder(node.Right);
            }
        }

        public void PostOrder(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrder(node.Left);
                PostOrder(node.Right);
                if (node.IsLeaf())
                    encoding = pathTable[node.GetHashCode()];
            }
        }

        private BinaryTreeNode<T> FindParent(BinaryTreeNode<T> node)
        {
            Stack<BinaryTreeNode<T>> s = new Stack<BinaryTreeNode<T>>();
            node = Root;
            while (node.Left != Current && node.Right != Current)
            {
                if (node.Right != null)
                    s.Push(node.Right);

                if (node.Left != null)
                    node = node.Left;
                else
                    node = s.Pop();
            }
            s.Clear();
            return node;
        }

        public Boolean Insert(BinaryTreeNode<T> node, Relative rel)
        {
            Boolean inserted = true;

            if ((rel == Relative.LeftChild && Current.Left != null)
                || (rel == Relative.RightChild && Current.Right != null))
                inserted = false;
            else
            {
                switch (rel)
                {
                    case Relative.LeftChild:
                        Current.Left = node;
                        break;
                    case Relative.RightChild:
                        Current.Right = node;
                        break;
                    case Relative.Root:
                        if (Root == null)
                            Root = node;
                        Current = Root;
                        break;
                }
                size++;
            }

            return inserted;
        }

        public Boolean Insert(T data, Relative rel)
        {
            Boolean inserted = true;

            BinaryTreeNode<T> node = new BinaryTreeNode<T>(data);

            if ((rel == Relative.LeftChild && Current.Left != null)
                || (rel == Relative.RightChild && Current.Right != null))
            {
                inserted = false;
            }
            else
            {
                switch (rel)
                {
                    case Relative.LeftChild:
                        Current.Left = node;
                        break;
                    case Relative.RightChild:
                        Current.Right = node;
                        break;
                    case Relative.Root:
                        if (Root == null)
                            Root = node;
                        Current = Root;
                        break;
                }
                size++;
            }

            return inserted;
        }

        public Boolean MoveTo(Relative rel)
        {
            Boolean found = false;

            switch (rel)
            {
                case Relative.LeftChild:
                    if (Current.Left != null)
                    {
                        Current = Current.Left;
                        found = true;
                    }
                    break;
                case Relative.RightChild:
                    if (Current.Right != null)
                    {
                        Current = Current.Right;
                        found = true;
                    }
                    break;
                case Relative.Parent:
                    if (Current != Root)
                    {
                        Current = FindParent(Current);
                    }
                    break;
                case Relative.Root:
                    if (Root != null)
                    {
                        Current = Root;
                        found = true;
                    }
                    break;
            }

            return found;
        }
    }
}