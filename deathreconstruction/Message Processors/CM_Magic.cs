using System.IO;

public class CM_Magic {

    //public override bool acceptMessageData(BinaryReader messageDataReader, TreeView outputTreeView) {
    //    bool handled = true;

    //    PacketOpcode opcode = Util.readOpcode(messageDataReader);
    //    switch (opcode) {
    //        case PacketOpcode.Evt_Magic__PurgeEnchantments_ID:
    //        case PacketOpcode.Evt_Magic__PurgeBadEnchantments_ID: {
    //                EmptyMessage message = new EmptyMessage(opcode);
    //                message.contributeToTreeView(outputTreeView);
    //                ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //                break;
    //            }
    //        case PacketOpcode.Evt_Magic__CastUntargetedSpell_ID: {
    //                CastUntargetedSpell message = CastUntargetedSpell.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Magic__CastTargetedSpell_ID: {
    //                CastTargetedSpell message = CastTargetedSpell.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        // TODO: Evt_Magic__ResearchSpell_ID
    //        //case PacketOpcode.UPDATE_SPELL_EVENT: {
    //        //        UpdateSpell message = UpdateSpell.read(messageDataReader);
    //        //        message.contributeToTreeView(outputTreeView);
    //        //        break;
    //        //    }
    //        //case PacketOpcode.REMOVE_SPELL_EVENT: {
    //        //        RemoveSpell message = RemoveSpell.read(messageDataReader);
    //        //        message.contributeToTreeView(outputTreeView);
    //        //        break;
    //        //    }
    //        //case PacketOpcode.UPDATE_ENCHANTMENT_EVENT: {
    //        //        UpdateEnchantment message = UpdateEnchantment.read(messageDataReader);
    //        //        message.contributeToTreeView(outputTreeView);
    //        //        break;
    //        //    }
    //        //case PacketOpcode.REMOVE_ENCHANTMENT_EVENT: {
    //        //        RemoveEnchantment message = RemoveEnchantment.read(messageDataReader);
    //        //        message.contributeToTreeView(outputTreeView);
    //        //        break;
    //        //    }
    //        case PacketOpcode.Evt_Magic__RemoveSpell_ID:
    //            {
    //                RemoveSpell message = RemoveSpell.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Magic__UpdateMultipleEnchantments_ID: {
    //                UpdateMultipleEnchantments message = UpdateMultipleEnchantments.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Magic__RemoveMultipleEnchantments_ID: {
    //                RemoveMultipleEnchantments message = RemoveMultipleEnchantments.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Magic__DispelEnchantment_ID: {
    //                DispelEnchantment message = DispelEnchantment.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Magic__DispelMultipleEnchantments_ID: {
    //                DispelMultipleEnchantments message = DispelMultipleEnchantments.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Magic__UpdateSpell_ID:
    //            {
    //                UpdateSpell message = UpdateSpell.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Magic__UpdateEnchantment_ID:
    //            {
    //                UpdateEnchantment message = UpdateEnchantment.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Magic__RemoveEnchantment_ID:
    //            {
    //                RemoveEnchantment message = RemoveEnchantment.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        default: {
    //                handled = false;
    //                break;
    //            }
    //    }

    //    return handled;
    //}

    //public class CastTargetedSpell : Message {
    //    public uint i_target;
    //    public uint i_spell_id;
        
    //    public static CastTargetedSpell read(BinaryReader binaryReader) {
    //        CastTargetedSpell newObj = new CastTargetedSpell();
    //        newObj.i_target = binaryReader.ReadUInt32();
    //        newObj.i_spell_id = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_target = " + Utility.FormatHex(this.i_target));
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //        rootNode.Nodes.Add("i_spell_id = " + "(" + i_spell_id + ") " + (SpellID)i_spell_id);
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.SpellID_uint });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class CastUntargetedSpell : Message {
    //    public uint i_spell_id;

    //    public static CastUntargetedSpell read(BinaryReader binaryReader) {
    //        CastUntargetedSpell newObj = new CastUntargetedSpell();
    //        newObj.i_spell_id = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_spell_id = " + "(" + i_spell_id + ") " + (SpellID)i_spell_id);
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.SpellID_uint });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class RemoveSpell : Message {
    //    public uint i_spell_id;
    //    public bool isClientToServer;

    //    public static RemoveSpell read(BinaryReader binaryReader) {
    //        RemoveSpell newObj = new RemoveSpell();
    //        newObj.isClientToServer = (binaryReader.BaseStream.Position == 12); 
    //        newObj.i_spell_id = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        rootNode.Nodes.Add("i_spell_id = " + "(" + i_spell_id + ") " + (SpellID)i_spell_id);
    //        treeView.Nodes.Add(rootNode);

    //        if (isClientToServer)
    //        {
    //            ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //            ContextInfo.AddToList(new ContextInfo { DataType = DataType.SpellID_uint });
    //        }
    //        else
    //        {
    //            ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //            ContextInfo.AddToList(new ContextInfo { DataType = DataType.SpellID_uint });
    //        }
    //    }
    //}

    //public class UpdateSpell : Message {
    //    public uint i_spell_id;

    //    public static UpdateSpell read(BinaryReader binaryReader) {
    //        UpdateSpell newObj = new UpdateSpell();
    //        newObj.i_spell_id = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //        rootNode.Nodes.Add("i_spell_id = " + "(" + i_spell_id + ") " + (SpellID)i_spell_id);
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.SpellID_uint });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    public class StatMod {
        public uint type;
        public uint key;
        public float val;
        public int Length = 12;

        public static StatMod read(BinaryReader binaryReader) {
            StatMod newObj = new StatMod();
            newObj.type = binaryReader.ReadUInt32();
            newObj.key = binaryReader.ReadUInt32();
            newObj.val = binaryReader.ReadSingle();
            return newObj;
        }
    }

    public class Enchantment {
        public EnchantmentID eid;
        public ushort spell_category;
        public ushort has_spell_set_id;
        public uint power_level;
        public double start_time;
        public double duration;
        public uint caster;
        public float degrade_modifier;
        public float degrade_limit;
        public double last_time_degraded;
        public StatMod smod;
        public uint spell_set_id;
        public int Length;

        public static Enchantment read(BinaryReader binaryReader) {
            Enchantment newObj = new Enchantment();
            var startPosition = binaryReader.BaseStream.Position;
            newObj.eid = EnchantmentID.read(binaryReader);
            newObj.spell_category = binaryReader.ReadUInt16();
            newObj.has_spell_set_id = binaryReader.ReadUInt16();
            newObj.power_level = binaryReader.ReadUInt32();
            newObj.start_time = binaryReader.ReadDouble();
            newObj.duration = binaryReader.ReadDouble();
            newObj.caster = binaryReader.ReadUInt32();
            newObj.degrade_modifier = binaryReader.ReadSingle();
            newObj.degrade_limit = binaryReader.ReadSingle();
            newObj.last_time_degraded = binaryReader.ReadDouble();
            newObj.smod = StatMod.read(binaryReader);
            if (newObj.has_spell_set_id >= 1)
                newObj.spell_set_id = binaryReader.ReadUInt32();
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
            return newObj;
        }
    }

    public class EnchantmentID {
        public ushort i_spell_id;
        public ushort layer;
        
        public static EnchantmentID read(BinaryReader binaryReader)
        {
            EnchantmentID newObj = new EnchantmentID();
            newObj.i_spell_id = binaryReader.ReadUInt16();
            newObj.layer = binaryReader.ReadUInt16();
            return newObj;
        }
    }

    

    //public class DispelEnchantment : Message {
    //    public EnchantmentID eid;

    //    public static DispelEnchantment read(BinaryReader binaryReader) {
    //        DispelEnchantment newObj = new DispelEnchantment();
    //        newObj.eid = EnchantmentID.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //        TreeNode enchantmentIDNode = rootNode.Nodes.Add("enchantment id = ");
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.EnchantmentID }, updateDataIndex: false);
    //        eid.contributeToTreeNode(enchantmentIDNode);
    //        enchantmentIDNode.Expand();
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class RemoveEnchantment : Message {
    //    public EnchantmentID eid;

    //    public static RemoveEnchantment read(BinaryReader binaryReader) {
    //        RemoveEnchantment newObj = new RemoveEnchantment();
    //        newObj.eid = EnchantmentID.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView)
    //    {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //        TreeNode enchantmentIDNode = rootNode.Nodes.Add("enchantment id = ");
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.EnchantmentID }, updateDataIndex: false);
    //        eid.contributeToTreeNode(enchantmentIDNode);
    //        enchantmentIDNode.Expand();
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class UpdateEnchantment : Message {
    //    public Enchantment enchant;

    //    public static UpdateEnchantment read(BinaryReader binaryReader) {
    //        UpdateEnchantment newObj = new UpdateEnchantment();
    //        newObj.enchant = Enchantment.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //        TreeNode enchantmentNode = rootNode.Nodes.Add("enchantment = ");
    //        ContextInfo.AddToList(new ContextInfo { Length = enchant.Length }, updateDataIndex: false);
    //        enchant.contributeToTreeNode(enchantmentNode);
    //        enchantmentNode.ExpandAll();
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class DispelMultipleEnchantments : Message {
    //    public PList<EnchantmentID> enchantmentList;

    //    public static DispelMultipleEnchantments read(BinaryReader binaryReader) {
    //        DispelMultipleEnchantments newObj = new DispelMultipleEnchantments();
    //        newObj.enchantmentList = PList<EnchantmentID>.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //        TreeNode plistNode = rootNode.Nodes.Add($"PackableList<EnchantmentID>: {enchantmentList.list.Count} objects");
    //        ContextInfo.AddToList(new ContextInfo { Length = enchantmentList.Length }, updateDataIndex: false);
    //        // Skip Plist count uint
    //        ContextInfo.DataIndex += 4;
    //        for (int i = 0; i < enchantmentList.list.Count; i++) {
    //            TreeNode listNode = plistNode.Nodes.Add($"enchantment {i+1} = ");
    //            ContextInfo.AddToList(new ContextInfo { Length = 4 }, updateDataIndex: false);
    //            var enchantment = enchantmentList.list[i];
    //            enchantment.contributeToTreeNode(listNode);
    //            listNode.Expand();
    //        }
    //        plistNode.Expand();
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class RemoveMultipleEnchantments : Message {
    //    public PList<EnchantmentID> enchantmentList;

    //    public static RemoveMultipleEnchantments read(BinaryReader binaryReader) {
    //        RemoveMultipleEnchantments newObj = new RemoveMultipleEnchantments();
    //        newObj.enchantmentList = PList<EnchantmentID>.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView)
    //    {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //        TreeNode plistNode = rootNode.Nodes.Add($"PackableList<EnchantmentID>: {enchantmentList.list.Count} objects");
    //        ContextInfo.AddToList(new ContextInfo { Length = enchantmentList.Length }, updateDataIndex: false);
    //        // Skip Plist count uint
    //        ContextInfo.DataIndex += 4;
    //        for (int i = 0; i < enchantmentList.list.Count; i++)
    //        {
    //            TreeNode listNode = plistNode.Nodes.Add($"enchantment {i + 1} = ");
    //            ContextInfo.AddToList(new ContextInfo { Length = 4 }, updateDataIndex: false);
    //            var enchantment = enchantmentList.list[i];
    //            enchantment.contributeToTreeNode(listNode);
    //            listNode.Expand();
    //        }
    //        plistNode.Expand();
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //// This message does not appear to be used. It was not found in any pcaps.
    //public class UpdateMultipleEnchantments : Message { 
    //    public PList<Enchantment> list;

    //    public static UpdateMultipleEnchantments read(BinaryReader binaryReader) {
    //        UpdateMultipleEnchantments newObj = new UpdateMultipleEnchantments();
    //        newObj.list = PList<Enchantment>.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        TreeNode listNode = rootNode.Nodes.Add("list = ");
    //        list.contributeToTreeNode(listNode);
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}
}
