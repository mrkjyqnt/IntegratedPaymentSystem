Imports System.ComponentModel

Public Class UserInformationModel
    Inherits UserInformationAbstract
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

    Private _fullName As String
    Public Property FullName() As String
        Get
            Return _fullName
        End Get
        Set(value As String)
            If _fullName <> value Then
                _fullName = value
                RaisePropertyChanged("FullName")
            End If
        End Set
    End Property

    Private _address As String
    Public Property Address() As String
        Get
            Return _address
        End Get
        Set(value As String)
            If _address <> value Then
                _address = value
                RaisePropertyChanged("Address")
            End If
        End Set
    End Property

    Private _contactNumber As String
    Public Property ContactNumber() As String
        Get
            Return _contactNumber
        End Get
        Set(value As String)
            If _contactNumber <> value Then
                _contactNumber = value
                RaisePropertyChanged("ContactNumber")
            End If
        End Set
    End Property

    Private _email As String
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(value As String)
            If _email <> value Then
                _email = value
                RaisePropertyChanged("Email")
            End If
        End Set
    End Property

    Private Sub RaisePropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub New()
        ' Default constructor with no arguments
    End Sub

    Public Sub New(ID As Integer, AccountID As Integer, FullName As String, Address As String, ContactNumber As String, Email As String)
        ' Constructor with parameters
        Me.ID = ID
        Me.AccountID = AccountID
        Me.FullName = FullName
        Me.Address = Address
        Me.ContactNumber = ContactNumber
        Me.Email = Email
    End Sub

     Public Shared Function GetAll() As List(Of UserInformationModel)
        Dim transactionList = From ai In Models.AccountInformations
                              Order By ai.ID Descending
                              Select New UserInformationModel(
                                ai.ID,
                                ai.AccountID,
                                FullName := $"{ai.FirstName} {ai.MiddleName} {ai.LastName}",
                                ai.Address,
                                ai.ContactNumber,
                                ai.Email
                                )

        Return transactionList.ToList()
    End Function

    Public Shared Function GetAllByID(ID As Integer) As UserInformationModel
        Dim queryResult = (From ai In Models.AccountInformations
                           Where ai.AccountID = ID
                           Order By ai.ID Descending
                           Select New With {
                               ai.ID,
                               ai.AccountID,
                               .FullName = $"{ai.FirstName} {ai.MiddleName} {ai.LastName}",
                               ai.Address,
                               ai.ContactNumber,
                               ai.Email
                           }).FirstOrDefault()

        If queryResult IsNot Nothing Then
            Return New UserInformationModel(
                queryResult.ID,
                queryResult.AccountID,
                queryResult.FullName,
                queryResult.Address,
                queryResult.ContactNumber,
                queryResult.Email
            )
        Else
            Return Nothing ' Or create a default UserInformationModel instance
        End If
    End Function
End Class
