#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : EventDescriptor.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Reflection;

namespace ubta.SchemaGeneration
{
    public class EventDescriptor : Descriptor<EventInfo>
    {
        public EventDescriptor()
        {
        }

        public EventDescriptor(IAssemblyAnalyzer analyzer, EventInfo eventInfo)
            : base(analyzer, eventInfo)
        {
        }

        public EventInfo EventInformation
        {
            get
            {
                return myMember as EventInfo;
            }
        }

        public override Type KnownType
        {
            get
            {
                return EventInformation.EventHandlerType;
            }
        }
    }
}