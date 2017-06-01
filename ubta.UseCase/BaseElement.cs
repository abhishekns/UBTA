
namespace ubta.UseCase
{
    public class BaseElement
    {
        protected BaseElement()
        {
        }
        public BaseElement(string name_in)
        {
            myName = name_in;
        }
        public BaseElement(BaseElement parent_in, string name_in)
            :this(name_in)
        {
            myParent = parent_in;
        }
        protected string myName;
        public string Name
        {
            get
            {
                return myName;
            }
        }
        protected BaseElement myParent;
        public BaseElement Parent
        {
            get
            {
                return myParent;
            }
        }
    }
}
