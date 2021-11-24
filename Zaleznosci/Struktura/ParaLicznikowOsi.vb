Public Class ParaLicznikowOsi
    Implements IObiektPliku

    Public Property Adres1 As UShort
    Public Property Adres2 As UShort
    Public Property X1 As Single
    Public Property Y1 As Single
    Public Property X2 As Single
    Public Property Y2 As Single
    Public Property Odcinek1 As OdcinekToru
    Public Property Odcinek2 As OdcinekToru

    Friend Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        If Odcinek1 Is odcinek Then Odcinek1 = Nothing
        If Odcinek2 Is odcinek Then Odcinek2 = Nothing
    End Sub

    Friend Function Zapisz(konf As KonfiguracjaZapisu) As Byte() Implements IObiektPliku.Zapisz
        Using ms As New MemoryStream
            Using bw As New BinaryWriter(ms)
                bw.Write(Adres1)
                bw.Write(Adres2)
                bw.Write(X1)
                bw.Write(Y1)
                bw.Write(X2)
                bw.Write(Y2)
                bw.Write(If(Odcinek1 Is Nothing, PUSTE_ODWOLANIE, konf.OdcinkiTorow(Odcinek1)))
                bw.Write(If(Odcinek2 Is Nothing, PUSTE_ODWOLANIE, konf.OdcinkiTorow(Odcinek2)))
                Return ms.ToArray()
            End Using
        End Using
    End Function

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytu) As IObiektPliku
        Return New ParaLicznikowOsi
    End Function

    Friend Sub Otworz(dane() As Byte, konf As KonfiguracjaOdczytu, p As Pulpit) Implements IObiektPliku.Otworz
        Using ms As New MemoryStream(dane)
            Using br As New BinaryReader(ms)
                Dim id As Integer

                Adres1 = br.ReadUInt16
                Adres2 = br.ReadUInt16
                X1 = br.ReadSingle
                Y1 = br.ReadSingle
                X2 = br.ReadSingle
                Y2 = br.ReadSingle
                id = br.ReadInt32
                Odcinek1 = konf.OdcinkiTorow(id)
                id = br.ReadInt32
                Odcinek2 = konf.OdcinkiTorow(id)
            End Using
        End Using
        p.LicznikiOsi.Add(Me)
    End Sub
End Class