﻿using System;
using System.Collections.Generic;
using NeoServer.Game.Common.Creatures;
using NeoServer.Game.Common.Item;
using NeoServer.Game.Common.Location;

namespace NeoServer.Game.Common.Contracts.Items
{
    public interface IItemAttributeList
    {
        Dictionary<DamageType, sbyte> DamageProtection { get; }
        Dictionary<SkillType, byte> SkillBonuses { get; }

        string GetAttribute(ItemAttribute attribute);
        T GetAttribute<T>(ItemAttribute attribute) where T : struct;
        dynamic[] GetAttributeArray(string attribute);
        dynamic[] GetAttributeArray(ItemAttribute attribute);
        EffectT GetEffect();
        FloorChangeDirection GetFloorChangeDirection();
        IItemAttributeList GetInnerAttributes(ItemAttribute attribute);
        byte[] GetRequiredVocations();
        ushort GetTransformationItem();
        Tuple<DamageType, byte> GetWeaponElementDamage();
        bool HasAttribute(ItemAttribute attribute);
        bool HasAttribute(string attribute);
        void SetAttribute(ItemAttribute attribute, IConvertible attributeValue);
        void SetAttribute(ItemAttribute attribute, int attributeValue);
        void SetAttribute(ItemAttribute attribute, IConvertible attributeValue, IItemAttributeList attrs);
        void SetAttribute(ItemAttribute attribute, dynamic values);
        void SetCustomAttribute(string attribute, int attributeValue);
        void SetCustomAttribute(string attribute, IConvertible attributeValue);
        void SetCustomAttribute(string attribute, dynamic values);
        void SetCustomAttribute(string attribute, IConvertible attributeValue, IItemAttributeList attrs);
        Dictionary<TKey, TValue> ToDictionary<TKey, TValue>();
        bool TryGetAttribute<T>(ItemAttribute attribute, out T attrValue) where T : struct;
    }
}