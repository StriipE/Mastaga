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
            EnemySprite = (GameObject) Instantiate(UnityEngine.Resources.Load(@"Prefabs/Spanker"));
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
        }

        public override void attack()
        {
            throw new NotImplementedException();
        }
    }
}
