using System;

namespace deathreconstruction
{
    class Item
    {
        public uint ID;
        public string Name;
        public uint Value;
        public uint StackSize;
        public uint ContainerID;
        public BondedStatusEnum BondedStatus;
        public uint Wcid;
        public ITEM_TYPE Type;
        public uint WielderID;

        public Item()
        {
            ID = 0x0;
            Name = "";
            Value = 0;
            StackSize = 0;
            ContainerID = 0x0;
            Type = ITEM_TYPE.TYPE_UNDEF;
            BondedStatus = BondedStatusEnum.Normal_BondedStatus;
            Wcid = 0x0;
            WielderID = 0x0;
        }

        public Item(uint createdID, uint wielderID = 0x0)
        {
            ID = createdID;
            WielderID = wielderID;
        }

        public Item(uint itemID, CM_Physics.PublicWeenieDesc wdesc)
        {
            ID = itemID;
            Name = wdesc._name.ToString();
            Value = wdesc._value;
            StackSize = wdesc._stackSize;
            if (StackSize == 0)
            {
                StackSize = 1;
            }
            ContainerID = wdesc._containerID;
            Type = wdesc._type;
            BondedStatus = BondedStatusEnum.Normal_BondedStatus;
            Wcid = wdesc._wcid;
            WielderID = wdesc._wielderID;
        }

        public Item Copy()
        {
            return (Item)this.MemberwiseClone();
        }

        public void Print()
        {
            Console.WriteLine(ID.ToString("X") + " " + Name + " (" + StackSize + "): " + Value + " " + Wcid);
        }

        public void Print2()
        {
            Console.WriteLine(Name + " " + Value / StackSize + " " + Type);
        }
    }
}
