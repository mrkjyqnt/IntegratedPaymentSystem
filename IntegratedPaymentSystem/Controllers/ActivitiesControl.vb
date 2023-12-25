Module ActivitiesControl
    Private _activities As New ActivitiesDAL

    Public Function NewActivity(activity As ActivitiesModel) As ActivitiesModel
        Return _activities.Create(activity)
    End Function

End Module
