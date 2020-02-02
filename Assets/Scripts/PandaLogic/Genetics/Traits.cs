using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.PandaLogic.Genetics
{
    [SingleTraitAttribute(GeneticTrait.LegLength)]
    public enum LegLengthTrait
    {
        [TraitSingleValueCharacteristic(1, 0f)]Short,
        [TraitSingleValueCharacteristic(1, 0.5f)]Medium,
        [TraitSingleValueCharacteristic(2, 1f)]Long
    }

    [SingleTraitAttribute(GeneticTrait.EyeColor)]
    public enum EyeColorTrait
    {
        [TraitSingleValueCharacteristic(1, 0f)]Brown,
        [TraitSingleValueCharacteristic(1, 0.5f)]Blue,
        [TraitSingleValueCharacteristic(2, 1f)]Red
    }

    [SingleTraitAttribute(GeneticTrait.TailType)]
    public enum TailTypeTrait
    {
       [TraitSingleValueCharacteristic(1, 0f)]Curvy,
       [TraitSingleValueCharacteristic(1, 0.5f)] Straight,
       [TraitSingleValueCharacteristic(2, 1f)] Looped
    }
}


public enum GeneticTrait
{
    LegLength, EyeColor, TailType
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

        var enumValuesWithAttribute = new List<object>( enumValues.Cast<object>()).Select(c =>
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
