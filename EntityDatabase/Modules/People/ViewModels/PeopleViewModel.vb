Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers
Imports Infrastructure.Helpers

Namespace Modules.People.ViewModels
    Public Class PeopleViewModel
        Inherits ViewModelBase

        Public Shadows _person As Add_EditPerson
        Private _people As ObservableCollection(Of Person)
        Private dataAccess As IPeopleService
        Private _delete As ICommand
        Private _insert As ICommand
        Private _selected As Person
        Private _personToInsert As Person

        Public Property People As ObservableCollection(Of Person)
            Get
                Return Me._people
            End Get
            Set(value As ObservableCollection(Of Person))
                Me._people = value
                OnPropertyChanged("People")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllPeople() As IQueryable(Of Person)
            Return Me.dataAccess.GetAllPeople
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
                DataContext.DBEntities.People.Remove((From per In DataContext.DBEntities.People
                                 Where Selected.PersonID = per.PersonID
                                 Select per).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Refresh()
            End If
        End Sub


        Public Property Selected As Person
            Get
                Return _selected
            End Get
            Set(value As Person)
                _selected = value
            End Set
        End Property


        Public Property AddCommand As ICommand
            Get
                If Me._insert Is Nothing Then
                    Me._insert = New RelayCommand(AddressOf AddPersonToDB)
                End If
                Return Me._insert
            End Get
            Set(value As ICommand)
                _insert = value
            End Set
        End Property

        Public Property AddPerson As Person
            Get
                Return _personToInsert
            End Get
            Set(value As Person)
                _personToInsert = value
                OnPropertyChanged("InsertPerson")
            End Set
        End Property
        Sub AddPersonToDB()
            Using school As New SchoolEntities
                _person = New Add_EditPerson
                _person.ShowDialog()
                Refresh()
            End Using
        End Sub

        Sub Refresh()
            'Initialize property variable of departments
            Me.People.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IPeopleService)(New PersonService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IPeopleService)()
            ' Populate departments property variable  
            For Each element In Me.GetAllPeople
                Me._people.Add(element)
            Next
        End Sub

        Sub New()
            Me.People = New ObservableCollection(Of Person)
            Refresh()
        End Sub

        Public _personToAdd As New Person
        Private _instructor As Boolean
        Private _student As Boolean
        Public _okButton As ICommand
        Public _cancelButton As ICommand
        Public _resetButton As ICommand

        Public Property FirstName As String
            Get
                Return Me._personToAdd.FirstName
            End Get
            Set(value As String)
                Me._personToAdd.FirstName = value
                OnPropertyChanged("FirstName")
            End Set
        End Property

        Public Property LastName As String
            Get
                Return Me._personToAdd.LastName
            End Get
            Set(value As String)
                Me._personToAdd.LastName = value
                OnPropertyChanged("LastName")
            End Set
        End Property

        Public Property instructor As Boolean
            Get
                Return Me._instructor
            End Get
            Set(value As Boolean)
                Me._instructor = value
                OnPropertyChanged("instructor")
                _personToAdd.HireDate = Date.Now
                _personToAdd.EnrollmentDate = Nothing
            End Set
        End Property

        Public Property student As Boolean
            Get
                Return Me._student
            End Get
            Set(value As Boolean)
                Me._student = value
                OnPropertyChanged("instructor")
                _personToAdd.HireDate = Nothing
                _personToAdd.EnrollmentDate = Date.Now
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
                DataContext.DBEntities.People.Add(_personToAdd)
                DataContext.DBEntities.SaveChanges()
                _person.Close()
            Catch ex As Exception
                MsgBox("No se ha podido ingresar la persona", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub CancelCommand()
            _person.Close()
        End Sub
    End Class
End Namespace

