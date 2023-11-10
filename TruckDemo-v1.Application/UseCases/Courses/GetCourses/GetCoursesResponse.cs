using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckDemo_v1.Application.DTO.Courses;

namespace TruckDemo_v1.Application.UseCases.Courses.GetCourses
{
    public record GetCoursesResponse(IEnumerable<CourseDTO> Courses);
}
