Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers

Namespace BusinessLogic.Services.Implementations
    Public Class PersonService
        Implements IPeopleService

        Public Function GetAllPeople() As IQueryable(Of Person) Implements IPeopleService.GetAllPeople
            Return DataContext.DBEntities.People
        End Function
    End Class
End Namespace
