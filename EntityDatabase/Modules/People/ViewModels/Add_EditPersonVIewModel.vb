Imports BusinessLogic.Helpers

Public Class Add_EditPersonVIewModel
    Inherits ViewModelBase


    Private _firstName As String
    Private _lastName As String
    Public Property FirstName As String
        Get
            Return Me._FirstName
        End Get
        Set(value As String)
            Me._FirstName = value
        End Set
    End Property

    Public Property LastName As String
        Get
            Return Me._LastName
        End Get
        Set(value As String)
            Me._LastName = value
        End Set
    End Property
End Class
