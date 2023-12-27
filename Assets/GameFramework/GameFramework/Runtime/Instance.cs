using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EblanDev.ScenarioCore.GameFramework
{
    public class Instance : MonoBehaviour, IInstance
    {
        [SerializeField] protected bool isBusy;
        [SerializeField] protected bool autoKill;
        [SerializeField] protected float lifeTime;

        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;

        public bool IsBusy => isBusy;

        public virtual void Init()
        {
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            
            isBusy = true;
            if (autoKill)
            {
                StartLifeTime();
            }
        }
        
        protected virtual async void StartLifeTime(float time = -1f)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(time < 0 ? lifeTime : time), token);
            }
            catch
            {
                return;
            }

            if (token.IsCancellationRequested == false)
            {
                End();
            }
        }
        
        public virtual void End()
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();   
            }
            isBusy = false;
        }

        protected virtual void OnDestroy()
        {
            End();
        }
    }
}