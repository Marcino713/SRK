Imports Zaleznosci.PlikiPulpitu
Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.PlikiPulpitu.KonfiguracjaZapisu, Zaleznosci.PlikiPulpitu.KonfiguracjaOdczytu)

Public Class Lampa
    Implements IObiektPlikuTyp

    Public Property Adres As UShort
    Public Property X As Single
    Public Property Y As Single

    Friend Function Zapisz(konf As KonfiguracjaZapisu) As Byte() Implements IObiektPlikuTyp.Zapisz
        Using ms As New MemoryStream
            Using bw As New BinaryWriter(ms)
                bw.Write(Adres)
                bw.Write(X)
                bw.Write(Y)
                Return ms.ToArray()
            End Using
        End Using
    End Function

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytu) As IObiektPlikuTyp
        Return New Lampa
    End Function

    Friend Sub Otworz(dane() As Byte, konf As KonfiguracjaOdczytu) Implements IObiektPlikuTyp.Otworz
        Using ms As New MemoryStream(dane)
            Using br As New BinaryReader(ms)
                Adres = br.ReadUInt16
                X = br.ReadSingle
                Y = br.ReadSingle
            End Using
        End Using
        konf.Pulpit.Lampy.Add(Me)
    End Sub

End Class