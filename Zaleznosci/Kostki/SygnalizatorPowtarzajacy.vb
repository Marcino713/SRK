Imports Zaleznosci.PlikiPulpitu

Public Class SygnalizatorPowtarzajacy
    Inherits Tor
    Implements IAdres

    Private Const BLAD As String = "Dla kostki sygnalizatora powtarzającego nie można zdefiniować nazwy."
    Private Const NAZWA_SP As String = "Sp"

    Public Property Adres As UShort = 0 Implements IAdres.Adres
    Public Property Kolejnosc As KolejnoscSygnalizatoraPowtarzajacego
    Public Property SygnalizatorPowtarzany As SygnalizatorPolsamoczynny
    Public Overloads Property Nazwa As String
        Get
            Dim n As String = ""

            Select Case Kolejnosc
                Case KolejnoscSygnalizatoraPowtarzajacego.Pierwszy
                    n = "I"
                Case KolejnoscSygnalizatoraPowtarzajacego.Drugi
                    n = "II"
                Case KolejnoscSygnalizatoraPowtarzajacego.Trzeci
                    n = "III"
            End Select

            n &= NAZWA_SP
            If SygnalizatorPowtarzany IsNot Nothing Then n &= SygnalizatorPowtarzany.Nazwa
            Return n
        End Get
        Set(value As String)
            Throw New NotSupportedException(BLAD)
        End Set
    End Property


    Public Property Stan As StanSygnalizatoraPowtarzajacego = StanSygnalizatoraPowtarzajacego.BrakWyjazdu

    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorPowtarzajacy)
    End Sub

    Protected Friend Overrides Sub UsunPowiazanie(kostka As Kostka)
        If SygnalizatorPowtarzany Is kostka Then SygnalizatorPowtarzany = Nothing
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(Adres)
        bw.Write(CByte(Kolejnosc))
        bw.Write(If(SygnalizatorPowtarzany Is Nothing, PUSTE_ODWOLANIE, konf.Kostki(SygnalizatorPowtarzany)))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytu)
        MyBase.OtworzKostke(br, konf)
        Adres = br.ReadUInt16
        Kolejnosc = CType(br.ReadByte, KolejnoscSygnalizatoraPowtarzajacego)
        Dim id As Integer = br.ReadInt32
        SygnalizatorPowtarzany = CType(konf.Kostki(id), SygnalizatorPolsamoczynny)
    End Sub
End Class

Public Enum KolejnoscSygnalizatoraPowtarzajacego
    Pierwszy
    Drugi
    Trzeci
End Enum

Public Enum StanSygnalizatoraPowtarzajacego
    BrakWyjazdu = 1
    Zezwalajacy = 2
End Enum