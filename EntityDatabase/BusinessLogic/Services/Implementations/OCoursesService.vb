Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Namespace BusinessLogic.Services.Implementations
    Public Class OCoursesService
        Implements IOnlineCoursesService

        Public Function GetAllOCourses() As IQueryable(Of OnlineCourse) Implements IOnlineCoursesService.GetAllOCourses
            Return DataContext.DBEntities.OnlineCourses
        End Function
    End Class
End Namespace
