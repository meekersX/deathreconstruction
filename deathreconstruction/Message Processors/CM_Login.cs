using System.Collections.Generic;
using System.IO;
using static CM_Inventory;
using System.Diagnostics;
using deathreconstruction;

public class CM_Login
{

    //public override bool acceptMessageData(BinaryReader messageDataReader, TreeView outputTreeView)
    //{
    //    bool handled = true;

    //    PacketOpcode opcode = Util.readOpcode(messageDataReader);
    //    switch (opcode)
    //    {
    //        case PacketOpcode.Evt_Login__CharacterSet_ID:
    //            {
    //                Login__CharacterSet message = Login__CharacterSet.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Login__WorldInfo_ID:
    //            {
    //                WorldInfo message = WorldInfo.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.PLAYER_DESCRIPTION_EVENT:
    //            {
    //                PlayerDescription message = PlayerDescription.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_DDD__Interrogation_ID:
    //            {
    //                DDD_InterrogationMessage message = DDD_InterrogationMessage.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_DDD__InterrogationResponse_ID:
    //            {
    //                DDD_InterrogationResponseMessage message = DDD_InterrogationResponseMessage.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_DDD__BeginDDD_ID:
    //            {
    //                DDD_BeginDDDMessage message = DDD_BeginDDDMessage.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_DDD__Data_ID:
    //            {
    //                DDD_DataMessage message = DDD_DataMessage.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_DDD__EndDDD_ID:
    //            {
    //                DDD_EndDDDMessage message = DDD_EndDDDMessage.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_DDD__RequestData_ID:
    //            {
    //                DDD_RequestDataMessage message = DDD_RequestDataMessage.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        default:
    //            {
    //                handled = false;
    //                break;
    //            }
    //    }

    //    return handled;
    //}

    //public class CharacterIdentity
    //{
    //    public uint gid_;
    //    public PStringChar name_;
    //    public uint secondsGreyedOut_;
    //    public int Length;

    //    public static CharacterIdentity read(BinaryReader binaryReader)
    //    {
    //        CharacterIdentity newObj = new CharacterIdentity();
    //        var startPosition = binaryReader.BaseStream.Position;
    //        newObj.gid_ = binaryReader.ReadUInt32();
    //        newObj.name_ = PStringChar.read(binaryReader);
    //        newObj.secondsGreyedOut_ = binaryReader.ReadUInt32();
    //        newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
    //        return newObj;
    //    }

    //    public void contributeToTreeNode(TreeNode node)
    //    {
    //        node.Nodes.Add("gid_ = " + Utility.FormatHex(gid_));
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //        node.Nodes.Add("name_ = " + name_.m_buffer);
    //        ContextInfo.AddToList(new ContextInfo { Length = name_.Length, DataType = DataType.Serialized_AsciiString });
    //        node.Nodes.Add("secondsGreyedOut_ = " + secondsGreyedOut_);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //    }
    //}

    //public class Login__CharacterSet : Message
    //{
    //    public uint status_;
    //    public List<CharacterIdentity> set_ = new List<CharacterIdentity>();
    //    public List<CharacterIdentity> delSet_ = new List<CharacterIdentity>();
    //    public uint numAllowedCharacters_;
    //    public PStringChar account_;
    //    public uint m_fUseTurbineChat;
    //    public uint m_fHasThroneofDestiny;

    //    public static Login__CharacterSet read(BinaryReader binaryReader)
    //    {
    //        Login__CharacterSet newObj = new Login__CharacterSet();
    //        newObj.status_ = binaryReader.ReadUInt32();
    //        uint setNum = binaryReader.ReadUInt32();
    //        for (uint i = 0; i < setNum; ++i)
    //        {
    //            newObj.set_.Add(CharacterIdentity.read(binaryReader));
    //        }
    //        uint delSetNum = binaryReader.ReadUInt32();
    //        for (uint i = 0; i < delSetNum; ++i)
    //        {
    //            newObj.delSet_.Add(CharacterIdentity.read(binaryReader));
    //        }
    //        newObj.numAllowedCharacters_ = binaryReader.ReadUInt32();
    //        newObj.account_ = PStringChar.read(binaryReader);
    //        newObj.m_fUseTurbineChat = binaryReader.ReadUInt32();
    //        newObj.m_fHasThroneofDestiny = binaryReader.ReadUInt32();

    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView)
    //    {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });
    //        rootNode.Nodes.Add("status_ = " + status_);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        TreeNode setNode = rootNode.Nodes.Add("set_ = ");
    //        // Calculate character set size
    //        var charSetSize = 4;
    //        for (int i = 0; i < set_.Count; i++)
    //        {
    //            charSetSize += set_[i].Length;
    //        }
    //        ContextInfo.AddToList(new ContextInfo { Length = charSetSize }, updateDataIndex: false);
    //        // Skip character list count uint
    //        ContextInfo.DataIndex += 4;
    //        for (int i = 0; i < set_.Count; i++)
    //        {
    //            TreeNode characterNode = setNode.Nodes.Add($"character {i + 1} = ");
    //            ContextInfo.AddToList(new ContextInfo { Length = set_[i].Length }, updateDataIndex: false);
    //            set_[i].contributeToTreeNode(characterNode);
    //        }
    //        TreeNode delSetNode = rootNode.Nodes.Add("delSet_ = ");
    //        // Calculate deleted character set size
    //        charSetSize = 4;
    //        for (int i = 0; i < delSet_.Count; i++)
    //        {
    //            charSetSize += delSet_[i].Length;
    //        }
    //        ContextInfo.AddToList(new ContextInfo { Length = charSetSize }, updateDataIndex: false);
    //        // Skip character list count uint
    //        ContextInfo.DataIndex += 4;
    //        for (int i = 0; i < delSet_.Count; i++)
    //        {
    //            TreeNode characterNode = delSetNode.Nodes.Add($"character {i + 1} = ");
    //            ContextInfo.AddToList(new ContextInfo { Length = delSet_[i].Length }, updateDataIndex: false);
    //            delSet_[i].contributeToTreeNode(characterNode);
    //        }
    //        rootNode.Nodes.Add("numAllowedCharacters_ = " + numAllowedCharacters_);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("account_ = " + account_.m_buffer);
    //        ContextInfo.AddToList(new ContextInfo { Length = account_.Length, DataType = DataType.Serialized_AsciiString });
    //        rootNode.Nodes.Add("m_fUseTurbineChat = " + m_fUseTurbineChat);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("m_fHasThroneofDestiny = " + m_fHasThroneofDestiny);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class WorldInfo : Message
    //{
    //    public int cConnections;
    //    public int cMaxConnections;
    //    public PStringChar strWorldName;

    //    public static WorldInfo read(BinaryReader binaryReader)
    //    {
    //        WorldInfo newObj = new WorldInfo();
    //        newObj.cConnections = binaryReader.ReadInt32();
    //        newObj.cMaxConnections = binaryReader.ReadInt32();
    //        newObj.strWorldName = PStringChar.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView)
    //    {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });
    //        rootNode.Nodes.Add("cConnections = " + cConnections);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("cMaxConnections = " + cMaxConnections);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("strWorldName = " + strWorldName.m_buffer);
    //        ContextInfo.AddToList(new ContextInfo { Length = strWorldName.Length, DataType = DataType.Serialized_AsciiString });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}


    public class PlayerDescription
    {
        public CACQualities CACQualities;
        public CM_Character.PlayerModule PlayerModule;
        public PList<ContentProfile> clist;
        public PList<InventoryPlacement> ilist;

        public static PlayerDescription read(BinaryReader binaryReader)
        {
            PlayerDescription newObj = new PlayerDescription();
            newObj.CACQualities = CACQualities.read(binaryReader);
            newObj.PlayerModule = CM_Character.PlayerModule.read(binaryReader);
            newObj.clist = PList<ContentProfile>.read(binaryReader);
            newObj.ilist = PList<InventoryPlacement>.read(binaryReader);
            return newObj;
        }
    }

    public class CACQualities
    {
        public enum QualitiesPackHeader
        {
            Packed_None = 0,
            Packed_AttributeCache = (1 << 0),  //0x01
            Packed_SkillHashTable = (1 << 1), //0x02
            Packed_Body = (1 << 2), //0x04
            Packed_Attributes = (1 << 3), //0x08
            Packed_EmoteTable = (1 << 4), //0x10
            Packed_CreationProfileList = (1 << 5), //0x20
            Packed_PageDataList = (1 << 6), //0x40
            Packed_GeneratorTable = (1 << 7), //0x80
            Packed_SpellBook = (1 << 8), //0x0100
            Packed_EnchantmentRegistry = (1 << 9), //0x0200
            Packed_GeneratorRegistry = (1 << 10), //0x0400
            Packed_GeneratorQueue = (1 << 11), //0x0800
        }

        public CBaseQualities CBaseQualities;
        public uint header;
        public WeenieType _weenie_type;
        public AttributeCache _attribCache;
        public PackableHashTable<STypeSkill, Skill> _skillStatsTable = new PackableHashTable<STypeSkill, Skill>();
        public PackableHashTable<uint, float> _spell_book = new PackableHashTable<uint, float>();
        public EnchantmentRegistry _enchantment_reg;
        public int Length;
        public List<string> packedItems = new List<string>(); // Display purposes

        public static CACQualities read(BinaryReader binaryReader)
        {
            CACQualities newObj = new CACQualities();
            var startPosition = binaryReader.BaseStream.Position;
            newObj.CBaseQualities = CBaseQualities.read(binaryReader);
            newObj.header = binaryReader.ReadUInt32();
            newObj._weenie_type = (WeenieType)binaryReader.ReadUInt32();
            if ((newObj.header & (uint)QualitiesPackHeader.Packed_AttributeCache) != 0)
            {
                newObj._attribCache = AttributeCache.read(binaryReader);
                newObj.packedItems.Add(QualitiesPackHeader.Packed_AttributeCache.ToString());
            }
            if ((newObj.header & (uint)QualitiesPackHeader.Packed_SkillHashTable) != 0)
            {
                newObj._skillStatsTable = PackableHashTable<STypeSkill, Skill>.read(binaryReader);
                newObj.packedItems.Add(QualitiesPackHeader.Packed_SkillHashTable.ToString());
            }
            if ((newObj.header & (uint)QualitiesPackHeader.Packed_SpellBook) != 0)
            {
                newObj._spell_book = PackableHashTable<uint, float>.read(binaryReader);
                newObj.packedItems.Add(QualitiesPackHeader.Packed_SpellBook.ToString());
            }
            if ((newObj.header & (uint)QualitiesPackHeader.Packed_EnchantmentRegistry) != 0)
            {
                newObj._enchantment_reg = EnchantmentRegistry.read(binaryReader);
                newObj.packedItems.Add(QualitiesPackHeader.Packed_EnchantmentRegistry.ToString());
            }
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
            return newObj;
        }
    }

    public class CBaseQualities
    {
        public enum BaseQualitiesPackHeader
        {
            Packed_None = 0,
            Packed_IntStats = (1 << 0),  //0x01
            Packed_BoolStats = (1 << 1), //0x02
            Packed_FloatStats = (1 << 2), //0x04
            Packed_DataIDStats = (1 << 3), //0x08
            Packed_StringStats = (1 << 4), //0x10
            Packed_PositionHashTable = (1 << 5), //0x20
            Packed_IIDStats = (1 << 6), //0x40
            Packed_Int64Stats = (1 << 7), //0x80
        }

        public uint header;
        public WeenieType _weenie_type;
        public PackableHashTable<STypeInt, int> _intStatsTable = new PackableHashTable<STypeInt, int>();
        public PackableHashTable<STypeInt64, long> _int64StatsTable = new PackableHashTable<STypeInt64, long>();
        public PackableHashTable<STypeBool, int> _boolStatsTable = new PackableHashTable<STypeBool, int>();
        public PackableHashTable<STypeFloat, double> _floatStatsTable = new PackableHashTable<STypeFloat, double>();
        public PackableHashTable<STypeString, PStringChar> _strStatsTable = new PackableHashTable<STypeString, PStringChar>();
        public PackableHashTable<STypeDID, uint> _didStatsTable = new PackableHashTable<STypeDID, uint>();
        public PackableHashTable<STypeIID, uint> _iidStatsTable = new PackableHashTable<STypeIID, uint>();
        public PackableHashTable<STypePosition, Position> _posStatsTable = new PackableHashTable<STypePosition, Position>();
        public int Length;
        public List<string> packedItems = new List<string>(); // Display purposes

        public static CBaseQualities read(BinaryReader binaryReader)
        {
            CBaseQualities newObj = new CBaseQualities();
            var startPosition = binaryReader.BaseStream.Position;
            newObj.header = binaryReader.ReadUInt32();
            newObj._weenie_type = (WeenieType)binaryReader.ReadUInt32();
            if ((newObj.header & (uint)BaseQualitiesPackHeader.Packed_IntStats) != 0)
            {
                newObj._intStatsTable = PackableHashTable<STypeInt, int>.read(binaryReader);
                newObj.packedItems.Add(BaseQualitiesPackHeader.Packed_IntStats.ToString());
            }
            if ((newObj.header & (uint)BaseQualitiesPackHeader.Packed_Int64Stats) != 0)
            {
                newObj._int64StatsTable = PackableHashTable<STypeInt64, long>.read(binaryReader);
                newObj.packedItems.Add(BaseQualitiesPackHeader.Packed_Int64Stats.ToString());
            }
            if ((newObj.header & (uint)BaseQualitiesPackHeader.Packed_BoolStats) != 0)
            {
                newObj._boolStatsTable = PackableHashTable<STypeBool, int>.read(binaryReader);
                newObj.packedItems.Add(BaseQualitiesPackHeader.Packed_BoolStats.ToString());
            }
            if ((newObj.header & (uint)BaseQualitiesPackHeader.Packed_FloatStats) != 0)
            {
                newObj._floatStatsTable = PackableHashTable<STypeFloat, double>.read(binaryReader);
                newObj.packedItems.Add(BaseQualitiesPackHeader.Packed_FloatStats.ToString());
            }
            if ((newObj.header & (uint)BaseQualitiesPackHeader.Packed_StringStats) != 0)
            {
                newObj._strStatsTable = PackableHashTable<STypeString, PStringChar>.read(binaryReader);
                newObj.packedItems.Add(BaseQualitiesPackHeader.Packed_StringStats.ToString());
            }
            if ((newObj.header & (uint)BaseQualitiesPackHeader.Packed_DataIDStats) != 0)
            {
                newObj._didStatsTable = PackableHashTable<STypeDID, uint>.read(binaryReader);
                newObj.packedItems.Add(BaseQualitiesPackHeader.Packed_DataIDStats.ToString());
            }
            if ((newObj.header & (uint)BaseQualitiesPackHeader.Packed_IIDStats) != 0)
            {
                newObj._iidStatsTable = PackableHashTable<STypeIID, uint>.read(binaryReader);
                newObj.packedItems.Add(BaseQualitiesPackHeader.Packed_IIDStats.ToString());
            }
            if ((newObj.header & (uint)BaseQualitiesPackHeader.Packed_PositionHashTable) != 0)
            {
                newObj._posStatsTable = PackableHashTable<STypePosition, Position>.read(binaryReader);
                newObj.packedItems.Add(BaseQualitiesPackHeader.Packed_PositionHashTable.ToString());
            }
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
            return newObj;
        }
    }

    public class AttributeCache
    {
        public uint header;
        public Attribute _strength;
        public Attribute _endurance;
        public Attribute _quickness;
        public Attribute _coordination;
        public Attribute _focus;
        public Attribute _self;
        public SecondaryAttribute _health;
        public SecondaryAttribute _stamina;
        public SecondaryAttribute _mana;
        public int Length;

        public static AttributeCache read(BinaryReader binaryReader)
        {
            AttributeCache newObj = new AttributeCache();
            var startPosition = binaryReader.BaseStream.Position;
            newObj.header = binaryReader.ReadUInt32();
            newObj._strength = Attribute.read(binaryReader);
            newObj._endurance = Attribute.read(binaryReader);
            newObj._quickness = Attribute.read(binaryReader);
            newObj._coordination = Attribute.read(binaryReader);
            newObj._focus = Attribute.read(binaryReader);
            newObj._self = Attribute.read(binaryReader);
            newObj._health = SecondaryAttribute.read(binaryReader);
            newObj._stamina = SecondaryAttribute.read(binaryReader);
            newObj._mana = SecondaryAttribute.read(binaryReader);
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
            return newObj;
        }
    }

    public class EnchantmentRegistry
    {
        public uint header;
        public PList<CM_Magic.Enchantment> _mult_list = new PList<CM_Magic.Enchantment>();
        public PList<CM_Magic.Enchantment> _add_list = new PList<CM_Magic.Enchantment>();
        public PList<CM_Magic.Enchantment> _cooldown_list = new PList<CM_Magic.Enchantment>();
        public CM_Magic.Enchantment _vitae = new CM_Magic.Enchantment();
        public int Length;
        public List<string> packedItems = new List<string>(); // Display purposes

        public static EnchantmentRegistry read(BinaryReader binaryReader)
        {
            EnchantmentRegistry newObj = new EnchantmentRegistry();
            var startPosition = binaryReader.BaseStream.Position;
            newObj.header = binaryReader.ReadUInt32();
            if ((newObj.header & (uint)EnchantmentRegistryPackHeader.Packed_MultList) != 0)
            {
                newObj._mult_list = PList<CM_Magic.Enchantment>.read(binaryReader);
                newObj.packedItems.Add(EnchantmentRegistryPackHeader.Packed_MultList.ToString());
            }
            if ((newObj.header & (uint)EnchantmentRegistryPackHeader.Packed_AddList) != 0)
            {
                newObj._add_list = PList<CM_Magic.Enchantment>.read(binaryReader);
                newObj.packedItems.Add(EnchantmentRegistryPackHeader.Packed_AddList.ToString());
            }
            if ((newObj.header & (uint)EnchantmentRegistryPackHeader.Packed_Cooldown) != 0)
            {
                newObj._cooldown_list = PList<CM_Magic.Enchantment>.read(binaryReader);
                newObj.packedItems.Add(EnchantmentRegistryPackHeader.Packed_Cooldown.ToString());
            }
            if ((newObj.header & (uint)EnchantmentRegistryPackHeader.Packed_Vitae) != 0)
            {
                newObj._vitae = CM_Magic.Enchantment.read(binaryReader);
                newObj.packedItems.Add(EnchantmentRegistryPackHeader.Packed_Vitae.ToString());
            }
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
            return newObj;
        }
    }

    public class InventoryPlacement
    {
        public uint iid_;
        public uint loc_;
        public uint priority_;
        public int Length = 12;

        public static InventoryPlacement read(BinaryReader binaryReader)
        {
            InventoryPlacement newObj = new InventoryPlacement();
            newObj.iid_ = binaryReader.ReadUInt32();
            newObj.loc_ = binaryReader.ReadUInt32();
            newObj.priority_ = binaryReader.ReadUInt32();
            return newObj;
        }
    }

    //public class DDD_InterrogationMessage : Message
    //{
    //    public uint m_dwServersRegion;
    //    public uint m_NameRuleLanguage;
    //    public uint m_dwProductID;
    //    public PList<uint> m_SupportedLanguages;

    //    public static DDD_InterrogationMessage read(BinaryReader binaryReader)
    //    {
    //        DDD_InterrogationMessage newObj = new DDD_InterrogationMessage();
    //        newObj.m_dwServersRegion = binaryReader.ReadUInt32();
    //        newObj.m_NameRuleLanguage = binaryReader.ReadUInt32();
    //        newObj.m_dwProductID = binaryReader.ReadUInt32();
    //        newObj.m_SupportedLanguages = PList<uint>.read(binaryReader);

    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView)
    //    {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();

    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });

    //        rootNode.Nodes.Add("m_dwServersRegion = " + m_dwServersRegion);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });

    //        rootNode.Nodes.Add("m_NameRuleLanguage = " + m_NameRuleLanguage);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });

    //        rootNode.Nodes.Add("m_dwProductID = " + m_dwProductID);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });

    //        TreeNode supportedLanguageNode = rootNode.Nodes.Add("m_SupportedLanguages = ");
    //        // skip count header
    //        ContextInfo.DataIndex += 4;
    //        for (int i = 0; i < m_SupportedLanguages.list.Count; i++)
    //        {
    //            supportedLanguageNode.Nodes.Add(m_SupportedLanguages.list[i].ToString());
    //            ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        }

    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    ///// <summary>
    ///// TODO -- FINISH THIS ... This is all sorts of wrong - OptimShi
    ///// </summary>
    //public class DDD_InterrogationResponseMessage : Message
    //{
    //    public uint m_ClientLanguage;
    //    public CAllIterationList m_ItersWithKeys;
    //    public CAllIterationList m_ItersWithoutKeys;
    //    public uint m_dwFlags;

    //     public static DDD_InterrogationResponseMessage read(BinaryReader binaryReader)
    //     {
    //         DDD_InterrogationResponseMessage newObj = new DDD_InterrogationResponseMessage();
    //         newObj.m_ClientLanguage = binaryReader.ReadUInt32();

    //         newObj.m_ItersWithKeys = CAllIterationList.read(binaryReader);
    //         newObj.m_ItersWithoutKeys = CAllIterationList.read(binaryReader);

    //         newObj.m_dwFlags = binaryReader.ReadUInt32();

    //         return newObj;
    //     }

    //     public override void contributeToTreeView(TreeView treeView)
    //     {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();

    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });

    //        rootNode.Nodes.Add("m_ClientLanguage = " + m_ClientLanguage);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });

    //        TreeNode m_ItersWithKeysNode = rootNode.Nodes.Add("m_ItersWithKeys = ");
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 + m_ItersWithKeys.m_Lists.Length }, updateDataIndex: false);
    //        // Skip PackableHashTable count dword
    //        ContextInfo.DataIndex += 4;
    //        for (int i = 0; i < m_ItersWithKeys.m_Lists.list.Count; i++)
    //        {
    //            TreeNode m_ListsNode = m_ItersWithKeysNode.Nodes.Add("m_Lists");
    //            m_ListsNode.Nodes.Add("idDatFile.Type = " + m_ItersWithKeys.m_Lists.list[i].idDatFile_Type);
    //            m_ListsNode.Nodes.Add("idDatFile.Id = " + m_ItersWithKeys.m_Lists.list[i].idDatFile_Id);
    //            TreeNode listNode = m_ListsNode.Nodes.Add("List");
    //            TreeNode mIntsNode = listNode.Nodes.Add("m_Ints");
    //            for (int j = 0; j < m_ItersWithKeys.m_Lists.list[i].List.m_Ints.Count; j++)
    //            {
    //                mIntsNode.Nodes.Add(m_ItersWithKeys.m_Lists.list[i].List.m_Ints[j].ToString());
    //            }
    //            listNode.Nodes.Add("m_bSorted = " + m_ItersWithKeys.m_Lists.list[i].List.m_bSorted);
    //        }

    //        TreeNode m_ItersWithoutKeysNode = rootNode.Nodes.Add("m_ItersWithoutKeys = ");
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 + m_ItersWithoutKeys.m_Lists.Length }, updateDataIndex: false);
    //        // Skip PackableHashTable count dword
    //        ContextInfo.DataIndex += 4;
    //        for (int i = 0; i < m_ItersWithoutKeys.m_Lists.list.Count; i++)
    //        {
    //            TreeNode m_ListsNode = m_ItersWithoutKeysNode.Nodes.Add("m_Lists");
    //            m_ListsNode.Nodes.Add("idDatFile.Type = " + m_ItersWithoutKeys.m_Lists.list[i].idDatFile_Type);
    //            m_ListsNode.Nodes.Add("idDatFile.Id = " + m_ItersWithoutKeys.m_Lists.list[i].idDatFile_Id);
    //            TreeNode listNode = m_ListsNode.Nodes.Add("List");
    //            TreeNode mIntsNode = listNode.Nodes.Add("m_Ints");
    //            for (int j = 0; j < m_ItersWithoutKeys.m_Lists.list[i].List.m_Ints.Count; j++)
    //            {
    //                mIntsNode.Nodes.Add(m_ItersWithoutKeys.m_Lists.list[i].List.m_Ints[j].ToString());
    //            }
    //            mIntsNode.Nodes.Add("m_bSorted = " + m_ItersWithoutKeys.m_Lists.list[i].List.m_bSorted);
    //        }

    //        rootNode.Nodes.Add("m_dwFlags = " + m_dwFlags);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });

    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class CAllIterationList
    //{
    //    public PList<PTaggedIterationList> m_Lists;

    //    public static CAllIterationList read(BinaryReader binaryReader)
    //    {
    //        CAllIterationList newObj = new CAllIterationList();
    //        newObj.m_Lists = PList<PTaggedIterationList>.read(binaryReader);
    //        return newObj;
    //    }
    //}

    //public class PTaggedIterationList
    //{
    //    public int idDatFile_Type;
    //    public int idDatFile_Id;
    //    public CMostlyConsecutiveIntSet List;

    //    public static PTaggedIterationList read(BinaryReader binaryReader)
    //    {
    //        PTaggedIterationList newObj = new PTaggedIterationList();
    //        newObj.idDatFile_Type = binaryReader.ReadInt32();
    //        newObj.idDatFile_Id = binaryReader.ReadInt32();
    //        newObj.List = CMostlyConsecutiveIntSet.read(binaryReader);
    //        return newObj;
    //    }
    //}

    //public class CMostlyConsecutiveIntSet
    //{
    //    public List<int> m_Ints = new List<int>();
    //    public bool m_bSorted;

    //    public static CMostlyConsecutiveIntSet read(BinaryReader binaryReader)
    //    {
    //        CMostlyConsecutiveIntSet newObj = new CMostlyConsecutiveIntSet();
    //        newObj.m_Ints.Add(binaryReader.ReadInt32());
    //        newObj.m_Ints.Add(binaryReader.ReadInt32());
    //        newObj.m_bSorted = binaryReader.ReadBoolean();
    //        Util.readToAlign(binaryReader);

    //        return newObj;
    //    }
    //}

    //public class DDD_BeginDDDMessage : Message
    //{
    //    public uint m_cbDataExpected;
    //    public PList<MissingIteration> m_MissingIterations;

    //    public static DDD_BeginDDDMessage read(BinaryReader binaryReader)
    //    {
    //        DDD_BeginDDDMessage newObj = new DDD_BeginDDDMessage();
    //        newObj.m_cbDataExpected = binaryReader.ReadUInt32();

    //        newObj.m_MissingIterations = PList<MissingIteration>.read(binaryReader);

    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView)
    //    {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();

    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });

    //        rootNode.Nodes.Add("m_cbDataExpected = " + m_cbDataExpected);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });

    //        TreeNode m_MissingIterationsNode = rootNode.Nodes.Add("m_MissingIterations = ");
    //        ContextInfo.AddToList(new ContextInfo { Length = m_MissingIterations.Length }, updateDataIndex: false);
    //        // Skip PList count dword
    //        ContextInfo.DataIndex += 4;
    //        for (int i = 0; i<m_MissingIterations.list.Count; i++)
    //        {
    //            int ContextLength = 8 + 4 + m_MissingIterations.list[i].IDsToDownload.Length + m_MissingIterations.list[i].IDsToPurge.Length;
    //            ContextInfo.AddToList(new ContextInfo { Length = ContextLength }, updateDataIndex: false);
    //            TreeNode newMissingIterationNode;
    //            newMissingIterationNode = m_MissingIterationsNode.Nodes.Add("MissingIteration");
    //            m_MissingIterations.list[i].contributeToTreeNode(newMissingIterationNode);
    //        }

    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class MissingIteration
    //{
    //    public long idDatFile;
    //    public int idIteration;
    //    public PList<uint> IDsToDownload;
    //    public PList<uint> IDsToPurge;

    //    public static MissingIteration read(BinaryReader binaryReader)
    //    {
    //        MissingIteration newObj = new MissingIteration();
    //        newObj.idDatFile = binaryReader.ReadInt32() | binaryReader.ReadInt32();
    //        newObj.idIteration = binaryReader.ReadInt32();
    //        newObj.IDsToDownload = PList<uint>.read(binaryReader);
    //        newObj.IDsToPurge = PList<uint>.read(binaryReader);
    //        return newObj;
    //    }

    //    public void contributeToTreeNode(TreeNode node)
    //    {
    //        TreeNode ilistIIDNode = node.Nodes.Add("idDatFile = " + idDatFile);
    //        ContextInfo.AddToList(new ContextInfo { Length = 8 });

    //        TreeNode ilistLocNode = node.Nodes.Add("idIteration = " + idIteration);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });

    //        TreeNode IDsToDownloadNode = node.Nodes.Add("IDsToDownload = ");
    //        ContextInfo.AddToList(new ContextInfo { Length = IDsToDownload.Length }, updateDataIndex: false);
    //        // Skip PList count dword
    //        ContextInfo.DataIndex += 4;
    //        for (int i = 0; i < IDsToDownload.list.Count; i++)
    //        {
    //            IDsToDownloadNode.Nodes.Add(Utility.FormatHex(IDsToDownload.list[i]));
    //            ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        }

    //        TreeNode IDsToPurgeNode = node.Nodes.Add("IDsToPurge = ");
    //        ContextInfo.AddToList(new ContextInfo { Length = IDsToPurge.Length }, updateDataIndex: false);
    //        // Skip PList count dword
    //        ContextInfo.DataIndex += 4;
    //        for (int i = 0; i < IDsToPurge.list.Count; i++)
    //        {
    //            IDsToPurgeNode.Nodes.Add(Utility.FormatHex(IDsToPurge.list[i]));
    //            ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        }
    //    }
    //}

    //public class DDD_DataMessage : Message
    //{
    //    public long m_idDatFile;
    //    public uint m_qdid_Type;
    //    public uint m_qdid_Id;
    //    public int m_idIteration;
    //    public bool m_bCompressed;
    //    public Cache_Pack_t m_cpData;

    //    public static DDD_DataMessage read(BinaryReader binaryReader)
    //    {
    //        DDD_DataMessage newObj = new DDD_DataMessage();
            
    //        newObj.m_idDatFile = binaryReader.ReadUInt32() | binaryReader.ReadUInt32();
    //        newObj.m_qdid_Type = binaryReader.ReadUInt32();
    //        newObj.m_qdid_Id = binaryReader.ReadUInt32();
    //        newObj.m_idIteration = binaryReader.ReadInt32();
    //        newObj.m_bCompressed = binaryReader.ReadBoolean();

    //        newObj.m_cpData = Cache_Pack_t.read(binaryReader);

    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView)
    //    {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();

    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });

    //        rootNode.Nodes.Add("m_idDatFile = " + m_idDatFile);
    //        ContextInfo.AddToList(new ContextInfo { Length = 8 });

    //        rootNode.Nodes.Add("m_qdid.Type = " + m_qdid_Type);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });

    //        rootNode.Nodes.Add("m_qdid.ID = " + Utility.FormatHex(m_qdid_Id));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });

    //        rootNode.Nodes.Add("m_idIteration = " + m_idIteration);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });

    //        rootNode.Nodes.Add("m_bCompressed = " + m_bCompressed);
    //        ContextInfo.AddToList(new ContextInfo { Length = 1 });

    //        TreeNode m_cpDataNode = rootNode.Nodes.Add("m_cpData = ");
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 + 4 + m_cpData.m_buff.Length}, updateDataIndex: false);
    //        m_cpDataNode.Nodes.Add("m_iVersion = " + m_cpData.m_iVersion);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        m_cpDataNode.Nodes.Add("m_buff = " + m_cpData.m_buff);
    //        ContextInfo.DataIndex += 4; // skip m_cpData.m_buff Size
    //        ContextInfo.AddToList(new ContextInfo { Length = m_cpData.m_buff.Length });

    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class Cache_Pack_t
    //{
    //    public int m_iVersion;
    //    public byte[] m_buff;

    //    public static Cache_Pack_t read(BinaryReader binaryReader)
    //    {
    //        Cache_Pack_t newObj = new Cache_Pack_t();
    //        newObj.m_iVersion = binaryReader.ReadInt32();
    //        newObj.m_buff = binaryReader.ReadBytes(binaryReader.ReadInt32());
    //        return newObj;
    //    }
    //}

    ///// <summary>
    ///// This message is blank
    ///// </summary>
    //public class DDD_EndDDDMessage : Message
    //{
    //    public static DDD_EndDDDMessage read(BinaryReader binaryReader)
    //    {
    //        DDD_EndDDDMessage newObj = new DDD_EndDDDMessage();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView)
    //    {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class DDD_RequestDataMessage : Message
    //{
    //    public uint m_qdid_Id; // QualifiedDataID
    //    public uint m_qdid_Type; // QualifiedDataID

    //    public static DDD_RequestDataMessage read(BinaryReader binaryReader)
    //    {
    //        DDD_RequestDataMessage newObj = new DDD_RequestDataMessage();
    //        newObj.m_qdid_Type = binaryReader.ReadUInt32();
    //        newObj.m_qdid_Id = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView)
    //    {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();

    //        rootNode.Nodes.Add("m_qdid.Type = " + m_qdid_Type);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("m_qdid.ID = " + Utility.FormatHex(m_qdid_Id));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}
}
