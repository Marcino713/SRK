Public Class PrzejazdRogatka
    Inherits PrzejazdElementWykonawczy

    Public Property CzasDoZamkniecia As UShort

    Public Overrides Sub Zapisz(bw As BinaryWriter)
        MyBase.Zapisz(bw)
        bw.Write(CzasDoZamkniecia)
    End Sub

    Public Overrides Sub Otworz(br As BinaryReader)
        MyBase.Otworz(br)
        CzasDoZamkniecia = br.ReadUInt16
    End Sub
End Class