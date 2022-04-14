namespace UCARPG.View.Input
{
    public class SwitchSelectedConsumableInputTriggered
    {
        public bool IsRight { get; private set; }

        public SwitchSelectedConsumableInputTriggered(bool isRight)
        {
            IsRight = isRight;
        }
    }
}