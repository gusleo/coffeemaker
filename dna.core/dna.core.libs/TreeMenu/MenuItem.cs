using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.TreeMenu
{
    
    public class MenuItem
    {
        
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Icon { get; set; }
        public string[] Roles { get; set; }
        public int Order { get; set; }
        public string Type {
            get
            {
                return (Children != null && Children.Count > 0) ? "sub" : "link";
            }
        }
       

        public IList<MenuItem> Children { get; set; }
    }
}
