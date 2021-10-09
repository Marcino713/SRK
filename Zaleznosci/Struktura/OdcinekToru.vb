Public Class OdcinekToru
    Public Property Adres As Integer
    Public Property Nazwa As String = ""
    Public Property Opis As String = ""

    Private _KostkiTory As New List(Of ITor)
    Public ReadOnly Property KostkiTory As List(Of ITor)
        Get
            Return _KostkiTory
        End Get
    End Property

    Public Event ZajetoOdcinek()
    Public Event ZwolnionoOdcinek()
End Class