using System;

namespace deathreconstruction
{
    class Item
    {
        public uint ID = 0x0;
        public string Name = "";
        public uint Value = 0;
        public uint StackSize = 0;
        public uint ContainerID = 0x0;
        public BondedStatusEnum BondedStatus = BondedStatusEnum.Normal_BondedStatus;
        public uint Wcid = 0x0;
        public ITEM_TYPE Type = ITEM_TYPE.TYPE_UNDEF;
        public uint WielderID = 0x0;

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
