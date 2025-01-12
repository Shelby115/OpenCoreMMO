﻿using System;
using System.Collections.Generic;
using NeoServer.Game.Common.Contracts.Items;
using NeoServer.Game.Common.Creatures;
using NeoServer.Game.Common.Item;
using NeoServer.Game.Common.Location;

namespace NeoServer.Game.Items
{
    public sealed class ItemAttributeList : IItemAttributeList
    {
        private readonly IDictionary<ItemAttribute, (dynamic, IItemAttributeList)> _defaultAttributes;
        private IDictionary<string, (dynamic, IItemAttributeList)> customAttributes;

        public ItemAttributeList()
        {
            _defaultAttributes = new Dictionary<ItemAttribute, (dynamic, IItemAttributeList)>();
        }

        private IDictionary<string, (dynamic, IItemAttributeList)> _customAttributes
        {
            get
            {
                customAttributes ??= new Dictionary<string, (dynamic, IItemAttributeList)>(StringComparer
                    .InvariantCultureIgnoreCase);
                return customAttributes;
            }
        }

        public void SetCustomAttribute(string attribute, int attributeValue)
        {
            _customAttributes[attribute] = (attributeValue, null);
        }

        public void SetCustomAttribute(string attribute, IConvertible attributeValue)
        {
            _customAttributes[attribute] = (attributeValue, null);
        }

        public void SetCustomAttribute(string attribute, dynamic values)
        {
            _customAttributes[attribute] = (values, null);
        }

        public void SetCustomAttribute(string attribute, IConvertible attributeValue, IItemAttributeList attrs)
        {
            _customAttributes[attribute] = (attributeValue, attrs);
        }

        public void SetAttribute(ItemAttribute attribute, int attributeValue)
        {
            _defaultAttributes[attribute] = (attributeValue, null);
        }

        public void SetAttribute(ItemAttribute attribute, IConvertible attributeValue)
        {
            _defaultAttributes[attribute] = (attributeValue, null);
        }

        public void SetAttribute(ItemAttribute attribute, dynamic values)
        {
            _defaultAttributes[attribute] = (values, null);
        }

        public void SetAttribute(ItemAttribute attribute, IConvertible attributeValue, IItemAttributeList attrs)
        {
            _defaultAttributes[attribute] = (attributeValue, attrs);
        }

        public bool HasAttribute(ItemAttribute attribute)
        {
            return _defaultAttributes.ContainsKey(attribute);
        }

        public bool HasAttribute(string attribute)
        {
            return _customAttributes.ContainsKey(attribute);
        }

        public T GetAttribute<T>(ItemAttribute attribute) where T : struct
        {
            if (_defaultAttributes is null) return default;

            if (_defaultAttributes.TryGetValue(attribute, out var value))
                return (T)Convert.ChangeType(value.Item1, typeof(T));

            return default;
        }

        public bool TryGetAttribute<T>(ItemAttribute attribute, out T attrValue) where T : struct
        {
            attrValue = default;

            if (_defaultAttributes is null) return false;

            if (!_defaultAttributes.TryGetValue(attribute, out var value)) return false;

            attrValue = (T)Convert.ChangeType(value.Item1, typeof(T));
            return true;
        }

        public string GetAttribute(ItemAttribute attribute)
        {
            if (_defaultAttributes is null) return default;

            if (_defaultAttributes.TryGetValue(attribute, out var value)) return (string)value.Item1;

            return default;
        }

        public dynamic[] GetAttributeArray(ItemAttribute attribute)
        {
            if (_defaultAttributes is null) return default;

            if (_defaultAttributes.TryGetValue(attribute, out var value))
            {
                if (value.Item1 is not Array) return default;
                return value.Item1;
            }

            return default;
        }

        public dynamic[] GetAttributeArray(string attribute)
        {
            if (_customAttributes is null) return default;

            if (_customAttributes.TryGetValue(attribute, out var value))
            {
                if (value.Item1 is not Array) return default;
                return value.Item1;
            }

            return default;
        }

        public Dictionary<TKey, TValue> ToDictionary<TKey, TValue>()
        {
            if (_defaultAttributes is null && _customAttributes is null) return default;

            var dictionary = new Dictionary<TKey, TValue>();

            if (_defaultAttributes is not null)
                foreach (var item in _defaultAttributes)
                    dictionary.Add((TKey)Convert.ChangeType(item.Key, typeof(TKey)), (TValue)item.Value.Item1);
            if (_customAttributes is not null)
                foreach (var item in _customAttributes)
                    dictionary.Add((TKey)Convert.ChangeType(item.Key, typeof(TKey)), (TValue)item.Value.Item1);
            return dictionary;
        }

        public IItemAttributeList GetInnerAttributes(ItemAttribute attribute)
        {
            if (_defaultAttributes is null) return default;

            if (_defaultAttributes.TryGetValue(attribute, out var value)) return value.Item2;

            return default;
        }

        public FloorChangeDirection GetFloorChangeDirection()
        {
            if (_defaultAttributes?.ContainsKey(ItemAttribute.FloorChange) ?? false)
            {
                var floorChange = GetAttribute(ItemAttribute.FloorChange);

                return floorChange switch
                {
                    "down" => FloorChangeDirection.Down,
                    "north" => FloorChangeDirection.North,
                    "south" => FloorChangeDirection.South,
                    "southalt" => FloorChangeDirection.SouthAlternative,
                    "west" => FloorChangeDirection.West,
                    "east" => FloorChangeDirection.East,
                    "eastalt" => FloorChangeDirection.EastAlternative,
                    "up" => FloorChangeDirection.Up,
                    _ => FloorChangeDirection.None
                };
            }

            return FloorChangeDirection.None;
        }

        public byte[] GetRequiredVocations()
        {
            if (_defaultAttributes is null) return default;

            if (_defaultAttributes.TryGetValue(ItemAttribute.Vocation, out var value)) return (byte[])value.Item1;

            return default;
        }

        public EffectT GetEffect()
        {
            if (_defaultAttributes?.ContainsKey(ItemAttribute.Effect) ?? false)
            {
                var effect = GetAttribute(ItemAttribute.Effect);

                return effect switch
                {
                    "teleport" => EffectT.BubbleBlue,
                    "blueshimmer" => EffectT.GlitterBlue,
                    "bluebubble" => EffectT.BubbleBlue,
                    "greenbubble" => EffectT.GlitterGreen,
                    _ => EffectT.None
                };
            }

            return EffectT.None;
        }

        public ushort GetTransformationItem()
        {
            if (_defaultAttributes?.ContainsKey(ItemAttribute.TransformEquipTo) ?? false)
                return GetAttribute<ushort>(ItemAttribute.TransformEquipTo);

            if (_defaultAttributes?.ContainsKey(ItemAttribute.TransformDequipTo) ?? false)
                return GetAttribute<ushort>(ItemAttribute.TransformDequipTo);

            if (_defaultAttributes?.ContainsKey(ItemAttribute.TransformTo) ?? false)
                return GetAttribute<ushort>(ItemAttribute.TransformTo);

            return 0;
        }

        public Tuple<DamageType, byte> GetWeaponElementDamage()
        {
            if (_defaultAttributes?.ContainsKey(ItemAttribute.ElementEarth) ?? false)
                return new Tuple<DamageType, byte>(DamageType.Earth, GetAttribute<byte>(ItemAttribute.ElementEarth));

            if (_defaultAttributes?.ContainsKey(ItemAttribute.ElementEnergy) ?? false)
                return new Tuple<DamageType, byte>(DamageType.Energy, GetAttribute<byte>(ItemAttribute.ElementEnergy));

            if (_defaultAttributes?.ContainsKey(ItemAttribute.ElementFire) ?? false)
                return new Tuple<DamageType, byte>(DamageType.FireField,
                    GetAttribute<byte>(ItemAttribute.ElementFire)); //todo

            if (_defaultAttributes?.ContainsKey(ItemAttribute.ElementIce) ?? false)
                return new Tuple<DamageType, byte>(DamageType.Ice, GetAttribute<byte>(ItemAttribute.ElementIce));

            return null;
        }

        public Dictionary<SkillType, byte> SkillBonuses
        {
            get
            {
                var dictionary = new Dictionary<SkillType, byte>();

                if (_defaultAttributes?.ContainsKey(ItemAttribute.SkillAxe) ?? false)
                    dictionary.TryAdd(SkillType.Axe, GetAttribute<byte>(ItemAttribute.SkillAxe));

                if (_defaultAttributes?.ContainsKey(ItemAttribute.SkillClub) ?? false)
                    dictionary.TryAdd(SkillType.Club, GetAttribute<byte>(ItemAttribute.SkillClub));

                if (_defaultAttributes?.ContainsKey(ItemAttribute.SkillDistance) ?? false)
                    dictionary.TryAdd(SkillType.Distance, GetAttribute<byte>(ItemAttribute.SkillDistance));

                if (_defaultAttributes?.ContainsKey(ItemAttribute.SkillFishing) ?? false)
                    dictionary.TryAdd(SkillType.Fishing, GetAttribute<byte>(ItemAttribute.SkillFishing));

                if (_defaultAttributes?.ContainsKey(ItemAttribute.SkillFist) ?? false)
                    dictionary.TryAdd(SkillType.Fist, GetAttribute<byte>(ItemAttribute.SkillFist));

                if (_defaultAttributes?.ContainsKey(ItemAttribute.SkillShield) ?? false)
                    dictionary.TryAdd(SkillType.Shielding, GetAttribute<byte>(ItemAttribute.SkillShield));

                if (_defaultAttributes?.ContainsKey(ItemAttribute.SkillSword) ?? false)
                    dictionary.TryAdd(SkillType.Sword, GetAttribute<byte>(ItemAttribute.SkillSword));

                if (_defaultAttributes?.ContainsKey(ItemAttribute.Speed) ?? false)
                    dictionary.TryAdd(SkillType.Speed, GetAttribute<byte>(ItemAttribute.Speed));

                if (_defaultAttributes?.ContainsKey(ItemAttribute.MagicPoints) ?? false)
                    dictionary.TryAdd(SkillType.Magic, GetAttribute<byte>(ItemAttribute.MagicPoints));

                return dictionary;
            }
        }

        public Dictionary<DamageType, sbyte> DamageProtection
        {
            get
            {
                var dictionary = new Dictionary<DamageType, sbyte>();

                foreach (var (attr, (value, list)) in _defaultAttributes)
                {
                    var (damage,protection) = attr switch
                    {
                        ItemAttribute.AbsorbPercentDeath => (DamageType.Death, (sbyte) value),
                        ItemAttribute.AbsorbPercentEnergy => (DamageType.Energy, (sbyte)value),
                        ItemAttribute.AbsorbPercentPhysical => (DamageType.Physical, (sbyte)value),
                        ItemAttribute.AbsorbPercentPoison => (DamageType.Earth, (sbyte)value),
                        ItemAttribute.AbsorbPercentFire => (DamageType.Fire, (sbyte)value),
                        ItemAttribute.AbsorbPercentDrown => (DamageType.Drown, (sbyte)value),
                        ItemAttribute.AbsorbPercentHoly => (DamageType.Holy, (sbyte)value),
                        ItemAttribute.AbsorbPercentIce => (DamageType.Ice, (sbyte)value),
                        ItemAttribute.AbsorbPercentManaDrain => (DamageType.ManaDrain, (sbyte)value),
                        ItemAttribute.AbsorbPercentLifeDrain => (DamageType.LifeDrain, (sbyte)value),
                        ItemAttribute.AbsorbPercentMagic => (DamageType.Elemental, (sbyte)value),
                        ItemAttribute.AbsorbPercentAll => (DamageType.All, (sbyte)value),
                        ItemAttribute.AbsorbPercentElements => (DamageType.Elemental, (sbyte)value),
                        _ => (DamageType.None, (sbyte)0)
                    };

                    if (damage == DamageType.None) continue;
                    dictionary.TryAdd(damage, protection);
                }
                
                return dictionary;
            }
        }
    }
}