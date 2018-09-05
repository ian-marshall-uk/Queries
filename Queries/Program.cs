using System;
using System.Data.Entity.Core;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            UpdatingData.AddCourse_AuthorNotInContext(context);

        }

    }
}
