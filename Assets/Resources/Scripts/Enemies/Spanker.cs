﻿using Assets.Resources.Scripts.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Enemies
{
    public class Spanker : Enemy
    {
        void Start()
        {
            EnemySprite = (GameObject) Instantiate(UnityEngine.Resources.Load(@"Prefabs/Monsters/Spanker"));
            EnemySprite.transform.parent = gameObject.transform;
        }

        protected override void setAttacks()
        {
            gameObject.AddComponent<BasicAttack>();
            gameObject.AddComponent<Fireball>();
        }

        public Spanker()
        {
            HP = 20;
            Strength = 20;
            Dexterity = 20;
            MagicPower = 2;
            PhysicalDefense = 10;
            MagicalDefense = 10;
            Speed = 1f;
            AttackRate = 1f;
        }

    }
}