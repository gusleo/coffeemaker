using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.TreeMenu
{
    public class TreeMenuBuilder
    {
        public List<MenuItem> Build(string role, IEnumerable<MenuItem> list, MenuItem parentNode = null)
        {
            List<MenuItem> treeList = new List<MenuItem>();
            var nodes = list.Where(x => parentNode == null ? x.ParentId == 0 : x.ParentId == parentNode.Id);
            foreach ( var node in nodes )
            {                

                if ( parentNode == null )
                {
                    //if contain rules, or no rules (means the menu is public)
                    if ( node.Roles.ToList().Contains(role) || node.Roles.Count() == 0 )
                    {
                        treeList.Add(node);
                    }
                    
                }
                else
                {
                    //if contain rules, or no rules (means the menu is public)
                    if ( node.Roles.ToList().Contains(role) || node.Roles.Count() == 0 )
                    {
                        if(parentNode.Children == null )
                        {
                            parentNode.Children = new List<MenuItem>();
                        }
                        parentNode.Children.Add(node);
                        parentNode.Children = parentNode.Children.OrderBy(x => x.Order).ToList();
                    }
                }
                //recursive
                Build(role, list, node);
            }
            treeList = treeList.OrderBy(x => x.Order).ToList();
            return treeList;
        }
    }
}
