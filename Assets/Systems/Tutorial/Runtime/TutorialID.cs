using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using EblanDev.ScenarioCore.UtilsFramework.ID;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.TutorialSystemUnit
{
    [System.Serializable]
    [InlineProperty]
    public struct TutorialID
    {
        [TutorialID] [HideLabel] [SerializeField] private int _value;
        public static TutorialID Invalid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new TutorialID(IDHelper.Invalid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(TutorialID v) => v._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator TutorialID(int v) => new TutorialID(v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TutorialID(int v) => _value = v;

        private const string ScriptableKey = "TutorialID";
        
        private static ScriptableID so;
        
        public static TutorialID Random
        {
            get
            {
                if (so == null)
                {
                    so = ScriptableID.Load(ScriptableKey);
                }
                return so != null ? so.ToList.GetRandom().id : 0;
            }
        }

        public static IEnumerable<TutorialID> GetAll
        {
            get
            {
                if (so == null)
                {
                    so = ScriptableID.Load(ScriptableKey);
                }

                return so != null ? so.ToList.Select((v) => (TutorialID) v.id) : new List<TutorialID>();
            }
        }
        
        public override string ToString()
        {
            if (so == null)
            {
                so = ScriptableID.Load(ScriptableKey);
            }
            return so != null ? so.IDToString(_value) : string.Empty;
        }
    }

    public class TutorialIDAttribute : ScriptableIDDropdownAttribute
    {
        public TutorialIDAttribute() : base("TutorialID") { }
    }
    
    public class TutorialIDDrawer : ScriptableIDDropdownDrawer<TutorialIDAttribute> { }
}