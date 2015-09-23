Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.OnlineCourses.ViewModels
    Public Class OCoursesViewModel
        Inherits ViewModelBase

        Private _oCourses As ObservableCollection(Of OnlineCourse)
        Private dataAccess As IOnlineCoursesService

        Public Property OnlineCourses As ObservableCollection(Of OnlineCourse)
            Get
                Return Me._oCourses
            End Get
            Set(value As ObservableCollection(Of OnlineCourse))
                Me._oCourses = value
                OnPropertyChanged("OnlineCourses")
            End Set
        End Property

        ' Function to get all OnlineCourses from service
        Private Function GetAllOCourses() As IQueryable(Of OnlineCourse)
            Return Me.dataAccess.GetAllOCourses
        End Function

        Sub New()
            'Initialize property variable of OnlineCourses
            Me._oCourses = New ObservableCollection(Of OnlineCourse)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IOnlineCoursesService)(New OCoursesService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IOnlineCoursesService)()
            ' Populate OnlineCourses property variable 
            For Each element In Me.GetAllOCourses
                Me._oCourses.Add(element)
            Next
        End Sub
    End Class
End Namespace
