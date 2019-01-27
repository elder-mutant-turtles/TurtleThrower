using System.Collections.Generic;
using UnityEngine;

namespace TurtleThrower
{
    public class Inventory : MonoBehaviour
    {
        
        private List<string> itemTags;

        public bool Add(CollectableItem item)
        {
            if (itemTags.Contains(item.id.ToString()))
            {
                return false;
            }
            
            itemTags.Add(item.id.ToString());

            return true;
        }

        public bool Contains(CollectableItem.ItemID id)
        {
            return itemTags.Contains(id.ToString());
        }
        
    }
}