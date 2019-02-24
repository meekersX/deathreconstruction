﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum PScriptType {
    PS_Invalid,
    PS_Test1,
    PS_Test2,
    PS_Test3,
    PS_Launch,
    PS_Explode,
    PS_AttribUpRed,
    PS_AttribDownRed,
    PS_AttribUpOrange,
    PS_AttribDownOrange,
    PS_AttribUpYellow,
    PS_AttribDownYellow,
    PS_AttribUpGreen,
    PS_AttribDownGreen,
    PS_AttribUpBlue,
    PS_AttribDownBlue,
    PS_AttribUpPurple,
    PS_AttribDownPurple,
    PS_SkillUpRed,
    PS_SkillDownRed,
    PS_SkillUpOrange,
    PS_SkillDownOrange,
    PS_SkillUpYellow,
    PS_SkillDownYellow,
    PS_SkillUpGreen,
    PS_SkillDownGreen,
    PS_SkillUpBlue,
    PS_SkillDownBlue,
    PS_SkillUpPurple,
    PS_SkillDownPurple,
    PS_SkillDownBlack,
    PS_HealthUpRed,
    PS_HealthDownRed,
    PS_HealthUpBlue,
    PS_HealthDownBlue,
    PS_HealthUpYellow,
    PS_HealthDownYellow,
    PS_RegenUpRed,
    PS_RegenDownREd,
    PS_RegenUpBlue,
    PS_RegenDownBlue,
    PS_RegenUpYellow,
    PS_RegenDownYellow,
    PS_ShieldUpRed,
    PS_ShieldDownRed,
    PS_ShieldUpOrange,
    PS_ShieldDownOrange,
    PS_ShieldUpYellow,
    PS_ShieldDownYellow,
    PS_ShieldUpGreen,
    PS_ShieldDownGreen,
    PS_ShieldUpBlue,
    PS_ShieldDownBlue,
    PS_ShieldUpPurple,
    PS_ShieldDownPurple,
    PS_ShieldUpGrey,
    PS_ShieldDownGrey,
    PS_EnchantUpRed,
    PS_EnchantDownRed,
    PS_EnchantUpOrange,
    PS_EnchantDownOrange,
    PS_EnchantUpYellow,
    PS_EnchantDownYellow,
    PS_EnchantUpGreen,
    PS_EnchantDownGreen,
    PS_EnchantUpBlue,
    PS_EnchantDownBlue,
    PS_EnchantUpPurple,
    PS_EnchantDownPurple,
    PS_VitaeUpWhite,
    PS_VitaeDownBlack,
    PS_VisionUpWhite,
    PS_VisionDownBlack,
    PS_SwapHealth_Red_To_Yellow,
    PS_SwapHealth_Red_To_Blue,
    PS_SwapHealth_Yellow_To_Red,
    PS_SwapHealth_Yellow_To_Blue,
    PS_SwapHealth_Blue_To_Red,
    PS_SwapHealth_Blue_To_Yellow,
    PS_TransUpWhite,
    PS_TransDownBlack,
    PS_Fizzle,
    PS_PortalEntry,
    PS_PortalExit,
    PS_BreatheFlame,
    PS_BreatheFrost,
    PS_BreatheAcid,
    PS_BreatheLightning,
    PS_Create,
    PS_Destroy,
    PS_ProjectileCollision,
    PS_SplatterLowLeftBack,
    PS_SplatterLowLeftFront,
    PS_SplatterLowRightBack,
    PS_SplatterLowRightFront,
    PS_SplatterMidLeftBack,
    PS_SplatterMidLeftFront,
    PS_SplatterMidRightBack,
    PS_SplatterMidRightFront,
    PS_SplatterUpLeftBack,
    PS_SplatterUpLeftFront,
    PS_SplatterUpRightBack,
    PS_SplatterUpRightFront,
    PS_SparkLowLeftBack,
    PS_SparkLowLeftFront,
    PS_SparkLowRightBack,
    PS_SparkLowRightFront,
    PS_SparkMidLeftBack,
    PS_SparkMidLeftFront,
    PS_SparkMidRightBack,
    PS_SparkMidRightFront,
    PS_SparkUpLeftBack,
    PS_SparkUpLeftFront,
    PS_SparkUpRightBack,
    PS_SparkUpRightFront,
    PS_PortalStorm,
    PS_Hide,
    PS_UnHide,
    PS_Hidden,
    PS_DisappearDestroy,
    SpecialState1,
    SpecialState2,
    SpecialState3,
    SpecialState4,
    SpecialState5,
    SpecialState6,
    SpecialState7,
    SpecialState8,
    SpecialState9,
    SpecialState0,
    SpecialStateRed,
    SpecialStateOrange,
    SpecialStateYellow,
    SpecialStateGreen,
    SpecialStateBlue,
    SpecialStatePurple,
    SpecialStateWhite,
    SpecialStateBlack,
    PS_LevelUp,
    PS_EnchantUpGrey,
    PS_EnchantDownGrey,
    PS_WeddingBliss,
    PS_EnchantUpWhite,
    PS_EnchantDownWhite,
    PS_CampingMastery,
    PS_CampingIneptitude,
    PS_DispelLife,
    PS_DispelCreature,
    PS_DispelAll,
    PS_BunnySmite,
    PS_BaelZharonSmite,
    PS_WeddingSteele,
    PS_RestrictionEffectBlue,
    PS_RestrictionEffectGreen,
    PS_RestrictionEffectGold,
    PS_LayingofHands,
    PS_AugmentationUseAttribute,
    PS_AugmentationUseSkill,
    PS_AugmentationUseResistances,
    PS_AugmentationUseOther,
    PS_BlackMadness,
    PS_AetheriaLevelUp,
    PS_AetheriaSurgeDestruction,
    PS_AetheriaSurgeProtection,
    PS_AetheriaSurgeRegeneration,
    PS_AetheriaSurgeAffliction,
    PS_AetheriaSurgeFestering,
    PS_HealthDownVoid,
    PS_RegenDownVoid,
    PS_SkillDownVoid,
    PS_DirtyFightingHealDebuff,
    PS_DirtyFightingAttackDebuff,
    PS_DirtyFightingDefenseDebuff,
    PS_DirtyFightingDamageOverTime,
    NUM_PSCRIPT_TYPES
}
