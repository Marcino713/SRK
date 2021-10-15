Public Class Pulpit
    Public Const NAGLOWEK As String = "STAC"
    Public Shared ReadOnly ObslugiwaneWersje As New Dictionary(Of Integer, Integer) From {{0, 1}}

    Private _WersjaGlowna As UShort
    Public ReadOnly Property WersjaGlowna As UShort
        Get
            Return _WersjaGlowna
        End Get
    End Property

    Private _WersjaBoczna As UShort
    Public ReadOnly Property WersjaBoczna As UShort
        Get
            Return _WersjaBoczna
        End Get
    End Property

    Public Property Nazwa As String = ""
    Public Property Adres As Integer
    Public ReadOnly Property DataUtworzenia As Date

    Private _Szerokosc As Integer
    Public ReadOnly Property Szerokosc As Integer
        Get
            Return _Szerokosc
        End Get
    End Property

    Private _Wysokosc As Integer
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

    Private _Lampy As New List(Of Lampa)
    Public ReadOnly Property Lampy As List(Of Lampa)
        Get
            Return _Lampy
        End Get
    End Property

    Private _Odcinki As New List(Of OdcinekToru)
    Public ReadOnly Property OdcinkiTorow As List(Of OdcinekToru)
        Get
            Return _Odcinki
        End Get
    End Property

    Private _LicznikiOsi As New List(Of ParaLicznikowOsi)
    Public ReadOnly Property LicznikiOsi As List(Of ParaLicznikowOsi)
        Get
            Return _LicznikiOsi
        End Get
    End Property

    Public Sub New()
        Me.New(10, 10)
    End Sub

    Public Sub New(szer As Integer, wys As Integer)
        _Szerokosc = szer
        _Wysokosc = wys
        ReDim _Kostki(_Szerokosc - 1, _Wysokosc - 1)
        DataUtworzenia = Now
    End Sub

    Public Sub Zapisz(Strumien As BinaryWriter)

    End Sub

    Public Sub Otworz(Strumien As BinaryReader)

    End Sub

    Public Sub UsunKostkeZPowiazan(kostka As Kostka)
        For x As Integer = 0 To _Szerokosc - 1
            For y As Integer = 0 To _Wysokosc - 1
                If _Kostki(x, y) IsNot Nothing Then _Kostki(x, y).UsunPowiazanie(kostka)
            Next
        Next
    End Sub

    Public Sub UsunOdcinekToruZPowiazan(odcinek As OdcinekToru)
        For x As Integer = 0 To _Szerokosc - 1
            For y As Integer = 0 To _Wysokosc - 1
                If TypeOf _Kostki(x, y) Is ITor Then
                    Dim t As ITor = DirectCast(_Kostki(x, y), ITor)
                    If t.NalezyDoOdcinka Is odcinek Then
                        t.NalezyDoOdcinka = Nothing
                    End If
                ElseIf TypeOf (_Kostki(x, y)) Is Sygnalizator
                    Dim s As Sygnalizator = DirectCast(_Kostki(x, y), Sygnalizator)
                    If s.OdcinekNastepujacy Is odcinek Then
                        s.OdcinekNastepujacy = Nothing
                    End If
                End If
            Next
        Next

        Dim en As List(Of ParaLicznikowOsi).Enumerator = _LicznikiOsi.GetEnumerator
        While en.MoveNext
            If en.Current.Odcinek1 Is odcinek Then en.Current.Odcinek1 = Nothing
            If en.Current.Odcinek2 Is odcinek Then en.Current.Odcinek2 = Nothing
        End While
    End Sub

    Public Sub PowiekszPulpit(kierunek As KierunekEdycjiPulpitu, rozmiar As Integer)
        If rozmiar < 0 Then
            Throw New ArgumentException("Rozmiar nie może być ujemny.")
        End If

        Dim staraszer As Integer = _Szerokosc
        Dim starawys As Integer = _Wysokosc
        Dim przesx As Integer = 0
        Dim przesy As Integer = 0

        If kierunek = KierunekEdycjiPulpitu.Lewo Or kierunek = KierunekEdycjiPulpitu.Prawo Then
            _Szerokosc += rozmiar
            If kierunek = KierunekEdycjiPulpitu.Lewo Then przesx = rozmiar
        Else
            _Wysokosc += rozmiar
            If kierunek = KierunekEdycjiPulpitu.Gora Then przesy = rozmiar
        End If

        Dim tab(_Szerokosc - 1, _Wysokosc - 1) As Kostka
        For x As Integer = 0 To staraszer - 1
            For y As Integer = 0 To starawys - 1
                tab(x + przesx, y + przesy) = _Kostki(x, y)
            Next
        Next

        _Kostki = tab
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
                _Wysokosc -= rozmiar
                koncy = rozmiar - 1
                przesy = rozmiar
            Case KierunekEdycjiPulpitu.Prawo
                _Szerokosc -= rozmiar
                poczx = _Szerokosc
            Case KierunekEdycjiPulpitu.Dol
                _Wysokosc -= rozmiar
                poczy = _Wysokosc
            Case KierunekEdycjiPulpitu.Lewo
                _Szerokosc -= rozmiar
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
        Dim tab(_Szerokosc - 1, _Wysokosc - 1) As Kostka

        For x As Integer = 0 To _Szerokosc - 1
            For y As Integer = 0 To _Wysokosc - 1
                tab(x, y) = _Kostki(x + przesx, y + przesy)
            Next
        Next

        _Kostki = tab
        Return True
    End Function

End Class

Public Enum KierunekEdycjiPulpitu
    Gora
    Prawo
    Dol
    Lewo
End Enum