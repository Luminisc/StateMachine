using Xunit;

namespace StateMachine.Tests
{
    public class StateMachineTests
    {
        #region Enum StateMachine
        [Fact]
        public void EnumStateMachine_HasValidStartingState()
        {
            var sm = GetEnumStateMachine()
                .StartsWith(TestEnum.Invalidated);

            Assert.Equal(TestEnum.Invalidated, sm.CurrentState);
        }

        [Fact]
        public void EnumStateMachine_SuccessfullyChangesState()
        {
            var sm = GetEnumStateMachine()
                .StartsWith(TestEnum.Pending)
                .WithEnumTransitions()
                .SetState(TestEnum.Completed);

            Assert.Equal(TestEnum.Completed, sm.CurrentState);
        }

        [Fact]
        public void EnumStateMachine_NotChangingStateForNonExistsTransition()
        {
            var sm = GetEnumStateMachine()
                .StartsWith(TestEnum.Completed)
                .WithEnumTransitions()
                .SetState(TestEnum.Invalidated);

            Assert.Equal(TestEnum.Completed, sm.CurrentState);
        }

        private StateMachine<TestEnum> GetEnumStateMachine()
        {
            return new StateMachine<TestEnum>()
                .FromEnum();
        }
        #endregion

        #region Struct StateMachine
        [Fact]
        public void StructMachine_HasValidStartingState()
        {
            var sm = GetStructStateMachine()
                .StartsWith(TestState.Pending);

            Assert.Equal(TestState.Pending, sm.CurrentState);
        }

        [Fact]
        public void StructStateMachine_SuccessfullyChangesState()
        {
            var sm = GetStructStateMachine()
                .WithStructTransitions()
                .StartsWith(TestState.Pending)
                .SetState(TestState.Completed);

            Assert.Equal(TestState.Completed, sm.CurrentState);
        }

        [Fact]
        public void StructStateMachine_NotChangingStateForNonExistsTransition()
        {
            var sm = GetStructStateMachine()
                .StartsWith(TestState.Completed)
                .WithStructTransitions()
                .SetState(TestState.Pending);

            Assert.Equal(TestState.Completed, sm.CurrentState);
        }

        private StateMachine<TestState> GetStructStateMachine()
        {
            return new StateMachine<TestState>()
                .WithState(TestState.Completed)
                .WithState(TestState.Pending)
                .WithState(TestState.Denied);
        }
        #endregion

        #region Interface StateMachine
        [Fact]
        public void InterfaceStateMachine_HasValidStartingState()
        {
            var sm = GetInterfaceStateMachine()
                .StartsWith(new PendingState());

            Assert.Equal(new PendingState(), sm.CurrentState);
        }

        [Fact]
        public void InterfaceStateMachine_SuccessfullyChangesState()
        {
            var sm = GetInterfaceStateMachine()
                .WithInterfaceTransitions()
                .StartsWith(new PendingState())
                .SetState(new CompletedState());

            Assert.Equal(new CompletedState(), sm.CurrentState);
        }

        [Fact]
        public void InterfaceStateMachine_NotChangingStateForNonExistsTransition()
        {
            var sm = GetInterfaceStateMachine()
                .WithInterfaceTransitions()
                .StartsWith(new CompletedState())
                .SetState(new PendingState());

            Assert.Equal(new CompletedState(), sm.CurrentState);
        }

        private StateMachine<IState> GetInterfaceStateMachine()
        {
            return new StateMachine<IState>()
                .WithState(new PendingState())
                .WithState(new CompletedState())
                .WithState(new DeniedState());
        }
        #endregion
    }

    internal static class TestsExtensions
    {
        public static StateMachine<TestEnum> WithEnumTransitions(this StateMachine<TestEnum> sm)
        {
            return sm
                .WithTransition(new Transition<TestEnum>(TestEnum.Pending, TestEnum.Completed))
                .WithTransition(new Transition<TestEnum>(TestEnum.Pending, TestEnum.Invalidated))
                .WithTransition(new Transition<TestEnum>(TestEnum.Pending, TestEnum.Denied))
                .WithTransition(new Transition<TestEnum>(TestEnum.Invalidated, TestEnum.Completed))
                .WithTransition(new Transition<TestEnum>(TestEnum.Invalidated, TestEnum.Pending));
        }

        public static StateMachine<TestState> WithStructTransitions(this StateMachine<TestState> sm)
        {
            return sm
                .WithTransition(new Transition<TestState>(TestState.Pending, TestState.Completed))
                .WithTransition(new Transition<TestState>(TestState.Pending, TestState.Denied));
        }

        public static StateMachine<IState> WithInterfaceTransitions(this StateMachine<IState> sm)
        {
            return sm
                .WithTransition(new Transition<IState>(new PendingState(), new CompletedState()))
                .WithTransition(new Transition<IState>(new PendingState(), new CompletedState()));
        }
    }
}
