using KellermanSoftware.CompareNetObjects;

namespace MethodsRecorder
{
    internal class ObjectComparator
    {
        private readonly CompareLogic CompareLogic = new CompareLogic();

        public bool Compare(object a, object b)
        {
            var result = CompareLogic.Compare(a, b);
            return result.AreEqual;
        }
    }
}
