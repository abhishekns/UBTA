#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : constants.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;

namespace usecases.UBTA.Base
{
    public enum enumSequence
    {
        enumPrecondition,
        enumMainPart,
        enumPostCondition
    };

    class SequenceAndDelegateClass
    {
        protected enumSequence m_sequence;
        protected Delegate m_DelegateItem;

        public object sequenceObj
        {
            get
            {
                return m_sequence;
            }
            set
            {
                m_sequence = (enumSequence)value;
            }
        }
        public object DelegateObj
        {
            get
            {
                return m_DelegateItem;
            }
            set
            {
                m_DelegateItem = (Delegate)value;
            }
        }
    }
}