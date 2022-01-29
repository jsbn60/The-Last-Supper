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
    public List<DialogueOptions> dialogueOptions;
    
    // Arg1 = TextObject to display text
    // Arg2 = DialogueButtons
    public override void runEvent(Object[] args)
    {
        // showtext

        Button[] buttons = new Button[4];

        for (int i = 1; i <= 3; i++)
        {
            buttons[i - 1] = (Button) args[i];
        }

        string[] answers = new string[dialogueOptions.Count];

        for (int i = 0; i < dialogueOptions.Count; i++)
        {
            answers[i] = dialogueOptions[i].answer;
        }

        SceneManager.Instance.changeUI(SceneManager.UIModes.ChoiceMode);
        SceneManager.Instance.displayChoices(answers);

        for (int i = 0; i < dialogueOptions.Count; i++)
        {
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

    public DialoguePoint(int id, string type, List<DialogueOptions> dialogueOptions) : base(type, id)
    {
        this.dialogueOptions = dialogueOptions;
    }
}