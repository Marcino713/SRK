Friend MustInherit Class Sygnalizator
    Protected Const KAT_PROSTY As Single = 90.0F
    Protected Const KAT_POLPELNY As Single = 2.0F * KAT_PROSTY
    Protected Const PROMIEN As Single = 7.0F
    Protected Const SREDNICA As Single = 2.0F * PROMIEN
    Protected Const MODUL As Single = 3.0F * PROMIEN
    Protected Const MODUL_2 As Single = 2.0F * MODUL
    Protected Const SLUP_SZER As Single = 10.0F
    Protected Const MARGINES_Y As Single = 7.0F

    Private Const MASKA_SWIATLA As UShort = 3
    Private Const BITY_SWIATLA As UShort = 2

    Private Shared ReadOnly SWIATLO_WYLACZONE As Brush = Brushes.DarkSlateGray
    Private Shared ReadOnly KOLORY_SWIATEL As New Dictionary(Of SwiatloSygnalizatora, Brush) From {
        {SwiatloSygnalizatora.Zielone, Brushes.Lime},
        {SwiatloSygnalizatora.Pomaranczowe, Brushes.Orange},
        {SwiatloSygnalizatora.Czerwone, Brushes.Red},
        {SwiatloSygnalizatora.Biale, Brushes.White},
        {SwiatloSygnalizatora.Niebieskie, Brushes.Blue}
    }

    Friend Property Kostka As Zaleznosci.Sygnalizator
    Friend Property Kontrolka As PokazywaczSygnalizatora
    Friend MustOverride ReadOnly Property Szerokosc As Single

    Private Swiatla As List(Of DaneSwiatla)
    Private CzyMigajacy As Boolean
    Private WithEvents Migacz As Migacz

    Friend Sub New(migacz As Migacz, swiatla As List(Of SwiatloSygnalizatora))
        Me.Swiatla = New List(Of DaneSwiatla)(swiatla.Count)
        Me.Migacz = migacz

        For i As Integer = 0 To swiatla.Count - 1
            Me.Swiatla.Add(New DaneSwiatla() With {.Kolor = swiatla(i)})
        Next
    End Sub

    Friend MustOverride Sub Rysuj(gr As Graphics)

    Friend Sub UstawSwiatla(stan As UShort)
        Dim stanSwiatla As Zaleznosci.StanSwiatlaSygnalizatora
        Dim pozycja As UShort = 0
        CzyMigajacy = False

        For i As Integer = 0 To Swiatla.Count - 1
            stanSwiatla = CType((stan >> pozycja) And MASKA_SWIATLA, Zaleznosci.StanSwiatlaSygnalizatora)
            Swiatla(i).Stan = stanSwiatla
            If stanSwiatla = Zaleznosci.StanSwiatlaSygnalizatora.Migajace Then CzyMigajacy = True
            pozycja += BITY_SWIATLA
        Next

        Kontrolka.Invalidate()
    End Sub

    Friend Sub Usun()
        Migacz = Nothing
    End Sub

    Protected Function PobierzPedzel(ix As Integer) As Brush
        Dim sw As DaneSwiatla = Swiatla(ix)

        If sw.Stan = Zaleznosci.StanSwiatlaSygnalizatora.Wlaczone Or (sw.Stan = Zaleznosci.StanSwiatlaSygnalizatora.Migajace And Migacz.Stan) Then
            Return KOLORY_SWIATEL(sw.Kolor)
        Else
            Return SWIATLO_WYLACZONE
        End If
    End Function

    Private Sub Migacz_ZmienionoStan() Handles Migacz.ZmienionoStan
        If CzyMigajacy Then Kontrolka.Invalidate()
    End Sub

    Private Class DaneSwiatla
        Friend Kolor As SwiatloSygnalizatora
        Friend Stan As Zaleznosci.StanSwiatlaSygnalizatora
    End Class
End Class

Friend Enum SwiatloSygnalizatora
    Zielone
    Pomaranczowe
    Czerwone
    Biale
    Niebieskie
End Enum