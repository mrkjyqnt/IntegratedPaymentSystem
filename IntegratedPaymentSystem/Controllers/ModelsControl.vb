Public Class ModelsControl

    Private ReadOnly _accounts As New AccountsDAL
    Private ReadOnly _accountInformations As New AccountInformationsDAL
    Private ReadOnly _connections As New ConnectionsDAL
    Private ReadOnly _billings As New BillingsDAL
    Private ReadOnly _internetPlans As New InternetPlansDAL
    Private ReadOnly _internalTransactions As New InternalTransactionsDAL
    Private ReadOnly _externalTransactions As New ExternalTransactionsDAL
    Private ReadOnly _activities As New ActivitiesDAL

    Public Sub BuildModels()

        Models.Accounts = _accounts.GetAll
        Models.AccountInformations = _accountInformations.GetAll
        Models.Connections = _connections.GetAll
        Models.Billings = _billings.GetAll
        Models.InternetPlans = _internetPlans.GetAll
        Models.InternalTransactions = _internalTransactions.GetAll
        Models.ExternalTransactions = _externalTransactions.GetAll
        Models.Activities = _activities.GetAll

        Models.UsersTransactions = UserTransactionsModel.GetAll

    End Sub

    Public Sub BuildCustomerModels()

        Models.UserInformation = UserInformationModel.GetAllByID(Models.User.ID)
        Models.UserTransactions = UserTransactionsModel.GetAllByCustomerID(Models.User.ID)
        Models.UserConnection = UserConnectionModel.GetAllByID(Models.User.ID)
        Models.UserActivities = UserActivitiesModel.GetAllByID(Models.User.ID)


    End Sub
    Public Sub BuildCollectorModels()

        Models.UserInformation = UserInformationModel.GetAllByID(Models.User.ID)
        Models.UserTransactions = UserTransactionsModel.GetAllByCollectorID(Models.User.ID)
        Models.UserActivities = UserActivitiesModel.GetAllByID(Models.User.ID)

    End Sub

    Public Sub BuildAdministratorModels()

        Models.UserInformation = UserInformationModel.GetAllByID(Models.User.ID)
        Models.UserActivities = UserActivitiesModel.GetAllByID(Models.User.ID)

    End Sub

End Class
