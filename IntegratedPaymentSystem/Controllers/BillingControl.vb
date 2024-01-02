Module BillingControl

    Private ReadOnly _BillingDal As New BillingsDAL
    Private _BillingModel As New BillingsModel
    Private _BillingsModel As New List(of BillingsModel)

    Private _index As Integer

    Public Sub AddBilling(billing As BillingsModel)

        _BillingModel = _BillingDal.Read(billing)

        If _BillingModel Is Nothing

            _BillingModel = _BillingDal.Create(billing)
            Models.Billings.Add(_BillingModel)

            Message.Show("Success", $"Added Billing ID: {_BillingModel.ID}")

        Else

            Message.Show("Failed", "Account Number already exist</newline>Please remove the current one to continue</newline>")

        End If

    End Sub

    Public Sub UpdateBilling(billing As BillingsModel)

        _BillingModel = _BillingDal.Read(billing)

        If _BillingModel IsNot Nothing

            _BillingModel = _BillingDal.Update(billing)
            _index = Models.Billings.FindIndex(Function(data) data.ID = _BillingModel.ID)

            If _index >= 0 Then
                Models.Billings(_index) = _BillingModel
            End If

            Message.Show("Success", $"Updated Billing ID: {_BillingModel.ID}")

        Else

            Message.Show("Failed", $"Billing ID: {billing}</newline>is deleted on the database")

        End If

    End Sub

    Public Sub DeleteBilling(billing As BillingsModel)

        _BillingModel = _BillingDal.Read(billing)

        If _BillingModel IsNot Nothing

            _BillingDal.Delete(billing)
            Models.Billings.RemoveAll(Function(data) data.ID = billing.ID)
            Message.Show("Success", "Successfully Deleted")

        Else

            Message.Show("Failed", $"Billing ID: {billing}</newline>Is already deleted on the database")

        End If
            

    End Sub

End Module
