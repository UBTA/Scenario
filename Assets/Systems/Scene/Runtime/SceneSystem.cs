using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EblanDev.ScenarioCore.GameFramework.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EblanDev.ScenarioCore.Systems.SceneSystemUnit
{
    /// <summary>
    /// Система управления сценами.
    /// Загрузка и выгрузка сцен.
    /// </summary>
    public partial class SceneSystem : SystemDated<SceneSystemData>
    {
        /// <summary>
        /// Вызывается автоматически.
        /// Точка входа для юзер кода.
        /// </summary>
        public override void Init()
        {
        }

        /// <summary>
        /// Загрузка сцены.
        /// </summary>
        /// <param name="id">
        /// Айди контейнера.
        /// </param>
        /// <param name="token">
        /// Токен для контроля асинхронности.
        /// </param>
        /// <param name="isAdditive">
        /// Аддитивно ли загрузить сцену.
        /// </param>
        /// <param name="OnProgress">
        /// Событие для отслеживания прогресса 0.0f-1.0f.
        /// </param>
        public virtual async UniTask Load(int id, CancellationToken token, bool isAdditive = false, Action<float> OnProgress = null)
        {
            var scene = Data.GetScene(id);
            
            AsyncOperation loadingProcess = null;
            if (SceneManager.GetSceneByPath(scene.Path).isLoaded == false)
            {
                loadingProcess = SceneManager.LoadSceneAsync(scene.Path, 
                    isAdditive ? 
                        LoadSceneMode.Additive : 
                        LoadSceneMode.Single);
            }

            if (loadingProcess is { })
            {
                await UniTask.WaitUntil(() =>
                {
                    OnProgress?.Invoke(loadingProcess.progress);
                    return loadingProcess.isDone;
                }, cancellationToken: token);
            }
        }
        
        /// <summary>
        /// Загрузка сцены.
        /// </summary>
        /// <param name="id">
        /// Айди контейнера.
        /// </param>
        /// <param name="isAdditive">
        /// Аддитивно ли загрузить сцену.
        /// </param>
        /// <param name="OnProgress">
        /// Событие для отслеживания прогресса 0.0f-1.0f.
        /// </param>
        public virtual async UniTask Load(int id, bool isAdditive = false, Action<float> OnProgress = null)
        {
            var scene = Data.GetScene(id);
            
            AsyncOperation loadingProcess = null;
            if (SceneManager.GetSceneByPath(scene.Path).isLoaded == false)
            {
                loadingProcess = SceneManager.LoadSceneAsync(scene.Path, 
                   isAdditive ? 
                        LoadSceneMode.Additive : 
                        LoadSceneMode.Single);
            }

            if (loadingProcess is { })
            {
                await UniTask.WaitUntil(() =>
                {
                    OnProgress?.Invoke(loadingProcess.progress);
                    return loadingProcess.isDone;
                });
            }
        }
        
        /// <summary>
        /// Выгрузка сцены.
        /// </summary>
        /// <param name="id">
        /// Айди контейнера.
        /// </param>
        /// <param name="token">
        /// Токен для контроля асинхронности.
        /// </param>
        /// <param name="OnProgress">
        /// Событие для отслеживания прогресса 0.0f-1.0f.
        /// </param>
        public virtual async UniTask Unload(int id, CancellationToken token, Action<float> OnProgress = null)
        {
            var scene = Data.GetScene(id);
            
            AsyncOperation unloadingProcess = null;
            if (SceneManager.GetSceneByPath(scene.Path).isLoaded)
            {
                unloadingProcess = SceneManager.UnloadSceneAsync(scene.Path);
            }

            if (unloadingProcess is { })
            {
                await UniTask.WaitUntil(() =>
                {
                    OnProgress?.Invoke(unloadingProcess.progress);
                    return unloadingProcess.isDone;
                }, cancellationToken: token);
            }
        }
        
        /// <summary>
        /// Выгрузка сцены.
        /// </summary>
        /// <param name="id">
        /// Айди контейнера.
        /// </param>
        /// <param name="OnProgress">
        /// Событие для отслеживания прогресса 0.0f-1.0f.
        /// </param>
        public virtual async UniTask Unload(int id, Action<float> OnProgress = null)
        {
            var scene = Data.GetScene(id);
            
            AsyncOperation unloadingProcess = null;
            if (SceneManager.GetSceneByPath(scene.Path).isLoaded)
            {
                unloadingProcess = SceneManager.UnloadSceneAsync(scene.Path);
            }

            if (unloadingProcess is { })
            {
                await UniTask.WaitUntil(() =>
                {
                    OnProgress?.Invoke(unloadingProcess.progress);
                    return unloadingProcess.isDone;
                });
            }
        }
    }
}