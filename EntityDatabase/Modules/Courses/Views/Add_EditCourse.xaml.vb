Imports Modules.Courses.ViewModels
Public Class Add_EditCourse
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Grid.DataContext = New Add_EditCourseViewModel(Me)
    End Sub
End Class
