Imports Modules.StudentGrades.ViewModels

Public Class Add_EditStudentGrade
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Grid.DataContext = New Add_EditStudentGradeVM(Me)
    End Sub
End Class
