Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Namespace BusinessLogic.Services.Implementations
    Public Class OfficeAService
        Implements IOfficeAService

        Public Function GetAllOfficeAssignments() As IQueryable(Of OfficeAssignment) Implements IOfficeAService.GetAllOfficeAssignments
            Return DataContext.DBEntities.OfficeAssignments
        End Function
    End Class
End Namespace