Module InformationControl
    Private ReadOnly AccountsDal As New AccountsDAL
    Private ReadOnly AccountInformationDal As New AccountInformationsDAL

    Private accountsModel As New AccountsModel
    Private accountsInformationModel As New AccountInformationsModel

    Private userInformationModel As New UserInformationModel

    Public Function UpdateAccount(type As String ,account As AccountsModel) As AccountsModel
        accountsModel = AccountsDal.Update(account)
        
        If type Is "me" Then
            Models.User = accountsModel
        End If

        If type Is "customer" Then
            Models.CustomerAccount = accountsModel
        End If

        If type Is "collector" Then
            Models.CollectorAccount = accountsModel
        End If

        If type Is "administrator" Then
            Models.AdministratorAccount = accountsModel
        End If

        Return accountsModel
    End Function

    Public Function UpdateInformation(type As String, accountInformation As AccountInformationsModel) As AccountInformationsModel
        accountsInformationModel = AccountInformationDal.Update(accountInformation)

        If type Is "me" Then
            Models.UserInformation = UserInformationModel.GetAllByID(accountsInformationModel.AccountID)
        End If

        If type Is "customer" Then
            Models.CustomerInformation = UserInformationModel.GetAllByID(accountsInformationModel.AccountID)
        End If

        If type Is "collector" Then
            Models.CollectorInformation = UserInformationModel.GetAllByID(accountsInformationModel.AccountID)
        End If

        If type Is "administrator" Then
            Models.AdministratorInformation = UserInformationModel.GetAllByID(accountsInformationModel.AccountID)
        End If

        Return accountsInformationModel
    End Function

End Module
