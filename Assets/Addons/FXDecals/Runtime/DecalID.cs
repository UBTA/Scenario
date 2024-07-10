using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using EblanDev.ScenarioCore.UtilsFramework.ID;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EblanDev.ScenarioCore.FXDecals
{
    [System.Serializable]
    [InlineProperty]
    public struct DecalID
    {
        [DecalID] [HideLabel] [SerializeField] private int _value;
        public static DecalID Invalid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new DecalID(IDHelper.Invalid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(DecalID v) => v._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator DecalID(int v) => new DecalID(v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DecalID(int v) => _value = v;

        private const string ScriptableKey = "DecalID";
        
        private static ScriptableID so;
        
        public static DecalID Random
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

        public static IEnumerable<DecalID> GetAll
        {
            get
            {
                if (so == null)
                {
                    so = ScriptableID.Load(ScriptableKey);
                }

                return so != null ? so.ToList.Select((v) => (DecalID) v.id) : new List<DecalID>();
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

    public class DecalIDAttribute : ScriptableIDDropdownAttribute
    {
        public DecalIDAttribute() : base("DecalID") { }
    }
    
    public class DecalIDDrawer : ScriptableIDDropdownDrawer<DecalIDAttribute> { }
}