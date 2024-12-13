Public Class SygnalizatorPowtarzajacy
    Inherits SygnalizatorInformujacy

    Private Const NAZWA_SP As String = "Sp"

    Public Property Kolejnosc As KolejnoscSygnalizatoraPowtarzajacego
    Public Overrides Property Nazwa As String
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

            Return $"{n}{NAZWA_SP}{SygnalizatorPowtarzany?.Nazwa}"
        End Get
        Set(value As String)
        End Set
    End Property

    Public Sub New()
        MyBase.New(TypKostki.SygnalizatorPowtarzajacy)
    End Sub

    Friend Overrides Sub ZapiszKostke(bw As BinaryWriter, konf As KonfiguracjaZapisuPulpitu)
        MyBase.ZapiszKostke(bw, konf)
        bw.Write(CByte(Kolejnosc))
    End Sub

    Friend Overrides Sub OtworzKostke(br As BinaryReader, konf As KonfiguracjaOdczytuPulpitu)
        MyBase.OtworzKostke(br, konf)
        Kolejnosc = CType(br.ReadByte, KolejnoscSygnalizatoraPowtarzajacego)
    End Sub
End Class

Public Enum KolejnoscSygnalizatoraPowtarzajacego
    Pierwszy
    Drugi
    Trzeci
End Enum