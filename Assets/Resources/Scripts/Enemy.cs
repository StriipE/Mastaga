using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        public const int MINIMAL_DISTANCE_TO_HERO = -10;

        public int HP { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int MagicPower { get; set; }
        public int PhysicalDefense { get; set; }
        public int MagicalDefense { get; set; }
        public float Speed { get; set; }
        public GameObject EnemySprite { get; set; }

        public virtual void moveTowardsHero()
        {
            if (EnemySprite.transform.position.x > MINIMAL_DISTANCE_TO_HERO)
                EnemySprite.transform.position = Vector3.Lerp(EnemySprite.transform.position, 
                                                              new Vector3(EnemySprite.transform.position.x - Speed, 1, EnemySprite.transform.position.z), Speed * Time.time);
            else
                Destroy(gameObject); // Temporary enemy kill if it's too far on the left 
        }

        public abstract void attack();

        public void Update()
        {
            moveTowardsHero();
        }

        public void OnDestroy()
        {
            Destroy(EnemySprite);
        } 
    }
}
