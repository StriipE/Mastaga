using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public const int MINIMAL_DISTANCE_TO_HERO = 15;

        public Player TargetPlayer { get; set; }
        private float timeSinceLastAttack;

        private GameObject enemyHPBarHandler;
        private GenericProgressBar enemyHPBar;

        public float HP { get; set; }
        public float MaxHP;
        public float Strength;
        public float Dexterity;
        public float MagicPower;

        public int PhysicalDefense;
        public int MagicalDefense;
        public float Speed;
        public float AttackRate;
        public Attack[] EnemyAttacks;


        public void Awake()
        {
            TargetPlayer = GameObject.Find("Player").GetComponent<Player>();
          //  setAttacks();
            timeSinceLastAttack = 1f / AttackRate;
            
            HP = MaxHP;
            setupHPBar();
        }

        public void getDamaged(float damage)
        {
            HP -= damage;
            enemyHPBar.updateCurrent(HP);
        }


        public virtual void moveTowardsHero()
        {
            if (gameObject.transform.position.x > MINIMAL_DISTANCE_TO_HERO)
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
                                                      new Vector3(gameObject.transform.position.x - Speed, 1, gameObject.transform.position.z), Speed * Time.time);
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
            renderHPBar();
            if (HP <= 0)
                Destroy(this);
        }

        public void OnDestroy()
        {
            Destroy(enemyHPBarHandler);
            Destroy(gameObject.transform.gameObject);
        }

        public void setupHPBar()
        {
            enemyHPBarHandler = (GameObject)Instantiate(UnityEngine.Resources.Load(@"Prefabs/Fight/MonsterHPBar"));
            enemyHPBar = enemyHPBarHandler.GetComponent<GenericProgressBar>();
            enemyHPBar.barColor = new Color32(255, 0, 0, 255);
            enemyHPBar.setValues(HP, MaxHP);
            enemyHPBarHandler.transform.SetParent(GameObject.Find("Canvas").transform);
        }

        public void renderHPBar()
        {
            Vector3 enemyHPBarPositionInCanvas = GameObject.Find("Main Camera").GetComponent<Camera>().
                                                 WorldToScreenPoint(gameObject.transform.position  // Renders HPBarPosition on Canvas
                                                  + new Vector3(0, 0, gameObject.transform.localScale.y / 2 + 2f)); // Offset z axis with enemy height
            enemyHPBarHandler.GetComponent<RectTransform>().anchoredPosition = enemyHPBarPositionInCanvas;
            enemyHPBar.renderValues();
        }

       // protected abstract void setAttacks();
    }
}
