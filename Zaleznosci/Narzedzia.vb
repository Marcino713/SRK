Friend Module Narzedzia
    Friend Const PUSTE_ODWOLANIE As Integer = -1
    Private ReadOnly Kodowanie As New Text.UTF8Encoding

    Friend Function PobierzBajty(tekst As String) As Byte()
        If tekst Is Nothing Then tekst = ""
        Return Kodowanie.GetBytes(tekst)
    End Function

    Friend Function PobierzTekst(bajty As Byte()) As String
        Return Kodowanie.GetString(bajty)
    End Function

    Friend Sub ZapiszTekst(bw As BinaryWriter, tekst As String)
        If tekst Is Nothing Then tekst = ""
        Dim b As Byte() = Kodowanie.GetBytes(tekst)
        bw.Write(CType(b.Length, UShort))
        bw.Write(b)
    End Sub

    Friend Function OdczytajTekst(br As BinaryReader) As String
        Dim ile As UShort = br.ReadUInt16
        Dim b As Byte() = br.ReadBytes(ile)
        Return Kodowanie.GetString(b)
    End Function

    Friend Function PobierzInt32(b As Byte(), pocz As Integer, liczba_bajtow As Integer) As Integer
        Dim liczba As Integer = 0
        For i As Integer = 0 To liczba_bajtow - 1
            liczba = liczba Or (CType(b(pocz + i), Integer) << i * 8)
        Next
        Return liczba
    End Function

End Module