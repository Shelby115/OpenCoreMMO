﻿using System.Text;
using NeoServer.Game.Common.Item;

namespace NeoServer.Game.Common.Contracts.Items.Types.Body
{
    public interface IDistanceWeaponItem : IWeapon
    {
        byte ExtraAttack => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Attack);
        byte ExtraHitChance => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.HitChance);
        byte Range => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Range);

        private string AttributesText
        {
            get
            {
                var range = Range > 0 ? $"Range:{Range}" : string.Empty;
                var atk = ExtraAttack > 0 ? $"Atk+{ExtraAttack}" : string.Empty;
                var hit = ExtraHitChance > 0 ? $"Hit%+{ExtraHitChance}" : string.Empty;

                if (range == "" && atk == "" && hit == "") return string.Empty;

                var stringBuilder = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(range)) stringBuilder.Append($"{range}, ");
                if (!string.IsNullOrWhiteSpace(atk)) stringBuilder.Append($"{atk}, ");
                if (!string.IsNullOrWhiteSpace(hit)) stringBuilder.Append($"{hit}, ");

                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                return $"({stringBuilder})";
            }
        }

        string IItem.LookText => $"{Metadata.Article} {Metadata.Name} {AttributesText}{RequirementText}";
        string IThing.InspectionText => $"{LookText}";
    }
}