Attribute VB_Name = "rho_error"
Option Explicit

Const GPIB0 = 0

Public Sub GpibErr(msg As String)
  msg = msg + AddIbsta() + AddIberr() + AddIbcnt() + Chr(13) + Chr(13) + "GPIB Error!"
  MsgBox msg, vbOKOnly + vbExclamation, "Error"
  
  '  Take the board offline.
  ilonl GPIB0, 0
  End
End Sub

Public Function AddIbcnt() As String
  AddIbcnt = Chr$(13) + Chr$(10) + "ibcnt = 0x" + Hex$(ibcnt)
End Function

Public Function AddIberr() As String
    If (ibsta And EERR) Then
        If (iberr = EDVR) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = EDVR <DOS Error>"
        If (iberr = ECIC) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = ECIC <Not CIC>"
        If (iberr = ENOL) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = ENOL <No Listener>"
        If (iberr = EADR) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = EADR <Address Error>"
        If (iberr = EARG) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = EARG <Invalid argument>"
        If (iberr = ESAC) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = ESAC <Not Sys Ctrlr>"
        If (iberr = EABO) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = EABO <Op. aborted>"
        If (iberr = ENEB) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = ENEB <No GPIB board>"
        If (iberr = EOIP) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = EOIP <Async I/O in prg>"
        If (iberr = ECAP) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = ECAP <No capability>"
        If (iberr = EFSO) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = EFSO <File sys. error>"
        If (iberr = EBUS) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = EBUS <Command error>"
        If (iberr = ESTB) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = ESTB <Status byte lost>"
        If (iberr = ESRQ) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = ESRQ <SRQ stuck high>"
        If (iberr = ETAB) Then AddIberr = Chr$(13) + Chr$(10) + "iberr = ETAB <Table overflow>"
    Else
        AddIberr = Chr$(13) + Chr$(10) + "No error, iberr = " + Str$(iberr)
    End If
End Function

Public Function AddIbsta() As String
   Dim sta As String ' by me
    sta$ = Chr$(13) + Chr$(10) + "ibsta = &H" + Hex$(ibsta) + " <"
    If (ibsta And EERR) Then sta$ = sta$ + " ERR"
    If (ibsta And TIMO) Then sta$ = sta$ + " TIMO"
    If (ibsta And EEND) Then sta$ = sta$ + " END"
    If (ibsta And SRQI) Then sta$ = sta$ + " SRQI"
    If (ibsta And RQS) Then sta$ = sta$ + " RQS"
    If (ibsta And CMPL) Then sta$ = sta$ + " CMPL"
    If (ibsta And LOK) Then sta$ = sta$ + " LOK"
    If (ibsta And RREM) Then sta$ = sta$ + " REM"
    If (ibsta And CIC) Then sta$ = sta$ + " CIC"
    If (ibsta And AATN) Then sta$ = sta$ + " ATN"
    If (ibsta And TACS) Then sta$ = sta$ + " TACS"
    If (ibsta And LACS) Then sta$ = sta$ + " LACS"
    If (ibsta And DTAS) Then sta$ = sta$ + " DTAS"
    If (ibsta And DCAS) Then sta$ = sta$ + " DCAS"
    sta$ = sta$ + ">"
    AddIbsta = sta$
End Function
