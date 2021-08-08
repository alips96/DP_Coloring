using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dp_Coloring
{
	public class NodeBuilder 
	{
        public int father;
        public int quantity;

        public NodeBuilder(int _father,int _quantity)
        {
            father = _father;
            quantity = _quantity;
        }

        public int GetFather
        {
            get
            {
                return father;
            }
        }
	}
}

