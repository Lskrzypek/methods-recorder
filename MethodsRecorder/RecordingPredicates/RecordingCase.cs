using System;
using System.Reflection;
using System.Linq;

namespace MethodsRecorder.RecordingPredicates
{
    public class RecordingCase
    {
        private readonly MethodInformations MethodInformations;

        public RecordingCase(MethodInformations methodInformations)
        {
            MethodInformations = methodInformations;
        }

        public bool AllMethods()
        {
            return MethodInformations?.ReflectionMethodInfo is not null;
        }

        public RecordingMethodCase Methods(params string[] methods)
        {
            if (MethodInformations is null)
                return new RecordingMethodCase(false, null);

            var isCorrect = methods.Contains(MethodInformations.MethodName);

            return new RecordingMethodCase(isCorrect, MethodInformations);
        }

        public RecordingMethodCase Methods(Func<RecordingMethodAttributes, bool> predicate)
        {
            if (MethodInformations?.ReflectionMethodInfo is null)
                return new RecordingMethodCase(false, null);

            var methodInfo = MethodInformations.ReflectionMethodInfo;

            var isCorrect = predicate(new RecordingMethodAttributes()
            {
                Accessibility = methodInfo.GetMethodAccessibility(),
                ReturnType = methodInfo.ReturnType
            });

            return new RecordingMethodCase(isCorrect, MethodInformations);
        }
    }
}