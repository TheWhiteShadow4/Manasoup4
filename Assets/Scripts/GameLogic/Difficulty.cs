namespace Difficulty
{
    public enum Level
    {
        Einfach,
        Mittel,
        Schwer
    }

    [System.Serializable]
    public struct FloatValue
    {
        public readonly float[] _values;

        public float Get()
        {
            return _values[(int)ModeHandler.Instance.difficulty];
        }

        public FloatValue(float easy, float medium, float hard)
        {
            _values = new float[3];
            _values[0] = easy;
            _values[1] = medium;
            _values[2] = hard;
        }
    }

    [System.Serializable]
    public struct IntValue
    {
        public readonly int[] _values;

        public float Get()
        {
            return _values[(int)ModeHandler.Instance.difficulty];
        }

        public IntValue(int easy, int medium, int hard)
        {
            _values = new int[3];
            _values[0] = easy;
            _values[1] = medium;
            _values[2] = hard;
        }
    }
}