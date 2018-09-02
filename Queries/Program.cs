
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

            LinqSyntax7(context);

        }

        static void LinqSyntax(PlutoContext context)
        {
            Console.WriteLine("LINQSyntax");
            Console.WriteLine();

            var query = from c in context.Courses
                        where c.Name.Contains("c#")
                        orderby c.Name
                        select c;

            foreach (var course in query)
            {
                Console.WriteLine(course.Name);
            }
        }

        static void LinqSyntax2(PlutoContext context)
        {
            Console.WriteLine("LinqSyntax2");
            Console.WriteLine();

            var query =
                from c in context.Courses
                where c.Author.Id == 1
                orderby c.Level descending, c.Name
                select new
                {
                    Name = c.Name,
                    Author = c.Author.Name
                };

            foreach (var course in query)
            {
                Console.WriteLine("{0} - {1}", course.Name, course.Author);
            }
        }

        static void LinqSyntax3(PlutoContext context)
        {
            Console.WriteLine("LinqSyntax3");
            Console.WriteLine();

            var query =
                from c in context.Courses
                group c by c.Level
                into g
                select g;

            foreach (var group in query)
            {
                Console.WriteLine(group.Key);
                foreach (var course in group)
                {
                    Console.WriteLine("\t{0}", course.Name);
                }
            }

        }

        static void LinqSyntax4(PlutoContext context)
        {
            Console.WriteLine("LinqSyntax4");
            Console.WriteLine();

            var query =
                from c in context.Courses
                group c by c.Level
                into g
                select g;

            foreach (var group in query)
            {
                Console.WriteLine("{0} ({1})", group.Key, group.Count());
            }

        }

        static void LinqSyntax5(PlutoContext context) //inner joins
        {
            Console.WriteLine("LinqSyntax5");
            Console.WriteLine();

            var query =
                from c in context.Courses
                join a in context.Authors on c.AuthorId equals a.Id
                select new
                {
                    CourseName = c.Name,
                    AuthorName = a.Name
                };

            foreach (var item in query)
            {
                Console.WriteLine("{0} ({1})", item.CourseName, item.AuthorName);
            }

        }

        static void LinqSyntax6(PlutoContext context) //group joins
        {
            Console.WriteLine("LinqSyntax6");
            Console.WriteLine();

            var query =
                from a in context.Authors
                join c in context.Courses on a.Id equals c.AuthorId into g
                select new
                {
                    AuthorName = a.Name,
                    Courses = g.Count()
                };

            foreach (var item in query)
            {
                Console.WriteLine("{0} ({1})", item.AuthorName, item.Courses);
            }

        }

        static void LinqSyntax7(PlutoContext context) //cross joins
        {
            Console.WriteLine("LinqSyntax7");
            Console.WriteLine();

            var query =
                from a in context.Authors
                from c in context.Courses
                select new
                {
                    AuthorName = a.Name,
                    Courses = c.Name
                };

            foreach (var item in query)
            {
                Console.WriteLine("{0} - {1}", item.AuthorName, item.Courses);
            }

        }

        static void ExtensionMethods(PlutoContext context)
        {
            Console.WriteLine("EntensionMethods");
            Console.WriteLine();

            var courses = context.Courses
                .Where(c => c.Name.Contains("c#"))
                .OrderBy(c => c.Name);

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }
        }
    }
}
