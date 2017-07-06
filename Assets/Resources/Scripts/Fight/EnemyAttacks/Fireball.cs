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
            AttackSprites = new List<GameObject>();
        }

        void Update()
        {
            for (int i = 0; i < AttackSprites.Count; i++)
            { 
                    if (AttackSprites[i].transform.position.x > (playerPosition.x + playerSize.x / 2f))
                        AttackSprites[i].transform.position = Vector3.Lerp(AttackSprites[i].transform.position,
                                                                       new Vector3(AttackSprites[i].transform.position.x - SPELL_SPEED, 1, AttackSprites[i].transform.position.z),
                                                                       SPELL_SPEED * Time.time);
                    else
                    {
                        Destroy(AttackSprites[i]);
                        AttackSprites.RemoveAt(i);
                    }
                
            }
        }

        public override float calculateDamage()
        {
            float magicPower = enemyCastingThisSpell.MagicPower;
            return magicPower * SPELL_BASE_DAMAGE;
        }

        public override void castAttackOnPlayer(Player player)
        {
            GameObject sprite = (GameObject)Instantiate(UnityEngine.Resources.Load(@"Prefabs/Fight/Spells/Fireball"));
            sprite.transform.position = enemyCastingThisSpell.transform.position 
                                              - new Vector3(enemyCastingThisSpell.transform.localScale.x / 2f, 0, 0); // Casts fireball at current enemy position 

            AttackSprites.Add(sprite);

            float damage = calculateDamage();
            player.getDamaged(damage);
            renderDamageOnPlayer(player, damage);
            Debug.Log("Casted Fireball on player for " + damage + " damage.");
        }

        void OnDestroy()
        {
           foreach (GameObject sprite in AttackSprites)
               Destroy(sprite);
        }
    }
}
