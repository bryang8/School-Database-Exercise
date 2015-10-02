Imports Modules.Departments.ViewModels
Public Class Add_EditDepartment
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Grid.DataContext = New Add_EditDepartmentViewModel(Me)
    End Sub

End Class
