using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public static class UpdatingData
    {
        public static void AddCourse(PlutoContext context)
        {
            var course = new Course
            {
                Name = "New course",
                Description = "Description for new course",
                FullPrice = 19.95f,
                Level = 1,
                Author = new Author {Id = 1, Name = "Mosh Hamedani"}
            };

            context.Courses.Add(course);
            context.SaveChanges();
        }

        public static void AddCourse_WPF(PlutoContext context)
        {
            var author = context.Authors.Single(a => a.Id == 1);

            var course = new Course
            {
                Name = "New course 2",
                Description = "Description for new course 2",
                FullPrice = 19.95f,
                Level = 1,
                Author = author
            };

            context.Courses.Add(course);
            context.SaveChanges();
        }

        public static void AddCourse_MVC(PlutoContext context)
        {
            var author = context.Authors.Single(a => a.Id == 1);

            var course = new Course
            {
                Name = "New course 3",
                Description = "Description for new course 3",
                FullPrice = 19.95f,
                Level = 1,
                AuthorId = 1
            };

            context.Courses.Add(course);
            context.SaveChanges();
        }

        public static void AddCourse_AuthorNotInContext(PlutoContext context)
        {
            var author = new Author() {Id = 1, Name = "Mosh Hamedani"};

            context.Authors.Attach(author);
            var course = new Course
            {
                Name = "New course 4",
                Description = "Description for new course 4",
                FullPrice = 19.95f,
                Level = 1,
                Author = author
            };

            context.Courses.Add(course);
            context.SaveChanges();
        }
    }
}
