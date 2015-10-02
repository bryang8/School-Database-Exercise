Imports BusinessLogic.Helpers
Imports Infrastructure.Helpers
Imports BusinessObjects.Helpers
Imports System.Collections.ObjectModel
Imports BusinessLogic.Services.Interfaces


Namespace Modules.StudentGrades.ViewModels
    Public Class Add_EditStudentGradeVM
        Inherits ViewModelBase

        Private _studentGrade As New StudentGrade
        Public _okCommand As ICommand
        Public _cancelCommand As ICommand
        Public _window As Add_EditStudentGrade
        Private _courses As ObservableCollection(Of Course)
        Private _students As ObservableCollection(Of Person)
        Private _selectedCourse As Course
        Private _selectedStudent As Person
        Private dataAccess As ICoursesService
        Private _grade As String


        Public Property Grade As String
            Get
                Return _grade
            End Get
            Set(value As String)
                _grade = value
                _studentGrade.Grade = _grade
                OnPropertyChanged("Grade")
            End Set
        End Property
        Public Property SelectedCourse As Course
            Get
                Return _selectedCourse
            End Get
            Set(value As Course)
                _studentGrade.Course = value
                OnPropertyChanged("SelectedCourse")
            End Set
        End Property

        Public Property SelectedStudent As Person
            Get
                Return _selectedStudent
            End Get
            Set(value As Person)
                _studentGrade.Person = value
                OnPropertyChanged("SelectedStudent")
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

        Public Property Students As ObservableCollection(Of Person)
            Get
                Return _students
            End Get
            Set(value As ObservableCollection(Of Person))
                _students = value
                OnPropertyChanged("Students")
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
                DataContext.DBEntities.StudentGrades.Add(_studentGrade)
                DataContext.DBEntities.SaveChanges()
                _window.Close()
            Catch ex As Exception
                MessageBox.Show("No se ha podido ingresar la relacion studiante/curso", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub CancelCommand()
            _window.Close()
        End Sub

        Sub New(ByRef view As Add_EditStudentGrade)
            _courses = New ObservableCollection(Of Course)
            Dim courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In courses
                _courses.Add(element)
            Next
            _students = New ObservableCollection(Of Person)
            Dim students As IQueryable(Of Person) = DataContext.DBEntities.People
            For Each element In students
                If element.EnrollmentDate IsNot Nothing Then
                    _students.Add(element)
                End If
            Next
            Me._window = view
        End Sub
    End Class
End Namespace

