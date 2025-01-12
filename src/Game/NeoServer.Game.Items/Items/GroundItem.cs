﻿using NeoServer.Game.Common;
using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Game.Common.Contracts.Items.Types;
using NeoServer.Game.Common.Item;
using NeoServer.Game.Common.Location.Structs;

namespace NeoServer.Game.Items.Items
{
    public struct GroundItem : IGround
    {
        public ushort StepSpeed { get; }

        public byte MovementPenalty { get; }

        public IItemType Metadata { get; }
        public Location Location { get; set; }
        public string CustomLookText => null; //todo: revise

        public GroundItem(IItemType type, Location location)
        {
            Metadata = type;
            StepSpeed = type?.Speed != 0 ? type.Speed : (ushort) 150;
            Location = location;
            MovementPenalty = type.Attributes.GetAttribute<byte>(ItemAttribute.Waypoints);
        }

        public static bool IsApplicable(IItemType type)
        {
            return type.Group == ItemGroup.Ground;
        }
    }
}