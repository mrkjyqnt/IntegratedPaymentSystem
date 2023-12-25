Imports System.ComponentModel
Imports Org.BouncyCastle.Math.EC

Public Class UserActivitiesModel
    Inherits UserActivitiesAbstract
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

    Private _accountName As String
    Public Property AccountName() As String
        Get
            Return _accountName
        End Get
        Set(value As String)
            If _accountName <> value Then
                _accountName = value
                RaisePropertyChanged("AccountName")
            End If
        End Set
    End Property

    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(value As String)
            If _description <> value Then
                _description = value
                RaisePropertyChanged("Description")
            End If
        End Set
    End Property

    Private _amount As Decimal
    Public Property Amount() As Decimal
        Get
            Return _amount
        End Get
        Set(value As Decimal)
            If _amount <> value Then
                _amount = value
                RaisePropertyChanged("Amount")
            End If
        End Set
    End Property

    Private _activityDate As Date
    Public Property ActivityDate() As Date
        Get
            Return _activityDate
        End Get
        Set(value As Date)
            If _activityDate <> value Then
                _activityDate = value
                RaisePropertyChanged("ActivityDate")
            End If
        End Set
    End Property

    Private _type As String
    Public Property Type() As String
        Get
            Return _type
        End Get
        Set(value As String)
            If _type <> value Then
                _type = value
                RaisePropertyChanged("Type")
            End If
        End Set
    End Property

    Private _category As String
    Public Property Category() As String
        Get
            Return _category
        End Get
        Set(value As String)
            If _category <> value Then
                _category = value
                RaisePropertyChanged("Category")
            End If
        End Set
    End Property

    Private Sub RaisePropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub New(ID As Long, AccountID As Long, AccountName As String, Description As String, Amount As Decimal, ActivityDate As Date, Type As String, Category As String)
        With Me
            .ID = ID
            .AccountID = AccountID
            .AccountName = AccountName
            .Description = Description
            .Amount = Amount
            .ActivityDate = ActivityDate
            .Type = Type
            .Category = Category
        End With
    End Sub

    Public Sub New()
    End Sub

    Public Shared Function GetAllByID(ID As Integer) As List(Of UserActivitiesModel)
        Dim TransactionList = From a In Models.Activities
                              Join ai In Models.AccountInformations On a.AccountID Equals ai.ID
                              Order By a.ID Descending
                              Where a.AccountID = ID
                              Select New UserActivitiesModel(
                                  a.ID,
                                  a.AccountID,
                                  AccountName:= $"{ai.FirstName} {ai.LastName}",
                                  a.Description,
                                  a.Amount,
                                  a.ActivityDate,
                                  a.Type,
                                  a.Category
                          )

        Return TransactionList.ToList()
    End Function

End Class