using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Attacks
{
    public class Fireball : Attack
    {
        public override float calculateDamage()
        {
            int magicPower = gameObject.GetComponent<Enemy>().MagicPower;
            return magicPower * 1.5f;
        }

        public override void castAttackOnPlayer(Player player)
        {
            float damage = calculateDamage();
            player.HP -= damage;
            Debug.Log("Casted Fireball on player for " + damage + " damage. New HP count = " + player.HP);
        }
    }
}
