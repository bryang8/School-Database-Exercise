Imports BusinessLogic.Helpers
Imports Infrastructure.Helpers
Imports BusinessObjects.Helpers


Namespace Modules.People.ViewModels
    Public Class Add_EditPersonVIewModel
        Inherits ViewModelBase

        Private _person As New Person
        Public _okCommand As ICommand
        Public _cancelCommand As ICommand
        Public _window As Add_EditPerson
        Public _studentRadio As Boolean
        Public _instructorRadio As Boolean
        Public Property FirstName As String
            Get
                Return Me._person.FirstName
            End Get
            Set(value As String)
                Me._person.FirstName = value
                OnPropertyChanged("FirstName")
            End Set
        End Property

        Public Property LastName As String
            Get
                Return Me._person.LastName
            End Get
            Set(value As String)
                Me._person.LastName = value
                OnPropertyChanged("LastName")
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
                DataContext.DBEntities.People.Add(_person)
                DataContext.DBEntities.SaveChanges()
                _window.Close()
            Catch ex As Exception
                MessageBox.Show("No se ha podido ingresar la persona", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub CancelCommand()
            _window.Close()
        End Sub

        Public Property instructorButton As Boolean
            Get
                Return Me._instructorRadio
            End Get
            Set(value As Boolean)
                Me._instructorRadio = value
                OnPropertyChanged("instructorButton")
                _person.HireDate = Date.Now
                _person.EnrollmentDate = Nothing
            End Set
        End Property

        Public Property studentButton As Boolean
            Get
                Return Me._studentRadio
            End Get
            Set(value As Boolean)
                Me._studentRadio = value
                OnPropertyChanged("studentButton")
                _person.HireDate = Nothing
                _person.EnrollmentDate = Date.Now
            End Set
        End Property

        Sub New(ByRef view As Add_EditPerson)
            Me._window = view
        End Sub
    End Class
End Namespace
