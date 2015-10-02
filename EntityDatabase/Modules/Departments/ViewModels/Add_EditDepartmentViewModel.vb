Imports BusinessLogic.Helpers
Imports Infrastructure.Helpers
Imports BusinessObjects.Helpers


Namespace Modules.Departments.ViewModels
    Public Class Add_EditDepartmentViewModel
        Inherits ViewModelBase

        Private _department As New Department
        Public _okCommand As ICommand
        Public _cancelCommand As ICommand
        Public _window As Add_EditDepartment
        Public Property Name As String
            Get
                Return Me._department.Name
            End Get
            Set(value As String)
                Me._department.Name = value
                OnPropertyChanged("Name")
            End Set
        End Property

        Public Property Budget As String
            Get
                Return Me._department.Budget
            End Get
            Set(value As String)
                Me._department.Budget = value
                OnPropertyChanged("Budget")
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
                Dim departments As IQueryable(Of Department) = DataContext.DBEntities.Departments
                _department.StartDate = Date.Today
                For Each element In departments
                    _department.DepartmentID = Integer.Parse(element.DepartmentID.ToString) + 1
                Next
                DataContext.DBEntities.Departments.Add(_department)
                DataContext.DBEntities.SaveChanges()
                _window.Close()
            Catch ex As Exception
                MessageBox.Show("No se ha podido ingresar el departamentp", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub CancelCommand()
            _window.Close()
        End Sub

        Sub New(ByRef view As Add_EditDepartment)
            Me._window = view
        End Sub
    End Class
End Namespace
