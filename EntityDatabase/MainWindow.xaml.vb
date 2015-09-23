Imports Modules.Departments.ViewModels
Imports Modules.Courses.ViewModels
Imports Modules.People.ViewModels
Imports Modules.OnlineCourses.ViewModels
Imports Modules.OnsiteCourses.ViewModels
Imports Modules.OfficeAs.ViewModels
Imports Modules.StudentGrades.ViewModels
Class MainWindow
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.DepartmentsUserControl.MainGrid.DataContext = New DepartmentsViewModel()
        Me.CoursesUserControl.MainGrid.DataContext = New CoursesViewModel()
        Me.PeopleUserControl.MainGrid.DataContext = New PeopleViewModel()
        Me.OnlineCoursesUserControl.MainGrid.DataContext = New Modules.OnlineCourses.ViewModels.OCoursesViewModel()
        Me.OnsiteCoursesUserControl.MainGrid.DataContext = New Modules.OnsiteCourses.ViewModels.OCoursesViewModel()
        Me.OfficeAssignmentsUserControl.MainGrid.DataContext = New OfficeAViewModel()
        Me.StudentGradesUserControl.MainGrid.DataContext = New StudentGradesViewModel()

    End Sub
End Class
