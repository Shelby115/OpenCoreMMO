﻿using Autofac;
using NeoServer.Game.Combat.Spells;
using NeoServer.Game.Common;
using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Game.Common.Contracts.World;
using NeoServer.Server.Events.Combat;
using NeoServer.Server.Events.Creature;
using NeoServer.Server.Events.Items;
using NeoServer.Server.Events.Player;
using NeoServer.Server.Events.Tiles;

namespace NeoServer.Server.Events.Subscribers
{
    public class EventSubscriber
    {
        private readonly ILiquidPoolFactory itemFactory;
        private readonly IMap map;
        private readonly IComponentContext container;

        public EventSubscriber(IMap map, IComponentContext container, ILiquidPoolFactory itemFactory)
        {
            this.map = map;
            this.container = container;
            this.itemFactory = itemFactory;
        }

        public virtual void AttachEvents()
        {
            map.OnCreatureAddedOnMap += (creature, cylinder) =>
                container.Resolve<PlayerAddedOnMapEventHandler>().Execute(creature, cylinder);
            map.OnThingRemovedFromTile += container.Resolve<ThingRemovedFromTileEventHandler>().Execute;
            map.OnCreatureMoved += container.Resolve<CreatureMovedEventHandler>().Execute;
            map.OnThingMovedFailed += container.Resolve<InvalidOperationEventHandler>().Execute;
            map.OnThingAddedToTile += container.Resolve<ThingAddedToTileEventHandler>().Execute;
            map.OnThingUpdatedOnTile += container.Resolve<ThingUpdatedOnTileEventHandler>().Execute;
            BaseSpell.OnSpellInvoked += container.Resolve<SpellInvokedEventHandler>().Execute;
            itemFactory.OnItemCreated += container.Resolve<ItemCreatedEventHandler>().Execute;
            OperationFailService.OnOperationFailed += container.Resolve<PlayerOperationFailedEventHandler>().Execute;
        }
    }
}