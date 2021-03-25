using Xunit;

namespace StateMachine.Tests
{
    public class StateMachineTests
    {
        [Fact]
        public void StateMachine_HasValidStartingState()
        {
            var sm = GetEnumStateMachine()
                .StartsWith(TestEnum.Invalidated);

            Assert.Equal(TestEnum.Invalidated, sm.CurrentState);
        }

        [Fact]
        public void EnumStateMachine_SuccessfullyChangesState()
        {
            var sm = GetEnumStateMachine()
                .StartsWith(TestEnum.Pending);

            sm = WithTestTransitions(sm)
                .SetState(TestEnum.Completed);

            Assert.Equal(TestEnum.Completed, sm.CurrentState);
        }

        [Fact]
        public void EnumStateMachine_NotChangingStateForNonExistsTransition()
        {
            var sm = GetEnumStateMachine()
                .StartsWith(TestEnum.Completed);

            sm = WithTestTransitions(sm)
                .SetState(TestEnum.Invalidated);

            Assert.Equal(TestEnum.Completed, sm.CurrentState);
        }

        private StateMachine<TestEnum> GetEnumStateMachine()
        {
            return new StateMachine<TestEnum>()
                .FromEnum();
        }

        private static StateMachine<TestEnum> WithTestTransitions(StateMachine<TestEnum> sm)
        {
            return sm
                .WithTransition(new Transition<TestEnum>(TestEnum.Pending, TestEnum.Completed))
                .WithTransition(new Transition<TestEnum>(TestEnum.Pending, TestEnum.Invalidated))
                .WithTransition(new Transition<TestEnum>(TestEnum.Pending, TestEnum.Denied))
                .WithTransition(new Transition<TestEnum>(TestEnum.Invalidated, TestEnum.Completed))
                .WithTransition(new Transition<TestEnum>(TestEnum.Invalidated, TestEnum.Pending));
        }
    }
}
