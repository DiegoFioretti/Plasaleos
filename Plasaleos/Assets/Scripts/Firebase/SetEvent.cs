using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetEvent : MonoBehaviour {

    private int alien = 0;

    private List<GameObject> aliensCollided;

    public UnityEvent ev;

    string parameter;
    string value;

    private float time;

    private void Start()
    {
        time = Time.time;
        aliensCollided = new List<GameObject>();
    }

    public void SetParameter(string _parameter)
    {
        parameter = _parameter;
    }

    public void SetValue(string _value)
    {
        value = _value;
    }

    public void SetTimeValue()
    {
        value = (Time.time - time).ToString();
    }

    public void NewEvent(string eventName)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName);
    }

    public void NewEventWithParameter(string eventName)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName, parameter, value);
        if(parameter == "Time")
        {
            Destroy(gameObject);
        }
    }

    public void NewAlienEvent(string eventName)
    {
        alien++;
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName + alien);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Alien")
        {
            bool found = false;
            for (int i = 0; i < aliensCollided.Count; i++)
            {
                if(aliensCollided[i].gameObject == collision.gameObject)
                {
                    found = true;
                    i = aliensCollided.Count;
                }
            }
            if (!found)
            {
                aliensCollided.Add(collision.gameObject);
                ev.Invoke();
            }
        }
    }
}
