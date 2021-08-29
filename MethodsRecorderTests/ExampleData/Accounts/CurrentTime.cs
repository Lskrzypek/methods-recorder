using System;

namespace MethodsRecorderTests.ExampleData.Accounts
{
    public class CurrentTime : ICurrentTime
    {
        private int hour = 12;

        public DateTime GetCurrentTime()
        {
            return new DateTime(2021, 7, 1, hour++, 0, 0);
        }
    }
}