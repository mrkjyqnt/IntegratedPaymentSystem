Imports System.ComponentModel

Public Class UserBillingModel
    Inherits BillingsAbstract
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

    Private _accountID As String
    Public Property AccountID() As String
        Get
            Return _accountID
        End Get
        Set(value As String)
            If _accountID <> value Then
                _accountID = value
                RaisePropertyChanged("Name")
            End If
        End Set
    End Property

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(value As String)
            If _name <> value Then
                _name = value
                RaisePropertyChanged("Name")
            End If
        End Set
    End Property

    Private _number As String
    Public Property Number() As String
        Get
            Return _number
        End Get
        Set(value As String)
            If _number <> value Then
                _number = value
                RaisePropertyChanged("Number")
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

    Private _isAdmin As Boolean
    Public Property IsAdmin() As Boolean
        Get
            Return _isAdmin
        End Get
        Set(value As Boolean)
            If _isAdmin <> value Then
                _isAdmin = value
                RaisePropertyChanged("IsAdmin")
            End If
        End Set
    End Property

    Private _isEnabled As Boolean
    Public Property IsEnabled() As Boolean
        Get
            Return _isEnabled
        End Get
        Set(value As Boolean)
            If _isEnabled <> value Then
                _isEnabled = value
                RaisePropertyChanged("IsEnabled")
            End If
        End Set
    End Property

    Private Sub RaisePropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub New(ID As Long, AccountID As Long, Name As String, Number As String, Type As String, IsAdmin As Boolean, IsEnabled As Boolean)
        With Me
            .ID = ID
            .AccountID = AccountID
            .Name = Name
            .Number = Number
            .Type = Type
            .IsAdmin = IsAdmin
            .IsEnabled = IsEnabled
        End With
    End Sub

    Public Shared Function GetAllByID(ID As Integer) As UserBillingModel
        Dim queryResult = (
                From b In Models.Billings
                Where b.ID = ID
                Select New With {
                b.ID,
                b.AccountID,
                b.Name,
                b.Number,
                b.Type,
                b.IsAdmin,
                b.IsEnabled
                }).FirstOrDefault()

        If queryResult IsNot Nothing Then

            Return New UserBillingModel(
                queryResult.ID,
                queryResult.AccountID,
                queryResult.Name,
                queryResult.Number,
                queryResult.Type,
                queryResult.IsAdmin,
                queryResult.IsEnabled
                )

        Else

            Return Nothing

        End If
    End Function
End Class
