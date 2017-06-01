using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ubta.UseCase
{
    public class UseCaseAnalysisResult
    {
        private List<UseCaseElement> myElements = new List<UseCaseElement>();
        public UseCaseAnalysisResult(IList<UseCaseElement> elements_in)
        {
            myElements.AddRange(elements_in);
        }
        public UseCaseAnalysisResult()
        {
        }

        public UseCaseElement[] GetElements(ElementType et_in)
        {
            return (from e in myElements where (e.ElemType == et_in) select e).ToArray();
        }

        [CategoryAttribute("InputElements"), BrowsableAttribute(true), DescriptionAttribute("the input elements")]
        public UseCaseElement[] InputElements
        {
            get
            {
                return GetElements(ElementType.In);
            }
        }

        [CategoryAttribute("OutputElements"), BrowsableAttribute(true), DescriptionAttribute("the output elements")]
        public UseCaseElement[] OutputElements
        {
            get
            {
                return GetElements(ElementType.Out);
            }
        }

        [CategoryAttribute("RefElements"), BrowsableAttribute(true), DescriptionAttribute("the reference elements")]
        public UseCaseElement[] RefElements
        {
            get
            {
                return GetElements(ElementType.Ref);
            }
        }

    }
}
