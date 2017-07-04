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
        private Vector3 enemySize;
        private Player playerCastingThisSpell;

        void Start()
        {
            GameObject enemyGO = GameObject.FindGameObjectsWithTag("Enemy")[0];
            enemyPosition = enemyGO.transform.position;
            enemySize = enemyGO.transform.localScale;
            playerCastingThisSpell = gameObject.GetComponent<Player>();
        }

        void Update()
        {
            if (AttackSprite != null)
            {
                if (AttackSprite.transform.position.x < (enemyPosition.x + enemyPosition.x / 2f))
                    AttackSprite.transform.position = Vector3.Lerp(AttackSprite.transform.position,
                                                                   new Vector3(AttackSprite.transform.position.x + SPELL_SPEED, 1, AttackSprite.transform.position.z),
                                                                   SPELL_SPEED * Time.time);
                else
                    Destroy(AttackSprite);
            }

        }

        public override float calculateDamage()
        {
            float magicPower = playerCastingThisSpell.MagicPower;
            return magicPower * SPELL_BASE_DAMAGE;
        }

        public override void castAttackOnEnemy(Enemy enemy)
        {
            AttackSprite = (GameObject)Instantiate(UnityEngine.Resources.Load(@"Prefabs/Fight/Spells/Fireball"));
            AttackSprite.transform.position = gameObject.transform.position 
                                              + new Vector3(gameObject.transform.localScale.x / 2f, 0, 0); // Casts fireball at current enemy position 

            float damage = calculateDamage();
            enemy.getDamaged(damage);
            Debug.Log("Casted Fireball on player for " + damage + " damage.");
        }

        void OnDestroy()
        {
          //  Destroy(AttackSprite);
        }
    }
}
