using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : ParticuleText
{
    private const float TEXT_SPEED = 1f;   

    public Text damageText;

    public DamageText(GameObject target, string text, float timerValue) : base(target, text, timerValue) { this.setColor(Color.red); }

}

