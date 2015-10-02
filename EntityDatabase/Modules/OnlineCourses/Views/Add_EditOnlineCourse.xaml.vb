Imports Modules.OnlineCourses.ViewModels
    Public Class Add_EditOnlineCourse
        Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Grid.DataContext = New Add_EditOnlineCourseVM(Me)
        End Sub
    End Class