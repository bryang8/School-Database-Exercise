Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Namespace BusinessLogic.Services.Implementations
    Public Class OnsiteCoursesService
        Implements IOnsiteCoursesService

        Public Function GetAllOCourses() As IQueryable(Of OnsiteCourse) Implements IOnsiteCoursesService.GetAllOCourses
            Return DataContext.DBEntities.OnsiteCourses
        End Function
    End Class
End Namespace