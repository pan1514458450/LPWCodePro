using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Diagnostics;
using System.Text;

namespace RuntimeExp
{
    [Generator]
    public class RunTimeExpCode : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {

        }

        public void Initialize(GeneratorInitializationContext context)
        {
            Debugger.Launch();
        }
    }
}
