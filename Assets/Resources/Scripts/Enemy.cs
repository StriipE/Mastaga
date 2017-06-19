using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        public const int MINIMAL_DISTANCE_TO_HERO = 15;

        public Player TargetPlayer;
        private float timeSinceLastAttack;

        public int HP { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int MagicPower { get; set; }
        public int PhysicalDefense { get; set; }
        public int MagicalDefense { get; set; }
        public float Speed { get; set; }
        public float AttackRate { get; set; }
        public GameObject EnemySprite { get; set; }
        public Attack[] EnemyAttacks { get{ return gameObject.GetComponents<Attack>(); } }

        public void Awake()
        {
            TargetPlayer = GameObject.Find("Player").GetComponent<Player>();
            setAttacks();
            timeSinceLastAttack = 1f / AttackRate;
        }

        public virtual void moveTowardsHero()
        {
            if (EnemySprite.transform.position.x > MINIMAL_DISTANCE_TO_HERO)
                EnemySprite.transform.position = Vector3.Lerp(EnemySprite.transform.position,
                                                              new Vector3(EnemySprite.transform.position.x - Speed, 1, EnemySprite.transform.position.z), Speed * Time.time);
            else
            {
                timeSinceLastAttack -= Time.deltaTime;
                if (timeSinceLastAttack < 0)
                {
                    castRandomAttack();
                    timeSinceLastAttack = 1f / AttackRate;
                }
            }
        }

        public virtual void castRandomAttack()
        {
            int random = UnityEngine.Random.Range(0, EnemyAttacks.Length);
            EnemyAttacks.ElementAt(random).castAttackOnPlayer(TargetPlayer);
        }

        public void Update()
        {
            moveTowardsHero();
        }

        public void OnDestroy()
        {
            Destroy(EnemySprite);
        }

        protected abstract void setAttacks();
    }
}
