using System;
using FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Interfaces;
using UnityEngine;

namespace FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Example
{
    [Serializable]
    public class ExamplePlaceContainer : IPlaceContainer
    {
        [SerializeField] private int Type;
        [SerializeField] private int WaveID;

        [SerializeField] private Vector3 spawnPoint;
        [SerializeField] private Vector3 spawnRotation;
        [SerializeField] private Vector3 spawnScale;

        [SerializeField] private string additionalValue;
        
        public string AdditionalValue
        {
            get => additionalValue;
            set => additionalValue = value;
        }
        
        public int TypeID
        {
            get => Type;
            set => Type = value;
        }

        public int Wave
        {
            get => WaveID;
            set => WaveID = value;
        }
        public Vector3 SpawnPoint
        {
            get => spawnPoint;
            set => spawnPoint = value;
        }
        public Vector3 SpawnRotation
        {
            get => spawnRotation;
            set => spawnRotation = value;
        }
        public Vector3 SpawnScale
        {
            get => spawnScale;
            set => spawnScale = value;
        }
    }
}