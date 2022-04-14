namespace UCARPG.View.Input
{
    public class ActionInputTriggered
    {
        public string Action { get; private set; }

        public ActionInputTriggered(string action)
        {
            Action = action;
        }
    }
}