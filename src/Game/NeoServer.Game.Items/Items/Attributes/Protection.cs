﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoServer.Game.Common.Combat.Structs;
using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Game.Common.Contracts.Items.Types;
using NeoServer.Game.Common.Item;

namespace NeoServer.Game.Items.Items.Attributes
{
    public sealed class Protection: IProtection
    {
        public Protection(Dictionary<DamageType, sbyte> damageProtection)
        {
            DamageProtection = damageProtection;
        }

        public Dictionary<DamageType, sbyte> DamageProtection { get; }

        private sbyte GetProtection(DamageType damageType)
        {
            if (DamageProtection is null || damageType == DamageType.None) return 0;

            return !DamageProtection.TryGetValue(damageType, out var value) ? (sbyte)0 : value;
        }
        

        public bool Protect(ref CombatDamage damage)
        {
            var protection = GetProtection(damage.Type);
            if (protection == 0) return false;
            damage.ReduceDamageByPercent(protection);
            return true;
        }
    }
}
