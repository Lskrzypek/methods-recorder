using System.Collections.Generic;

namespace MethodsRecorderTests.ExampleData.Accounts
{
    public interface IAccountValuesReader
    {
        IEnumerable<AccountValue> ReadAllAccountValues();
    }
}