using System;

namespace MethodsRecorderTests.ExampleData.Accounts
{
    public interface IAccountValuesDao
    {
        AccountValue Get(Account account, DateTime dateTime);
        AccountValue GetCurrent(string accountNum);
    }
}