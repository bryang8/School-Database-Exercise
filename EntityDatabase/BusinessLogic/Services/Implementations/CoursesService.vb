Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Public Class CoursesService
    Implements ICoursesService

    Public Function GetAllCourses() As IQueryable(Of Course) Implements ICoursesService.GetAllCourses
        Return DataContext.DBEntities.Courses
    End Function
End Class
