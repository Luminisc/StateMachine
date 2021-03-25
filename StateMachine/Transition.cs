namespace StateMachine
{
    public readonly struct Transition<T>
    {
        public T From { get; }
        public T To { get; }

        public Transition(T from, T to)
        {
            From = from;
            To = to;
        }
    }
}
