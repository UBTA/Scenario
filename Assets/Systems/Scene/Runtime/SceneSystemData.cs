using System;
using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework.Data;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.SceneSystemUnit
{
    /// <summary>
    /// Скриптабл хранилище контейнеров сцен.
    /// </summary>
    [CreateAssetMenu(fileName = "SceneData", menuName = "ScenarioFramework/Systems/SceneData", order = 1)]
    public partial class SceneSystemData : DataClear
    {
        /// <summary>
        /// Коллекция контейнеров.
        /// </summary>
        [SerializeField] protected List<SceneContainer> Scenes;
        /// <summary>
        /// FallBack контейнер.
        /// </summary>
        [Space]
        [SerializeField] protected SceneContainer fallBackScene;

        /// <summary>
        /// Получить контейнер сцены из хранилища.
        /// </summary>
        /// <param name="ID">
        /// Айди контейнера.
        /// </param>
        /// <returns>
        /// Возвращает контейнер с нужным айди, либо fallBack контейнер.
        /// </returns>
        public SceneContainer GetScene(int ID)
        {
            foreach (var container in Scenes)
            {
                if (container.ID == ID)
                {
                    return container;
                }
            }

            return fallBackScene;
        }
    }

    /// <summary>
    /// Контейнер сцены.
    /// </summary>
    [Serializable]
    public class SceneContainer
    {
        /// <summary>
        /// Айди контейнера.
        /// </summary>
        public int ID;
        
        /// <summary>
        /// Путь к сцене.
        /// </summary>
        public string Path;
    }
}