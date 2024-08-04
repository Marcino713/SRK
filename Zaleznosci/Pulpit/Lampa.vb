Imports IObiektPlikuTyp = Zaleznosci.IObiektPliku(Of Zaleznosci.KonfiguracjaZapisuPulpitu, Zaleznosci.KonfiguracjaOdczytuPulpitu)

Public Class Lampa
    Implements IObiektPlikuTyp, IObiektPunktowy(Of Single)

    Public Property Adres As UShort
    Public Property X As Single Implements IObiektPunktowy(Of Single).X
    Public Property Y As Single Implements IObiektPunktowy(Of Single).Y

    Private kolejka As New Queue(Of Byte)({0})

    Public Sub ZakolejkujZmianeJasnosci(jasnosc As Byte)
        kolejka.Enqueue(jasnosc)
    End Sub

    Public Function OdkolejkujZmianeJasnosci() As Byte
        If kolejka.Count > 1 Then
            Return kolejka.Dequeue
        Else
            Return kolejka.Peek
        End If
    End Function

    Public Function PobierzJasnosc() As Byte
        Return kolejka.Peek
    End Function

    Friend Function Zapisz(konf As KonfiguracjaZapisuPulpitu) As Byte() Implements IObiektPlikuTyp.Zapisz
        Using ms As New MemoryStream
            Using bw As New BinaryWriter(ms)
                bw.Write(Adres)
                bw.Write(X)
                bw.Write(Y)

                Return ms.ToArray()
            End Using
        End Using
    End Function

    Friend Shared Function UtworzObiekt(dane As Byte(), konf As KonfiguracjaOdczytuPulpitu) As IObiektPlikuTyp
        Return New Lampa
    End Function

    Friend Sub Otworz(dane() As Byte, konf As KonfiguracjaOdczytuPulpitu) Implements IObiektPlikuTyp.Otworz
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