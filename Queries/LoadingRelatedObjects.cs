using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public static class LoadingRelatedObjects
    {
        public static void LazyLoading(PlutoContext context) // avoid lazy loading in web applications
        {
            Console.WriteLine("Lazy loading");
            Console.WriteLine();

            var course = context.Courses.Single(c => c.Id == 2);
            foreach (var tag in course.Tags)
            {
                Console.WriteLine(tag.Name);
            }
        }

        public static void NPlusOne(PlutoContext context)
        {
            Console.WriteLine("N plus one");
            Console.WriteLine();

            // Bad because it has a magic string and if you rename the Author property in the Course class then KABOOM
            //var courses = context.Courses.Include("Author").ToList(); 
            var courses = context.Courses.Include(c => c.Author).ToList();
            foreach (var course in courses)
            {
                Console.WriteLine("{0} by {1}", course.Name, course.Author.Name);
            }

            //context.Courses
            //    .Include(c => c.Author)
            //    .Include(a => a.Tags.Select(t => t.Moderator))
            //    .ToList();
        }

        public static void ExplicitLoading(PlutoContext context)
        {
            Console.WriteLine("Explicit loading 1");
            Console.WriteLine();

            //var author = context.Authors.Include(a => a.Courses).Single(a => a.Id == 1); // Eager loading
            var author = context.Authors.Single(a => a.Id == 1);

            //MSDN - Only works for a single author not multiples ie if the line above was ToList and not Single
            context.Entry(author).Collection(a => a.Courses).Load();
            //context.Entry(author).Collection(a => a.Courses).Query().Where(c => c.FullPrice == 0).Load();

            //Better way
            context.Courses.Where(c => c.AuthorId == author.Id).Load();
            //context.Courses.Where(c => c.AuthorId == author.Id && c.FullPrice == 0).Load();

            foreach (var course in author.Courses)
            {
                Console.WriteLine("{0}", course.Name);
            }
        }

        public static void ExplicitLoading2(PlutoContext context)
        {
            Console.WriteLine("Explicit loading 2");
            Console.WriteLine();

            var authors = context.Authors.ToList();
            var authorIds = authors.Select(a => a.Id);

            context.Courses.Where(c => authorIds.Contains(c.AuthorId) && c.FullPrice == 0);

            foreach (var author in authors)
            {
                Console.WriteLine("{0}", author.Name);
                foreach (var course in author.Courses)
                {
                    Console.WriteLine("\t{0}", course.Name);
                }
            }
        }
    }
}
