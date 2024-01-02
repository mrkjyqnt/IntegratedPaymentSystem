Module TransactionsControl

    Public Property importedCount As Integer = 0
    Public Property nonimportedCount As Integer = 0
    Public Property confirmedCount As Integer = 0
    Public Property nonconfirmedCount As Integer = 0

    Private _index As Integer
    Private _indexApp As Integer

    Private ReadOnly ExternalTransactionDal As New ExternalTransactionsDAL
    Private _ExternalTransactionModel As New ExternalTransactionsModel
    Private _ExternalTransactionsModel As New List(Of ExternalTransactionsModel)

    Private ReadOnly InternalTransactionDal As New InternalTransactionsDAL
    Private _InternalTransactionModel As New InternalTransactionsModel
    Private _InternalTransactionsModel As New List(Of InternalTransactionsModel)

    Private _UserTransactionsModel As New UserTransactionsModel
    Private _UsersTransactionsModel As New List(Of UserTransactionsModel)

    Public Function AddExternalTransactions(externalTransaction As List(Of ExternalTransactionsModel))

        For Each transaction As ExternalTransactionsModel In externalTransaction

            _ExternalTransactionModel = ExternalTransactionDal.Read(transaction)

            If _ExternalTransactionModel Is Nothing Then

                transaction.Status = "Unclaimed"
                _ExternalTransactionModel = ExternalTransactionDal.Create(transaction)
                Models.ExternalTransactions.Add(_ExternalTransactionModel)
                importedCount +=1

            Else

                nonimportedCount +=1

            End If

        Next

        Return Nothing
    End Function

    Public Function UpdateExternalTransaction(externalTransaction As ExternalTransactionsModel)

        _ExternalTransactionModel = ExternalTransactionDal.Read(externalTransaction)

        If _ExternalTransactionModel IsNot Nothing

            _ExternalTransactionModel = ExternalTransactionDal.Update(externalTransaction)
            _index = Models.ExternalTransactions.FindIndex(Function(data) data.ID = _ExternalTransactionModel.ID)

            If _index >= 0 Then

                Models.ExternalTransactions(_index) = _ExternalTransactionModel
                confirmedCount +=1

            Else

                nonconfirmedCount +=1

            End If

        End If

        Return Nothing
    End Function

    Public Function UpdateInternalTransaction(internalTransaction As InternalTransactionsModel)

        _InternalTransactionModel = InternalTransactionDal.Read(internalTransaction)

        If _InternalTransactionModel IsNot Nothing

            _InternalTransactionModel = InternalTransactionDal.Update(internalTransaction)
            _UserTransactionsModel = Models.UsersTransactions.FirstOrDefault(Function(data) data.ID = _InternalTransactionModel.ID)

                With _UserTransactionsModel
                    .Status = _InternalTransactionModel.Status
                    .Collector = Models.UsersInformation.FirstOrDefault(Function(data) data.AccountID = _InternalTransactionModel.CollectorID).FullName.ToString
                End With

            _index = Models.InternalTransactions.FindIndex(Function(data) data.ID = _InternalTransactionModel.ID)
            _indexApp = Models.UsersTransactions.FindIndex(Function(data) data.ID = _InternalTransactionModel.ID)

            If _index >= 0 Then
                Models.InternalTransactions(_index) = _InternalTransactionModel
                Models.UsersTransactions(_indexApp) = _UserTransactionsModel
            End If

        End If

        Return Nothing
    End Function

End Module
