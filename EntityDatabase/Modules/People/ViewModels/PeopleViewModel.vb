Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers
Imports Infrastructure.Helpers

Namespace Modules.People.ViewModels
    Public Class PeopleViewModel
        Inherits ViewModelBase

        Private _people As ObservableCollection(Of Person)
        Private dataAccess As IPeopleService
        Private _delete As ICommand
        Private _insert As ICommand
        Private _selected As ICommand
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
                    _delete = New RelayCommand(AddressOf DeletePersonToDB)
                End If
                Return _delete
            End Get
            Set(value As ICommand)
                _delete = value
            End Set
        End Property

        Sub DeletePersonToDB()
            If Selected IsNot Nothing Then
                Using school As New SchoolEntities
                    Dim query = (From per In school.People
                                 Where Selected.PersonID = per.PersonID
                                 Select per).FirstOrDefault
                    school.People.Remove(query)
                    school.SaveChanges()
                    Refresh()
                End Using
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
                Dim view As New Add_EditPerson
                view.ShowDialog()
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
    End Class
End Namespace

