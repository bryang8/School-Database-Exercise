Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Infrastructure.Helpers
Imports BusinessObjects.Helpers

Namespace Modules.OnlineCourses.ViewModels
    Public Class OCoursesViewModel
        Inherits ViewModelBase

        Private _oCourses As ObservableCollection(Of OnlineCourse)
        Private dataAccess As IOnlineCoursesService
        Public Shadows _course As Add_EditOnlineCourse
        Private _delete As ICommand
        Private _insert As ICommand
        Private _selected As OnlineCourse
        Private _courseToInsert As OnlineCourse

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

        Public Property DeleteCommand As ICommand
            Get
                If _delete Is Nothing Then
                    _delete = New RelayCommand(AddressOf DeleteDB)
                End If
                Return _delete
            End Get
            Set(value As ICommand)
                _delete = value
            End Set
        End Property

        Sub DeleteDB()
            If Selected IsNot Nothing Then
                DataContext.DBEntities.OnlineCourses.Remove((From per In DataContext.DBEntities.OnlineCourses
                                 Where Selected.CourseID = per.CourseID And Selected.URL = per.URL
                                 Select per).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Refresh()
            End If
        End Sub

        Public Property Selected As OnlineCourse
            Get
                Return _selected
            End Get
            Set(value As OnlineCourse)
                _selected = value
            End Set
        End Property

        Public Property AddCommand As ICommand
            Get
                If Me._insert Is Nothing Then
                    Me._insert = New RelayCommand(AddressOf AddDepartmentToDB)
                End If
                Return Me._insert
            End Get
            Set(value As ICommand)
                _insert = value
            End Set
        End Property

        Public Property AddCourse As OnlineCourse
            Get
                Return _courseToInsert
            End Get
            Set(value As OnlineCourse)
                _courseToInsert = value
                OnPropertyChanged("OnlineCourse")
            End Set
        End Property
        Sub AddDepartmentToDB()
            Using school As New SchoolEntities
                _course = New Add_EditOnlineCourse
                _course.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub Refresh()
            'Initialize property variable of departments
            Me.OnlineCourses.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IOnlineCoursesService)(New OCoursesService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IOnlineCoursesService)()
            ' Populate departments property variable  
            For Each element In Me.GetAllOCourses
                Me._oCourses.Add(element)
            Next
        End Sub

        Sub New()
            Me._oCourses = New ObservableCollection(Of OnlineCourse)
            Refresh()
        End Sub
    End Class
End Namespace
