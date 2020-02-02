using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.AI.Activity;
using Assets.Scripts.Utils;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;
using Random = System.Random;

namespace Assets.Scripts.PandaLogic.Genetics
{
    public class ChildPandaCreator : MonoBehaviour
    {
        [SerializeField] private float _mutationStrength;
        [SerializeField] private GameObject _pandaPrefab;
        [SerializeField] private GameObject _penArea;
        [SerializeField] private Camera _camera;

        private Random _random;
        private int _createdPandasCount;

        public void InstantianteChild(GameObject father, GameObject mother)
        {
            var childStats = CreateChildStats(
                father.GetComponentNotNull<IPanda>().GetStats(),
                mother.GetComponentNotNull<IPanda>().GetStats()
            );

            var childPosition = (father.transform.position + mother.transform.position) * 0.5f;
            var childGo = GameObject.Instantiate(_pandaPrefab, childPosition, Quaternion.identity);
            childGo.GetComponentNotNull<IPanda>().SetStats(childStats);

            var behaviourExecutor = childGo.GetComponentNotNull<BehaviorExecutor>();
            Assert.IsTrue(behaviourExecutor.SetBehaviorParam("camera", _camera));
            Assert.IsTrue(behaviourExecutor.SetBehaviorParam("penArea", _penArea));
            childGo.GetComponentInChildrenNotNull<PandaMatingActivityArea>().ChildPandaCreator = this;
        }

        public PandaStats CreateChildStats(PandaStats father, PandaStats mother)
        {
            _random = new Random(_createdPandasCount++);
            Assert.IsTrue(father.Genotype.Count == mother.Genotype.Count);

            var newGenotype = DrawSingleValuesAndMutate(father.Genotype)
                .Join(DrawSingleValuesAndMutate(mother.Genotype), c => c.Trait, c => c.Trait,
                    (fatherGene, motherGene) => new Gene() {FatherGene = fatherGene.Value, MotherGene = motherGene.Value, Trait = fatherGene.Trait})
                .ToList();
            Phenotype newPhenotype = CreatePhenotype(newGenotype);

            var gender = _random.NextBool() ? Gender.Male : Gender.Female;

            return new PandaStats()
            {
                name = $"Panda No:{_createdPandasCount}",
                birthdate = _createdPandasCount,
                gender = gender,
                Genotype = newGenotype,
                Phenotype = newPhenotype
            };

        }

        private List<OneValueGene> DrawSingleValuesAndMutate(List<Gene> genes)
        {
            return genes.Select(gene =>
            {
                float geneValue;
                if (_random.NextBool())
                {
                    geneValue = gene.MotherGene;
                }
                else
                {
                    geneValue = gene.FatherGene;
                }

                return new OneValueGene() {Trait = gene.Trait, Value = geneValue};
            })
                .Select(MutateGene)
                .ToList();
        }

        private OneValueGene MutateGene(OneValueGene gene)
        {
            return new OneValueGene(){Trait = gene.Trait, Value = (float) (gene.Value + (_random.NextDouble()-0.5)*2*_mutationStrength)};
        }

        private Phenotype CreatePhenotype(List<Gene> newGenotype)
        {
            var newPhenotype = new Phenotype();
            foreach (var aGene in newGenotype)
            {
                var fatherQueryResult = TraitUtils.QueryTraitValue(aGene.Trait, aGene.FatherGene);
                var motherQueryResult = TraitUtils.QueryTraitValue(aGene.Trait, aGene.MotherGene);

                TraitValueQueryResult strongerQueryResult;
                if (fatherQueryResult.Strength > motherQueryResult.Strength)
                {
                    strongerQueryResult = fatherQueryResult;
                }else if (fatherQueryResult.Strength < motherQueryResult.Strength)
                {
                    strongerQueryResult = motherQueryResult;
                }
                else
                {
                    strongerQueryResult = _random.NextBool() ? fatherQueryResult : motherQueryResult;
                }


                TraitUtils.SetQueryValue(strongerQueryResult.QuantisizedEnumValue, newPhenotype);
            }

            return newPhenotype;
        }

        class OneValueGene
        {
            public GeneticTrait Trait;
            public float Value;
        }
    }
}
