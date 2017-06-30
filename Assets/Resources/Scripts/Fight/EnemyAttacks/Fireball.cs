using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Attacks
{
    public class Fireball : Attack
    {
        private const float SPELL_BASE_DAMAGE = 1.5f;
        private const float SPELL_SPEED = .4f;
        private Vector3 playerPosition;
        private Vector3 playerSize;
        private Enemy enemyCastingThisSpell;

        void Start()
        {
            enemyCastingThisSpell = gameObject.GetComponent<Enemy>();
            playerPosition = GameObject.Find("Player").transform.position;
            playerSize = GameObject.Find("Player").transform.localScale;
        }

        void Update()
        {
            if (AttackSprite != null)
            {
                if (AttackSprite.transform.position.x > (playerPosition.x + playerSize.x / 2f))
                    AttackSprite.transform.position = Vector3.Lerp(AttackSprite.transform.position,
                                                                   new Vector3(AttackSprite.transform.position.x - SPELL_SPEED, 1, AttackSprite.transform.position.z),
                                                                   SPELL_SPEED * Time.time);
                else
                    Destroy(AttackSprite);
            }

        }

        public override float calculateDamage()
        {
            float magicPower = enemyCastingThisSpell.MagicPower;
            return magicPower * SPELL_BASE_DAMAGE;
        }

        public override void castAttackOnPlayer(Player player)
        {
            AttackSprite = (GameObject)Instantiate(UnityEngine.Resources.Load(@"Prefabs/Fight/Spells/Fireball"));
            AttackSprite.transform.position = enemyCastingThisSpell.transform.GetChild(0).position 
                                              - new Vector3(enemyCastingThisSpell.transform.GetChild(0).localScale.x / 2f, 0, 0); // Casts fireball at current enemy position 

            float damage = calculateDamage();
            player.getDamaged(damage);
            Debug.Log("Casted Fireball on player for " + damage + " damage.");
        }

        void OnDestroy()
        {
           Destroy(AttackSprite);
        }
    }
}
