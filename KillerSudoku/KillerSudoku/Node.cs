using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku
{
    class Node
    {
        private int i;
        private int j;
        private int n;
        public List<Node> childs;
        private Node father;
        private bool hasOptions;
        private bool visited;
        private int matrixOrder;

        public Node(int order)
        {
            childs = new List<Node>();
            hasOptions = true;
            father = null;
            i = 0;
            j = -1;
            n = 0;
            matrixOrder = order;
            fillChilds(0,0);
            visited = false;
        }
        public Node(Node f, int i, int j, int n, int o)
        {
            father = f;
            this.i = i;
            this.j = j;
            this.n = n;
            childs = new List<Node>();
            matrixOrder = o;
            hasOptions = true;
            visited = false;
        }
        public void setNoOptions()
        {
            hasOptions = false;
        }
        public void fillChilds(int i, int j)
        {
            for(int k = 1; k < matrixOrder + 1; k++)
            {
                childs.Add(new Node(this, i, j, k, matrixOrder));
            }
        }
        public void addChild(Node node, int i, int j, int n, int o)
        {
            childs.Add(new Node(node, i, j, n, o));
        }
        public void deleteChild(Node node)
        {
            childs.Remove(node);
        }
        public int getI()
        {
            return i;
        }
        public int getJ()
        {
            return j;
        }
        public int getNumber()
        {
            return n;
        }
        public Node getFather()
        {
            return father;
        }
        public void setVisited()
        {
            this.visited = true;
        }
        public bool isVisited()
        {
            return visited;
        }
        public List<Node> getChildren()
        {
            return childs;
        }
        public bool getHasOptions()
        {
            return hasOptions;
        }
        public void setI(int i)
        {
            this.i = i;
        }
        public void setJ(int j)
        {
            this.j = j;
        }
        public void deleteChildren()
        {
            this.childs.Clear();
        }
    }
}
