using System;
using System.Workflow.ComponentModel;
namespace ubta.UseCase
{
    public static class UseCaseExtn
    {
        public static string NewUseCaseName(this IUseCase uc)
        {
            DefaultSequentialUseCase dsuc;
            if(null != (dsuc = uc as DefaultSequentialUseCase))
            {
                return uc.GetType().Name + "_" + Guid.NewGuid().ToString();
            }
            return Guid.NewGuid().ToString();
        }
        public static string GetInstanceName(this IUseCase uc, CompositeActivity root)
        {
            DefaultSequentialUseCase dsuc;
            if (null != (dsuc = root as DefaultSequentialUseCase))
            {
                return dsuc.ActiveInstance.InstanceName;
            }
            return GetBaseIdentifier(uc);
        }


        private static string GetBaseIdentifier(IUseCase activity)
        {
            string baseIdentifier = activity.GetType().Namespace;
            return "a" + baseIdentifier.Substring(baseIdentifier.LastIndexOf('.') + 1);
        }
    }

    public interface IUseCase
    {
        string Name { get; set; }
        string ActionName { get; set; }
        string InstanceName { get; set; }
        CompositeActivity Parent { get; }
        Type InstanceType { get; set; }
        event EventHandler InstanceTypeAvailable;
        event InstanceNameChangedHandler InstanceNameChanged;
        event ParamValueChanged ParameterChanged;
        string Parameters { get; }
    }
}
