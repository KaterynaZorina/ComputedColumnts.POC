using System.Collections.Generic;
using System.Linq;
using ComputedColumns.POC.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ComputedColumns.POC.EntityFramework.ValuGenerators
{
    public class ComputedPropertyGenerator : Microsoft.EntityFrameworkCore.ValueGeneration.ValueGenerator
    {
        protected override object NextValue(EntityEntry entry)
        {
            if (entry.Entity is not Animal animalEntity)
            {
                return entry.Property(nameof(Animal.ComputedProperty)).CurrentValue;
            }

            var itemsToConcatenate = new List<string>
            {
                animalEntity.Name,
                animalEntity.Breed,
                animalEntity.RegistrationCode,
                animalEntity.OwnerName,
                animalEntity.Description
            };

            if (itemsToConcatenate.All(string.IsNullOrEmpty))
            {
                return null;
            }

            var concatenatedValue = string.Join(" ", itemsToConcatenate);

            return concatenatedValue;
        }

        public override bool GeneratesTemporaryValues => false;
    }
}