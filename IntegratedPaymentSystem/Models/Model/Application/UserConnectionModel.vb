Imports System.ComponentModel

Public Class UserConnectionModel
    Inherits UserConnectionAbstract
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            If _id <> value Then
                _id = value
                RaisePropertyChanged("ID")
            End If
        End Set
    End Property

    Private _accountID As Integer
    Public Property AccountID() As Integer
        Get
            Return _accountID
        End Get
        Set(value As Integer)
            If _accountID <> value Then
                _accountID = value
                RaisePropertyChanged("AccountID")
            End If
        End Set
    End Property

    Private _status As String
    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(value As String)
            If _status <> value Then
                _status = value
                RaisePropertyChanged("Status")
            End If
        End Set
    End Property

    Private _planID As Integer
    Public Property PlanID() As Integer
        Get
            Return _planID
        End Get
        Set(value As Integer)
            If _planID <> value Then
                _planID = value
                RaisePropertyChanged("PlanID")
            End If
        End Set
    End Property

    Private _planName As String
    Public Property PlanName() As String
        Get
            Return _planName
        End Get
        Set(value As String)
            If _planName <> value Then
                _planName = value
                RaisePropertyChanged("PlanName")
            End If
        End Set
    End Property

    Private _planAmount As String
    Public Property PlanAmount() As String
        Get
            Return _planAmount
        End Get
        Set(value As String)
            If _planAmount <> value Then
                _planAmount = value
                RaisePropertyChanged("PlanAmount")
            End If
        End Set
    End Property

    Private Sub RaisePropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub New(ID As Integer, AccountID As Integer, Status As String, PlanID As Integer, PlanName As String, PlanAmount As String)
        With Me
            .ID = ID
            .AccountID = AccountID
            .Status = Status
            .PlanID = PlanID
            .PlanName = PlanName
            .PlanAmount = PlanAmount
        End With
    End Sub

    Public Shared Function GetAllByID(ID As Integer) As UserConnectionModel
        Dim queryResult = (
                From c In Models.Connections
                Join ip In Models.InternetPlans On c.InternetPlanID Equals ip.ID
                Where c.AccountID = ID
                Select New With {
                c.ID,
                c.AccountID,
                c.Status,
                .PlanID = c.InternetPlanID,
                .PlanName = ip.Name,
                .PlanAmount = ip.Price
                }).FirstOrDefault()

        If queryResult IsNot Nothing Then

            Return New UserConnectionModel(
                queryResult.ID,
                queryResult.AccountID,
                queryResult.Status,
                queryResult.PlanID,
                queryResult.PlanName,
                queryResult.PlanAmount
                )

        Else

            Return Nothing

        End If
    End Function
End Class
