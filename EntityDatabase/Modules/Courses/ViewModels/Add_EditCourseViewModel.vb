Imports BusinessLogic.Helpers
Imports Infrastructure.Helpers
Imports BusinessObjects.Helpers
Imports System.Collections.ObjectModel
Imports BusinessLogic.Services.Interfaces


Namespace Modules.Courses.ViewModels
    Public Class Add_EditCourseViewModel
        Inherits ViewModelBase

        Private _course As New Course
        Public _okCommand As ICommand
        Public _cancelCommand As ICommand
        Public _window As Add_EditCourse
        Private _departmentsNames As ObservableCollection(Of String)
        Private _departments As ObservableCollection(Of Department)
        Private _selectedDepartment As Department
        Private dataAccess As IDepartmentService


        Public Property Title As String
            Get
                Return Me._course.Title
            End Get
            Set(value As String)
                Me._course.Title = value
                OnPropertyChanged("Title")
            End Set
        End Property

        Public Property Credits As String
            Get
                Return Me._course.Credits
            End Get
            Set(value As String)
                Me._course.Credits = value
                OnPropertyChanged("Credits")
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
                DataContext.DBEntities.Courses.Add(_course)
                DataContext.DBEntities.SaveChanges()
                _window.Close()
            Catch ex As Exception
                MessageBox.Show("No se ha podido ingresar el departamentp", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub CancelCommand()
            _window.Close()
        End Sub

        Sub New(ByRef view As Add_EditCourse)
            Me._window = view
        End Sub
    End Class
End Namespace

