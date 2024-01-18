Imports System.ComponentModel
Imports System.Data
Imports System.Security.Principal
Imports System.Windows.Interop
Imports iText.StyledXmlParser.Jsoup.Select

Public Class AdminAccountsView
    Inherits UserControl

    Private ReadOnly _mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)

    Private _accountInformationModel As New AccountInformationsModel
    Private _accountConnectionModel As New ConnectionsModel
    Private _acccountTransactionsModel As New List(Of InternalTransactionsModel)

    Public Property userAccountModel As New AccountsModel
    Public Property userInformationModel As New UserInformationModel
    Public Property userConnectionModel As New UserConnectionModel

    Public Sub New(account As UserInformationModel)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If _mainWindow IsNot Nothing Then
            ViewControl.Instance.Initialize(_mainWindow)
        End If

        userAccountModel = Models.Accounts.FirstOrDefault(Function(data) data.ID = account.AccountID)
        userInformationModel = Models.UsersInformation.FirstOrDefault(Function(data) data.AccountID = account.AccountID)
        userConnectionModel = Models.UsersConnection.FirstOrDefault(Function(data) data.AccountID = account.AccountID)

        If userAccountModel IsNot Nothing

            If userAccountModel.Role = "customer" Then

                PreviewAccountSection.DataContext = userInformationModel
                AccountSection.DataContext = userInformationModel
                ConnectionSection.DataContext = userConnectionModel
                PreviewUserSection.DataContext = userAccountModel

            End If

            If userAccountModel.Role = "collector" Then

                PreviewAccountSection.DataContext = userInformationModel
                AccountSection.DataContext = userInformationModel
                PreviewUserSection.DataContext = userAccountModel

                ConnectionSection.Visibility = Visibility.Collapsed

                With BodyBlock
                    .SetRow(TransactionsSection, 4)
                    .SetRow(ActivitiesSection, 6)
                End With

            End If

            If userAccountModel.Role = "administrator" Then

                PreviewAccountSection.DataContext = userInformationModel
                AccountSection.DataContext = userInformationModel
                PreviewUserSection.DataContext = userAccountModel

                ConnectionSection.Visibility = Visibility.Collapsed
                ActivitiesSection.Visibility = Visibility.Collapsed

                With BodyBlock
                    .SetRow(TransactionsSection, 4)
                End With

            End If

        End If
        
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
