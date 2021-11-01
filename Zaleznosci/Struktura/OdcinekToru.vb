Public Class OdcinekToru
    Public Property Adres As Integer = 0
    Public Property Nazwa As String = ""
    Public Property Opis As String = ""

    Private _KostkiTory As New List(Of Tor)
    Public ReadOnly Property KostkiTory As List(Of Tor)
        Get
            Return _KostkiTory
        End Get
    End Property

    Public Event ZajetoOdcinek()
    Public Event ZwolnionoOdcinek()
End Class