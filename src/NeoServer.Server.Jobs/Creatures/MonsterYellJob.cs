﻿using NeoServer.Game.Contracts.Creatures;
using NeoServer.Game.Common.Creatures;
using NeoServer.Server.Helpers;
using NeoServer.Server.Tasks;
using System;

namespace NeoServer.Server.Jobs.Creatures
{
    public class MonsterYellJob
    {        
        public static void Execute(ICombatActor creature)
        {
          
            if (!(creature is IMonster monster))
            {
                return;
            }
            if (monster.IsDead || monster.IsSleeping)
            {
                return;
            }
           
            monster.Yell();
        }
    }
}
