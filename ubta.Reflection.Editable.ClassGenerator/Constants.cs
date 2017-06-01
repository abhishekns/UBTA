using System;

namespace ubta.Reflection.Editable.ClassGenerator
{
    public class Constants : ubta.Common.Constants
    {
        public static string NAME_SPACE = "ubta.Reflection";
        public static string BASE_NAME_SPACE = "Editable";
        public static string BASE_CLASS = "EditableObject";
        public static string ACTUAL_OBJECT = "myActualObject";
        public static string CHANGED_METHOD = "Changed";
        public static string ASSEMBLY_NAME = typeof(Constants).Assembly.GetName().Name + DLL_EXT;

        public static string FILE_OUTPUT_PATH = string.Format(@"{0}\ClassGeneratorOutput\", HOME);
        public static string ASSEMBLY_NAME_EXT = "Editable";
        public static string LIB_PATH_EXTERN = @"Z:\syngoBASE\i586-WinNT5.01\debug\DoBin\Bin\extern\";
    }
}
