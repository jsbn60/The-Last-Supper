using UnityEngine;

namespace Events
{
    public class EndDay : Event
    {
        public EndDay(string type, int id) : base(type, id)
        {
            
        }

        public override void runEvent(object[] args)
        {
            Debug.Log("Called Event!");
            DayManager.Instance.runNextDay(SceneManager.Instance.day+1);
        }
    }
}