﻿using NeoServer.Game.Common.Contracts;
using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Game.Common.Location.Structs;
using NeoServer.Game.DataStore;
using NeoServer.Game.Items.Factories.AttributeFactory;
using NeoServer.Game.Items.Items;

namespace NeoServer.Game.Items.Factories
{
    public class DefenseEquipmentFactory : IFactory
    {
        private readonly ItemTypeStore _itemTypeStore;
        private readonly ChargeableFactory _chargeableFactory;
        public DefenseEquipmentFactory(ItemTypeStore itemTypeStore, ChargeableFactory chargeableFactory)
        {
            _itemTypeStore = itemTypeStore;
            _chargeableFactory = chargeableFactory;
        }

        public event CreateItem OnItemCreated;

        public BodyDefenseEquipment Create(IItemType itemType, Location location)
        {
            var chargeable = _chargeableFactory.Create(itemType);

            if (!BodyDefenseEquipment.IsApplicable(itemType)) return null;

            return new BodyDefenseEquipment(itemType, location)
            {
                Chargeable = chargeable,
                ItemTypeFinder = _itemTypeStore.Get
            };
        }
    }
}