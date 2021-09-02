using Castle.DynamicProxy;
using MethodsRecorder.RecordingPredicates;
using MethodsRecorder.Writters;
using System.Collections.Generic;
using System.Linq;

namespace MethodsRecorder.RecordedObjectCreators
{
    internal class RecordObjectInterceptor : IInterceptor, IRecordObjectInterceptor
    {
        public List<IRecordingPredicate> Constraints { get; set; } = new ();

        private readonly IWriteManager WriteManager;

        public RecordObjectInterceptor(IWriteManager writeManager)
        {
            WriteManager = writeManager;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            var methodConstraints = GetConstraints(invocation);
            if (!methodConstraints.Any() && Constraints.Any())
                return;

            var recordElements = GetRecordElements(methodConstraints);

            var data = new MethodData()
            {
                MethodName = invocation.Method.Name,
                ClasName = invocation.Method.DeclaringType.FullName,
                OrderNumber = WriteManager.CurrentRecordNumber
            };

            if (CheckRecordArguments(recordElements))
            {
                data.Arguments = invocation.Arguments;
            }

            if(CheckRecordReturnValue(recordElements))
            {
                data.ReturnValue = invocation.ReturnValue;
            }
            
            WriteManager.Write(data);
        }

        private IEnumerable<IRecordingPredicate> GetConstraints(IInvocation invocation)
        {
            var parametersInfo = invocation.Method.GetParameters();

            return Constraints.Where(x => x.Check(new MethodInformations()
            {
                MethodName = invocation.Method.Name,
                ReflectionMethodInfo = invocation.Method,
                Arguments = invocation.Arguments.Select((x, index) => new ArgumentValue()
                {
                    Name = parametersInfo.Length > index ? parametersInfo[index]?.Name : null,
                    Value = x
                }).ToArray()
            }));
        }

        private static RecordElements GetRecordElements(IEnumerable<IRecordingPredicate> predicates)
        {
            RecordElements ret = RecordElements.Nothing;
            predicates.ToList().ForEach(x => { ret |= x.RecordElements; });
            return ret;
        }

        private static bool CheckRecordArguments(RecordElements recordElements)
        {
            return recordElements.HasFlag(RecordElements.Arguments);
        }

        private static bool CheckRecordReturnValue(RecordElements recordElements)
        {
            return recordElements.HasFlag(RecordElements.ReturnValue);
        }
    }
}