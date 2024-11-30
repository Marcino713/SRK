Public MustInherit Class KomunikacjaZUrzadzeniami
    Public Event OdebranoUstawionoStanSygnalizatora(kom As UstawionoStanSygnalizatoraUrz)
    Public Event OdebranoUstawionoStanSygnalizatoraDrogowego(kom As UstawionoStanSygnalizatoraDrogowegoUrz)
    Public Event OdebranoUstawionoJasnoscLampy(kom As UstawionoJasnoscLampyUrz)
    Public Event OdebranoWykrytoOs(kom As WykrytoOsUrz)
    Public Event OdebranoZmienionoStanZwrotnicy(kom As ZmienionoStanZwrotnicyUrz)
    Public Event OdebranoZmienionoStanRogatki(kom As ZmienionoStanRogatkiUrz)

    Public MustOverride Sub UstawStanSygnalizatoraSamoczynnego(kom As UstawStanSygnalizatoraSamoczynnegoUrz)
    Public MustOverride Sub UstawStanSygnalizatoraManewrowego(kom As UstawStanSygnalizatoraManewrowegoUrz)
    Public MustOverride Sub UstawStanSygnalizatoraPowtarzajacego(kom As UstawStanSygnalizatoraPowtarzajacegoUrz)
    Public MustOverride Sub UstawStanSygnalizatoraPolsamoczynnego(kom As UstawStanSygnalizatoraPolsamoczynnegoUrz)
    Public MustOverride Sub UstawStanSygnalizatoraPrzejazdowego(kom As UstawStanSygnalizatoraPrzejazdowegoUrz)
    Public MustOverride Sub UstawStanSygnalizatoraDrogowego(kom As UstawStanSygnalizatoraDrogowegoUrz)
    Public MustOverride Sub UstawJasnoscLampy(kom As UstawJasnoscLampyUrz)
    Public MustOverride Sub UstawZwrotnice(kom As UstawZwrotniceUrz)
    Public MustOverride Sub UstawZwrotniceSerwisowo(kom As UstawZwrotniceSerwisowoUrz)
    Public MustOverride Sub ZamknijRogatke(kom As ZamknijRogatkeUrz)
    Public MustOverride Sub OtworzRogatke(kom As OtworzRogatkeUrz)

    Protected Sub OdebrUstawionoStanSygnalizatora(kom As UstawionoStanSygnalizatoraUrz)
        RaiseEvent OdebranoUstawionoStanSygnalizatora(kom)
    End Sub

    Protected Sub OdebrUstawionoStanSygnalizatoraDrogowego(kom As UstawionoStanSygnalizatoraDrogowegoUrz)
        RaiseEvent OdebranoUstawionoStanSygnalizatoraDrogowego(kom)
    End Sub

    Protected Sub OdebrUstawionoJasnoscLampy(kom As UstawionoJasnoscLampyUrz)
        RaiseEvent OdebranoUstawionoJasnoscLampy(kom)
    End Sub

    Protected Sub OdebrWykrytoOs(kom As WykrytoOsUrz)
        RaiseEvent OdebranoWykrytoOs(kom)
    End Sub

    Protected Sub OdebrZmienionoStanZwrotnicy(kom As ZmienionoStanZwrotnicyUrz)
        RaiseEvent OdebranoZmienionoStanZwrotnicy(kom)
    End Sub

    Protected Sub OdebrZmienionoStanRogatki(kom As ZmienionoStanRogatkiUrz)
        RaiseEvent OdebranoZmienionoStanRogatki(kom)
    End Sub
End Class