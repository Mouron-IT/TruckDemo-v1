using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo_v1.Domain.Entities
{
    public class Lesson
    {
        public Lesson(string title, string content, Guid sectionId, int order)
        {
            Title = title;
            Content = content;
            SectionId = sectionId;
            Order = order;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid SectionId { get; set; }
        public int Order { get; set; }
        public Section Section { get; set; } = null!;
    }
}
