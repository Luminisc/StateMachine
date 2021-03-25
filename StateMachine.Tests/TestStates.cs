namespace StateMachine.Tests
{
    internal readonly struct TestState
    {
        public TestState(string stateName)
        {
            StateName = stateName;
        }

        public string StateName { get; }

        public static TestState Pending => new("Pending");
        public static TestState Denied => new("Denied");
        public static TestState Completed => new("Completed");
    }
    
    public enum TestEnum
    {
        Pending,
        Completed,
        Denied,
        Invalidated
    }
}
