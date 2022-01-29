using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;
using Object = System.Object;

public class DialoguePoint : Event
{
    public string shownText;
    public string character;
    public List<DialogueOptions> dialogueOptions;
    
    // Arg1 = TextObject to display text
    // Arg2 = DialogueButtons
    public override void runEvent(Object[] args)
    {
        ((Text) args[0]).GetComponent<UITextTypeWriter>().showText(character+": "+shownText);
        // showtext

        Button[] buttons = new Button[4];

        for (int i = 1; i <= 4; i++)
        {
           buttons[i - 1] = ((DialogeButton) args[i]).GetComponent<Button>();
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
                SoundManager.Instance.playsoundEffect(SoundManager.SoundClip.ButtonClick);
                SceneManager.Instance.setupNextEvent();
            });
        }
    }

    public DialoguePoint(int id, string type, string shownText, List<DialogueOptions> dialogueOptions, string character) : base(type, id)
    {
        this.shownText = shownText;
        this.dialogueOptions = dialogueOptions;
        this.character = character;
    }
}