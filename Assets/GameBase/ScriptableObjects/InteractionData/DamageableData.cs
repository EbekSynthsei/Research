using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LaniakeaCode.Utilities
{
    [CreateAssetMenu(fileName = "Damageable Data", menuName = "LaniakeaTools/Damageable")]
    public class DamageableData : InteractableData, IDamageable
    {
        [Header("DamageData")]
        [SerializeField]
        IDamageable damageableTarget;
        [SerializeField]
        WeaponData damageType;
        [SerializeField]
        float damageMultiplier;
        [SerializeField]
        float persistent;

        public void Damage(float amount)
        {
            throw new NotImplementedException();
        }
    }
}
