using System.Collections.Generic;
using FAwesome.ScenarioCore.GameFramework;
using FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Interfaces;
using UnityEngine;

namespace FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Example
{
    public class ExampleWavePlacerWorld : Instance, IWavePlacerWorld<ExamplePlaceContainer>
    {
        [SerializeField] private List<ExamplePlaceContainer> places;

        public List<ExamplePlaceContainer> Points
        {
            get => places;
            set => places = value;
        }
    }
}