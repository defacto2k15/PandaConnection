using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PandaLogic.Genetics
{
    [Serializable]
    public class Gene
    {
        public GeneticTrait Trait;
        public float FatherGene;
        public float MotherGene;
    }
}
