Public Class PrzejazdElementWykonawczy
    Implements IObiektPunktowy(Of Single)

    Public Property Adres As UShort
    Public Property X As Single Implements IObiektPunktowy(Of Single).X
    Public Property Y As Single Implements IObiektPunktowy(Of Single).Y

    Public Overridable Sub Zapisz(bw As BinaryWriter)
        bw.Write(Adres)
        bw.Write(X)
        bw.Write(Y)
    End Sub

    Public Overridable Sub Otworz(br As BinaryReader)
        Adres = br.ReadUInt16
        X = br.ReadSingle
        Y = br.ReadSingle
    End Sub
End Class