using System;
namespace ubta.Reflection.Editable.ClassGenerator
{
    public interface IGenerator
    {
        IElement BuildTree();
        void GenerateClasses();
    }
}
