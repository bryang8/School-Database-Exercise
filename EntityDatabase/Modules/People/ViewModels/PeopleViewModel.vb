Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.People.ViewModels
    Public Class PeopleViewModel
        Inherits ViewModelBase

        Private _people As ObservableCollection(Of Person)
        Private dataAccess As IPeopleService

        Public Property People As ObservableCollection(Of Person)
            Get
                Return Me._people
            End Get
            Set(value As ObservableCollection(Of Person))
                Me._people = value
                OnPropertyChanged("People")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllDepartments() As IQueryable(Of Person)
            Return Me.dataAccess.GetAllPeople
        End Function

        Sub New()
            'Initialize property variable of departments
            Me._people = New ObservableCollection(Of Person)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IPeopleService)(New PersonService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IPeopleService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllDepartments
                Me._people.Add(element)
            Next
        End Sub
    End Class
End Namespace

