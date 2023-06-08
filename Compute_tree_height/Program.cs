using System.Collections.Generic;
using System;

namespace Compute_tree_height
{
    public class Node
    {
        public int Data;
        public List<Node> Children;

        public Node()
        {
            Children = new List<Node>();
        }
        public Node(int value)
        {
            Data = value;
            Children = new List<Node>();
        }

        public Node AddChild(int value)
        {
            var node = new Node(value);
            Children.Add(node);
            return node;
        }

        public Node AddChild(Node node)
        {
            Children.Add(node);
            return node;
        }
    }

    public class Tree
    {
        public Node Root;

        public Tree()
        {
            Root = null;
        }


        public void Preorder(Node root)
        {
            if (root == null)
                return;


            Console.Write(root.Data + "\t");
            foreach (var item in root.Children)
            {
                Preorder(item);
            }
        }

        private int TreeHeightUsingStackAux(Node root)
        {
            if (root == null)
                return 0;

            var stack = new Stack<Tuple<Node, int>>();
            stack.Push(new Tuple<Node, int>(root, 1));
            int treeHeight = 0;

            while (stack.Count > 0)
            {
                var tuple = stack.Pop();
                var node = tuple.Item1;
                var depth = tuple.Item2;

                treeHeight = Math.Max(treeHeight, depth);
                foreach (var item in node.Children)
                {
                    stack.Push(new Tuple<Node, int>(item, depth + 1));
                }
            }

            return treeHeight;
        }

        public int GetTreeHeight()
        {
            return TreeHeightUsingStackAux(Root);
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Node[] nodes = new Node[n];

            for (int i = 0; i < n; i++)
            {
                nodes[i] = new Node(i);
            }

            int[] treeValues = new int[n];
            string[] inputs = Console.ReadLine().Split(' ');

            for (int i = 0; i < n; i++)
            {
                treeValues[i] = int.Parse(inputs[i]);
            }

            var tree = new Tree();

            for (int child_index = 0; child_index < n; child_index++)
            {
                int parent_index = treeValues[child_index];

                if (parent_index == -1)
                    tree.Root = nodes[child_index];

                else
                {
                    nodes[parent_index].AddChild(nodes[child_index]);
                }
            }

            //tree.Preorder(tree.Root);

            int res = tree.GetTreeHeight();
            Console.WriteLine(res);
        }
    }
}