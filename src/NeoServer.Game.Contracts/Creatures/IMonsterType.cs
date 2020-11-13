﻿using NeoServer.Game.Contracts.Combat;
using NeoServer.Game.Creatures.Combat.Attacks;
using NeoServer.Game.Enums.Creatures;
using System.Collections.Generic;

namespace NeoServer.Game.Contracts.Creatures
{
    public interface IMonsterType : ICreatureType
    {
        ushort Armor { get; set; }
        ushort Defence { get; set; }

        public uint Experience { get; set; }
        public CombatAttackOption[] Attacks { get; set; }
        public List<ICombatDefense> Defenses { get; set; }

        IDictionary<CreatureFlagAttribute, byte> Flags { get; set; }
        IIntervalChance TargetChance { get; set; }

        /// <summary>
        /// Monster's phases
        /// </summary>
        string[] Voices { get; set; }
        /// <summary>
        /// Voice interval and chance to happen
        /// </summary>
        IIntervalChance VoiceConfig { get; set; }
    }
}
