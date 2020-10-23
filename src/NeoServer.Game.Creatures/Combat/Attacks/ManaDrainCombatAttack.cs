﻿using NeoServer.Game.Contracts.Creatures;
using NeoServer.Game.Enums.Item;
using NeoServer.Server.Model.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoServer.Game.Creatures.Combat.Attacks
{
    public class ManaDrainCombatAttack : DistanceCombatAttack
    {
        public ManaDrainCombatAttack(CombatAttackOption option) : base(DamageType.ManaDrain, option)
        {
            option.Target = 1;
        }

        public void Damage(IPlayer enemy)
        {
            //enemy.ReceiveAttack();
        }
    }
}