using System;
using System.Reflection;
using System.Linq;

namespace MethodsRecorder.RecordingPredicates
{
    public class RecordingCase
    {
        private readonly MethodInfo MethodInfo;

        public RecordingCase(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
        }

        public bool AllMethods()
        {
            return MethodInfo != null;
        }

        public RecordingMethodCase Methods(params string[] methods)
        {
            if (MethodInfo is null)
                return new RecordingMethodCase(false, null);

            var isCorrect = methods.Contains(MethodInfo.Name);

            return new RecordingMethodCase(isCorrect, MethodInfo);
        }

        public RecordingMethodCase Methods(Func<RecordingMethodAttributes, bool> predicate)
        {
            if (MethodInfo is null)
                return new RecordingMethodCase(false, null);

            var isCorrect = predicate(new RecordingMethodAttributes()
            {
                Accessibility = MethodInfo.GetMethodAccessibility(),
                ReturnType = MethodInfo.ReturnType
            });

            return new RecordingMethodCase(isCorrect, MethodInfo);
        }
    }
}