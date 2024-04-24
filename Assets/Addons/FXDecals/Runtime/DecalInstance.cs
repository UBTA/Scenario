using EblanDev.ScenarioCore.CharacterFramework.Modules.Hittable;
using EblanDev.ScenarioCore.GameFramework;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using UnityEngine;

namespace EblanDev.ScenarioCore.FXDecals
{
    public class DecalInstance : Instance
    {
        public int TypeID;

        [SerializeField] private Material[] decals;
        [SerializeField] private MeshRenderer render;
        
        private Transform originalP;
        private Vector3 origScale;

        public override void Init()
        {
            base.Init();
            origScale = transform.localScale;
        }

        public void ParentTo(Transform parent, Transform currentParent)
        {
            originalP = currentParent;
            transform.SetParent(parent);
        }

        public void Place(Vector3 dir)
        {
            if (render != null)
            {
                render.material = decals.GetRandom();
            }

            gameObject.SetActive(true);
            transform.position -= dir.normalized * 0.01f;

            var hits = Physics.RaycastAll(transform.position - dir.normalized * 3f, dir, 5);

            if (hits.Length != 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.transform.TryGetComponent<PhysActivator>(out var act))
                    {
                        transform.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
                        break;
                    }
                }
            }

            StartLifeTime();
        }

        public override void End()
        {
            base.End();
            transform.SetParent(originalP);
            gameObject.SetActive(false);
            transform.localScale = origScale;
        }
    }
}