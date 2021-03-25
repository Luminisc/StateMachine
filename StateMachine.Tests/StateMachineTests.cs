using System;
using Xunit;

namespace StateMachine.Tests
{
    public class StateMachineTests
    {
        public StateMachineTests()
        {

        }

        [Fact]
        public void StateMachine_HasValidStartingState()
        {
            var sm = GetTestStateMachine()
                .StartsWith(TestEnum.Invalidated);

            Assert.Equal(TestEnum.Invalidated, sm.CurrentState);
        }

        [Fact]
        public void StateMachine_SuccessfullyChangesState()
        {
            var sm = GetTestStateMachine()
                .StartsWith(TestEnum.Pending);

            sm = WithTestTransitions(sm)
                .SetState(TestEnum.Completed);

            Assert.Equal(TestEnum.Completed, sm.CurrentState);
        }

        private StateMachine<TestEnum> GetTestStateMachine()
        {
            return new StateMachine<TestEnum>()
                .WithState(TestEnum.Completed)
                .WithState(TestEnum.Denied)
                .WithState(TestEnum.Invalidated)
                .WithState(TestEnum.Pending);
        }

        private StateMachine<TestEnum> WithTestTransitions(StateMachine<TestEnum> sm)
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
