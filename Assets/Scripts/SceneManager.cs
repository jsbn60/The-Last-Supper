using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;

    public static SceneManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SceneManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    [SerializeField] private DialogeButton[] dialogeButtons;

    [SerializeField] private Text npcText;

    [SerializeField] private Text playerAnswerText;
    
    string jsonPath = "Assets/DayFiles/Day_Test.json.txt";
    
    public LinkedList<Event> dayEvents = new LinkedList<Event>();

    void Start()
    {
        string jsonString = File.ReadAllText(jsonPath);

        JSONNode jsonNode = JSONNode.Parse(jsonString);
        
        if (jsonNode.Tag == JSONNodeType.Object)
        {
            foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)jsonNode)
            {
                dayEvents.AddLast(recEventParser(kvp));
            }
        }

        setupNextEvent();
    }

    public void setupNextEvent()
    {
        setupEvent(dayEvents.First());
    }

    private void setupEvent(Event dayEvent)
    {
        Object[] objects;
        switch (dayEvent.type)
        {
            case "dPoint":
                objects = new object[]
                {
                    npcText,
                    dialogeButtons[0],
                    dialogeButtons[1],
                    dialogeButtons[2],
                    dialogeButtons[3]
                };
                dayEvent.runEvent(objects);
                break;

        }
    }

    private Event recEventParser(KeyValuePair<string, JSONNode> kvp)
    {
        switch (kvp.Key)
        {
            case "dPoint":
                string shownText = kvp.Value["shownText"].Value;
                List<DialogueOptions> options = new List<DialogueOptions>();

                foreach (JSONNode option in kvp.Value["options"].Children)
                {
                    LinkedList<Event> followEvents = new LinkedList<Event>();
                    JSONNode jsonEvents = option["events"];
                    if (jsonEvents.Tag == JSONNodeType.Object)
                    {
                        foreach (KeyValuePair<string, JSONNode> kvpOption in (JSONObject)jsonEvents)
                        {
                            followEvents.AddLast(recEventParser(kvpOption));
                        }
                    }

                    options.Add(new DialogueOptions(
                        option["response"].Value,
                        followEvents));
                }

                return new DialoguePoint("dPoint",shownText, options);
                break;
            default:
                Debug.Log("Failed to Parse: "+kvp.Key);
                throw new Exception("Failed");
                break;
        }
    }
}
