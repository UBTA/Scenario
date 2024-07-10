using System.Collections.Generic;
using EblanDev.ScenarioCore.GameFramework;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.FXSystemUnit
{
    public class ParticleInstance : Instance
    {
        public FXID ID;

        [SerializeField] private List<GameObject> enableObjects;
        [SerializeField] private List<ParticleSystem> particles;

        [SerializeField] private int emitCount;
        
        private Transform originalP;
        
        public void ParentTo(Transform parent, Transform currentParent)
        {
            originalP = currentParent;
            transform.SetParent(parent);
        }

        [Button]
        public void Play()
        {
            gameObject.SetActive(true);

            if (enableObjects.Count != 0)
            {
                foreach (var obj in enableObjects)
                {
                    obj.SetActive(false);
                }

                enableObjects.GetRandom().SetActive(true);
            }

            foreach (var particle in particles)
            {
                if (particle != null)
                {
                    particle.Play();
                }
            }
            StartLifeTime();
        }

        public override void End()
        {
            base.End();
            
            if (originalP != null)
            {
                transform.SetParent(originalP);
            }

            gameObject.SetActive(false);
        }
    }
}