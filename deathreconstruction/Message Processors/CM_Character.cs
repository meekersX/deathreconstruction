using System.Collections.Generic;
using System.IO;
using deathreconstruction;

public class CM_Character
{

    //public override bool acceptMessageData(BinaryReader messageDataReader, TreeView outputTreeView)
    //{
    //    bool handled = true;

    //    PacketOpcode opcode = Util.readOpcode(messageDataReader);
    //    switch (opcode)
    //    {
    //        case PacketOpcode.Evt_Character__TeleToPKLArena_ID:
    //        case PacketOpcode.Evt_Character__TeleToPKArena_ID:
    //        case PacketOpcode.Evt_Character__TeleToLifestone_ID:
    //        case PacketOpcode.Evt_Character__LoginCompleteNotification_ID:
    //        case PacketOpcode.Evt_Character__RequestPing_ID:
    //        case PacketOpcode.Evt_Character__ClearPlayerConsentList_ID:
    //        case PacketOpcode.Evt_Character__DisplayPlayerConsentList_ID:
    //        case PacketOpcode.Evt_Character__Suicide_ID:
    //        case PacketOpcode.Evt_Character__TeleToMarketplace_ID:
    //        case PacketOpcode.Evt_Character__EnterPKLite_ID:
    //            {
    //                EmptyMessage message = new EmptyMessage(opcode);
    //                message.contributeToTreeView(outputTreeView);
    //                ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__ReturnPing_ID:
    //            {
    //                EmptyMessage message = new EmptyMessage(opcode);
    //                message.contributeToTreeView(outputTreeView);
    //                ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__EnterGame_ServerReady_ID:
    //            {
    //                EmptyMessage message = new EmptyMessage(opcode);
    //                message.contributeToTreeView(outputTreeView);
    //                ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__PlayerOptionChangedEvent_ID:
    //            {
    //                PlayerOptionChangedEvent message = PlayerOptionChangedEvent.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__StartBarber_ID:
    //            {
    //                StartBarber message = StartBarber.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__AbuseLogRequest_ID:
    //            {
    //                AbuseLogRequest message = AbuseLogRequest.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__AddShortCut_ID:
    //            {
    //                AddShortCut message = AddShortCut.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__RemoveShortCut_ID:
    //            {
    //                RemoveShortCut message = RemoveShortCut.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__CharacterOptionsEvent_ID:
    //            {
    //                CharacterOptionsEvent message = CharacterOptionsEvent.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__QueryAge_ID:
    //            {
    //                QueryAge message = QueryAge.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__QueryAgeResponse_ID:
    //            {
    //                QueryAgeResponse message = QueryAgeResponse.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__QueryBirth_ID:
    //            {
    //                QueryBirth message = QueryBirth.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__AddSpellFavorite_ID:
    //            {
    //                AddSpellFavorite message = AddSpellFavorite.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__RemoveSpellFavorite_ID:
    //            {
    //                RemoveSpellFavorite message = RemoveSpellFavorite.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__RemoveFromPlayerConsentList_ID:
    //            {
    //                RemoveFromPlayerConsentList message = RemoveFromPlayerConsentList.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__AddPlayerPermission_ID:
    //            {
    //                AddPlayerPermission message = AddPlayerPermission.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__RemovePlayerPermission_ID:
    //            {
    //                RemovePlayerPermission message = RemovePlayerPermission.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__SetDesiredComponentLevel_ID:
    //            {
    //                SetDesiredComponentLevel message = SetDesiredComponentLevel.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__ConfirmationRequest_ID:
    //            {
    //                ConfirmationRequest message = ConfirmationRequest.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__ConfirmationResponse_ID:
    //            {
    //                ConfirmationResponse message = ConfirmationResponse.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__ConfirmationDone_ID:
    //            {
    //                ConfirmationDone message = ConfirmationDone.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.Evt_Character__SpellbookFilterEvent_ID:
    //            {
    //                SpellbookFilterEvent message = SpellbookFilterEvent.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        // TODO: Missing Evt_Character__Rename_ID
    //        case PacketOpcode.Evt_Character__FinishBarber_ID:
    //            {
    //                FinishBarber message = FinishBarber.read(messageDataReader);
    //                message.contributeToTreeView(outputTreeView);
    //                break;
    //            }
    //        case PacketOpcode.CHARACTER_ERROR_EVENT:
    //            {
    //                CharacterErrorEvent message = CharacterErrorEvent.read(messageDataReader);
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

    //public class PlayerOptionChangedEvent : Message {
    //    public PlayerOption i_po;
    //    public int i_value;

    //    public static PlayerOptionChangedEvent read(BinaryReader binaryReader) {
    //        PlayerOptionChangedEvent newObj = new PlayerOptionChangedEvent();
    //        newObj.i_po = (PlayerOption)binaryReader.ReadUInt32();
    //        newObj.i_value = binaryReader.ReadInt32();
    //        Util.readToAlign(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_po = " + i_po);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_value = " + i_value);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    public class ShortCutData {
        public int index_;
        public uint objectID_;
        public uint spellID_;
        public int Length = 12;

        public static ShortCutData read(BinaryReader binaryReader) {
            ShortCutData newObj = new ShortCutData();
            newObj.index_ = binaryReader.ReadInt32();
            newObj.objectID_ = binaryReader.ReadUInt32();
            newObj.spellID_ = binaryReader.ReadUInt32();
            return newObj;
        }
    }

    public class ShortCutManager {
        public List<ShortCutData> shortCuts_ = new List<ShortCutData>();
        public int Length;

        public static ShortCutManager read(BinaryReader binaryReader) {
            var startPosition = binaryReader.BaseStream.Position;
            uint numShortcuts = binaryReader.ReadUInt32();
            ShortCutManager newObj = new ShortCutManager();
            for (int i = 0; i < numShortcuts; ++i) {
                newObj.shortCuts_.Add(ShortCutData.read(binaryReader));
            }
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
            return newObj;
        }
    }

    public class PlayerModule {
        public enum PlayerModulePackHeader {
            PM_Packed_None = 0,
            PM_Packed_ShortCutManager = (1 << 0),
            PM_Packed_SquelchList = (1 << 1),
            PM_Packed_MultiSpellLists = (1 << 2),
            PM_Packed_DesiredComps = (1 << 3),
            PM_Packed_ExtendedMultiSpellLists = (1 << 4),
            PM_Packed_SpellbookFilters = (1 << 5),
            PM_Packed_2ndCharacterOptions = (1 << 6),
            PM_Packed_TimeStampFormat = (1 << 7),
            PM_Packed_GenericQualitiesData = (1 << 8),
            PM_Packed_GameplayOptions = (1 << 9),
            PM_Packed_8_SpellLists = (1 << 10)
        }

        public uint header;
        public uint options_;
        public ShortCutManager shortcuts_;
        public PList<SpellID>[] favorite_spells_ = new PList<SpellID>[8];
        public PackableHashTable<uint, int> desired_comps_ = new PackableHashTable<uint, int>();
        public uint spell_filters_;
        public uint options2;
        public PStringChar m_TimeStampFormat;
        public GenericQualitiesData m_pPlayerOptionsData;
        public PackObjPropertyCollection m_colGameplayOptions;
        public int Length;
        public byte padding;
        public List<string> packedItems; // For display purposes

        public static PlayerModule read(BinaryReader binaryReader) {
            PlayerModule newObj = new PlayerModule();
            newObj.packedItems = new List<string>();
            var startPosition = binaryReader.BaseStream.Position;
            newObj.header = binaryReader.ReadUInt32();
            newObj.options_ = binaryReader.ReadUInt32();
            if ((newObj.header & (uint)PlayerModulePackHeader.PM_Packed_ShortCutManager) != 0) {
                newObj.shortcuts_ = ShortCutManager.read(binaryReader);
                newObj.packedItems.Add(PlayerModulePackHeader.PM_Packed_ShortCutManager.ToString());
            }

            newObj.favorite_spells_[0] = PList<SpellID>.read(binaryReader);
            if ((newObj.header & (uint)PlayerModulePackHeader.PM_Packed_MultiSpellLists) != 0) {
                for (int i = 1; i < 5; ++i) {
                    newObj.favorite_spells_[i] = PList<SpellID>.read(binaryReader);
                }
                newObj.packedItems.Add(PlayerModulePackHeader.PM_Packed_MultiSpellLists.ToString());
            } else if ((newObj.header & (uint)PlayerModulePackHeader.PM_Packed_ExtendedMultiSpellLists) != 0) {
                for (int i = 1; i < 7; ++i) {
                    newObj.favorite_spells_[i] = PList<SpellID>.read(binaryReader);
                }
                newObj.packedItems.Add(PlayerModulePackHeader.PM_Packed_ExtendedMultiSpellLists.ToString());
            } else if ((newObj.header & (uint)PlayerModulePackHeader.PM_Packed_8_SpellLists) != 0) {
                for (int i = 1; i < 8; ++i) {
                    newObj.favorite_spells_[i] = PList<SpellID>.read(binaryReader);
                }
                newObj.packedItems.Add(PlayerModulePackHeader.PM_Packed_8_SpellLists.ToString());
            }
            if ((newObj.header & (uint)PlayerModulePackHeader.PM_Packed_DesiredComps) != 0) {
                newObj.desired_comps_ = PackableHashTable<uint, int>.read(binaryReader);
                newObj.packedItems.Add(PlayerModulePackHeader.PM_Packed_DesiredComps.ToString());
            }
            if ((newObj.header & (uint)PlayerModulePackHeader.PM_Packed_SpellbookFilters) != 0) {
                newObj.spell_filters_ = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PlayerModulePackHeader.PM_Packed_SpellbookFilters.ToString());
            } else {
                newObj.spell_filters_ = 0x3FFF;
            }
            if ((newObj.header & (uint)PlayerModulePackHeader.PM_Packed_2ndCharacterOptions) != 0) {
                newObj.options2 = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PlayerModulePackHeader.PM_Packed_2ndCharacterOptions.ToString());
            } else {
                newObj.options2 = 0x948700;
            }
            if ((newObj.header & (uint)PlayerModulePackHeader.PM_Packed_TimeStampFormat) != 0) {
                newObj.m_TimeStampFormat = PStringChar.read(binaryReader);
                newObj.packedItems.Add(PlayerModulePackHeader.PM_Packed_TimeStampFormat.ToString());
            }
            if ((newObj.header & (uint)PlayerModulePackHeader.PM_Packed_GenericQualitiesData) != 0)
            {
                newObj.m_pPlayerOptionsData = GenericQualitiesData.read(binaryReader);
                newObj.packedItems.Add(PlayerModulePackHeader.PM_Packed_GenericQualitiesData.ToString());
            }
            if ((newObj.header & (uint)PlayerModulePackHeader.PM_Packed_GameplayOptions) != 0)
            {
                newObj.m_colGameplayOptions = PackObjPropertyCollection.read(binaryReader);
                newObj.packedItems.Add(PlayerModulePackHeader.PM_Packed_GameplayOptions.ToString());
            }
            newObj.padding = Util.readToAlign(binaryReader); // Align to dword boundary
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
            return newObj;
        }
    }

    public class GenericQualitiesData
    {
        public enum GenericQualitiesPackHeader
        {
            Packed_None = 0,
            Packed_IntStats = 1,
            Packed_BoolStats = 2,
            Packed_FloatStats = 4,
            Packed_StringStats = 8,
        };

        public uint header;
        public PackableHashTable<STypeInt, int> m_pIntStatsTable = new PackableHashTable<STypeInt, int>();
        public PackableHashTable<STypeBool, int> m_pBoolStatsTable = new PackableHashTable<STypeBool, int>();
        public PackableHashTable<STypeFloat, double> m_pFloatStatsTable = new PackableHashTable<STypeFloat, double>();
        public PackableHashTable<uint, PStringChar> m_pStrStatsTable = new PackableHashTable<uint, PStringChar>();
        public int Length;
        public List<string> packedItems; // For display purposes

        public static GenericQualitiesData read(BinaryReader binaryReader)
        {
            GenericQualitiesData newObj = new GenericQualitiesData();
            var startPosition = binaryReader.BaseStream.Position;
            newObj.packedItems = new List<string>();
            newObj.header = binaryReader.ReadUInt32();
            if ((newObj.header & (uint)GenericQualitiesPackHeader.Packed_IntStats) != 0)
            {
                newObj.m_pIntStatsTable = PackableHashTable<STypeInt, int>.read(binaryReader);
                newObj.packedItems.Add(GenericQualitiesPackHeader.Packed_IntStats.ToString());
            }
            if ((newObj.header & (uint)GenericQualitiesPackHeader.Packed_BoolStats) != 0)
            {
                newObj.m_pBoolStatsTable = PackableHashTable<STypeBool, int>.read(binaryReader);
                newObj.packedItems.Add(GenericQualitiesPackHeader.Packed_BoolStats.ToString());
            }
            if ((newObj.header & (uint)GenericQualitiesPackHeader.Packed_FloatStats) != 0)
            {
                newObj.m_pFloatStatsTable = PackableHashTable<STypeFloat, double>.read(binaryReader);
                newObj.packedItems.Add(GenericQualitiesPackHeader.Packed_FloatStats.ToString());
            }
            if ((newObj.header & (uint)GenericQualitiesPackHeader.Packed_StringStats) != 0)
            {
                newObj.m_pStrStatsTable = PackableHashTable<uint, PStringChar>.read(binaryReader);
                newObj.packedItems.Add(GenericQualitiesPackHeader.Packed_StringStats.ToString());
            }
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
            return newObj;
        }
    }

    public class PackObjPropertyCollection
    {
        public uint i_iVersion; // AKA g_TurbineCorePackVersion: will always be 0x00000002.
        public List<BaseProperty> PropertyCollection = new List<BaseProperty>();
        public int Length;

        public static PackObjPropertyCollection read(BinaryReader binaryReader)
        {
            PackObjPropertyCollection newObj = new PackObjPropertyCollection();
            var startPosition = binaryReader.BaseStream.Position;
            newObj.i_iVersion = binaryReader.ReadUInt32();
            byte m_numBuckets = binaryReader.ReadByte();
            byte m_numElements = binaryReader.ReadByte();
            for (byte i = 0; i < m_numElements; i++)
            {
                newObj.PropertyCollection.Add(BaseProperty.read(binaryReader));
            }
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
            return newObj;
        }
    }

    public enum OptionProperty
    {
        Option_TextType_Property = 0x1000007F,
        Option_ActiveOpacity_Property = 0x10000081,
        Option_Placement_X_Property = 0x10000086,
        Option_Placement_Y_Property = 0x10000087,
        Option_Placement_Width_Property = 0x10000088,
        Option_Placement_Height_Property = 0x10000089,
        Option_DefaultOpacity_Property = 0x10000080,
        Option_Placement_Visibility_Property = 0x1000008a,
        Option_Placement_Property = 0x1000008b,
        Option_PlacementArray_Property = 0x1000008c,
        Option_Placement_Title_Property = 0x1000008d,
    }

    //public enum ChatTextFilter : ulong
    //{
    //    ID_ChatOption_TextFilter_Tells = 0x18,
    //    ID_ChatOption_TextFilter_AreaSpeech = 0x1004,
    //    ID_ChatOption_TextFilter_Magic = 0x20080,
    //    ID_ChatOption_TextFilter_Allegience = 0x40C00,
    //    ID_ChatOption_TextFilter_Fellowship = 0x80000,
    //    ID_ChatOption_TextFilter_Combat = 0x600040,
    //    ID_ChatOption_TextFilter_Error = 0x4000000,
    //    ID_ChatOption_TextFilter_General = 0x8000000,
    //    ID_ChatOption_TextFilter_Trade = 0x10000000,
    //    ID_ChatOption_TextFilter_LFG = 0x20000000,
    //    ID_ChatOption_TextFilter_Roleplay = 0x40000000,
    //    ID_ChatOption_TextFilter_Gameplay = 0x83912021,
    //    ID_ChatOption_TextFilter_Society = 0x100000000,
    //}

    public class BaseProperty
    {
        public OptionProperty key;
        public OptionProperty m_pcPropertyDesc;
        public float floatPropertyValue;
        public byte bytePropertyValue;
        public uint intPropertyValue;
        public ulong int64PropertyValue;
        public string m_LiteralValue;
        public byte m_Override;
        public uint m_stringID;
        public uint m_tableID;
        public uint bHasStrings;
        public byte m_numBuckets;
        public byte m_numElements;
        public byte boolPropertyValue;
        public uint num_properties;
        public List<BaseProperty> PropertyCollectionValue;
        public int Length;

        public static BaseProperty read(BinaryReader binaryReader)
        {
            BaseProperty newObj = new BaseProperty();
            var startPosition = binaryReader.BaseStream.Position;
            newObj.key = (OptionProperty)binaryReader.ReadUInt32();
            switch (newObj.key)
            {
                case OptionProperty.Option_ActiveOpacity_Property:
                    newObj.m_pcPropertyDesc = (OptionProperty)binaryReader.ReadUInt32();
                    newObj.floatPropertyValue = binaryReader.ReadSingle();
                    break;
                case OptionProperty.Option_DefaultOpacity_Property:
                    newObj.m_pcPropertyDesc = (OptionProperty)binaryReader.ReadUInt32();
                    newObj.floatPropertyValue = binaryReader.ReadSingle();
                    break;
                case OptionProperty.Option_Placement_X_Property:
                    newObj.m_pcPropertyDesc = (OptionProperty)binaryReader.ReadUInt32();
                    newObj.intPropertyValue = binaryReader.ReadUInt32();
                    break;
                case OptionProperty.Option_Placement_Y_Property:
                    newObj.m_pcPropertyDesc = (OptionProperty)binaryReader.ReadUInt32();
                    newObj.intPropertyValue = binaryReader.ReadUInt32();
                    break;
                case OptionProperty.Option_Placement_Width_Property:
                    newObj.m_pcPropertyDesc = (OptionProperty)binaryReader.ReadUInt32();
                    newObj.intPropertyValue = binaryReader.ReadUInt32();
                    break;
                case OptionProperty.Option_Placement_Height_Property:
                    newObj.m_pcPropertyDesc = (OptionProperty)binaryReader.ReadUInt32();
                    newObj.intPropertyValue = binaryReader.ReadUInt32();
                    break;
                case OptionProperty.Option_Placement_Visibility_Property:
                    newObj.m_pcPropertyDesc = (OptionProperty)binaryReader.ReadUInt32();
                    newObj.boolPropertyValue = binaryReader.ReadByte();
                    break;
                case OptionProperty.Option_Placement_Title_Property:
                    newObj.m_pcPropertyDesc = (OptionProperty)binaryReader.ReadUInt32();
                    newObj.m_Override = binaryReader.ReadByte();
                    if (newObj.m_Override == 1)
                    {
                        newObj.m_LiteralValue = Util.readUnicodeString(binaryReader);
                    }
                    else
                    {
                        newObj.m_stringID = binaryReader.ReadUInt32();
                        newObj.m_tableID = binaryReader.ReadUInt32();
                    }
                    // This variable is set in the StringInfo::Serialize function.
                    newObj.bHasStrings = binaryReader.ReadUInt32();
                    // These next two are set in the SerializeIntrusiveHashTable function.
                    newObj.m_numBuckets = binaryReader.ReadByte();
                    newObj.m_numElements = binaryReader.ReadByte();
                    break;
                case OptionProperty.Option_TextType_Property:
                    newObj.m_pcPropertyDesc = (OptionProperty)binaryReader.ReadUInt32();
                    newObj.int64PropertyValue = binaryReader.ReadUInt64();
                    break;
                case OptionProperty.Option_PlacementArray_Property:
                    newObj.m_pcPropertyDesc = (OptionProperty)binaryReader.ReadUInt32();
                    newObj.num_properties = binaryReader.ReadUInt32();
                    newObj.PropertyCollectionValue = new List<BaseProperty>();
                    for (uint i = 0; i < newObj.num_properties; i++)
                    {
                        newObj.PropertyCollectionValue.Add(BaseProperty.read(binaryReader));
                        newObj.Length += newObj.PropertyCollectionValue[(int)i].Length;
                    }
                    break;
                case OptionProperty.Option_Placement_Property:
                    byte m_numBuckets = binaryReader.ReadByte();
                    byte m_numElements = binaryReader.ReadByte();
                    newObj.PropertyCollectionValue = new List<BaseProperty>();
                    for (byte i = 0; i < m_numElements; i++)
                    {
                        newObj.PropertyCollectionValue.Add(BaseProperty.read(binaryReader));
                        newObj.Length += newObj.PropertyCollectionValue[(int)i].Length;
                    }
                    break;
            }
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
            return newObj;
        }
    }

    //public class CharacterOptionsEvent : Message {
    //    public PlayerModule i_pMod;

    //    public static CharacterOptionsEvent read(BinaryReader binaryReader) {
    //        CharacterOptionsEvent newObj = new CharacterOptionsEvent();
    //        newObj.i_pMod = PlayerModule.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        TreeNode playerModuleNode = rootNode.Nodes.Add("i_pMod = ");
    //        ContextInfo.AddToList(new ContextInfo { Length = i_pMod.Length }, updateDataIndex: false);
    //        i_pMod.contributeToTreeNode(playerModuleNode);
    //        treeView.Nodes.Add(rootNode);
    //        playerModuleNode.Expand();
    //    }
    //}

    //public class AddShortCut : Message {
    //    public ShortCutData shortcut;

    //    public static AddShortCut read(BinaryReader binaryReader) {
    //        AddShortCut newObj = new AddShortCut();
    //        newObj.shortcut = ShortCutData.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        TreeNode shortcutNode = rootNode.Nodes.Add("shortcut = ");
    //        ContextInfo.AddToList(new ContextInfo { Length = shortcut.Length }, updateDataIndex: false);
    //        shortcut.contributeToTreeNode(shortcutNode);
    //        treeView.Nodes.Add(rootNode);
    //        rootNode.ExpandAll();
    //    }
    //}

    //public class AddSpellFavorite : Message {
    //    public SpellID i_spid;
    //    public uint i_index;
    //    public uint i_list;

    //    public static AddSpellFavorite read(BinaryReader binaryReader) {
    //        AddSpellFavorite newObj = new AddSpellFavorite();
    //        newObj.i_spid = (SpellID)binaryReader.ReadUInt32();
    //        newObj.i_index = binaryReader.ReadUInt32();
    //        newObj.i_list = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_spid = " + i_spid);
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.SpellID_uint });
    //        rootNode.Nodes.Add("i_index = " + i_index);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_list = " + i_list);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class ConfirmationResponse : Message {
    //    public uint i_confirmType;
    //    public uint i_context;
    //    public uint i_bAccepted;

    //    public static ConfirmationResponse read(BinaryReader binaryReader) {
    //        ConfirmationResponse newObj = new ConfirmationResponse();
    //        newObj.i_confirmType = binaryReader.ReadUInt32();
    //        newObj.i_context = binaryReader.ReadUInt32();
    //        newObj.i_bAccepted = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_confirmType = " + (ConfirmationType)i_confirmType);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_context = " + i_context);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_bAccepted = " + i_bAccepted);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class QueryAge : Message {
    //    public uint i_target;

    //    public static QueryAge read(BinaryReader binaryReader) {
    //        QueryAge newObj = new QueryAge();
    //        newObj.i_target = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_target = " + i_target);
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class QueryBirth : Message {
    //    public uint i_target;

    //    public static QueryBirth read(BinaryReader binaryReader) {
    //        QueryBirth newObj = new QueryBirth();
    //        newObj.i_target = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_target = " + i_target);
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class RemoveShortCut : Message {
    //    public uint i_index;

    //    public static RemoveShortCut read(BinaryReader binaryReader) {
    //        RemoveShortCut newObj = new RemoveShortCut();
    //        newObj.i_index = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_index = " + i_index);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class RemoveSpellFavorite : Message {
    //    public SpellID i_spid;
    //    public uint i_list;

    //    public static RemoveSpellFavorite read(BinaryReader binaryReader) {
    //        RemoveSpellFavorite newObj = new RemoveSpellFavorite();
    //        newObj.i_spid = (SpellID)binaryReader.ReadUInt32();
    //        newObj.i_list = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_spid = " + i_spid);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_list = " + i_list);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class SpellbookFilterEvent : Message {
    //    public uint i_options;

    //    public static SpellbookFilterEvent read(BinaryReader binaryReader) {
    //        SpellbookFilterEvent newObj = new SpellbookFilterEvent();
    //        newObj.i_options = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        var spellFilterNode = rootNode.Nodes.Add("i_options = " + Utility.FormatHex(i_options));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 }, updateDataIndex: false);
    //        foreach (SpellbookFilter filter in Enum.GetValues(typeof(SpellbookFilter)))
    //        {
    //            if ((i_options & (uint)filter) == (uint)filter && (uint)filter != 0)
    //            {
    //                spellFilterNode.Nodes.Add($"{Enum.GetName(typeof(SpellbookFilter), filter)}");
    //                ContextInfo.AddToList(new ContextInfo { Length = 4 }, updateDataIndex: false);
    //            }
    //        }
    //        treeView.Nodes.Add(rootNode);
    //        rootNode.ExpandAll();
    //    }
    //}

    //public class FinishBarber : Message {
    //    public uint i_base_palette;
    //    public uint i_head_object;
    //    public uint i_head_texture;
    //    public uint i_default_head_texture;
    //    public uint i_eyes_texture;
    //    public uint i_default_eyes_texture;
    //    public uint i_nose_texture;
    //    public uint i_default_nose_texture;
    //    public uint i_mouth_texture;
    //    public uint i_default_mouth_texture;
    //    public uint i_skin_palette;
    //    public uint i_hair_palette;
    //    public uint i_eyes_palette;
    //    public uint i_setup_id;
    //    public uint i_option1;
    //    public uint i_option2;

    //    public static FinishBarber read(BinaryReader binaryReader) {
    //        FinishBarber newObj = new FinishBarber();
    //        newObj.i_base_palette = binaryReader.ReadUInt32();
    //        newObj.i_head_object = binaryReader.ReadUInt32();
    //        newObj.i_head_texture = binaryReader.ReadUInt32();
    //        newObj.i_default_head_texture = binaryReader.ReadUInt32();
    //        newObj.i_eyes_texture = binaryReader.ReadUInt32();
    //        newObj.i_default_eyes_texture = binaryReader.ReadUInt32();
    //        newObj.i_nose_texture = binaryReader.ReadUInt32();
    //        newObj.i_default_nose_texture = binaryReader.ReadUInt32();
    //        newObj.i_mouth_texture = binaryReader.ReadUInt32();
    //        newObj.i_default_mouth_texture = binaryReader.ReadUInt32();
    //        newObj.i_skin_palette = binaryReader.ReadUInt32();
    //        newObj.i_hair_palette = binaryReader.ReadUInt32();
    //        newObj.i_eyes_palette = binaryReader.ReadUInt32();
    //        newObj.i_setup_id = binaryReader.ReadUInt32();
    //        newObj.i_option1 = binaryReader.ReadUInt32();
    //        newObj.i_option2 = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_base_palette = " + Utility.FormatHex(i_base_palette));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_head_object = " + Utility.FormatHex(i_head_object));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_head_texture = " + Utility.FormatHex(i_head_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_default_head_texture = " + Utility.FormatHex(i_default_head_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_eyes_texture = " + Utility.FormatHex(i_eyes_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_default_eyes_texture = " + Utility.FormatHex(i_default_eyes_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_nose_texture = " + Utility.FormatHex(i_nose_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_default_nose_texture = " + Utility.FormatHex(i_default_nose_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_mouth_texture = " + Utility.FormatHex(i_mouth_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_default_mouth_texture = " + Utility.FormatHex(i_default_mouth_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_skin_palette = " + Utility.FormatHex(i_skin_palette));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_hair_palette = " + Utility.FormatHex(i_hair_palette));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_eyes_palette = " + Utility.FormatHex(i_eyes_palette));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_setup_id = " + Utility.FormatHex(i_setup_id));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_option1 = " + i_option1);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_option2 = " + i_option2);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class SetDesiredComponentLevel : Message {
    //    public uint i_wcid;
    //    public uint i_amount;

    //    public static SetDesiredComponentLevel read(BinaryReader binaryReader) {
    //        SetDesiredComponentLevel newObj = new SetDesiredComponentLevel();
    //        newObj.i_wcid = binaryReader.ReadUInt32();
    //        newObj.i_amount = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_wcid = " + i_wcid);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_amount = " + i_amount);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class AbuseLogRequest : Message {
    //    public PStringChar i_target;
    //    public int i_status;
    //    public PStringChar i_complaint;

    //    public static AbuseLogRequest read(BinaryReader binaryReader) {
    //        AbuseLogRequest newObj = new AbuseLogRequest();
    //        newObj.i_target = PStringChar.read(binaryReader);
    //        newObj.i_status = binaryReader.ReadInt32();
    //        newObj.i_complaint = PStringChar.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_target = " + i_target);
    //        ContextInfo.AddToList(new ContextInfo { Length = i_target.Length, DataType = DataType.Serialized_AsciiString });
    //        rootNode.Nodes.Add("i_status = " + i_status);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("i_complaint = " + i_complaint);
    //        ContextInfo.AddToList(new ContextInfo { Length = i_complaint.Length, DataType = DataType.Serialized_AsciiString });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class AddPlayerPermission : Message {
    //    public PStringChar i_targetName;

    //    public static AddPlayerPermission read(BinaryReader binaryReader) {
    //        AddPlayerPermission newObj = new AddPlayerPermission();
    //        newObj.i_targetName = PStringChar.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_targetName = " + i_targetName);
    //        ContextInfo.AddToList(new ContextInfo { Length = i_targetName.Length, DataType = DataType.Serialized_AsciiString });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class RemoveFromPlayerConsentList : Message {
    //    public PStringChar i_targetName;

    //    public static RemoveFromPlayerConsentList read(BinaryReader binaryReader) {
    //        RemoveFromPlayerConsentList newObj = new RemoveFromPlayerConsentList();
    //        newObj.i_targetName = PStringChar.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_targetName = " + i_targetName);
    //        ContextInfo.AddToList(new ContextInfo { Length = i_targetName.Length, DataType = DataType.Serialized_AsciiString });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class RemovePlayerPermission : Message {
    //    public PStringChar i_targetName;

    //    public static RemovePlayerPermission read(BinaryReader binaryReader) {
    //        RemovePlayerPermission newObj = new RemovePlayerPermission();
    //        newObj.i_targetName = PStringChar.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //        rootNode.Nodes.Add("i_targetName = " + i_targetName);
    //        ContextInfo.AddToList(new ContextInfo { Length = i_targetName.Length, DataType = DataType.Serialized_AsciiString });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    

    //public class QueryAgeResponse : Message {
    //    public PStringChar targetName;
    //    public PStringChar age;

    //    public static QueryAgeResponse read(BinaryReader binaryReader) {
    //        QueryAgeResponse newObj = new QueryAgeResponse();
    //        newObj.targetName = PStringChar.read(binaryReader);
    //        newObj.age = PStringChar.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //        rootNode.Nodes.Add("targetName = " + targetName);
    //        ContextInfo.AddToList(new ContextInfo { Length = targetName.Length, DataType = DataType.Serialized_AsciiString });
    //        rootNode.Nodes.Add("age = " + age);
    //        ContextInfo.AddToList(new ContextInfo { Length = age.Length, DataType = DataType.Serialized_AsciiString });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class ConfirmationDone : Message {
    //    public ConfirmationType confirm;
    //    public uint context;

    //    public static ConfirmationDone read(BinaryReader binaryReader) {
    //    ConfirmationDone newObj = new ConfirmationDone();
    //        newObj.confirm = (ConfirmationType)binaryReader.ReadUInt32();
    //        newObj.context = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //        rootNode.Nodes.Add("confirm = " + confirm);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("context = " + context);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class StartBarber : Message {
    //    public uint _base_palette;
    //    public uint _head_object;
    //    public uint _head_texture;
    //    public uint _default_head_texture;
    //    public uint _eyes_texture;
    //    public uint _default_eyes_texture;
    //    public uint _nose_texture;
    //    public uint _default_nose_texture;
    //    public uint _mouth_texture;
    //    public uint _default_mouth_texture;
    //    public uint _skin_palette;
    //    public uint _hair_palette;
    //    public uint _eyes_palette;
    //    public uint _setup_id;
    //    public uint option1;
    //    public uint option2;

    //    public static StartBarber read(BinaryReader binaryReader) {
    //        StartBarber newObj = new StartBarber();
    //        newObj._base_palette = binaryReader.ReadUInt32();
    //        newObj._head_object = binaryReader.ReadUInt32();
    //        newObj._head_texture = binaryReader.ReadUInt32();
    //        newObj._default_head_texture = binaryReader.ReadUInt32();
    //        newObj._eyes_texture = binaryReader.ReadUInt32();
    //        newObj._default_eyes_texture = binaryReader.ReadUInt32();
    //        newObj._nose_texture = binaryReader.ReadUInt32();
    //        newObj._default_nose_texture = binaryReader.ReadUInt32();
    //        newObj._mouth_texture = binaryReader.ReadUInt32();
    //        newObj._default_mouth_texture = binaryReader.ReadUInt32();
    //        newObj._skin_palette = binaryReader.ReadUInt32();
    //        newObj._hair_palette = binaryReader.ReadUInt32();
    //        newObj._eyes_palette = binaryReader.ReadUInt32();
    //        newObj._setup_id = binaryReader.ReadUInt32();
    //        newObj.option1 = binaryReader.ReadUInt32();
    //        newObj.option2 = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //        rootNode.Nodes.Add("_base_palette = " + Utility.FormatHex(_base_palette));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_head_object = " + Utility.FormatHex(_head_object));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_head_texture = " + Utility.FormatHex(_head_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_default_head_texture = " + Utility.FormatHex(_default_head_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_eyes_texture = " + Utility.FormatHex(_eyes_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_default_eyes_texture = " + Utility.FormatHex(_default_eyes_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_nose_texture = " + Utility.FormatHex(_nose_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_default_nose_texture = " + Utility.FormatHex(_default_nose_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_mouth_texture = " + Utility.FormatHex(_mouth_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_default_mouth_texture = " + Utility.FormatHex(_default_mouth_texture));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_skin_palette = " + Utility.FormatHex(_skin_palette));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_hair_palette = " + Utility.FormatHex(_hair_palette));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_eyes_palette = " + Utility.FormatHex(_eyes_palette));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("_setup_id = " + Utility.FormatHex(_setup_id));
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("option1 = " + option1);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("option2 = " + option2);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class ConfirmationRequest : Message {
    //    public ConfirmationType confirm;
    //    public uint context;
    //    public PStringChar userData;

    //    public static ConfirmationRequest read(BinaryReader binaryReader) {
    //        ConfirmationRequest newObj = new ConfirmationRequest();
    //        newObj.confirm = (ConfirmationType)binaryReader.ReadUInt32();
    //        newObj.context = binaryReader.ReadUInt32();
    //        newObj.userData = PStringChar.read(binaryReader);
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView) {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //        rootNode.Nodes.Add("confirm = " + confirm);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("context = " + context);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        rootNode.Nodes.Add("userData = " + userData);
    //        ContextInfo.AddToList(new ContextInfo { Length = userData.Length, DataType = DataType.Serialized_AsciiString });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}

    //public class CharacterErrorEvent : Message
    //{
    //    public uint _error;

    //    public static CharacterErrorEvent read(BinaryReader binaryReader)
    //    {
    //        CharacterErrorEvent newObj = new CharacterErrorEvent();
    //        newObj._error = binaryReader.ReadUInt32();
    //        return newObj;
    //    }

    //    public override void contributeToTreeView(TreeView treeView)
    //    {
    //        TreeNode rootNode = new TreeNode(this.GetType().Name);
    //        rootNode.Expand();
    //        ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });
    //        rootNode.Nodes.Add("_error = " + (charError)_error);
    //        ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //        treeView.Nodes.Add(rootNode);
    //    }
    //}
}
