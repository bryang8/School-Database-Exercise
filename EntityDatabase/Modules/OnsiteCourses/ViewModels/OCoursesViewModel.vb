Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.OnsiteCourses.ViewModels
    Public Class OCoursesViewModel
        Inherits ViewModelBase

        Private _oCourses As ObservableCollection(Of OnsiteCourse)
        Private dataAccess As IOnsiteCoursesService

        Public Property OnsiteCourses As ObservableCollection(Of OnsiteCourse)
            Get
                Return Me._oCourses
            End Get
            Set(value As ObservableCollection(Of OnsiteCourse))
                Me._oCourses = value
                OnPropertyChanged("OnsiteCourses")
            End Set
        End Property

        ' Function to get all OnsiteCourses from service
        Private Function GetAllOCourses() As IQueryable(Of OnsiteCourse)
            Return Me.dataAccess.GetAllOCourses
        End Function

        Sub New()
            'Initialize property variable of OnsiteCourses
            Me._oCourses = New ObservableCollection(Of OnsiteCourse)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IOnsiteCoursesService)(New OnsiteCoursesService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IOnsiteCoursesService)()
            ' Populate OnsiteCourses property variable 
            For Each element In Me.GetAllOCourses
                Me._oCourses.Add(element)
            Next
        End Sub
    End Class
End Namespace
