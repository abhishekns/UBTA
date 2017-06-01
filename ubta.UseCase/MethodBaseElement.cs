
namespace ubta.UseCase
{
    public class MethodBaseElement : VirtualElement
    {
        public MethodBaseElement(BaseElement parent_in, InstanceElement[] parameters_in, string name_in)
            : base(parent_in, name_in)
        {
            myParameters = parameters_in;
        }

        protected InstanceElement[] myParameters;
        public InstanceElement[] Parameters
        {
            get
            {
                return myParameters;
            }
        }
    }
}
