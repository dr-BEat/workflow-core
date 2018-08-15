using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Sample03.Steps;

namespace WorkflowCore.Sample03
{
    public class PassingDataWorkflow : IWorkflow<MyDataClass>
    {  
        public void Build(IWorkflowBuilder<MyDataClass> builder)
        {
            builder
                .StartWith(context =>
                {
                    Console.WriteLine("Starting workflow...");
                    return ExecutionResult.Next();
                })
                .Then<AddNumbers>()
                    .Input((step, value) => step.Input1 = value, data => data.Value1)
                    .Input((step, value) => step.Input2 = value, data => data.Value2)
                    .Output((data, value) => data.Value3 = value, step => step.Output)
                .Then<CustomMessage>()
                    .Name("Print custom message")
                    .Input((step, value) => step.Message = value, data => "The answer is " + data.Value3.ToString())
                .Then(context =>
                    {
                        Console.WriteLine("Workflow complete");
                        return ExecutionResult.Next();
                    });
        }

        public string Id => "PassingDataWorkflow";
            
        public int Version => 1;

    }
}
