using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using EblanDev.ScenarioCore.UtilsFramework.ID;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.FXSystemUnit
{
    [System.Serializable]
    [InlineProperty]
    public struct FXID
    {
        [FXID] [HideLabel] [SerializeField] private int _value;
        public static FXID Invalid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new FXID(IDHelper.Invalid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(FXID v) => v._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator FXID(int v) => new FXID(v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FXID(int v) => _value = v;

        private const string ScriptableKey = "FXID";
        
        private static ScriptableID so;
        
        public static FXID Random
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

        public static IEnumerable<FXID> GetAll
        {
            get
            {
                if (so == null)
                {
                    so = ScriptableID.Load(ScriptableKey);
                }

                return so != null ? so.ToList.Select((v) => (FXID) v.id) : new List<FXID>();
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

    public class FXIDAttribute : ScriptableIDDropdownAttribute
    {
        public FXIDAttribute() : base("FXID") { }
    }
    
    public class FXIDDrawer : ScriptableIDDropdownDrawer<FXIDAttribute> { }
}