using System;

namespace ubta.xml.transformer
{
    public interface IXmlTransformer
    {
        string GetOutput();
        void SetInput(string input);
        void SetTransformer(string transformer);
        void Transform();
        Exception GetLastError();
    }
}