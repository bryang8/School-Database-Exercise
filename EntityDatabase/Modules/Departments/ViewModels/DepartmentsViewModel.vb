Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Infrastructure.Helpers
Imports BusinessObjects.Helpers

Namespace Modules.Departments.ViewModels
    Public Class DepartmentsViewModel
        Inherits ViewModelBase

        Public Shadows _department As Add_EditDepartment
        Private _delete As ICommand
        Private _insert As ICommand
        Private _selected As Department
        Private _departmentToInsert As Department
        Private _departments As ObservableCollection(Of Department)
        Private dataAccess As IDepartmentService

        Public Property Departments As ObservableCollection(Of Department)
            Get
                Return Me._departments
            End Get
            Set(value As ObservableCollection(Of Department))
                Me._departments = value
                OnPropertyChanged("Departments")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllDepartments() As IQueryable(Of Department)
            Return Me.dataAccess.GetAllDepartments
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
                DataContext.DBEntities.Departments.Remove((From per In DataContext.DBEntities.Departments
                                 Where Selected.DepartmentID = per.DepartmentID
                                 Select per).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Refresh()
            End If
        End Sub


        Public Property Selected As Department
            Get
                Return _selected
            End Get
            Set(value As Department)
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

        Public Property AddDepartment As Department
            Get
                Return _departmentToInsert
            End Get
            Set(value As Department)
                _departmentToInsert = value
                OnPropertyChanged("InsertDepartment")
            End Set
        End Property
        Sub AddDepartmentToDB()
            Using school As New SchoolEntities
                _department = New Add_EditDepartment
                _department.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub Refresh()
            'Initialize property variable of departments
            Me.Departments.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IDepartmentService)(New DepartmentService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IDepartmentService)()
            ' Populate departments property variable  
            For Each element In Me.GetAllDepartments
                Me._departments.Add(element)
            Next
        End Sub

        Sub New()
            Me.Departments = New ObservableCollection(Of Department)
            Refresh()
        End Sub

        Public _toAdd As New Person
        Public _okButton As ICommand
        Public _cancelButton As ICommand
        Public _resetButton As ICommand

        Public Property Name As String
            Get
                Return Me._toAdd.FirstName
            End Get
            Set(value As String)
                Me._toAdd.FirstName = value
                OnPropertyChanged("Name")
            End Set
        End Property

        Public Property Budget As String
            Get
                Return Me._toAdd.LastName
            End Get
            Set(value As String)
                Me._toAdd.LastName = value
                OnPropertyChanged("Budget")
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
                DataContext.DBEntities.People.Add(_toAdd)
                DataContext.DBEntities.SaveChanges()
                _department.Close()
            Catch ex As Exception
                MsgBox("No se ha podido ingresar la persona", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub CancelCommand()
            _department.Close()
        End Sub
    End Class
End Namespace

