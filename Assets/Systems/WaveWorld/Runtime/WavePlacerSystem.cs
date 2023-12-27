using System;
using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework;
using EblanDev.ScenarioCore.GameFramework.Systems;
using EblanDev.ScenarioCore.GameFramework.Workers;
using EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Data;
using EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Interfaces;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit
{
    /// <summary>
    /// Система обслуживающая работу мира волн.
    /// </summary>
    /// <typeparam name="WI">
    /// Инстанс мира волн
    /// </typeparam>
    /// <typeparam name="I">
    /// Инстанс размещаемой сущности
    /// </typeparam>
    /// <typeparam name="D">
    /// Скриптабл дата системы.
    /// </typeparam>
    /// <typeparam name="P">
    /// Контейнер точки в мире
    /// </typeparam>
    public abstract class WavePlacerSystem<WI, I, D, P> : SystemDated<D>
        where WI : MonoBehaviour, IInstance, IWavePlacerWorld<P>
        where I : MonoBehaviour, IInstance, IWavePlacerInstance
        where D : WavePlacerData<WI, P, I>
        where P : IPlaceContainer
    {
        /// <summary>
        /// Пул для инстансов.
        /// </summary>
        protected WorkerPulled<I> instances;
        /// <summary>
        /// Пул для миров.
        /// </summary>
        protected WorkerSolo<WI> world;
        
        /// <summary>
        /// Последние подготовленные сущности.
        /// </summary>
        protected List<I> preparedWave = new List<I>();
        
        /// <summary>
        /// Последние активные сущности.
        /// </summary>
        protected List<I> activeWave = new List<I>();
        
        /// <summary>
        /// Индекс волны.
        /// </summary>
        protected int waveIndex = -1;
        
        /// <summary>
        /// Создать мир.
        /// </summary>
        /// <param name="OnPoint">
        /// Событие для отслеживания создания каждой точки в мире.
        /// </param>
        public virtual void CreateWorld(Action<I, P> OnPoint = null)
        {
            world.Create(Data.GetWorld());

            var waves = world.Get.Points;
            foreach (var point in waves)
            {
                var inst = instances.Create(Data.GetInstance(point.TypeID));
                inst.WaveID = point.Wave;
                
                inst.transform.position = point.SpawnPoint;
                inst.transform.rotation = Quaternion.Euler(point.SpawnRotation);
                inst.transform.localScale = point.SpawnScale;
                
                OnPoint?.Invoke(inst, point);
            }
        }

        /// <summary>
        /// Подготовка мира волн к активации.
        /// </summary>
        public virtual void PrepareWave()
        {
            waveIndex++;
            preparedWave.Clear();
            
            foreach (var inst in instances.GetAll())
            {
                if (inst.WaveID == waveIndex)
                {
                    preparedWave.Add(inst);
                    inst.Prepare();
                }
            }
        }

        /// <summary>
        /// Активация мира волн.
        /// </summary>
        public virtual void ActivateWave()
        {
            activeWave.Clear();
            
            foreach (var inst in preparedWave)
            {
                activeWave.Add(inst);
                inst.Activate();
            }
        }

        /// <summary>
        /// Выход мира волн из рантайма.
        /// </summary>
        /// <param name="saveData"></param>
        public override void Exit(bool saveData = true)
        {
            waveIndex = -1;
            
            world.Clear();
            instances.Clear();
            preparedWave.Clear();
            
            base.Exit(saveData);
        }
    }
}