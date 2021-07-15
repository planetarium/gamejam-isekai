namespace LibUnity.Frontend
{
    public readonly struct EventInfo
    {
        public int Index { get; }
        public int Score { get; }
        public int Time { get; }

        public EventInfo(int index, int score, int time)
        {
            Index = index;
            Score = score;
            Time = time;
        }
    }
}