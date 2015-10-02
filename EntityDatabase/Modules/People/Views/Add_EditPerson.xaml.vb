Imports Modules.People.ViewModels
Public Class Add_EditPerson

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Grid.DataContext = New Add_EditPersonVIewModel(Me)
    End Sub


End Class
