using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = System.Object;

public class DialoguePoint : Event
{
    public string shownText;
    public List<DialogueOptions> dialogueOptions;
    
    // Arg1 = TextObject to display textdasd
    // Arg2 = DialogueButtons
    public override void runEvent(Object[] args)
    {
        ((Text) args[0]).GetComponent<UITextTypeWriter>().showText(shownText);
        // showtext

        Button[] buttons = new Button[4];

        for (int i = 1; i <= 4; i++)
        {
           buttons[i - 1] = ((DialogeButton) args[i]).GetComponent<Button>();
           //Debug.Log(((DialogeButton)args[i]).gameObject.name);
        }

        for (int i = 0; i < dialogueOptions.Count; i++)
        {
            buttons[i].GetComponent<DialogeButton>().setResponseText(dialogueOptions[i].answer);
            var i1 = i;
            buttons[i].onClick.AddListener(() =>
            {
                foreach (Pair<string, int> pair in dialogueOptions[i1].optionEvents.Reverse())
                {
                    SceneManager.Instance.QueueEventFront(pair.First, pair.Second);
                }
                SceneManager.Instance.setupNextEvent();
            });
        }
    }

    public DialoguePoint(int id, string type, string shownText, List<DialogueOptions> dialogueOptions) : base(type, id)
    {
        this.shownText = shownText;
        this.dialogueOptions = dialogueOptions;
    }
}