using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public static class ExtensionMethods
    {

        public static void UsingWhereAndOrderBy(PlutoContext context)
        {
            Console.WriteLine("ExtensionMethods.UsingWhereAndOrderBy");
            Console.WriteLine();

            var courses = context.Courses
                .Where(c => c.Name.Contains("c#"))
                .OrderBy(c => c.Name);

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }
        }

        public static void UsingWhere(PlutoContext context)
        {
            Console.WriteLine("ExtensionMethods.UsingWhere");
            Console.WriteLine();

            var courses = context.Courses
                .Where(c => c.Level == 1);

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
            }
        }

        public static void MultipleOrderBys(PlutoContext context)
        {
            Console.WriteLine("ExtensionMethods.MultipleOrderBys");
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

        public static void Projection1(PlutoContext context)
        {
            Console.WriteLine("ExtensionMethods.Projection1");
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

        public static void Projection2(PlutoContext context)
        {
            Console.WriteLine("ExtensionMethods.Projection2");
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

        public static void Groups(PlutoContext context)
        {
            Console.WriteLine("ExtensionMethods.Groups");
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

        public static void GroupsAndProjection(PlutoContext context)
        {
            Console.WriteLine("ExtensionMethods.GroupsAndProjection");
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

        public static void GroupJoin(PlutoContext context)
        {
            Console.WriteLine("ExtensionMethods.GroupJoin");
            Console.WriteLine();

            var courses = context.Authors
                .GroupJoin(context.Courses, a => a.Id, c => c.AuthorId, (author, course) => new
                {
                    Author = author,
                    CourseCount = course.Count()
                });

            foreach (var course in courses)
            {
                Console.WriteLine("{0}, {1}", course.Author.Name, course.CourseCount);
            }
        }

        public static void Pagination(PlutoContext context)
        {
            Console.WriteLine("ExtensionMethods.Pagination");
            Console.WriteLine();

            var courses = context.Courses.OrderBy(c => c.Name).Skip(2).Take(2);
            foreach (var course in courses)
            {
                Console.WriteLine("{0}, {1}", course.Author.Name, course.Name);
            }
        }

        public static void ElementsAndAggregates(PlutoContext context)
        {
            Console.WriteLine("ExtensionMethods.ElementsAndAggregates");
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
    }
}
