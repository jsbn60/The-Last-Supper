using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Events;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Object = System.Object;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;

    private LinkedList<Event> dPoints = new LinkedList<Event>();
    private LinkedList<Event> npcPoints = new LinkedList<Event>();
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
    
    [SerializeField] private Button[] dialogeButtons;

    [SerializeField] private Text npcText;

    //[SerializeField] private Text playerAnswerText;

    [SerializeField] private Button nextButton;

    [SerializeField] private GameObject dialogueBox;

    [SerializeField] private GameObject npcBox;
    
    private string jsonPath = "Assets/DayFiles/DayTest/";

    public enum UIModes
    {
        ChoiceMode,
        NPCMode,
        
    }

    public void changeUI(UIModes uiMode)
    {
        switch (uiMode)
        {
            case UIModes.ChoiceMode:
                npcBox.SetActive(false);
                dialogueBox.SetActive(true);
                break;
            case UIModes.NPCMode:
                npcBox.SetActive(true);
                dialogueBox.SetActive(false);
                break;
        }
    }
    public void displayChoices(string[] playerChoices)
    {
        if (playerChoices.Length >= 1)
        {
            dialogeButtons[0].gameObject.SetActive(true);
            dialogeButtons[0].GetComponentInChildren<Text>().text = playerChoices[0];
        }
        else
        {
            dialogeButtons[0].gameObject.SetActive(false);
        }

        if (playerChoices.Length >= 2)
        {
            dialogeButtons[1].gameObject.SetActive(true);
            dialogeButtons[1].GetComponentInChildren<Text>().text = playerChoices[1];
        }
        else
        {
            dialogeButtons[1].gameObject.SetActive(false);
        }
        
        if (playerChoices.Length >= 3)
        {
            dialogeButtons[2].gameObject.SetActive(true);
            dialogeButtons[2].GetComponentInChildren<Text>().text = playerChoices[2];
        }
        else
        {
            dialogeButtons[2].gameObject.SetActive(false);
        }
    }

    public void loadDayFiles()
    {
        // Read dPoint
        string jsonString = File.ReadAllText(jsonPath+"dPoints.json");
        JSONNode jsonNode = JSONNode.Parse(jsonString);
        // Read Files
        if (jsonNode.Tag == JSONNodeType.Object)
        {
            foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)jsonNode)
            {
                eventParser("dPoint",kvp);
            }
        }
        
        // Read npcPoint
        jsonString = File.ReadAllText(jsonPath+"npcPoints.json");
        jsonNode = JSONNode.Parse(jsonString);
        // Read Files
        if (jsonNode.Tag == JSONNodeType.Object)
        {
            foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)jsonNode)
            {
                eventParser("npcPoint",kvp);
            }
        }
        
        
        
        
        // Queue First Event
        QueueEventFront("dPoint",1);

        // Run Queue
        setupNextEvent();
        SoundManager.Instance.runBackgroundForDay(0);
    }

    public LinkedList<Event> eventQueue = new LinkedList<Event>();
    void Start()
    {
        loadDayFiles();
    }

    public void QueueEventFront(string type, int id)
    {
        switch (type)
        {
            case "dPoint":
                eventQueue.AddFirst(dPoints.First(e => e.id == id));
                break;
            case "npcPoint":
                eventQueue.AddFirst(npcPoints.First(e => e.id == id));
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
                eventQueue.AddLast(dPoints.First(e => e.id == id));
                break;
            case "npcPoint":
                eventQueue.AddLast(npcPoints.First(e => e.id == id));
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
        Debug.Log("QUEUE SIZE:" + eventQueue.Count);
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
                };
                dayEvent.runEvent(objects);
                break;
            case "npcPoint":
                objects = new object[]
                {
                    npcText,
                    nextButton
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
                dPoints.AddLast(new DialoguePoint(id, "dPoint",  options));
                break;
            case "npcPoint":
                int id2 = Int32.Parse(kvp.Key);
                string shownText2 = kvp.Value["shownText"].Value;
                string character2 = kvp.Value["character"].Value;
                npcPoints.AddLast(new NPCPoint(id2, "npcPoint", shownText2, character2));
                break;
            default:
                Debug.Log("Failed to Parse: "+kvp.Key);
                throw new Exception("Failed");
                break;
        }
    }
}
