namespace StateMachine.Tests
{
    internal readonly struct TestState
    {
        public static TestState Pending => new("Pending");
        public static TestState Denied => new("Denied");
        public static TestState Completed => new("Completed");

        public string StateName { get; }

        private TestState(string stateName)
        {
            StateName = stateName;
        }

        public override bool Equals(object obj)
        {
            if (obj is TestState state)
            {
                return StateName == state.StateName;
            }

            return false;
        }
    }
    
    internal enum TestEnum
    {
        Pending,
        Completed,
        Denied,
        Invalidated
    }

    internal interface IState
    {
        public TestEnum StateType { get; }
    }

    internal class PendingState : IState
    {
        public TestEnum StateType => TestEnum.Pending;

        public override bool Equals(object? obj) => (obj is IState state) && state.StateType == StateType;
    }

    internal class DeniedState : IState
    {
        public TestEnum StateType => TestEnum.Denied;

        public override bool Equals(object? obj) => (obj is IState state) && state.StateType == StateType;
    }

    internal class CompletedState : IState
    {
        public TestEnum StateType => TestEnum.Completed;

        public override bool Equals(object? obj) => (obj is IState state) && state.StateType == StateType;
    }
}
