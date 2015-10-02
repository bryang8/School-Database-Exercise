Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Infrastructure.Helpers
Imports BusinessObjects.Helpers

Namespace Modules.Courses.ViewModels
    Public Class CoursesViewModel
        Inherits ViewModelBase

        Public Shadows _course As Add_EditCourse
        Private _delete As ICommand
        Private _insert As ICommand
        Private _selected As Course
        Private _courseToInsert As Course
        Private _courses As ObservableCollection(Of Course)
        Private dataAccess As ICoursesService

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return Me._courses
            End Get
            Set(value As ObservableCollection(Of Course))
                Me._courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllCourses() As IQueryable(Of Course)
            Return Me.dataAccess.GetAllCourses
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
                DataContext.DBEntities.Courses.Remove((From per In DataContext.DBEntities.Courses
                                 Where Selected.CourseID = per.CourseID
                                 Select per).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Refresh()
            End If
        End Sub

        Public Property Selected As Course
            Get
                Return _selected
            End Get
            Set(value As Course)
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

        Public Property AddCourse As Course
            Get
                Return _courseToInsert
            End Get
            Set(value As Course)
                _courseToInsert = value
                OnPropertyChanged("InsertDepartment")
            End Set
        End Property
        Sub AddDepartmentToDB()
            Using school As New SchoolEntities
                _course = New Add_EditCourse
                _course.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub Refresh()
            'Initialize property variable of departments
            Me.Courses.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of ICoursesService)(New CoursesService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of ICoursesService)()
            ' Populate departments property variable  
            For Each element In Me.GetAllCourses
                Me._courses.Add(element)
            Next
        End Sub

        Sub New()
            Me.Courses = New ObservableCollection(Of Course)
            Refresh()
        End Sub

        Public _toAdd As New Course
        Public _okButton As ICommand
        Public _cancelButton As ICommand
        Public Property Title As String
            Get
                Return Me._toAdd.Title
            End Get
            Set(value As String)
                Me._toAdd.Title = value
                OnPropertyChanged("Title")
            End Set
        End Property

        Public Property Credits As String
            Get
                Return Me._toAdd.Credits
            End Get
            Set(value As String)
                Me._toAdd.Credits = value
                OnPropertyChanged("Credits")
            End Set
        End Property

        Public ReadOnly Property OkButton As ICommand
            Get
                If Me._okButton Is Nothing Then
                    Me._okButton = New RelayCommand(AddressOf OkCommand)
                End If
                Return Me._okButton
            End Get
        End Property

        Public ReadOnly Property CancelButton As ICommand
            Get
                If Me._cancelButton Is Nothing Then
                    Me._cancelButton = New RelayCommand(AddressOf CancelCommand)
                End If
                Return Me._cancelButton
            End Get
        End Property

        Sub OkCommand()
            Try
                DataContext.DBEntities.Courses.Add(_toAdd)
                DataContext.DBEntities.SaveChanges()
                _course.Close()
            Catch ex As Exception
                MsgBox("No se ha podido ingresar la persona", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub CancelCommand()
            _course.Close()
        End Sub
    End Class
End Namespace