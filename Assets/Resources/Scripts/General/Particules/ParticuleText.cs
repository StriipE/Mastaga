using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticuleText
{
    GameObject gameObject;
    private Text text;
    private float timer = 0f;
    private float maxTimer = 3.0f;
    private const float TEXT_SPEED = 1f;

    public ParticuleText(GameObject target, string text, float timerValue)
    {
        this.maxTimer = timerValue;
        GameObject canvas = GameObject.Find("NPData");
        gameObject = new GameObject(text);

        gameObject.transform.SetParent(canvas.transform);

        this.text = gameObject.AddComponent<Text>();
        this.text.text = text;
        this.text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        this.text.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);

        this.text.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().
                                             WorldToScreenPoint(target.transform.position +
                                             new Vector3(1, 0, 0)); // Offsets texts over the damaged target
    }

	public void Update () {
        timer += Time.deltaTime;
        if (timer > maxTimer)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            text.color = new Color(255, 0, 0, timer / maxTimer - 1 );
            text.transform.position = Vector3.Lerp(text.transform.position,
                           new Vector3(text.transform.position.x, text.transform.position.y, 0),
                           Time.time);
        }
    }

    public bool isOver()
    {
        return timer > maxTimer;
    }
}
