using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NVelocity.App;
using NVelocity;
using System.IO;

namespace TestTemplateVelocity
{
    class Program
    {
        static void Main(string[] args)
        {
            Velocity.Init();

            var model = new
            {
                Header = "Test Header",
                Items = new[]
                {
                    new { ID = 1, Name = "Name1", Bold = false},
                    new { ID = 2, Name = "Name2", Bold = false},
                    new { ID = 3, Name = "Name3", Bold = true}
                }
            };
            var velocityContext = new VelocityContext();
            velocityContext.Put("model", model);

            string template = string.Join(Environment.NewLine, new[] {
    "<p>",
    "   This is model.Header: <strong>$model.Header</strong>",
    "</p>",
    "<ul>",
    "#foreach($item in $model.Items)",
    "   <li>",
    "      item.ID: $item.ID,",
       "#if ($item.Bold)",
    "      item.Name: <b>$item.Name</b>",
       "#else",
    "      item.Name: $item.Name",
       "#end",
    "   </li>",
    "#end",
    "</ul>"});

            var sb = new StringBuilder();
            Velocity.Evaluate(
                velocityContext,
                new StringWriter(sb),
                "test template",
                new StringReader(template));
            Console.WriteLine(sb.ToString());
        }
    }
}
