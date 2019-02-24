using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclogview.Enum_Helpers
{
    class PropertyInt
    {
        public static Dictionary<STypeInt, Type> EnumMapper = new Dictionary<STypeInt, Type>()
        {
            {STypeInt.ATTACK_TYPE_INT, typeof(AttackType)},
            {STypeInt.CURRENT_WIELDED_LOCATION_INT, typeof(INVENTORY_LOC)},
            {STypeInt.PLAYER_KILLER_STATUS_INT, typeof(PKStatusEnum)},
            {STypeInt.EQUIPMENT_SET_ID_INT, typeof(EquipmentSet)},
            {STypeInt.PALETTE_TEMPLATE_INT, typeof(PALETTE_TEMPLATE)},
            {STypeInt.CLOTHING_PRIORITY_INT, typeof(CoverageMask)},
            {STypeInt.LOCATIONS_INT, typeof(INVENTORY_LOC)},
            {STypeInt.UI_EFFECTS_INT, typeof(UI_EFFECT_TYPE)},
            {STypeInt.ATTUNED_INT, typeof(AttunedStatusEnum)},
            {STypeInt.HOOK_TYPE_INT, typeof(HookTypeEnum)},
            {STypeInt.SLAYER_CREATURE_TYPE_INT, typeof(CreatureType)},
            {STypeInt.HERITAGE_SPECIFIC_ARMOR_INT, typeof(HeritageGroup)},
            {STypeInt.WIELD_REQUIREMENTS_INT, typeof(WieldRequirement)},
            {STypeInt.WIELD_REQUIREMENTS_2_INT, typeof(WieldRequirement)},
            {STypeInt.WIELD_REQUIREMENTS_3_INT, typeof(WieldRequirement)},
            {STypeInt.WIELD_REQUIREMENTS_4_INT, typeof(WieldRequirement)},
            {STypeInt.ITEM_XP_STYLE_INT, typeof(ItemXpStyle)},
            {STypeInt.COMBAT_MODE_INT, typeof(COMBAT_MODE)},
            {STypeInt.GENDER_INT, typeof(Gender)},
            {STypeInt.HERITAGE_GROUP_INT, typeof(HeritageGroup)},
            {STypeInt.FACTION1_BITS_INT, typeof(FactionBits)},
            {STypeInt.AETHERIA_BITFIELD_INT, typeof(AetheriaBitfield)},
            {STypeInt.MELEE_MASTERY_INT, typeof(WeaponType)},
            {STypeInt.RANGED_MASTERY_INT, typeof(WeaponType)},
            {STypeInt.SUMMONING_MASTERY_INT, typeof(SummoningMastery)}
        };
    }
}
