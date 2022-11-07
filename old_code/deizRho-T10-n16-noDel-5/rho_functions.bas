Attribute VB_Name = "rho_functions"
' rho_functions module
Option Explicit

Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

' Windows set top declaration
Public Const SWP_NOMOVE = 2
Public Const SWP_NOSIZE = 1
Public Const FLAGS = SWP_NOMOVE Or SWP_NOSIZE
Public Const HWND_TOPMOST = -1
Public Const HWND_NOTOPMOST = -2

Public Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Long, _
  ByVal hWndInsertAfter As Long, ByVal X As Long, ByVal Y As Long, _
  ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
  
  ' *** High Performance timer for calculating execution time *****************
' See two related sub/functions below
Public Declare Function QueryPerformanceFrequency Lib "kernel32" _
            (lpFrequency As Currency) As Long
            
Public Declare Function QueryPerformanceCounter Lib "kernel32" _
            (lpPerformanceCount As Currency) As Long
            
' These 4 var for global use. For timing other event,
' you can make other 4 variables anywhere
Public timReturn As Long ' ID ?
Public timFreq As Currency
Public timStart As Currency
Public timEnd As Currency
' **

' *** Calculating execution time ********************************************************
' This sub is coupled with function below
' Call this sub before a block/sub for starting timing
' and then at the end of the block call execTime function
Public Sub startTiming(valReturn As Long, curFreq As Currency, _
  curStart As Currency)
  
  valReturn = QueryPerformanceFrequency(curFreq)
  If valReturn <> 0 Then
    valReturn = QueryPerformanceCounter(curStart)
  Else
    MsgBox "Hardware does not support a high-resolution timer"
  End If
End Sub

' output in msec
Public Function execTime(valReturn As Long, curFreq As Currency, _
  curStart As Currency, curEnd As Currency) As Double
  
  If valReturn <> 0 Then
    valReturn = QueryPerformanceCounter(curEnd)
    execTime = (curEnd - curStart) * 1000 / curFreq
  Else
    MsgBox "Hardware does not support a high-resolution timer"
  End If
End Function
' ***************************************************************************************


' Forms set top function
Public Function SetTopMostWindow(hwnd As Long, Topmost As Boolean) As Long
  If Topmost = True Then 'Make the window top most
    SetTopMostWindow = SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, _
        0, FLAGS)
  Else
    SetTopMostWindow = SetWindowPos(hwnd, HWND_NOTOPMOST, 0, 0, _
        0, 0, FLAGS)
    SetTopMostWindow = False
  End If
End Function

Public Function Max(ByRef myArray() As Double) As Double
  On Error GoTo myErrorHandler

  Dim i As Integer
  Dim maxValue As Double
  maxValue = myArray(LBound(myArray))
  For i = LBound(myArray) To UBound(myArray)
    If maxValue < myArray(i) Then
      maxValue = myArray(i)
    End If
  Next i
  
  Max = maxValue
  
  Exit Function
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Function. : " & ".Max)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call frmMainForm.save_settings
  Call frmMainForm.save_status
End Function

Public Function hhmmss(ByVal s As Long) As String ' ByVal OK, ByRef disaster
  On Error GoTo myErrorHandler
  
  Dim h As Long ' If as integer error when h >= 10
  Dim m As Long
  
  h = Int(s / 3600)
  s = s - (h * 3600)
  m = Int(s / 60)
  s = s - (m * 60)
  
  Dim hh As String
  Dim mm As String
  Dim ss As String
  
  hh = CStr(h)
  mm = CStr(m)
  ss = CStr(s)
  If h < 10 Then
    hh = "0" & hh
  End If
  If m < 10 Then
    mm = "0" & mm
  End If
  If s < 10 Then
    ss = "0" & ss
  End If
  hhmmss = hh & ":" & mm & ":" & ss
  
  Exit Function
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Function. : " & ".hhmmss)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call frmMainForm.save_settings
  Call frmMainForm.save_status
End Function

Public Sub write_status(message As String)
  'On Error GoTo myErrorHandler
  frmMainForm.lstStatus.AddItem (message & " [" & Now & "]")
  If frmMainForm.experiment_is_running = True Then
    frmMainForm.lstStatus.AddItem ("Experiment is running ...")
  End If
  frmMainForm.lstStatus.Selected(frmMainForm.lstStatus.ListCount - 1) = True ' highlight
  frmMainForm.lstStatus.Selected(frmMainForm.lstStatus.ListCount - 1) = False

  frmMainForm.txtStatus.text = " " & message & " [" & Now & "]"

  'Exit Sub
'myErrorHandler:
  'Call turn_off_all_timers
  'write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".write_status)"
  'Call save_info
  'Call save_status
End Sub

Public Sub turn_off_all_timers()
  frmMainForm.tmrReadDevicesOnlyToDisplay.Enabled = False
  frmMainForm.tmrReadDevices = False
  frmMainForm.tmrDrawChart.Enabled = False
  frmMainForm.tmrShiftData = False
End Sub

