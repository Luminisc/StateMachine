using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine.Tests
{
    internal struct TestState
    {
    }

    public enum TestEnum
    {
        Pending,
        Completed,
        Denied,
        Invalidated
    }
}
