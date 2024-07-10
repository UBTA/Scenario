using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using EblanDev.ScenarioCore.UtilsFramework.ID;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.SoundSystemUnit
{
    [System.Serializable]
    [InlineProperty]
    public struct SoundID
    {
        [SoundID] [HideLabel] [SerializeField] private int _value;
        public static SoundID Invalid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new SoundID(IDHelper.Invalid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(SoundID v) => v._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SoundID(int v) => new SoundID(v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private SoundID(int v) => _value = v;

        private const string ScriptableKey = "SoundID";
        
        private static ScriptableID so;
        
        public static SoundID Random
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

        public static IEnumerable<SoundID> GetAll
        {
            get
            {
                if (so == null)
                {
                    so = ScriptableID.Load(ScriptableKey);
                }

                return so != null ? so.ToList.Select((v) => (SoundID) v.id) : new List<SoundID>();
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

    public class SoundIDAttribute : ScriptableIDDropdownAttribute
    {
        public SoundIDAttribute() : base("SoundID") { }
    }
    
    public class SoundIDDrawer : ScriptableIDDropdownDrawer<SoundIDAttribute> { }
}