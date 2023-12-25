Public Class Models

    ' Application Models
    Public Shared Property User As AccountsModel
    Public Shared Property UserInformation As UserInformationModel
    Public Shared Property UserTransactions As List(Of UserTransactionsModel)
    Public Shared Property UserConnection As UserConnectionModel
    Public Shared Property UserBilling As UserBillingModel
    Public Shared Property UserActivities As List(Of UserActivitiesModel)

    Public Shared Property CustomerAccount As AccountsModel
    Public Shared Property CustomerInformation As UserInformationModel
    Public Shared Property CustomerTransactions As List(Of UserTransactionsModel)
    Public Shared Property CustomerConnection As UserConnectionModel
    Public Shared Property CustomerActivities As List(Of UserActivitiesModel)

    Public Shared Property CollectorAccount As AccountsModel
    Public Shared Property CollectorInformation As UserInformationModel
    Public Shared Property CollectorTransactions As List(Of UserTransactionsModel)
    Public Shared Property CollectorActivities As List(Of UserActivitiesModel)

    Public Shared Property AdministratorAccount As AccountsModel
    Public Shared Property AdministratorInformation As UserInformationModel
    Public Shared Property AdministratorActivities As List(Of UserActivitiesModel)

    Public Shared Property AdminBilling As BillingsModel

    ' Database Models
    Public Shared Property Accounts As List(Of AccountsModel)
    Public Shared Property AccountInformations As List(Of AccountInformationsModel)
    Public Shared Property Connections As List(Of ConnectionsModel)
    Public Shared Property Billings As List(Of BillingsModel)
    Public Shared Property InternetPlans As List(Of InternetPlansModel)
    Public Shared Property InternalTransactions As List(Of InternalTransactionsModel)
    Public Shared Property Activities As List(Of ActivitiesModel)

End Class
