﻿namespace TruckDemo_v1.Domain.Entities
{
    public class UserLesson
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid LessonId { get; set; }
        public bool Completed { get; set; }
        public Lesson Lesson { get; set; } = null!;


    }
}
