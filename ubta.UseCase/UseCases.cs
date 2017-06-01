using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace ubta.UseCase
{
    [ToolboxItem(typeof(ActivityToolboxItem))]
    public class PreCondition : DefaultSequentialUseCase
    {
    }

    [ToolboxItem(typeof(ActivityToolboxItem))]
    public class PostCondition : DefaultSequentialUseCase
    {
    }

    [ToolboxItem(typeof(ActivityToolboxItem))]
    public class Setup : DefaultSequentialUseCase
    {
    }

    [ToolboxItem(typeof(ActivityToolboxItem))]
    public class TearDown : DefaultSequentialUseCase
    {
    }

    [ToolboxItem(typeof(ActivityToolboxItem))]
    public class Step : DefaultSequentialUseCase
    {
    }
}
