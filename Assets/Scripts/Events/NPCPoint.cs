using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using Util;

namespace Events
{
    public class NPCPoint : Event
    {
        public string shownText;
        public string character;
        LinkedList<Pair<string, int>> followEvents = new LinkedList<Pair<string, int>>();

        // Arg1 = TextObject to display textdasd
        // Arg2 = DialogueButtons
        public override void runEvent(Object[] args)
        {
            SceneManager.Instance.changeUI(SceneManager.UIModes.NPCMode);
            ((Text) args[0]).GetComponent<UITextTypeWriter>().showText(character+": "+shownText);
            // showtext
            
            

            ((Button)args[1]).onClick.AddListener(() =>
            {
                SoundManager.Instance.playsoundEffect(SoundManager.SoundClip.ButtonClick);
                
                foreach (Pair<string, int> pair in followEvents.Reverse())
                {
                    SceneManager.Instance.QueueEventFront(pair.First, pair.Second);
                }
                
                SceneManager.Instance.setupNextEvent();
            });

        }

        public NPCPoint(int id, string type, string shownText, string character, LinkedList<Pair<string,int>> followEvents) : base(type, id)
        {
            this.shownText = shownText;
            this.character = character;
            this.followEvents = followEvents;
        }

    }
}