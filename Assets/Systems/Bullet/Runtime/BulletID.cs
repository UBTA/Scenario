using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using EblanDev.ScenarioCore.UtilsFramework.ID;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EblanDev.ScenarioCore.Systems.BulletSystemUnit
{
    [System.Serializable]
    [InlineProperty]
    public struct BulletID
    {
        [BulletID] [HideLabel] [SerializeField] private int _value;
        public static BulletID Invalid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new BulletID(IDHelper.Invalid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(BulletID v) => v._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BulletID(int v) => new BulletID(v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BulletID(int v) => _value = v;

        private const string ScriptableKey = "BulletID";
        
        private static ScriptableID so;
        
        public static BulletID Random
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

        public static IEnumerable<BulletID> GetAll
        {
            get
            {
                if (so == null)
                {
                    so = ScriptableID.Load(ScriptableKey);
                }

                return so != null ? so.ToList.Select((v) => (BulletID) v.id) : new List<BulletID>();
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

    public class BulletIDAttribute : ScriptableIDDropdownAttribute
    {
        public BulletIDAttribute() : base("BulletID") { }
    }
    
    public class BulletIDDrawer : ScriptableIDDropdownDrawer<BulletIDAttribute> { }
}