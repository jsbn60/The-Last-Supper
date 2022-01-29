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

    private LinkedList<Event> dPoints = new LinkedList<Event>();
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            dialogBox.gameObject.SetActive(!dialogBox.gameObject.activeSelf);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    [SerializeField] private DialogeButton[] dialogeButtons;

    [SerializeField] private Text npcText;

    [SerializeField] private Text playerAnswerText;

    [SerializeField] private GameObject dialogBox;
    string jsonPath = "Assets/DayFiles/DayTest/dPoints.json";

    public LinkedList<Event> eventQueue = new LinkedList<Event>();
    void Start()
    {
        string jsonString = File.ReadAllText(jsonPath);

        JSONNode jsonNode = JSONNode.Parse(jsonString);
        
        
        // Read Files
        if (jsonNode.Tag == JSONNodeType.Object)
        {
            foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)jsonNode)
            {
                eventParser("dPoint",kvp);
            }
        }
        // Queue First Event
        QueueEventFront("dPoint",1);

        // Run Queue
        setupNextEvent();
    }

    public void QueueEventFront(string type, int id)
    {
        switch (type)
        {
            case "dPoint":
                eventQueue.AddFirst(dPoints.First(e => e.id == id));
                break;
            default:
                throw new Exception("Failed");
                break;
        }
    }

    public void QueueEventBack(string type, int id)
    {
        switch (type)
        {
            case "dPoint":
                eventQueue.AddFirst(dPoints.First(e => e.id == id));
                break;
            default:
                throw new Exception("Failed");
                break;
        }
    }

    public void setupNextEvent()
    {
        Event e = eventQueue.First();
        eventQueue.RemoveFirst();
        setupEvent(e);
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

    private void eventParser(string type, KeyValuePair<string, JSONNode> kvp)
    {
        switch (type)
        {
            case "dPoint":
                int id = Int32.Parse(kvp.Key);
                string shownText = kvp.Value["shownText"].Value;
                List<DialogueOptions> options = new List<DialogueOptions>();

                foreach (JSONNode option in kvp.Value["options"].Children)
                {
                    LinkedList<Pair<string, int>> followEvents = new LinkedList<Pair<string, int>>();
                    JSONNode jsonEvents = option["followEvents"];
                    if (jsonEvents.Tag == JSONNodeType.Object)
                    {
                        foreach (KeyValuePair<string, JSONNode> kvpOption in (JSONObject)jsonEvents)
                        {
                            followEvents.AddLast(new Pair<string, int>(kvpOption.Key, kvpOption.Value));
                        }
                    }

                    options.Add(new DialogueOptions(
                        option["response"].Value,
                        followEvents));
                }

                // Add dPoint to List
                dPoints.AddLast(new DialoguePoint(id, "dPoint", shownText, options));
                break;
            default:
                Debug.Log("Failed to Parse: "+kvp.Key);
                throw new Exception("Failed");
                break;
        }
    }
}
