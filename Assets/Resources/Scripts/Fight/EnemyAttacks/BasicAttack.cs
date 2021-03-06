﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Attacks
{
    public class BasicAttack : Attack
    {
        public override float calculateDamage()
        {
            float strength = gameObject.GetComponent<Enemy>().Strength;
            return strength * 2f;
        }

        public override void castAttackOnPlayer(Player player)
        {
            float damage = calculateDamage();
            player.getDamaged(damage);
        }
    }
}
