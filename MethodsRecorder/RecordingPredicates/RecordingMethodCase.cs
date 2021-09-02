using System;
using System.Linq;

namespace MethodsRecorder.RecordingPredicates
{
    public class RecordingMethodCase
    {
        private readonly MethodInformations MethodInformations;
        private bool CurrentValue;

        public RecordingMethodCase(bool beginValue, MethodInformations methodInformations)
        {
            CurrentValue = beginValue;
            MethodInformations = methodInformations;
        }

        public RecordingMethodCase WithParameters(params string[] parameters)
        {
            if (MethodInformations.ReflectionMethodInfo is null)
            {
                CurrentValue = false;
                return this;
            }

            bool isCorrect = true;
            var miParameters = MethodInformations.ReflectionMethodInfo.GetParameters();

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
            var argument = MethodInformations.Arguments?.FirstOrDefault(x => x.Name == parameterName);
            if(argument is null)
            {
                CurrentValue = false;
                return this;
            }

            var isCorrect = predicate(argument.Value);

            CurrentValue &= isCorrect;

            return this;
        }

        public static implicit operator bool(RecordingMethodCase mc) => mc.CurrentValue;
    }
}