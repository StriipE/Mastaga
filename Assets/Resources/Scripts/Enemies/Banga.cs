using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Enemies
{
    public class Banga : Enemy
    {
        void Start()
        {
            EnemySprite = (GameObject)Instantiate(UnityEngine.Resources.Load(@"Prefabs/Banga"));
        }

        public Banga()
        {
            HP = 40;
            Strength = 30;
            Dexterity = 10;
            MagicPower = 5;
            PhysicalDefense = 20;
            MagicalDefense = 10;
            Speed = 0.75f;
        }

        public override void attack()
        {
            throw new NotImplementedException();
        }
    }
}
