using System;

namespace deathreconstruction
{
    class Item
    {
        public uint ID;
        public string name;
        public uint value;
        public uint stackSize;
        public uint containerID;
        public BondedStatusEnum BondedStatus;
        public uint wcid;
        public ITEM_TYPE type;

        public Item()
        {
            ID = 0x0;
            name = "";
            value = 0;
            stackSize = 0;
            containerID = 0x0;
            type = ITEM_TYPE.TYPE_UNDEF;
            BondedStatus = BondedStatusEnum.Normal_BondedStatus;
        }

        public Item(uint createdID)
        {
            ID = createdID;
        }

        public Item(uint createdID, string createdName, uint createdValue, uint createdStackSize, uint createdContainerID, ITEM_TYPE createdType, uint createdWcid)
        {
            ID = createdID;
            name = createdName;
            value = createdValue;
            stackSize = createdStackSize;
            if (stackSize == 0)
            {
                stackSize = 1;
            }
            containerID = createdContainerID;
            type = createdType;
            wcid = createdWcid;
        }

        public void Print()
        {
            Console.WriteLine(ID.ToString("X") + " " + name + " (" + stackSize + "): " + value + " " + wcid);
        }

        public void Print2()
        {
            Console.WriteLine(name + " " + value / stackSize + " " + type);
        }
    }
}
