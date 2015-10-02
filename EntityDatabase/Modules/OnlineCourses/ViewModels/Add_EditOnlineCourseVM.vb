Imports BusinessLogic.Helpers
Imports Infrastructure.Helpers
Imports BusinessObjects.Helpers
Imports System.Collections.ObjectModel
Imports BusinessLogic.Services.Interfaces


Namespace Modules.OnlineCourses.ViewModels
    Public Class Add_EditOnlineCourseVM
        Inherits ViewModelBase

        Private _onlineCourse As New OnlineCourse
        Public _okCommand As ICommand
        Public _cancelCommand As ICommand
        Public _window As Add_EditOnlineCourse
        Private _courses As ObservableCollection(Of Course)
        Private _selectedCourse As Course
        Private dataAccess As ICoursesService


        Public Property URL As String
            Get
                Return Me._onlineCourse.URL
            End Get
            Set(value As String)
                Me._onlineCourse.URL = value
                OnPropertyChanged("URL")
            End Set
        End Property

        Public Property SelectedCourse As Course
            Get
                Return _selectedCourse
            End Get
            Set(value As Course)
                _onlineCourse.Course = value
                OnPropertyChanged("SelectedCourse")
            End Set
        End Property

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return _courses
            End Get
            Set(value As ObservableCollection(Of Course))
                _courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Public ReadOnly Property OkButton As ICommand
            Get
                If Me._okCommand Is Nothing Then
                    Me._okCommand = New RelayCommand(AddressOf OkCommand)
                End If
                Return Me._okCommand
            End Get
        End Property

        Public ReadOnly Property CancelButton As ICommand
            Get
                If Me._cancelCommand Is Nothing Then
                    Me._cancelCommand = New RelayCommand(AddressOf CancelCommand)
                End If
                Return Me._cancelCommand
            End Get
        End Property

        Sub OkCommand()
            Try
                DataContext.DBEntities.OnlineCourses.Add(_onlineCourse)
                DataContext.DBEntities.SaveChanges()
                _window.Close()
            Catch ex As Exception
                MessageBox.Show("No se ha podido ingresar el curso online", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub CancelCommand()
            _window.Close()
        End Sub

        Sub New(ByRef view As Add_EditOnlineCourse)
            _courses = New ObservableCollection(Of Course)
            Dim courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In courses
                _courses.Add(element)
            Next
            Me._window = view
        End Sub
    End Class
End Namespace

