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
        private float timeToLive = 10f;
        private GameObject enemyHPBarHandler;
        private GenericProgressBar enemyHPBar;

        public float HP { get; set; }
        public float MaxHP { get; set; }
        public float Strength { get; set; }
        public float Dexterity { get; set; }
        public float MagicPower { get; set; }

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

        public void getDamaged(float damage)
        {
            HP -= damage;
            enemyHPBar.updateCurrent(HP);
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
            timeToLive -= Time.deltaTime;
            renderHPBar();
            if (HP < 0)
                Destroy(this);
        }

        public void OnDestroy()
        {
            Destroy(enemyHPBarHandler);
            Destroy(EnemySprite.transform.parent.gameObject);
        }

        public void setupHPBar()
        {
            enemyHPBarHandler = (GameObject)Instantiate(UnityEngine.Resources.Load(@"Prefabs/MonsterHPBar"));
            enemyHPBar = enemyHPBarHandler.GetComponent<GenericProgressBar>();
            enemyHPBar.barColor = new Color32(255, 0, 0, 255);
            enemyHPBar.setValues(HP, MaxHP);
            enemyHPBarHandler.transform.SetParent(GameObject.Find("Canvas").transform);
        }

        public void renderHPBar()
        {
            Vector3 enemyHPBarPositionInCanvas = GameObject.Find("Main Camera").GetComponent<Camera>().
                                                 WorldToScreenPoint(gameObject.transform.GetChild(0).position  // Renders HPBarPosition on Canvas
                                                  + new Vector3(0, 0, gameObject.transform.GetChild(0).localScale.y / 2 + 2f)); // Offset z axis with enemy height
            enemyHPBarHandler.GetComponent<RectTransform>().anchoredPosition = enemyHPBarPositionInCanvas;
            enemyHPBar.renderValues();
        }

        protected abstract void setAttacks();
    }
}
