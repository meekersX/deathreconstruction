using System.Collections.Generic;
using System.IO;

public class CM_Physics {
    public class Subpalette {
        public uint subID;
        public uint offset;
        public uint numcolors;

        public static Subpalette read(BinaryReader binaryReader) {
            Subpalette newObj = new Subpalette();
            newObj.subID = Util.readDataIDOfKnownType(0x4000000, binaryReader);
            newObj.offset = binaryReader.ReadByte();
            newObj.numcolors = binaryReader.ReadByte();
            return newObj;
        }
    }

    public class TextureMapChange {
        public byte part_index;
        public uint old_tex_id;
        public uint new_tex_id;

        public static TextureMapChange read(BinaryReader binaryReader) {
            TextureMapChange newObj = new TextureMapChange();
            newObj.part_index = binaryReader.ReadByte();
            newObj.old_tex_id = Util.readDataIDOfKnownType(0x5000000, binaryReader);
            newObj.new_tex_id = Util.readDataIDOfKnownType(0x5000000, binaryReader);
            return newObj;
        }
    }

    public class AnimPartChange {
        public byte part_index;
        public uint part_id;

        public static AnimPartChange read(BinaryReader binaryReader) {
            AnimPartChange newObj = new AnimPartChange();
            newObj.part_index = binaryReader.ReadByte();
            newObj.part_id = Util.readDataIDOfKnownType(0x1000000, binaryReader);
            return newObj;
        }
    }

    public class ObjDesc {
        public uint paletteID;
        public List<Subpalette> subpalettes = new List<Subpalette>(); // NOTE: This should be sorted on insertion or something, see ObjDesc::AddSubpalette
        public List<TextureMapChange> tmChanges = new List<TextureMapChange>();
        public List<AnimPartChange> apChanges = new List<AnimPartChange>();

        public static ObjDesc read(BinaryReader binaryReader) {
            ObjDesc newObj = new ObjDesc();
            binaryReader.ReadByte(); // Unk, should always be == 17

            byte numPalettes = binaryReader.ReadByte();
            byte numTMCs = binaryReader.ReadByte();
            byte numAPCs = binaryReader.ReadByte();

            if (numPalettes > 0) {
                newObj.paletteID = Util.readDataIDOfKnownType(0x4000000, binaryReader);
                for (int i = 0; i < numPalettes; ++i) {
                    newObj.subpalettes.Add(Subpalette.read(binaryReader));
                }
            }

            if (numTMCs > 0) {
                for (int i = 0; i < numTMCs; ++i) {
                    newObj.tmChanges.Add(TextureMapChange.read(binaryReader));
                }
            }

            if (numAPCs > 0) {
                for (int i = 0; i < numAPCs; ++i) {
                    newObj.apChanges.Add(AnimPartChange.read(binaryReader));
                }
            }

            Util.readToAlign(binaryReader);

            return newObj;
        }
    }

    public class ChildInfo { // Not actually a class in client, but is here for convenience
        public uint id;
        public uint location_id;

        public static ChildInfo read(BinaryReader binaryReader) {
            ChildInfo newObj = new ChildInfo();
            newObj.id = binaryReader.ReadUInt32();
            newObj.location_id = binaryReader.ReadUInt32();
            return newObj;
        }
    }

    public class PhysicsDesc {
        public enum PhysicsDescInfo {
            CSetup = (1 << 0),
            MTABLE = (1 << 1),
            VELOCITY = (1 << 2),
            ACCELERATION = (1 << 3),
            OMEGA = (1 << 4),
            PARENT = (1 << 5),
            CHILDREN = (1 << 6),
            OBJSCALE = (1 << 7),
            FRICTION = (1 << 8),
            ELASTICITY = (1 << 9),
            TIMESTAMPS = (1 << 10),
            STABLE = (1 << 11),
            PETABLE = (1 << 12),
            DEFAULT_SCRIPT = (1 << 13),
            DEFAULT_SCRIPT_INTENSITY = (1 << 14),
            POSITION = (1 << 15),
            MOVEMENT = (1 << 16),
            ANIMFRAME_ID = (1 << 17),
            TRANSLUCENCY = (1 << 18)
        }

        public uint bitfield;
        public uint state;
        public uint buff_length;
        public CM_Movement.MovementDataUnpack movement_buffer;
        public int autonomous_movement;
        public uint animframe_id;
        public Position pos = new Position();
        public uint mtable_id;      // Motion table. These are tag ids like animpartchange
        public uint stable_id;      // Sound table
        public uint phstable_id;    // Physics script table
        public uint setup_id;       // Model setup table
        public uint parent_id;
        public uint location_id;
        public List<ChildInfo> children = new List<ChildInfo>();
        public float object_scale;
        public float friction;
        public float elasticity;
        public float translucency;
        public Vector3 velocity = new Vector3();
        public Vector3 acceleration = new Vector3();
        public Vector3 omega = new Vector3();
        public PScriptType default_script;
        public float default_script_intensity;
        public ushort[] timestamps = new ushort[9];
        public List<string> packedItems; // For display purposes

        public static PhysicsDesc read(BinaryReader binaryReader) {
            PhysicsDesc newObj = new PhysicsDesc();
            newObj.packedItems = new List<string>();
            newObj.bitfield = binaryReader.ReadUInt32();
            newObj.state = binaryReader.ReadUInt32();

            if ((newObj.bitfield & (uint)PhysicsDescInfo.MOVEMENT) != 0) {
                // Note: the client uses the MOVEMENT_TS and SERVER_CONTROLLED_MOVE_TS from the timestamps array
                // in addition to the following movement data. See SmartBox::HandleCreateObject.
                newObj.buff_length = binaryReader.ReadUInt32();
                newObj.movement_buffer = CM_Movement.MovementDataUnpack.read(binaryReader);
                newObj.autonomous_movement = binaryReader.ReadInt32();
                newObj.packedItems.Add(PhysicsDescInfo.MOVEMENT.ToString());
            } else if ((newObj.bitfield & (uint)PhysicsDescInfo.ANIMFRAME_ID) != 0) {
                newObj.animframe_id = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PhysicsDescInfo.ANIMFRAME_ID.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.POSITION) != 0) {
                newObj.pos = Position.read(binaryReader);
                newObj.packedItems.Add(PhysicsDescInfo.POSITION.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.MTABLE) != 0) {
                newObj.mtable_id = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PhysicsDescInfo.MTABLE.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.STABLE) != 0) {
                newObj.stable_id = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PhysicsDescInfo.STABLE.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.PETABLE) != 0) {
                newObj.phstable_id = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PhysicsDescInfo.PETABLE.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.CSetup) != 0) {
                newObj.setup_id = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PhysicsDescInfo.CSetup.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.PARENT) != 0) {
                newObj.parent_id = binaryReader.ReadUInt32();
                newObj.location_id = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PhysicsDescInfo.PARENT.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.CHILDREN) != 0) {
                uint num_children = binaryReader.ReadUInt32();
                for (int i = 0; i < num_children; ++i) {
                    newObj.children.Add(ChildInfo.read(binaryReader));
                }
                newObj.packedItems.Add(PhysicsDescInfo.CHILDREN.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.OBJSCALE) != 0) {
                newObj.object_scale = binaryReader.ReadSingle();
                newObj.packedItems.Add(PhysicsDescInfo.OBJSCALE.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.FRICTION) != 0) {
                newObj.friction = binaryReader.ReadSingle();
                newObj.packedItems.Add(PhysicsDescInfo.FRICTION.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.ELASTICITY) != 0) {
                newObj.elasticity = binaryReader.ReadSingle();
                newObj.packedItems.Add(PhysicsDescInfo.ELASTICITY.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.TRANSLUCENCY) != 0) {
                newObj.translucency = binaryReader.ReadSingle();
                newObj.packedItems.Add(PhysicsDescInfo.TRANSLUCENCY.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.VELOCITY) != 0) {
                newObj.velocity.x = binaryReader.ReadSingle();
                newObj.velocity.y = binaryReader.ReadSingle();
                newObj.velocity.z = binaryReader.ReadSingle();
                newObj.packedItems.Add(PhysicsDescInfo.VELOCITY.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.ACCELERATION) != 0) {
                newObj.acceleration.x = binaryReader.ReadSingle();
                newObj.acceleration.y = binaryReader.ReadSingle();
                newObj.acceleration.z = binaryReader.ReadSingle();
                newObj.packedItems.Add(PhysicsDescInfo.ACCELERATION.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.OMEGA) != 0) {
                newObj.omega.x = binaryReader.ReadSingle();
                newObj.omega.y = binaryReader.ReadSingle();
                newObj.omega.z = binaryReader.ReadSingle();
                newObj.packedItems.Add(PhysicsDescInfo.OMEGA.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.DEFAULT_SCRIPT) != 0) {
                newObj.default_script = (PScriptType)binaryReader.ReadUInt32();
                newObj.packedItems.Add(PhysicsDescInfo.DEFAULT_SCRIPT.ToString());
            }

            if ((newObj.bitfield & (uint)PhysicsDescInfo.DEFAULT_SCRIPT_INTENSITY) != 0) {
                newObj.default_script_intensity = binaryReader.ReadSingle();
                newObj.packedItems.Add(PhysicsDescInfo.DEFAULT_SCRIPT_INTENSITY.ToString());
            }

            for (int i = 0; i < newObj.timestamps.Length; ++i) {
                newObj.timestamps[i] = binaryReader.ReadUInt16();
            }

            Util.readToAlign(binaryReader);

            return newObj;
        }
    }

    public class PublicWeenieDesc {
        public enum PublicWeenieDescPackHeader {
            PWD_Packed_None = 0,
            PWD_Packed_PluralName = (1 << 0),
            PWD_Packed_ItemsCapacity = (1 << 1),
            PWD_Packed_ContainersCapacity = (1 << 2),
            PWD_Packed_Value = (1 << 3),
            PWD_Packed_Useability = (1 << 4),
            PWD_Packed_UseRadius = (1 << 5),
            PWD_Packed_Monarch = (1 << 6),
            PWD_Packed_UIEffects = (1 << 7),
            PWD_Packed_AmmoType = (1 << 8),
            PWD_Packed_CombatUse = (1 << 9),
            PWD_Packed_Structure = (1 << 10),
            PWD_Packed_MaxStructure = (1 << 11),
            PWD_Packed_StackSize = (1 << 12),
            PWD_Packed_MaxStackSize = (1 << 13),
            PWD_Packed_ContainerID = (1 << 14),
            PWD_Packed_WielderID = (1 << 15),
            PWD_Packed_ValidLocations = (1 << 16),
            PWD_Packed_Location = (1 << 17),
            PWD_Packed_Priority = (1 << 18),
            PWD_Packed_TargetType = (1 << 19),
            PWD_Packed_BlipColor = (1 << 20),
            PWD_Packed_Burden = (1 << 21),
            PWD_Packed_SpellID = (1 << 22),
            PWD_Packed_RadarEnum = (1 << 23), 
            PWD_Packed_Workmanship = (1 << 24),
            PWD_Packed_HouseOwner = (1 << 25),
            PWD_Packed_HouseRestrictions = (1 << 26),
            PWD_Packed_PScript = (1 << 27),
            PWD_Packed_HookType = (1 << 28),
            PWD_Packed_HookItemTypes = (1 << 29),
            PWD_Packed_IconOverlay = (1 << 30),
            PWD_Packed_MaterialType = (1 << 31)
        }

        public enum PublicWeenieDescPackHeader2 {
            PWD2_Packed_None = 0,
            PWD2_Packed_IconUnderlay = (1 << 0),
            PWD2_Packed_CooldownID = (1 << 1),
            PWD2_Packed_CooldownDuration = (1 << 2),
            PWD2_Packed_PetOwner = (1 << 3),
        }

        public enum BitfieldIndex {
            BF_OPENABLE = (1 << 0),
            BF_INSCRIBABLE = (1 << 1),
            BF_STUCK = (1 << 2),
            BF_PLAYER = (1 << 3),
            BF_ATTACKABLE = (1 << 4),
            BF_PLAYER_KILLER = (1 << 5),
            BF_HIDDEN_ADMIN = (1 << 6),
            BF_UI_HIDDEN = (1 << 7),
            BF_BOOK = (1 << 8),
            BF_VENDOR = (1 << 9),
            BF_PKSWITCH = (1 << 10),
            BF_NPKSWITCH = (1 << 11),
            BF_DOOR = (1 << 12),
            BF_CORPSE = (1 << 13),
            BF_LIFESTONE = (1 << 14),
            BF_FOOD = (1 << 15),
            BF_HEALER = (1 << 16),
            BF_LOCKPICK = (1 << 17),
            BF_PORTAL = (1 << 18),
            // NOTE: Skip 1
            BF_ADMIN = (1 << 20),
            BF_FREE_PKSTATUS = (1 << 21),
            BF_IMMUNE_CELL_RESTRICTIONS = (1 << 22),
            BF_REQUIRES_PACKSLOT = (1 << 23),
            BF_RETAINED = (1 << 24),
            BF_PKLITE_PKSTATUS = (1 << 25),
            BF_INCLUDES_SECOND_HEADER = (1 << 26),
            BF_BINDSTONE = (1 << 27),
            BF_VOLATILE_RARE = (1 << 28),
            BF_WIELD_ON_USE = (1 << 29),
            BF_WIELD_LEFT = (1 << 30),
        }

        public uint header;
        public byte headerPadding;
        public uint header2;
        public PStringChar _name;
        public uint _wcid;
        public int _wcid_length;
        public uint _iconID;
        public int _iconID_length;
        public ITEM_TYPE _type;
        public uint _bitfield;
        public PStringChar _plural_name;
        public byte _itemsCapacity;
        public byte _containersCapacity;
        public AMMO_TYPE _ammoType;
        public uint _value;
        public ITEM_USEABLE _useability;
        public float _useRadius;
        public ITEM_TYPE _targetType;
        public uint _effects;
        public byte _combatUse;
        public ushort _structure;
        public ushort _maxStructure;
        public ushort _stackSize;
        public ushort _maxStackSize;
        public uint _containerID;
        public uint _wielderID;
        public uint _valid_locations;
        public uint _location;
        public uint _priority;
        public byte _blipColor;
        public RadarEnum _radar_enum;
        public ushort _pscript;
        public float _workmanship;
        public ushort _burden;
        public ushort _spellID;
        public uint _house_owner_iid;
        public CM_House.RestrictionDB _db;
        public uint _hook_item_types;
        public uint _monarch;
        public ushort _hook_type;
        public uint _iconOverlayID;
        public int _iconOverlayLength;
        public uint _iconUnderlayID;
        public int _iconUnderlayLength;
        public MaterialType _material_type;
        public uint _cooldown_id;
        public double _cooldown_duration;
        public uint _pet_owner;
        public byte endPadding;
        public List<string> packedItems; // For display purposes
        public int Length;

        public static PublicWeenieDesc read(BinaryReader binaryReader) {
            PublicWeenieDesc newObj = new PublicWeenieDesc();
            var startPosition = binaryReader.BaseStream.Position;
            newObj.packedItems = new List<string>();
            newObj.header = binaryReader.ReadUInt32();
            newObj._name = PStringChar.read(binaryReader);
            var wcidStart = binaryReader.BaseStream.Position;
            newObj._wcid = Util.readWClassIDCompressed(binaryReader);
            newObj._wcid_length = (int)(binaryReader.BaseStream.Position - wcidStart);
            var iconIdStart = binaryReader.BaseStream.Position;
            newObj._iconID = Util.readDataIDOfKnownType(0x6000000, binaryReader);
            newObj._iconID_length = (int)(binaryReader.BaseStream.Position - iconIdStart);
            newObj._type = (ITEM_TYPE)binaryReader.ReadUInt32();
            newObj._bitfield = binaryReader.ReadUInt32();

            newObj.headerPadding = Util.readToAlign(binaryReader);

            if ((newObj._bitfield & (uint)BitfieldIndex.BF_INCLUDES_SECOND_HEADER) != 0)
            {
                newObj.header2 = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_PluralName) != 0) {
                newObj._plural_name = PStringChar.read(binaryReader);
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_PluralName.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_ItemsCapacity) != 0) {
                newObj._itemsCapacity = binaryReader.ReadByte();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_ItemsCapacity.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_ContainersCapacity) != 0) {
                newObj._containersCapacity = binaryReader.ReadByte();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_ContainersCapacity.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_AmmoType) != 0) {
                newObj._ammoType = (AMMO_TYPE)binaryReader.ReadUInt16();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_AmmoType.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_Value) != 0) {
                newObj._value = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_Value.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_Useability) != 0) {
                newObj._useability = (ITEM_USEABLE)binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_Useability.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_UseRadius) != 0) {
                newObj._useRadius = binaryReader.ReadSingle();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_UseRadius.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_TargetType) != 0) {
                newObj._targetType = (ITEM_TYPE)binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_TargetType.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_UIEffects) != 0) {
                newObj._effects = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_UIEffects.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_CombatUse) != 0) {
                newObj._combatUse = binaryReader.ReadByte();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_CombatUse.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_Structure) != 0) {
                newObj._structure = binaryReader.ReadUInt16();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_Structure.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_MaxStructure) != 0) {
                newObj._maxStructure = binaryReader.ReadUInt16();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_MaxStructure.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_StackSize) != 0) {
                newObj._stackSize = binaryReader.ReadUInt16();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_StackSize.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_MaxStackSize) != 0) {
                newObj._maxStackSize = binaryReader.ReadUInt16();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_MaxStackSize.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_ContainerID) != 0) {
                newObj._containerID = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_ContainerID.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_WielderID) != 0) {
                newObj._wielderID = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_WielderID.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_ValidLocations) != 0) {
                newObj._valid_locations = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_ValidLocations.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_Location) != 0) {
                newObj._location = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_Location.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_Priority) != 0) {
                newObj._priority = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_Priority.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_BlipColor) != 0) {
                newObj._blipColor = binaryReader.ReadByte();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_BlipColor.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_RadarEnum) != 0) {
                newObj._radar_enum = (RadarEnum)binaryReader.ReadByte();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_RadarEnum.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_PScript) != 0) {
                newObj._pscript = binaryReader.ReadUInt16();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_PScript.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_Workmanship) != 0) {
                newObj._workmanship = binaryReader.ReadSingle();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_Workmanship.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_Burden) != 0) {
                newObj._burden = binaryReader.ReadUInt16();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_Burden.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_SpellID) != 0) {
                newObj._spellID = binaryReader.ReadUInt16();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_SpellID.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_HouseOwner) != 0) {
                newObj._house_owner_iid = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_HouseOwner.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_HouseRestrictions) != 0) {
                newObj._db = CM_House.RestrictionDB.read(binaryReader);
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_HouseRestrictions.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_HookItemTypes) != 0) {
                newObj._hook_item_types = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_HookItemTypes.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_Monarch) != 0) {
                newObj._monarch = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_Monarch.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_HookType) != 0) {
                newObj._hook_type = binaryReader.ReadUInt16();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_HookType.ToString());
            }

            if ((newObj.header & (uint)PublicWeenieDescPackHeader.PWD_Packed_IconOverlay) != 0) {
                var _iconOverlayStart = binaryReader.BaseStream.Position;
                newObj._iconOverlayID = Util.readDataIDOfKnownType(0x6000000, binaryReader);
                newObj._iconOverlayLength = (int) (binaryReader.BaseStream.Position - _iconOverlayStart);
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_IconOverlay.ToString());
            }

            if ((newObj.header2 & (uint)PublicWeenieDescPackHeader2.PWD2_Packed_IconUnderlay) != 0) {
                var _iconUnderlayStart = binaryReader.BaseStream.Position;
                newObj._iconUnderlayID = Util.readDataIDOfKnownType(0x6000000, binaryReader);
                newObj._iconUnderlayLength = (int)(binaryReader.BaseStream.Position - _iconUnderlayStart);
                newObj.packedItems.Add(PublicWeenieDescPackHeader2.PWD2_Packed_IconUnderlay.ToString());
            }

            if ((newObj.header & unchecked((uint)PublicWeenieDescPackHeader.PWD_Packed_MaterialType)) != 0) {
                newObj._material_type = (MaterialType)binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader.PWD_Packed_MaterialType.ToString());
            }

            if ((newObj.header2 & (uint)PublicWeenieDescPackHeader2.PWD2_Packed_CooldownID) != 0) {
                newObj._cooldown_id = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader2.PWD2_Packed_CooldownID.ToString());
            }

            if ((newObj.header2 & (uint)PublicWeenieDescPackHeader2.PWD2_Packed_CooldownDuration) != 0) {
                newObj._cooldown_duration = binaryReader.ReadDouble();
                newObj.packedItems.Add(PublicWeenieDescPackHeader2.PWD2_Packed_CooldownDuration.ToString());
            }

            if ((newObj.header2 & (uint)PublicWeenieDescPackHeader2.PWD2_Packed_PetOwner) != 0) {
                newObj._pet_owner = binaryReader.ReadUInt32();
                newObj.packedItems.Add(PublicWeenieDescPackHeader2.PWD2_Packed_PetOwner.ToString());
            }
            newObj.endPadding = Util.readToAlign(binaryReader);
            newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);

            return newObj;
        }
    }

    public class HookType
    {
    }

    public class ClothingPriority
    {
    }

    public class OldPublicWeenieDesc
    {
        public enum OldPublicWeenieDescPackHeader
        {
            PWD_Packed_None = 0,
            PWD_Packed_PluralName = (1 << 0),
            PWD_Packed_ItemsCapacity = (1 << 1),
            PWD_Packed_ContainersCapacity = (1 << 2),
            PWD_Packed_Value = (1 << 3),
            PWD_Packed_Useability = (1 << 4),
            PWD_Packed_UseRadius = (1 << 5),
            PWD_Packed_Monarch = (1 << 6),
            PWD_Packed_UIEffects = (1 << 7),
            PWD_Packed_AmmoType = (1 << 8),
            PWD_Packed_CombatUse = (1 << 9),
            PWD_Packed_Structure = (1 << 10),
            PWD_Packed_MaxStructure = (1 << 11),
            PWD_Packed_StackSize = (1 << 12),
            PWD_Packed_MaxStackSize = (1 << 13),
            PWD_Packed_ContainerID = (1 << 14),
            PWD_Packed_WielderID = (1 << 15),
            PWD_Packed_ValidLocations = (1 << 16),
            PWD_Packed_Location = (1 << 17),
            PWD_Packed_Priority = (1 << 18),
            PWD_Packed_TargetType = (1 << 19),
            PWD_Packed_BlipColor = (1 << 20),
            PWD_Packed_VendorClassID = (1 << 21),
            PWD_Packed_SpellID = (1 << 22),
            PWD_Packed_RadarEnum = (1 << 23), 
            PWD_Packed_RadarDistance = (1 << 24),
            PWD_Packed_HouseOwner = (1 << 25),
            PWD_Packed_HouseRestrictions = (1 << 26),
            PWD_Packed_PScript = (1 << 27),
            PWD_Packed_HookType = (1 << 28),
            PWD_Packed_HookItemTypes = (1 << 29),
            PWD_Packed_IconOverlay = (1 << 30),
            PWD_Packed_MaterialType = (1 << 31)
        }

        public enum BitfieldIndex
        {
            BF_OPENABLE = (1 << 0),
            BF_INSCRIBABLE = (1 << 1),
            BF_STUCK = (1 << 2),
            BF_PLAYER = (1 << 3),
            BF_ATTACKABLE = (1 << 4),
            BF_PLAYER_KILLER = (1 << 5),
            BF_HIDDEN_ADMIN = (1 << 6),
            BF_UI_HIDDEN = (1 << 7),
            BF_BOOK = (1 << 8),
            BF_VENDOR = (1 << 9),
            BF_PKSWITCH = (1 << 10),
            BF_NPKSWITCH = (1 << 11),
            BF_DOOR = (1 << 12),
            BF_CORPSE = (1 << 13),
            BF_LIFESTONE = (1 << 14),
            BF_FOOD = (1 << 15),
            BF_HEALER = (1 << 16),
            BF_LOCKPICK = (1 << 17),
            BF_PORTAL = (1 << 18),
            // NOTE: Skip 1
            BF_ADMIN = (1 << 20),
            BF_FREE_PKSTATUS = (1 << 21),
            BF_IMMUNE_CELL_RESTRICTIONS = (1 << 22),
            BF_REQUIRES_PACKSLOT = (1 << 23),
            BF_RETAINED = (1 << 24),
            BF_PKLITE_PKSTATUS = (1 << 25),
            BF_INCLUDES_SECOND_HEADER = (1 << 26),
            BF_BINDSTONE = (1 << 27),
            BF_VOLATILE_RARE = (1 << 28),
            BF_WIELD_ON_USE = (1 << 29),
            BF_WIELD_LEFT = (1 << 30),
        }

        public uint header;
        public PStringChar _name;
        public PStringChar _plural_name;
        public uint _wcid;
        public uint _iconID;
        public uint _iconOverlayID;
        public uint _containerID;
        public uint _wielderID;
        public uint _priority;
        public uint _valid_locations;
        public uint _location;
        public byte _itemsCapacity;
        public byte _containersCapacity;
        public ITEM_TYPE _type;
        public uint _value;
        public ITEM_USEABLE _useability;
        public float _useRadius;
        public ITEM_TYPE _targetType;
        public uint _effects;
        public AMMO_TYPE _ammoType;
        public COMBAT_USE _combatUse;
        public ushort _structure;
        public ushort _maxStructure;
        public ushort _stackSize;
        public ushort _maxStackSize;
        public uint _bitfield;
        public byte _blipColor;
        public RadarEnum _radar_enum;
        public float _obvious_distance;
        public uint _vndwcid;
        public ushort _spellID;
        public uint _house_owner_iid;
        public CM_House.RestrictionDB _db;
        public ushort _pscript;
        public ITEM_TYPE _hook_type;
        public uint _hook_item_types;
        public uint _monarch;
        public MaterialType _material_type;

        // Context info has not been added to the old weenie description class as it is not used
        public static OldPublicWeenieDesc read(BinaryReader binaryReader)
        {
            OldPublicWeenieDesc newObj = new OldPublicWeenieDesc();
            newObj.header = binaryReader.ReadUInt32();
            newObj._name = PStringChar.read(binaryReader);
            newObj._wcid = binaryReader.ReadUInt16();
            newObj._iconID = binaryReader.ReadUInt16() | 0x6000000u;
            newObj._type = (ITEM_TYPE)binaryReader.ReadUInt32();
            newObj._bitfield = binaryReader.ReadUInt32();

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_PluralName) != 0)
            {
                newObj._plural_name = PStringChar.read(binaryReader);
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_ItemsCapacity) != 0)
            {
                newObj._itemsCapacity = binaryReader.ReadByte();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_ContainersCapacity) != 0)
            {
                newObj._containersCapacity = binaryReader.ReadByte();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_Value) != 0)
            {
                newObj._value = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_Useability) != 0)
            {
                newObj._useability = (ITEM_USEABLE)binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_UseRadius) != 0)
            {
                newObj._useRadius = binaryReader.ReadSingle();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_TargetType) != 0)
            {
                newObj._targetType = (ITEM_TYPE)binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_UIEffects) != 0)
            {
                newObj._effects = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_AmmoType) != 0)
            {
                newObj._ammoType = (AMMO_TYPE)binaryReader.ReadByte();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_CombatUse) != 0)
            {
                newObj._combatUse = (COMBAT_USE)binaryReader.ReadByte();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_Structure) != 0)
            {
                newObj._structure = binaryReader.ReadUInt16();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_MaxStructure) != 0)
            {
                newObj._maxStructure = binaryReader.ReadUInt16();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_StackSize) != 0)
            {
                newObj._stackSize = binaryReader.ReadUInt16();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_MaxStackSize) != 0)
            {
                newObj._maxStackSize = binaryReader.ReadUInt16();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_ContainerID) != 0)
            {
                newObj._containerID = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_WielderID) != 0)
            {
                newObj._wielderID = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_ValidLocations) != 0)
            {
                newObj._valid_locations = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_Location) != 0)
            {
                newObj._location = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_Priority) != 0)
            {
                newObj._priority = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_BlipColor) != 0)
            {
                newObj._blipColor = binaryReader.ReadByte();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_RadarEnum) != 0)
            {
                newObj._radar_enum = (RadarEnum)binaryReader.ReadByte();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_RadarDistance) != 0)
            {
                newObj._obvious_distance = binaryReader.ReadSingle();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_VendorClassID) != 0)
            {
                newObj._vndwcid = binaryReader.ReadUInt16();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_SpellID) != 0)
            {
                newObj._spellID = binaryReader.ReadUInt16();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_HouseOwner) != 0)
            {
                newObj._house_owner_iid = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_PScript) != 0)
            {
                newObj._pscript = binaryReader.ReadUInt16();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_HouseRestrictions) != 0)
            {
                // TODO: Read here once you get RestrictedDB read finished
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_HookType) != 0)
            {
                newObj._hook_type = (ITEM_TYPE)binaryReader.ReadUInt16();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_HookItemTypes) != 0)
            {
                newObj._hook_item_types = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_Monarch) != 0)
            {
                newObj._monarch = binaryReader.ReadUInt32();
            }

            if ((newObj.header & (uint)OldPublicWeenieDescPackHeader.PWD_Packed_IconOverlay) != 0)
            {
                newObj._iconOverlayID = binaryReader.ReadUInt16() | 0x6000000u;
            }

            if ((newObj.header & unchecked((uint)OldPublicWeenieDescPackHeader.PWD_Packed_MaterialType)) != 0)
            {
                newObj._material_type = (MaterialType)binaryReader.ReadUInt32();
            }

            Util.readToAlign(binaryReader);

            return newObj;
        }

    }

    public class PhysicsTimestampPack {
        public ushort ts1;
        public ushort ts2;

        public static PhysicsTimestampPack read(BinaryReader binaryReader) {
            PhysicsTimestampPack newObj = new PhysicsTimestampPack();
            newObj.ts1 = binaryReader.ReadUInt16();
            newObj.ts2 = binaryReader.ReadUInt16();
            Util.readToAlign(binaryReader);
            return newObj;
        }
    }

    public class ObjDescEvent {
        public uint object_id;
        public ObjDesc desc;
        public PhysicsTimestampPack timestamps;

        public static ObjDescEvent read(BinaryReader binaryReader) {
            ObjDescEvent newObj = new ObjDescEvent();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.desc = ObjDesc.read(binaryReader);
            newObj.timestamps = PhysicsTimestampPack.read(binaryReader);
            return newObj;
        }
    }

    public class CreateObject {
        public uint object_id;
        public ObjDesc objdesc;
        public PhysicsDesc physicsdesc;
        public PublicWeenieDesc wdesc;

        public static CreateObject read(BinaryReader binaryReader) {
            CreateObject newObj = new CreateObject();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.objdesc = ObjDesc.read(binaryReader);
            newObj.physicsdesc = PhysicsDesc.read(binaryReader);
            newObj.wdesc = PublicWeenieDesc.read(binaryReader);
            return newObj;
        }
    }

    public class CreatePlayer {
        public uint object_id;

        public static CreatePlayer read(BinaryReader binaryReader) {
            CreatePlayer newObj = new CreatePlayer();
            newObj.object_id = binaryReader.ReadUInt32();
            return newObj;
        }
    }

    public class DeleteObject {
        public uint object_id;
        public ushort instance_timestamp;

        public static DeleteObject read(BinaryReader binaryReader) {
            DeleteObject newObj = new DeleteObject();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.instance_timestamp = binaryReader.ReadUInt16();
            Util.readToAlign(binaryReader);
            return newObj;
        }
    }

    public class ParentEvent {
        public uint object_id;
        public uint child_id;
        public uint child_location;
        public uint placement_id;
        public PhysicsTimestampPack timestamps;

        public static ParentEvent read(BinaryReader binaryReader) {
            ParentEvent newObj = new ParentEvent();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.child_id = binaryReader.ReadUInt32();
            newObj.child_location = binaryReader.ReadUInt32();
            newObj.placement_id = binaryReader.ReadUInt32();
            newObj.timestamps = PhysicsTimestampPack.read(binaryReader);
            return newObj;
        }
    }

    public class PickupEvent {
        public uint object_id;
        public PhysicsTimestampPack timestamps;

        public static PickupEvent read(BinaryReader binaryReader) {
            PickupEvent newObj = new PickupEvent();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.timestamps = PhysicsTimestampPack.read(binaryReader);
            return newObj;
        }
    }

    public class SetState {
        public uint object_id;
        public uint new_state;
        public PhysicsTimestampPack timestamps;

        public static SetState read(BinaryReader binaryReader) {
            SetState newObj = new SetState();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.new_state = binaryReader.ReadUInt32();
            newObj.timestamps = PhysicsTimestampPack.read(binaryReader);
            return newObj;
        }
    }

    public class VectorUpdate {
        public uint object_id;
        public Vector3 velocity;
        public Vector3 omega;
        public PhysicsTimestampPack timestamps;

        public static VectorUpdate read(BinaryReader binaryReader) {
            VectorUpdate newObj = new VectorUpdate();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.velocity = Vector3.read(binaryReader);
            newObj.omega = Vector3.read(binaryReader);
            newObj.timestamps = PhysicsTimestampPack.read(binaryReader);
            return newObj;
        }
    }

    public class SoundEvent {
        public uint object_id;
        public int sound;
        public float volume;

        public static SoundEvent read(BinaryReader binaryReader) {
            SoundEvent newObj = new SoundEvent();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.sound = binaryReader.ReadInt32();
            newObj.volume = binaryReader.ReadSingle();
            return newObj;
        }
    }

    public class PlayerTeleport {
        public ushort physics_timestamp;

        public static PlayerTeleport read(BinaryReader binaryReader) {
            PlayerTeleport newObj = new PlayerTeleport();
            newObj.physics_timestamp = binaryReader.ReadUInt16();
            Util.readToAlign(binaryReader);
            return newObj;
        }
    }

    public class PlayScriptID {
        public uint object_id;
        public uint script_id;

        public static PlayScriptID read(BinaryReader binaryReader) {
            PlayScriptID newObj = new PlayScriptID();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.script_id = binaryReader.ReadUInt32();
            return newObj;
        }
    }

    public class PlayScriptType {
        public uint object_id;
        public PScriptType script_type;
        public float mod;

        public static PlayScriptType read(BinaryReader binaryReader) {
            PlayScriptType newObj = new PlayScriptType();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.script_type = (PScriptType)binaryReader.ReadUInt32();
            newObj.mod = binaryReader.ReadSingle();
            return newObj;
        }
    }

    public class UpdateObject {
        public uint object_id;
        public ObjDesc objdesc;
        public PhysicsDesc physicsdesc;
        public PublicWeenieDesc wdesc;

        public static UpdateObject read(BinaryReader binaryReader) {
            UpdateObject newObj = new UpdateObject();
            newObj.object_id = binaryReader.ReadUInt32();
            newObj.objdesc = ObjDesc.read(binaryReader);
            newObj.physicsdesc = PhysicsDesc.read(binaryReader);
            newObj.wdesc = PublicWeenieDesc.read(binaryReader);
            return newObj;
        }
    }
}
