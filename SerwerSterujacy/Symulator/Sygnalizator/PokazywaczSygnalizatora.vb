Imports System.ComponentModel

Friend Class PokazywaczSygnalizatora
    Private ReadOnly KOLOR_ZAZNACZENIE As Color = Pulpit.KolorRGB("#009DFF")

    Private _sygnalizator As Sygnalizator
    <Browsable(False)>
    Public Property Sygnalizator As Sygnalizator
        Get
            Return _sygnalizator
        End Get
        Set(value As Sygnalizator)
            value.Kontrolka = Me
            _sygnalizator = value
            Invalidate()
        End Set
    End Property

    Private _zaznaczony As Boolean
    <Browsable(False)>
    Public Property Zaznaczony As Boolean
        Get
            Return _zaznaczony
        End Get
        Set(value As Boolean)
            If value <> _zaznaczony Then
                _zaznaczony = value
                Invalidate()
                RaiseEvent ZmianaZaznaczenia(Me, value)
            End If
        End Set
    End Property

    Public Event ZmianaZaznaczenia(kontrolka As PokazywaczSygnalizatora, zaznaczony As Boolean)

    Private aktywna As Boolean

    Public Sub New()
        InitializeComponent()
        DoubleBuffered = True
    End Sub

    Public Overloads Sub Invalidate()
        If aktywna Then MyBase.Invalidate()
    End Sub

    Private Sub PokazywaczSygnalizatora_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(If(Zaznaczony, KOLOR_ZAZNACZENIE, BackColor))

        If _sygnalizator IsNot Nothing Then
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            e.Graphics.TranslateTransform((Width - _sygnalizator.Szerokosc) / 2.0F, 3.0F)
            _sygnalizator.Rysuj(e.Graphics)
        End If
    End Sub

    Private Sub PokazywaczSygnalizatora_Click() Handles Me.Click
        Zaznaczony = Not _zaznaczony
    End Sub

    Private Sub PokazywaczSygnalizatora_HandleCreated() Handles Me.HandleCreated
        aktywna = True
    End Sub

    Private Sub PokazywaczSygnalizatora_HandleDestroyed() Handles Me.HandleDestroyed
        aktywna = False
    End Sub
End Class