Imports Zaleznosci.PlikiPulpitu
Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.PlikiPulpitu.KonfiguracjaZapisu, Zaleznosci.PlikiPulpitu.KonfiguracjaOdczytu)

Public Class OdcinekToru
    Implements IObiektPlikuTyp

    Public Property Adres As UShort = 0
    Public Property Nazwa As String = ""
    Public Property Opis As String = ""

    Private _KostkiTory As New List(Of Tor)
    Public ReadOnly Property KostkiTory As List(Of Tor)
        Get
            Return _KostkiTory
        End Get
    End Property

    Friend Function Zapisz(konf As KonfiguracjaZapisu) As Byte() Implements IObiektPlikuTyp.Zapisz
        Using ms As New MemoryStream
            Using bw As New BinaryWriter(ms)
                bw.Write(konf.OdcinkiTorow(Me))
                bw.Write(Adres)
                ZapiszTekst(bw, Nazwa)
                ZapiszTekst(bw, Opis)
                Return ms.ToArray()
            End Using
        End Using
    End Function

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytu) As IObiektPlikuTyp
        Dim id As Integer = PobierzInt32(dane, 0, 4)
        Dim odc As New OdcinekToru
        konf.OdcinkiTorow.Add(id, odc)
        Return odc
    End Function

    Friend Sub Otworz(dane() As Byte, konf As KonfiguracjaOdczytu) Implements IObiektPlikuTyp.Otworz
        Using ms As New MemoryStream(dane)
            Using br As New BinaryReader(ms)
                ms.Seek(4, SeekOrigin.Begin)
                Adres = br.ReadUInt16()
                Nazwa = OdczytajTekst(br)
                Opis = OdczytajTekst(br)
            End Using
        End Using
        konf.Pulpit.OdcinkiTorow.Add(Me)
    End Sub
End Class