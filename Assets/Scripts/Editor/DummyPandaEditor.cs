using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.AI;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    
    //[CustomEditor(typeof(DummyPanda))]
    public class DummyPandaEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var panda = target as DummyPanda;
            if (GUILayout.Button("Create Genotype from Phenotype"))
            {
                panda.GetStats().CreateGenotypeFromPhenotype();

            }
        }
    }
}
