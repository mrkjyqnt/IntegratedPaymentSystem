Imports System.Data
Imports System.Data.SqlClient

Module Connection

    Public CONNECTION As New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=PaymentDatabase;Integrated Security=True;")
    Public COMMAND As SqlCommand
    Public ADAPTER As SqlDataAdapter

    Public DATA As DataSet
    Public DATATABLE As DataTable
    Public PARAMETERS As New List(Of SqlParameter)

    Public DATAFIRST As DataRow

    Public RECORDCOUNT As Integer
    Public COMMANDSTR As String

    Public HASERROR As Boolean = False
    Public HASRECORD As Boolean = False
    Public ERRORMESSAGE As String

    Public Function Prepare(query As String)
        COMMANDSTR = query
        Return Nothing
    End Function

    Public Function Execute()
        Query(COMMANDSTR)
        Return Nothing
    End Function

    Public Function Query(q As String)
        If q.Equals("") Then
            q = COMMANDSTR
        End If

        RECORDCOUNT = 0
        ERRORMESSAGE = ""
        HASERROR = False
        HASRECORD = False

        Try
            CONNECTION.Open()

            COMMAND = New SqlCommand(q, CONNECTION)

            PARAMETERS.ForEach(Sub(t) COMMAND.Parameters.Add(t))

            PARAMETERS.Clear()

            DATA = New DataSet()
            ADAPTER = New SqlDataAdapter(COMMAND)
            RECORDCOUNT = ADAPTER.Fill(DATA)

            If RECORDCOUNT > 0 Then
                DATAFIRST = DATA.Tables(0).Rows(0)
                HASRECORD = True
            End If
        Catch ex As Exception
            HASERROR = True
            ERRORMESSAGE = ex.Message
        Finally
            If CONNECTION.State.Equals(ConnectionState.Open) Then
                CONNECTION.Close()
            End If
        End Try

        PARAMETERS.Clear()

        Return Nothing
    End Function

    Public Sub AddParam(key As String, value As Object)
        PARAMETERS.Add(New SqlParameter(key, value))
    End Sub

    Public Function GetAll(table As String) As DataSet
        Query("SELECT * FROM " & table)

        Return DATA
    End Function

    Public Function ExecuteScalar() As Object
        Dim result As Object = Nothing

        Try
            CONNECTION.Open()
            COMMAND = New SqlCommand(COMMANDSTR, CONNECTION)
            PARAMETERS.ForEach(Sub(t) COMMAND.Parameters.Add(t))

            result = COMMAND.ExecuteScalar()
        Catch ex As Exception
            HASERROR = True
            ERRORMESSAGE = ex.Message
        Finally
            If CONNECTION.State.Equals(ConnectionState.Open) Then
                CONNECTION.Close()
            End If
        End Try

        PARAMETERS.Clear()
        Return result
    End Function

End Module
