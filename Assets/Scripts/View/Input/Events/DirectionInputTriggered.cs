namespace UCARPG.View.Input
{
    public class DirectionInputTriggered
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public DirectionInputTriggered(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}