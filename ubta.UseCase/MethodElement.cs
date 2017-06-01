
namespace ubta.UseCase
{
    public class MethodElement : MethodBaseElement
    {
        protected ParameterElement myReturnElement;
        public MethodElement(BaseElement parent_in, ParameterElement[] parameters_in, ParameterElement returnParam, string name_in)
            : base(parent_in, parameters_in, name_in)
        {
            myReturnElement = returnParam;
        }

        public ParameterElement ReturnParameter
        {
            get
            {
                return myReturnElement;
            }
        }

        public bool IsAbstract
        {
            get
            {
                return false;
            }
        }

        public bool IsVirtual
        {
            get
            {
                return false;
            }
        }
    }
}
