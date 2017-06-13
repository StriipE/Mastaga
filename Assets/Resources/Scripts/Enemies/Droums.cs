using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Enemies
{
    public class Droums : Enemy
    {
        void Start()
        {
            EnemySprite = (GameObject)Instantiate(UnityEngine.Resources.Load(@"Prefabs/Droums"));
        }

        public Droums()
        {
            HP = 30;
            Strength = 60;
            Dexterity = 15;
            MagicPower = 10;
            PhysicalDefense = 15;
            MagicalDefense = 5;
            Speed = 1.5f;
        }

        public override void attack()
        {
            throw new NotImplementedException();
        }
    }
}
