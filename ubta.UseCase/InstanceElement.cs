
namespace ubta.UseCase
{
    public class InstanceElement : BaseElement
    {
        public InstanceElement(BaseElement parent_in, string name_in)
            : base(name_in)
        {
            myParent = parent_in;
        }
    }
}
