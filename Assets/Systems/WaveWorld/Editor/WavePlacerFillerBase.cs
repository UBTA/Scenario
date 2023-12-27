using System;
using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework;
using EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Data;
using EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.WavePlacerSystemUnit
{
    /// <summary>
    /// Эдитор монобех для удобного фила миров волн.
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
    public abstract class WavePlacerFillerBase<WI, I, D, P> : MonoBehaviour, IWavePlacerWorldFiller<WI, I, D, P>
        where WI : MonoBehaviour, IInstance, IWavePlacerWorld<P>
        where I : MonoBehaviour, IInstance, IWavePlacerInstance
        where D : WavePlacerData<WI, P, I>
        where P : IPlaceContainer
    {
        /// <summary>
        /// Дата в которой хранятся сущности мира волн.
        /// </summary>
        public D Data;
        /// <summary>
        /// Префаб мира волн
        /// </summary>
        public WI World;

        /// <summary>
        /// Сущности созданные в эдиторе.
        /// </summary>
        [SerializeField] protected List<I> EditorInstances = new List<I>();

        /// <summary>
        /// Создание мира в инспеторе.
        /// </summary>
        [Button]
        public void Create()
        {
            var points = World.Points;

            foreach (var point in points)
            {
                var inst = Instantiate(Data.GetInstance(point.TypeID), transform);

                inst.transform.position = point.SpawnPoint;
                inst.transform.rotation= Quaternion.Euler(point.SpawnRotation);
                inst.transform.localScale = point.SpawnScale;

                inst.WaveID = point.Wave;
                EditorInstances.Add(inst);
                
                SetAdditionalValues(inst, point);
            }
        }
        
        /// <summary>
        /// Добавление в мир новой сущности.
        /// </summary>
        /// <param name="point"></param>
        [Button]
        public void Add(P point)
        {
            var inst = Instantiate(Data.GetInstance(point.TypeID), transform);

            inst.transform.position = point.SpawnPoint;
            inst.transform.rotation= Quaternion.Euler(point.SpawnRotation);
            inst.transform.localScale = point.SpawnScale;

            inst.WaveID = point.Wave;
            EditorInstances.Add(inst);
                
            SetAdditionalValues(inst, point);
        }

        /// <summary>
        /// Устанавливает доп параметры из точки в инстанс.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="place"></param>
        protected abstract void SetAdditionalValues(I instance, P place);
        /// <summary>
        /// Устанавливает доп параметры из инстанса в точку
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="place"></param>
        protected abstract void GetAdditionalValues(I instance, P place);

        /// <summary>
        /// Сохранение эдитор мира в мир волн
        /// </summary>
        [Button]
        public void End()
        {
            List<P> points = new List<P>();

            foreach (var inst in EditorInstances)
            {
                var point = Activator.CreateInstance<P>();

                point.SpawnPoint = inst.transform.position;
                point.SpawnRotation = inst.transform.rotation.eulerAngles;
                point.SpawnScale = inst.transform.localScale;

                point.Wave = inst.WaveID;
                point.TypeID = inst.GetTypeID;

                GetAdditionalValues(inst, point);
                
                DestroyImmediate(inst.gameObject);
                points.Add(point);
            }
            
            EditorInstances.Clear();

            World.Points = points;
        }
    }
}