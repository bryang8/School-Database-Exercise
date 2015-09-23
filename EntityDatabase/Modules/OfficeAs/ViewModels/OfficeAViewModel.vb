Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.OfficeAs.ViewModels
    Public Class OfficeAViewModel
        Inherits ViewModelBase

        Private _officeAs As ObservableCollection(Of OfficeAssignment)
        Private dataAccess As IOfficeAService

        Public Property OfficeAs As ObservableCollection(Of OfficeAssignment)
            Get
                Return Me._officeAs
            End Get
            Set(value As ObservableCollection(Of OfficeAssignment))
                Me._officeAs = value
                OnPropertyChanged("OfficeAs")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllOfficeAssignments() As IQueryable(Of OfficeAssignment)
            Return Me.dataAccess.GetAllOfficeAssignments
        End Function

        Sub New()
            'Initialize property variable of departments
            Me._officeAs = New ObservableCollection(Of OfficeAssignment)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IOfficeAService)(New OfficeAService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IOfficeAService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllOfficeAssignments
                Me._officeAs.Add(element)
            Next
        End Sub
    End Class
End Namespace

