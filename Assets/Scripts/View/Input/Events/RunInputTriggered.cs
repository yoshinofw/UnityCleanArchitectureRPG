namespace UCARPG.View.Input
{
    public class RunInputTriggered
    {
        public bool IsRun { get; private set; }

        public RunInputTriggered(bool isRun)
        {
            IsRun = isRun;
        }
    }
}