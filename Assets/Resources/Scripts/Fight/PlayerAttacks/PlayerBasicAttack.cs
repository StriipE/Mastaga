using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Attacks
{
    public class PlayerBasicAttack : Attack
    {
        public override float calculateDamage()
        {
            float strength = gameObject.GetComponent<Player>().Strength;
            return strength * 2f;
        }

        public override void castAttackOnEnemy(Enemy enemy)
        {
            float damage = calculateDamage();
            enemy.getDamaged(damage);
            Debug.Log("Casted BasicAttack on enemy for " + damage + " damage.");
        }
    }
}
