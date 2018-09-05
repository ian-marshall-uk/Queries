using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public static class LINQSyntax
    {
        public static void UsingWhereAndOrderBy(PlutoContext context)
        {
            Console.WriteLine("LINQSyntax.UsingWhereAndOrderBy");
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

        public static void Projection(PlutoContext context)
        {
            Console.WriteLine("LINQSyntax.Projection");
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

        public static void Grouping(PlutoContext context)
        {
            Console.WriteLine("LINQSyntax.Grouping");
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

        public static void InnerJoins(PlutoContext context)
        {
            Console.WriteLine("LINQSyntax.InnerJoins");
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

        public static void GroupJoins(PlutoContext context)
        {
            Console.WriteLine("LINQSyntax.GroupJoins");
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

        public static void CrossJoins(PlutoContext context)
        {
            Console.WriteLine("LINQSyntax.CrossJoins");
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
    }
}
