using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class SlotServices : MonoSingleton<SlotServices>
    {
        [SerializeField]
        private List<ChestSlot> slotList;

        public ChestSlot GetSlotAtPos(int i)
        {
            return slotList[i];
        }
        public int GetNumberOfSlots()
        {
            return slotList.Count;
        }
        public ChestSlot GetVacantSlot()
        {
            ChestSlot slot = null;
            foreach (var i in slotList)
            {
                if (i.IsSlotEmpty())
                {
                    slot = i;
                    slot.SetIsEmpty(false);
                    break;
                }
            }
            return slot;
        }
    }
}

