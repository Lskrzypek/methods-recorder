using System;
using System.Reflection;
using System.Linq;

namespace MethodsRecorder.RecordingPredicates
{
    public class RecordingMethodCase
    {
        private readonly MethodInfo MethodInfo;
        private bool CurrentValue;

        public RecordingMethodCase(bool beginValue, MethodInfo methodInfo)
        {
            CurrentValue = beginValue;
            MethodInfo = methodInfo;
        }

        public RecordingMethodCase WithParameters(params string[] parameters)
        {
            if (MethodInfo is null)
                return this;

            bool isCorrect = true;
            var miParameters = MethodInfo.GetParameters();

            if (parameters.Length != miParameters.Length)
                isCorrect = false;

            if(parameters
                .Select((par, index) => (par, index))
                .Any(x => miParameters[x.index].Name != x.par))
            {
                isCorrect = false;
            }

            CurrentValue &= isCorrect;

            return this;
        }

        public RecordingMethodCase WithParameterValue(string parameterName, Func<object, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static implicit operator bool(RecordingMethodCase mc) => mc.CurrentValue;
    }
}