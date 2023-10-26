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


        public int TypeID => Type;
        public int Wave => WaveID;
        public Vector3 SpawnPoint => spawnPoint;
        public Vector3 SpawnRotation => spawnRotation;
        public Vector3 SpawnScale => spawnScale;
    }
}