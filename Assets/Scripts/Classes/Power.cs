using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Classes
{
    [Serializable]
    public class Power
    {
        #region ridersBullshit
            //riders suggested me to do this, I don't really know why I would need this since I have ==
            //I'll do some more research into this later
            private bool Equals(Power other)
            {
                return powerName == other.powerName;
            }

            public override bool Equals([CanBeNull] object obj)
            {
                if (obj is null) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((Power)obj);
            }
        #endregion

        [SerializeField] string powerName;
        [SerializeField] private string powerDescription;

        [SerializeField] MonoBehaviour powerAbilityComponent;

        public static bool operator ==(Power a, Power b)
        {
            if (b == null && a == null) return true;
            if (b == null || a == null) return false;
            return a.powerName == b.powerName;
        }

        public static bool operator !=(Power a, Power b)
        {
            if (b == null && a == null) return true;
            if (b == null || a == null) return false;
            return a.powerName != b.powerName;
        }

        public static Power Empty = new Power();

        public Power(MonoBehaviour powerAbilityComponent,  string powerName, string powerDescription)
        {
            this.powerAbilityComponent = powerAbilityComponent;
            this.powerName = powerName;
            this.powerDescription = powerDescription;
        }

        private Power()
        {
            this.powerName = string.Empty;
            this.powerDescription = string.Empty;
            this.powerAbilityComponent = null;
        }
    }
}