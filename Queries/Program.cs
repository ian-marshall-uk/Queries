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

            DeferredExecution(context);

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

        static void ExtensionMethods1(PlutoContext context)
        {
            Console.WriteLine("EntensionMethods1");
            Console.WriteLine();

            var courses = context.Courses
                .Where(c => c.Name.Contains("c#"))
                .OrderBy(c => c.Name);

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }
        }

        static void ExtensionMethods2(PlutoContext context)
        {
            Console.WriteLine("EntensionMethods2");
            Console.WriteLine();

            var courses = context.Courses
                .Where(c => c.Level == 1);

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }
        }

        static void ExtensionMethods3(PlutoContext context)
        {
            Console.WriteLine("EntensionMethods3");
            Console.WriteLine();

            var courses = context.Courses
                .Where(c => c.Level == 1)
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Author);

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }
        }

        static void ExtensionMethods4(PlutoContext context) // Projection
        {
            Console.WriteLine("EntensionMethods4");
            Console.WriteLine();

            var courses = context.Courses
                .Where(c => c.Level == 1)
                .OrderBy(c => c.Name)
                .ThenByDescending(c => c.Author)
                .Select(c => new
                {
                    CourseName = c.Name,
                    AuthorName = c.Author.Name
                });

            foreach (var course in courses)
            {
                Console.WriteLine(course.CourseName);
            }
        }

        static void ExtensionMethods4a(PlutoContext context) // Projection 2
        {
            Console.WriteLine("EntensionMethods4a");
            Console.WriteLine();

            var tags = context.Courses
                .Where(c => c.Level == 1)
                .OrderBy(c => c.Name)
                .ThenByDescending(c => c.Author)
                .SelectMany(c => c.Tags)
                .Distinct();

            foreach (var tag in tags)
            {
                Console.WriteLine(tag.Name);
            }
        }

        static void ExtensionMethods5(PlutoContext context) // Groups
        {
            Console.WriteLine("EntensionMethods5");
            Console.WriteLine();

            var groups = context.Courses.GroupBy(c => c.Level);

            foreach (var group in groups)
            {
                Console.WriteLine("Key: {0}", group.Key);
                foreach (var course in group)
                {
                    Console.WriteLine("\t{0}", course.Name);
                }
            }
        }

        static void ExtensionMethods6(PlutoContext context) // Groups
        {
            Console.WriteLine("EntensionMethods6");
            Console.WriteLine();

            var courses = context.Courses
                .Join(context.Authors,
                    c => c.AuthorId,
                    a => a.Id, (course, author) => new
                    {
                        CourseName = course.Name,
                        AuthorName = author.Name
                    });

            foreach (var course in courses)
            {
                Console.WriteLine("{0}. {1}", course.CourseName, course.AuthorName);
            }
        }

        static void ExtensionMethods7(PlutoContext context) // Groups
        {
            Console.WriteLine("EntensionMethods7");
            Console.WriteLine();

            var courses = context.Authors.GroupJoin(context.Courses, a => a.Id, c => c.AuthorId, (author, course) => new
            {
                Author = author,
                CourseCount = course.Count()
            });

            foreach (var course in courses)
            {
                Console.WriteLine("{0}, {1}", course.Author.Name, course.CourseCount);
            }
        }

        static void ExtensionMethods8(PlutoContext context) // Pagination
        {
            Console.WriteLine("EntensionMethods8");
            Console.WriteLine();

            var courses = context.Courses.OrderBy(c => c.Name).Skip(2).Take(2);
            foreach (var course in courses)
            {
                Console.WriteLine("{0}, {1}", course.Author.Name, course.Name);
            }
        }

        static void ExtensionMethods9(PlutoContext context) // Elements
        {
            Console.WriteLine("EntensionMethods9");
            Console.WriteLine();

            var course = context.Courses.OrderBy(c => c.Level).FirstOrDefault(c => c.FullPrice > 25);
            Console.WriteLine("{0}, {1}", course.Author.Name, course.Name);

            var AllAbove10Quid = context.Courses.All(c => c.FullPrice > 50);
            var AnyCoursesInLevel1 = context.Courses.Any(c => c.Level == 1);
            Console.WriteLine("{0}, {1}", AllAbove10Quid, AnyCoursesInLevel1);

            var count = context.Courses.Count();
            var mostExpensive = context.Courses.Max(c => c.FullPrice);

            var count2 = context.Courses.Where(c => c.Level == 1).Count();
            var count3 = context.Courses.Count(c => c.Level == 1);

            Console.WriteLine("{0}, {1}, {2}. {3}", count, mostExpensive, count2, count3);

        }

        static void DeferredExecution(PlutoContext context) // Elements
        {
            var courses = context.Courses;
            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }
        }

    }
}
