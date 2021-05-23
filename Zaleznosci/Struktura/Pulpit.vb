﻿Public Class Pulpit
    Public Const NAGLOWEK As String = "STAC"
    Public Shared ReadOnly ObslugiwaneWersje As New Dictionary(Of Integer, Integer) From {{0, 1}}

    Private Property _WersjaGlowna As UShort
    Public ReadOnly Property WersjaGlowna As UShort
        Get
            Return _WersjaGlowna
        End Get
    End Property

    Private Property _WersjaBoczna As UShort
    Public ReadOnly Property WersjaBoczna As UShort
        Get
            Return _WersjaBoczna
        End Get
    End Property

    Public Property Nazwa As String

    Public ReadOnly Property DataUtworzenia As Date

    Private Property _Szerokosc As Integer
    Public ReadOnly Property Szerokosc As Integer
        Get
            Return _Szerokosc
        End Get
    End Property

    Private Property _Wysokosc As Integer
    Public ReadOnly Property Wysokosc As Integer
        Get
            Return _Wysokosc
        End Get
    End Property

    Private _Kostki As Kostka(,)
    Public ReadOnly Property Kostki As Kostka(,)
        Get
            Return _Kostki
        End Get
    End Property

    Public Sub New()
        Me.New(10, 10)
    End Sub

    Public Sub New(szer As Integer, wys As Integer)
        _Szerokosc = szer
        _Wysokosc = wys
        ReDim _Kostki(_Szerokosc - 1, _Wysokosc - 1)
    End Sub

    Public Sub Zapisz(Strumien As BinaryWriter)

    End Sub

    Public Sub Otworz(Strumien As BinaryReader)

    End Sub

    Public Sub PowiekszPulpit(kierunek As KierunekEdycjiPulpitu, rozmiar As Integer)
        If rozmiar < 0 Then
            Throw New ArgumentException("Rozmiar nie może być ujemny.")
        End If

        Dim przesx As Integer = 0
        Dim przesy As Integer = 0
        If kierunek = KierunekEdycjiPulpitu.Lewo Or kierunek = KierunekEdycjiPulpitu.Prawo Then
            _Szerokosc += rozmiar
            przesx = rozmiar
        Else
            _Wysokosc += rozmiar
            przesy = rozmiar
        End If
        ReDim Preserve _Kostki(_Szerokosc - 1, _Wysokosc - 1)

        If kierunek = KierunekEdycjiPulpitu.Lewo Or kierunek = KierunekEdycjiPulpitu.Gora Then
            For x As Integer = _Szerokosc - 1 To 0 Step -1
                For y As Integer = _Wysokosc - 1 To 0 Step -1
                    _Kostki(x, y) = _Kostki(x - przesx, y - przesy)
                    _Kostki(x - przesx, y - przesy) = Nothing
                Next
            Next
        End If
    End Sub

    Public Function PomniejszPulpit(kierunek As KierunekEdycjiPulpitu, rozmiar As Integer) As Boolean
        If rozmiar < 0 Then
            Throw New ArgumentException("Rozmiar nie może być ujemny.")
        End If

        If (kierunek = KierunekEdycjiPulpitu.Lewo Or kierunek = KierunekEdycjiPulpitu.Prawo) And rozmiar >= Szerokosc Then
            Throw New ArgumentException("Rozmiar musi być mniejszy niż szerokość pulpitu.")
        End If

        If (kierunek = KierunekEdycjiPulpitu.Gora Or kierunek = KierunekEdycjiPulpitu.Dol) And rozmiar >= Wysokosc Then
            Throw New ArgumentException("Rozmiar musi być mniejszy niż wysokość pulpitu.")
        End If

        'Sprawdź, czy w usuwanym zakresie nie ma żadnych kostek
        Dim poczx As Integer = 0
        Dim koncx As Integer = _Szerokosc - 1
        Dim poczy As Integer = 0
        Dim koncy As Integer = _Wysokosc - 1
        Dim przesx As Integer = 0
        Dim przesy As Integer = 0

        Select Case kierunek
            Case KierunekEdycjiPulpitu.Gora
                koncy = rozmiar - 1
                przesy = rozmiar
            Case KierunekEdycjiPulpitu.Prawo
                poczx = _Szerokosc - rozmiar
                przesx = rozmiar
            Case KierunekEdycjiPulpitu.Dol
                poczy = _Wysokosc - rozmiar
                przesy = rozmiar
            Case KierunekEdycjiPulpitu.Lewo
                koncx = rozmiar - 1
                przesx = rozmiar
        End Select

        Dim puste As Boolean = True
        For x As Integer = poczx To koncx
            For y As Integer = poczy To koncy
                If _Kostki(x, y) IsNot Nothing Then
                    puste = False
                    Exit For
                End If
            Next
            If Not puste Then Exit For
        Next

        If Not puste Then Return False

        'Usuń komórki
        If kierunek = KierunekEdycjiPulpitu.Lewo Or kierunek = KierunekEdycjiPulpitu.Gora Then
            For x As Integer = przesx To _Szerokosc - 1
                For y As Integer = przesy To _Wysokosc - 1
                    _Kostki(x - przesx, y - przesy) = _Kostki(x, y)
                    _Kostki(x, y) = Nothing
                Next
            Next
        End If

        _Szerokosc -= rozmiar
        _Wysokosc -= rozmiar
        ReDim Preserve _Kostki(_Szerokosc - 1, _Wysokosc - 1)

        Return True
    End Function

End Class

Public Enum KierunekEdycjiPulpitu
    Gora
    Prawo
    Dol
    Lewo
End Enum