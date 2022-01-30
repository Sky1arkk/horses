using System.Collections.Generic;

namespace Horses
{
    internal class TreeNode
    {
        public Point Data { get; set; }
        public TreeNode Parent { get; set; }
        public List<TreeNode> Children { get; } = new List<TreeNode>();
        public int Depth { get; set; } = 0;
    }
}
