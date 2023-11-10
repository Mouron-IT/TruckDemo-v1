using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Domain.Entities
{
    public class Section
    {
        public Section(string title, string content, Guid courseId, int order)
        {
            Title = title;
            Content = content;
            CourseId = courseId;
            Order = order;
            Lessons = new HashSet<Lesson>();

        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CourseId { get; set; }
        public int Order { get; set; }
        public Course Course { get; set; } = null!;
        public ICollection<Lesson> Lessons { get; set; }
    }
}
