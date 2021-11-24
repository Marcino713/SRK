Public Class Lampa
    Implements IObiektPliku

    Public Property Adres As UShort
    Public Property X As Single
    Public Property Y As Single

    Friend Function Zapisz(konf As KonfiguracjaZapisu) As Byte() Implements IObiektPliku.Zapisz
        Using ms As New MemoryStream
            Using bw As New BinaryWriter(ms)
                bw.Write(Adres)
                bw.Write(X)
                bw.Write(Y)
                Return ms.ToArray()
            End Using
        End Using
    End Function

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytu) As IObiektPliku
        Return New Lampa
    End Function

    Friend Sub Otworz(dane() As Byte, konf As KonfiguracjaOdczytu, p As Pulpit) Implements IObiektPliku.Otworz
        Using ms As New MemoryStream(dane)
            Using br As New BinaryReader(ms)
                Adres = br.ReadUInt16
                X = br.ReadSingle
                Y = br.ReadSingle
            End Using
        End Using
        p.Lampy.Add(Me)
    End Sub

End Class