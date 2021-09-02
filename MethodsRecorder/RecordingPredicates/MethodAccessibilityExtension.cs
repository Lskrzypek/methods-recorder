using System.Reflection;

namespace MethodsRecorder.RecordingPredicates
{
    internal static class MethodAccessibilityExtension
    {
        public static MethodAccessibility GetMethodAccessibility(this MethodInfo methodInfo)
        {
            if (methodInfo.IsPublic && methodInfo.IsPrivate)
                return MethodAccessibility.All;
            if (methodInfo.IsPrivate)
                return MethodAccessibility.Private;
            if (methodInfo.IsPrivate)
                return MethodAccessibility.Public;
            return MethodAccessibility.None;
        }
    }
}