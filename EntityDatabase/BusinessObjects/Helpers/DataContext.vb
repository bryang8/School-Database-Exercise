Namespace BusinessObjects.Helpers
    Public Class DataContext

        Private Shared _dbEntities As SchoolEntities

        Public Shared Property DBEntities() As SchoolEntities
            Get
                If _dbEntities Is Nothing Then
                    _dbEntities = New SchoolEntities()
                End If

                Return _dbEntities
            End Get
            Set(value As SchoolEntities)
                _dbEntities = value
            End Set
        End Property

    End Class
End Namespace

