using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Attacks
{
    public class PlayerFireball : Attack
    {
        private const float SPELL_BASE_DAMAGE = 1.5f;
        private const float SPELL_SPEED = .4f;
        private const float MP_COST = 10;

        private Vector3 enemyPosition;

        private Player playerCastingThisSpell;
        private GameObject enemyGO;

        void Start()
        {
            playerCastingThisSpell = gameObject.GetComponent<Player>();
            AttackSprites = new List<GameObject>();
        }

        void Update()
        {
            for (int i = 0; i < AttackSprites.Count; i++)
            {
                if (enemyGO != null)
                {
                    enemyPosition = enemyGO.transform.position;
                    if (AttackSprites[i].transform.position.x < (enemyPosition.x - enemyGO.transform.localScale.x / 2f))
                        AttackSprites[i].transform.position = Vector3.Lerp(AttackSprites[i].transform.position,
                                                                       new Vector3(AttackSprites[i].transform.position.x + SPELL_SPEED, 1, AttackSprites[i].transform.position.z),
                                                                       SPELL_SPEED * Time.time);
                    else
                    {
                        Destroy(AttackSprites[i]);
                        AttackSprites.RemoveAt(i);
                    }
                }
                else
                {
                    Destroy(AttackSprites[i]);
                    AttackSprites.RemoveAt(i);
                }
            }
        }

        public override float calculateDamage()
        {
            float magicPower = playerCastingThisSpell.MagicPower;
            return magicPower * SPELL_BASE_DAMAGE;
        }

        public override void castAttackOnEnemy(Enemy enemy)
        {
            enemyGO = GameObject.FindGameObjectsWithTag("Enemy")[0];

            GameObject sprite = (GameObject)Instantiate(UnityEngine.Resources.Load(@"Prefabs/Fight/Spells/Fireball"));
            sprite.transform.position = gameObject.transform.position 
                                              + new Vector3(gameObject.transform.localScale.x / 2f, 0, 0); // Casts fireball at current enemy position 

            AttackSprites.Add(sprite);

            float damage = calculateDamage();
            enemy.getDamaged(damage);
        }

        public float GetMPCost()
        {
            return MP_COST;
        }
        
        void OnDestroy()
        {
            foreach (GameObject sprite in AttackSprites)
                Destroy(sprite);
        }
    }
}
