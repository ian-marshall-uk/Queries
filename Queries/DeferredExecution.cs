using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public static class DeferredExecution
    {
        public static void Query(PlutoContext context)
        {
            Console.WriteLine("DeferredExecution.Query");

            var courses = context.Courses;
            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }
        }

    }
}
