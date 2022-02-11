﻿Friend Module Narzedzia
    Friend Const PUSTE_ODWOLANIE As Integer = -1
    Private ReadOnly Kodowanie As New Text.UTF8Encoding
    Private FunkcjaSkrotu As Security.Cryptography.SHA1 = Security.Cryptography.SHA1.Create()

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

    Friend Function ObliczSkrot(dane As Byte()) As Byte()
        Return FunkcjaSkrotu.ComputeHash(dane)
    End Function

    Friend Function CzyRowne(dane1 As Byte(), dane2 As Byte()) As Boolean
        If dane1.Length <> dane2.Length Then Return False

        For i As Integer = 0 To dane1.Length - 1
            If dane1(i) <> dane2(i) Then Return False
        Next

        Return True
    End Function

End Module