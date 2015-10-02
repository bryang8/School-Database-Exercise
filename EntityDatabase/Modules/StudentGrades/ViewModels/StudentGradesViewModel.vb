Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Infrastructure.Helpers
Imports BusinessObjects.Helpers

Namespace Modules.StudentGrades.ViewModels
    Public Class StudentGradesViewModel
        Inherits ViewModelBase

        Public Shadows _studentG As Add_EditStudentGrade
        Private _delete As ICommand
        Private _insert As ICommand
        Private _selected As StudentGrade
        Private _people As ObservableCollection(Of StudentGrade)
        Private dataAccess As IStudentGradeService

        Public Property StudentGrades As ObservableCollection(Of StudentGrade)
            Get
                Return Me._people
            End Get
            Set(value As ObservableCollection(Of StudentGrade))
                Me._people = value
                OnPropertyChanged("StudentGrades")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllStudentGrades() As IQueryable(Of StudentGrade)
            Return Me.dataAccess.GetAllStundentGrades
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
                DataContext.DBEntities.StudentGrades.Remove((From item In DataContext.DBEntities.StudentGrades
                                 Where Selected.EnrollmentID = item.EnrollmentID
                                 Select item).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Refresh()
            End If
        End Sub

        Public Property Selected As StudentGrade
            Get
                Return _selected
            End Get
            Set(value As StudentGrade)
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
        Sub AddDepartmentToDB()
            Using school As New SchoolEntities
                _studentG = New Add_EditStudentGrade
                _studentG.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub Refresh()
            'Initialize property variable of departments
            Me.StudentGrades.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IStudentGradeService)(New StudentGradeService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IStudentGradeService)()
            ' Populate departments property variable  
            For Each element In Me.GetAllStudentGrades
                Me._people.Add(element)
            Next
        End Sub

        Sub New()
            Me._people = New ObservableCollection(Of StudentGrade)
            Refresh()
        End Sub
    End Class
End Namespace
