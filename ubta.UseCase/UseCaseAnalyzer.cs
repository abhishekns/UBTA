using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ubta.Common;

namespace ubta.UseCase
{
    public class UseCaseAnalyzer
    {
        protected List<UseCaseElement> myElements = new List<UseCaseElement>();
        protected XmlNode myUseCase;
        protected IList<XmlNode> myPriorSiblings;

        public UseCaseAnalyzer(IList<XmlNode> priorSiblings, XmlNode useCase_in)
        {
            myPriorSiblings = priorSiblings;
            myUseCase = useCase_in;
        }
        
        protected UseCaseAnalyzer(IList<UseCaseElement> elements_in)
        {
            myElements.AddRange(elements_in);
        }

        public UseCaseAnalysisResult AnalyzeUseCase()
        {
            myElements.Clear();
            List<UseCaseElement> priorElements = AnalyzePriorSiblings();
            foreach (XmlNode instanceNode in myUseCase.ChildNodes)
            {
                if (!instanceNode.ParentNode.Name.Equals(Constants.USECASE_NODE_NAME))
                {
                    continue;
                }
                string nameOfElement = NameHelper.GetAttributeValue(instanceNode, Constants.NAME_NODE_ATTRIBUTE);
                string typeOfElement = NameHelper.GetTypeName(instanceNode);
                AddElement(nameOfElement, typeOfElement, priorElements, instanceNode);
                foreach (XmlNode method in instanceNode.ChildNodes)
                {
                    foreach (XmlNode parameter in method.ChildNodes)
                    {
                        nameOfElement = NameHelper.GetAttributeValue(parameter, Constants.NAME_NODE_ATTRIBUTE);
                        typeOfElement = NameHelper.GetTypeName(parameter);
                        if (string.IsNullOrEmpty(typeOfElement))
                        {
                            typeOfElement = NameHelper.GetAttributeValue(parameter, "u:"+Constants.PARAM_TYPE);
                        }
                        if (null != nameOfElement)
                        {
                            AddElement(nameOfElement, typeOfElement, priorElements, method);
                        }
                    }
                }
            }
            return new UseCaseAnalysisResult(myElements);
        }

        protected virtual void AddElement(string nameOfElement, string typeOfElement, IList<UseCaseElement> priorElements, XmlNode methodNode)
        {
            UseCaseElement e = new UseCaseElement(nameOfElement, typeOfElement, methodNode);
            bool r = AddElement(myElements, ref e);
            if (r && priorElements.Contains(e))// input
            {
                e.ElemType = ElementType.In;
                if (methodNode.IsConstructorNode())
                {
                    e.ElemType = ElementType.Ref;
                }
            }
            else if(e.ElemType == ElementType.Undef)
            {
                e.ElemType = ElementType.Out;
            }
        }

        protected virtual bool AddElement(List<UseCaseElement> list, ref UseCaseElement e)
        {
            bool ret = false;
            if (!list.Contains(e))
            {
                list.Add(e);
                ret = true;
            }
            else
            {
                string name = e.Name;
                e = (from uce in list where uce.Name.Equals(name) select uce).First();
            }
            return ret;
        }

        protected virtual List<UseCaseElement> AnalyzePriorSiblings()
        {
            List<UseCaseElement> list = new List<UseCaseElement>();
            if (null != myPriorSiblings)
            {
                List<XmlNode> priors = new List<XmlNode>();
                foreach (XmlNode ps in myPriorSiblings)
                {
                    UseCaseAnalyzer uca = new UseCaseAnalyzer(priors, ps);
                    UseCaseElement[] r = uca.AnalyzeUseCase().GetElements(ElementType.Undef);
                    for (int i = 0; i < r.Length; i++)
                    {
                        UseCaseElement uce = r[i];
                        AddElement(list, ref uce);
                        r[i] = uce;
                    }
                    priors.Add(ps);
                }
            }
            return list;
        }
    }
}
