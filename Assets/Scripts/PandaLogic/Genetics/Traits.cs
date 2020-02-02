using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.PandaLogic.Genetics;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.PandaLogic.Genetics
{
    [SingleTraitAttribute(GeneticTrait.EyeType)]
    public enum EyesTypeTrait
    {
        [TraitSingleValueCharacteristic(3, 0f)] Normal,
        [TraitSingleValueCharacteristic(2, 1f)] Big
    }

    [SingleTraitAttribute(GeneticTrait.EyeColor)]
    public enum EyeColorTrait
    {
        [TraitSingleValueCharacteristic(2, 0f)] Brown,
        [TraitSingleValueCharacteristic(3, 0.5f)] Blue,
        [TraitSingleValueCharacteristic(2, 1f)] Red,
        [TraitSingleValueCharacteristic(1, 2f)] White
    }

    [SingleTraitAttribute(GeneticTrait.EarType)]
    public enum EarTypeTrait
    {
        [TraitSingleValueCharacteristic(3, 0f)] Nomral,
        [TraitSingleValueCharacteristic(2, 1f)] Small,
        [TraitSingleValueCharacteristic(1, 2f)] Curved,
        [TraitSingleValueCharacteristic(2, 3f)] Long
    }

    [SingleTraitAttribute(GeneticTrait.Specialtype)]
    public enum SpecialTrait
    {
        [TraitSingleValueCharacteristic(3, 0f)] Without,
        [TraitSingleValueCharacteristic(2, 1f)] Butt,
        [TraitSingleValueCharacteristic(2, 2f)] Face,
        [TraitSingleValueCharacteristic(1, 3f)] Both
    }

    [SingleTraitAttribute(GeneticTrait.Tailtype)]
    public enum TailTypeTrait
    {
        [TraitSingleValueCharacteristic(3, 0f)] Normal,
        [TraitSingleValueCharacteristic(2, 0.5f)] Electric,
        [TraitSingleValueCharacteristic(2, 1f)] Fuzzy,
        [TraitSingleValueCharacteristic(1, 1.5f)] Pig,
        [TraitSingleValueCharacteristic(1, 2f)] Cat
    }

    [SingleTraitAttribute(GeneticTrait.NoseMouthSype)]
    public enum NoseMouthTypeTrait
    {
        [TraitSingleValueCharacteristic(4, 0f)] Normal,
        [TraitSingleValueCharacteristic(3, 1f)] Pink,
        [TraitSingleValueCharacteristic(3, 2f)] Heart,
        [TraitSingleValueCharacteristic(2, 3f)] Small,
        [TraitSingleValueCharacteristic(2, 4f)] Triangle,
        [TraitSingleValueCharacteristic(1, 5f)] Pig
    }

    [SingleTraitAttribute(GeneticTrait.BodyType)]
    public enum BodyTypeTrait
    {
        [TraitSingleValueCharacteristic(1, 0f)] Nomral,
        [TraitSingleValueCharacteristic(1, 1f)] Fat
    }

    [SingleTraitAttribute(GeneticTrait.PrimaryColor)]
    public enum PrimaryBodyColorTrait
    {
        [TraitSingleValueCharacteristic(3, 0f)] Black,
        [TraitSingleValueCharacteristic(2, 1f)] White,
        [TraitSingleValueCharacteristic(3, 2f)] Brown,
        [TraitSingleValueCharacteristic(2, 3f)] Gray,
        [TraitSingleValueCharacteristic(1, 4f)] Yellow
    }

    [SingleTraitAttribute(GeneticTrait.SecendaryColor)]
    public enum SecondaryColorTrait
    {
        [TraitSingleValueCharacteristic(3, 0f)] Black,
        [TraitSingleValueCharacteristic(2, 1f)] White,
        [TraitSingleValueCharacteristic(3, 2f)] Brown,
        [TraitSingleValueCharacteristic(2, 3f)] Gray,
        [TraitSingleValueCharacteristic(3, 4f)] Yellow
    }
}

public enum GeneticTrait
{
    EyeType, EyeColor, EarType, Specialtype, Tailtype, NoseMouthSype, BodyType, PrimaryColor, SecendaryColor
}

public class SingleTraitAttribute : System.Attribute
{
    private GeneticTrait _trait;

    public SingleTraitAttribute(GeneticTrait trait)
    {
        _trait = trait;
    }

    public GeneticTrait Trait => _trait;
}

public class TraitSingleValueCharacteristic : System.Attribute
{
    private int _strength;
    private float _continuousValue;

    public TraitSingleValueCharacteristic(int strength, float continuousValue)
    {
        _strength = strength;
        _continuousValue = continuousValue;
    }

    public int Strength => _strength;

    public float ContinuousValue => _continuousValue;
}

public static class TraitUtils
{
    public static TraitValueQueryResult QueryTraitValue(GeneticTrait trait, float floatValue)
    {
        var allTraitTypes = ReflectionUtils.GetTypesWithHelpAttribute(Assembly.GetCallingAssembly(), typeof(SingleTraitAttribute)).ToList();
        var searchedTraitTypes = allTraitTypes.Where(c => c.GetCustomAttribute<SingleTraitAttribute>().Trait == trait).ToList();
        Assert.IsTrue(searchedTraitTypes.Count == 1, $"Failure when looking for enums with GeneticTrait {trait}. Wanted exacly one, found {searchedTraitTypes.Count}");

        var searchedType = searchedTraitTypes.First();
        var enumValues = Enum.GetValues(searchedType);

        var enumValuesWithAttribute = new List<object>(enumValues.Cast<object>()).Select(c =>
       {
           var name = Enum.GetName(searchedType, c);
           var attribute = searchedType.GetField(name).GetCustomAttributes(false).OfType<TraitSingleValueCharacteristic>().SingleOrDefault();
           Assert.IsNotNull(attribute, $"Cannot find attribute SingleTraitAttribute in enum {searchedType} value {c}");

           return new
           {
               enumValue = c,
               attribute = attribute
           };
       }).ToList();
        var closestValue = enumValuesWithAttribute.OrderBy(c => Mathf.Abs(floatValue - c.attribute.ContinuousValue)).First();
        return new TraitValueQueryResult()
        {
            Strength = closestValue.attribute.Strength,
            QuantisizedEnumValue = closestValue.enumValue
        };
    }

    public static void SetQueryValue(object enumValue, Phenotype phenotype)
    {
        foreach (var prop in phenotype.GetType().GetFields())
        {
            if (prop.FieldType == enumValue.GetType())
            {
                prop.SetValue(phenotype, enumValue);
                return;
            }
        }
        Assert.IsTrue(false, $"Fail: In Phenotype class i cannot find field of enum type {enumValue}");
    }

    public static List<Gene> CreateGenotypeFromPhenotype(Phenotype phenotype, List<Gene> oldGenotype)
    {
        var newGenotype = new List<Gene>();
        var allTraitTypes = ReflectionUtils.GetTypesWithHelpAttribute(Assembly.GetCallingAssembly(), typeof(SingleTraitAttribute))
            .Select(c => new { c = c, ata = c.GetCustomAttribute<SingleTraitAttribute>() })
            .ToList();

        foreach (var fieldInfo in phenotype.GetType().GetFields())
        {
            var currentQuantisizedValue = fieldInfo.GetValue(phenotype);
            var name = Enum.GetName(fieldInfo.FieldType, currentQuantisizedValue);
            var attribute = fieldInfo.FieldType.GetField(name).GetCustomAttributes(false).OfType<TraitSingleValueCharacteristic>().SingleOrDefault();

            var traitWeAreLookingFor = allTraitTypes.SingleOrDefault(c => c.c == fieldInfo.FieldType);
            Assert.IsNotNull(traitWeAreLookingFor, "Cannot find enum with SingleTraitAttribute of type " + fieldInfo.FieldType);

            newGenotype.Add(new Gene()
            {
                FatherGene = attribute.ContinuousValue,
                MotherGene = attribute.ContinuousValue,
                Trait = traitWeAreLookingFor.ata.Trait
            });
        }

        return newGenotype;
    }
}

public class TraitValueQueryResult
{
    public int Strength;
    public object QuantisizedEnumValue;
}

public static class ReflectionUtils
{
    public static IEnumerable<Type> GetTypesWithHelpAttribute(Assembly assembly, Type attributeType)
    {
        foreach (Type type in assembly.GetTypes())
        {
            if (type.GetCustomAttributes(attributeType, true).Length > 0)
            {
                yield return type;
            }
        }
    }
}