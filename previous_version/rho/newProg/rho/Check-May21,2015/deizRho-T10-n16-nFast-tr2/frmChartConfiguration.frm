VERSION 5.00
Begin VB.Form frmSettings 
   BackColor       =   &H00E0E0E0&
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "   Settings"
   ClientHeight    =   3135
   ClientLeft      =   2985
   ClientTop       =   2745
   ClientWidth     =   7695
   Icon            =   "frmChartConfiguration.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3135
   ScaleWidth      =   7695
   ShowInTaskbar   =   0   'False
   Begin VB.CommandButton cmdApply 
      BackColor       =   &H00E0E0E0&
      Caption         =   "Apply"
      Height          =   375
      Left            =   6360
      Style           =   1  'Graphical
      TabIndex        =   34
      Top             =   2640
      Width           =   1095
   End
   Begin VB.Frame Frame2 
      BackColor       =   &H00E0E0E0&
      Caption         =   "Chart"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2295
      Left            =   120
      TabIndex        =   15
      Top             =   120
      Width           =   3975
      Begin VB.CheckBox chkCustomScale 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Custom"
         Enabled         =   0   'False
         Height          =   255
         Left            =   240
         TabIndex        =   35
         Top             =   360
         Width           =   975
      End
      Begin VB.VScrollBar VScroll4 
         Height          =   375
         Left            =   1080
         TabIndex        =   33
         Top             =   1710
         Width           =   255
      End
      Begin VB.VScrollBar VScroll3 
         Height          =   375
         Left            =   2400
         TabIndex        =   32
         Top             =   1680
         Width           =   255
      End
      Begin VB.VScrollBar VScroll2 
         Height          =   375
         Left            =   1080
         TabIndex        =   31
         Top             =   990
         Width           =   255
      End
      Begin VB.VScrollBar VScroll1 
         Height          =   375
         Left            =   2400
         TabIndex        =   30
         Top             =   960
         Width           =   255
      End
      Begin VB.VScrollBar VScroll6 
         Height          =   375
         Left            =   3600
         TabIndex        =   29
         Top             =   990
         Width           =   255
      End
      Begin VB.VScrollBar VScroll7 
         Height          =   375
         Left            =   3600
         TabIndex        =   28
         Top             =   1710
         Width           =   255
      End
      Begin VB.TextBox txtYMinScale 
         Enabled         =   0   'False
         Height          =   375
         Left            =   120
         TabIndex        =   21
         Text            =   "1"
         Top             =   1680
         Width           =   975
      End
      Begin VB.TextBox txtYMaxScale 
         Enabled         =   0   'False
         Height          =   375
         Left            =   1440
         TabIndex        =   20
         Text            =   "50"
         Top             =   1680
         Width           =   975
      End
      Begin VB.TextBox txtXMaxScale 
         Enabled         =   0   'False
         Height          =   375
         Left            =   1440
         TabIndex        =   19
         Text            =   "50"
         Top             =   960
         Width           =   975
      End
      Begin VB.TextBox txtXMinScale 
         Enabled         =   0   'False
         Height          =   375
         Left            =   120
         TabIndex        =   18
         Text            =   "1"
         Top             =   960
         Width           =   975
      End
      Begin VB.TextBox txtYGrid 
         Enabled         =   0   'False
         Height          =   375
         Left            =   2880
         TabIndex        =   17
         Text            =   "5"
         Top             =   1710
         Width           =   735
      End
      Begin VB.TextBox txtXGrid 
         Enabled         =   0   'False
         Height          =   375
         Left            =   2880
         TabIndex        =   16
         Text            =   "5"
         Top             =   990
         Width           =   735
      End
      Begin VB.Label Label7 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Y Top Scale"
         Height          =   375
         Left            =   1440
         TabIndex        =   27
         Top             =   1440
         Width           =   975
      End
      Begin VB.Label Label6 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Y Bottom Scale"
         Height          =   375
         Left            =   120
         TabIndex        =   26
         Top             =   1440
         Width           =   1215
      End
      Begin VB.Label Label5 
         BackColor       =   &H00E0E0E0&
         Caption         =   "X Right Scale"
         Height          =   375
         Left            =   1440
         TabIndex        =   25
         Top             =   720
         Width           =   975
      End
      Begin VB.Label Label4 
         BackColor       =   &H00E0E0E0&
         Caption         =   "X Left Scale"
         Height          =   375
         Left            =   120
         TabIndex        =   24
         Top             =   720
         Width           =   1215
      End
      Begin VB.Label Label8 
         BackColor       =   &H00E0E0E0&
         Caption         =   "X grid :"
         Height          =   255
         Left            =   2880
         TabIndex        =   23
         Top             =   750
         Width           =   615
      End
      Begin VB.Label Label9 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Y grid :"
         Height          =   255
         Left            =   2880
         TabIndex        =   22
         Top             =   1470
         Width           =   615
      End
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00E0E0E0&
      Caption         =   "Reading Display"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2295
      Left            =   4200
      TabIndex        =   0
      Top             =   120
      Width           =   3375
      Begin VB.ComboBox cboExpRho 
         Height          =   315
         ItemData        =   "frmChartConfiguration.frx":0B3A
         Left            =   2400
         List            =   "frmChartConfiguration.frx":0B6E
         TabIndex        =   14
         Text            =   "1E-07"
         Top             =   1800
         Width           =   855
      End
      Begin VB.ComboBox cboExpR 
         Height          =   315
         ItemData        =   "frmChartConfiguration.frx":0BE2
         Left            =   2400
         List            =   "frmChartConfiguration.frx":0C16
         TabIndex        =   13
         Text            =   "1E-03"
         Top             =   1440
         Width           =   855
      End
      Begin VB.ComboBox cboExpC 
         Height          =   315
         ItemData        =   "frmChartConfiguration.frx":0C8A
         Left            =   2400
         List            =   "frmChartConfiguration.frx":0CBE
         TabIndex        =   12
         Text            =   "1E-03"
         Top             =   1080
         Width           =   855
      End
      Begin VB.TextBox txtRoundRho 
         Height          =   285
         Left            =   1200
         TabIndex        =   11
         Text            =   "2"
         Top             =   1800
         Width           =   855
      End
      Begin VB.TextBox txtRoundR 
         Height          =   285
         Left            =   1200
         TabIndex        =   10
         Text            =   "2"
         Top             =   1440
         Width           =   855
      End
      Begin VB.TextBox txtRoundC 
         Height          =   285
         Left            =   1200
         TabIndex        =   9
         Text            =   "2"
         Top             =   1080
         Width           =   855
      End
      Begin VB.TextBox txtRoundV 
         Height          =   285
         Left            =   1200
         TabIndex        =   6
         Text            =   "2"
         Top             =   720
         Width           =   855
      End
      Begin VB.ComboBox cboExpV 
         Height          =   315
         ItemData        =   "frmChartConfiguration.frx":0D32
         Left            =   2400
         List            =   "frmChartConfiguration.frx":0D66
         TabIndex        =   5
         Text            =   "1E-05"
         Top             =   720
         Width           =   855
      End
      Begin VB.Label Label16 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Resistivity :"
         Height          =   255
         Left            =   240
         TabIndex        =   8
         Top             =   1800
         Width           =   975
      End
      Begin VB.Label Label15 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Resistance :"
         Height          =   255
         Left            =   240
         TabIndex        =   7
         Top             =   1440
         Width           =   975
      End
      Begin VB.Label Label14 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Exponent :"
         Height          =   255
         Left            =   2400
         TabIndex        =   4
         Top             =   480
         Width           =   855
      End
      Begin VB.Label Label13 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Round Digit :"
         Height          =   255
         Left            =   1200
         TabIndex        =   3
         Top             =   480
         Width           =   1095
      End
      Begin VB.Label Label12 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Current :"
         Height          =   255
         Left            =   240
         TabIndex        =   2
         Top             =   1080
         Width           =   735
      End
      Begin VB.Label Label11 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Voltage :"
         Height          =   255
         Left            =   240
         TabIndex        =   1
         Top             =   720
         Width           =   735
      End
   End
End
Attribute VB_Name = "frmSettings"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
' frmSettings

Option Explicit

Private Sub chkCustomScale_Click()
  If chkCustomScale.value = 1 Then
    txtXMinScale.Enabled = True
    txtXMaxScale.Enabled = True
    txtYMinScale.Enabled = True
    txtYMaxScale.Enabled = True
    txtXGrid.Enabled = True
    txtYGrid.Enabled = True
  Else
    txtXMinScale.Enabled = False
    txtXMaxScale.Enabled = False
    txtYMinScale.Enabled = False
    txtYMaxScale.Enabled = False
    txtXGrid.Enabled = False
    txtYGrid.Enabled = False
  End If
End Sub

Private Sub Form_Load()
  Dim ch As Long
  ch = SetTopMostWindow(frmSettings.hwnd, True)
End Sub

Private Sub Form_QueryUnLoad(cancel As Integer, UnLoadMode As Integer)
  cancel = 1
  Me.Hide
End Sub

Public Sub cmdApply_Click()
  On Error GoTo myErrorHandler
  
  If Not filter_is_OK Then Exit Sub

  expV = cboExpV.text
  expC = cboExpC.text
  expR = cboExpR.text
  expRho = cboExpRho.text
  roundV = txtRoundV.text
  roundC = txtRoundC.text
  roundR = txtRoundR.text
  roundRho = txtRoundRho.text
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".cmdApply_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call frmMainForm.save_settings
  Call frmMainForm.save_status
End Sub

Public Function filter_is_OK() As Boolean ' called by cmdApply and mnuConnectDevices
  On Error GoTo myErrorHandler
  
  Dim i As Integer
   
  If Not IsNumeric(txtRoundV.text) Or Val(txtRoundV.text) < 0 Or _
    Val(txtRoundV.text) > 10 Then
    MsgBox "Voltage round input must be between 0 - 10 !"
    filter_is_OK = False
    txtRoundV.text = "2"
    Exit Function
  End If
  
  If Not IsNumeric(txtRoundC.text) Or Val(txtRoundC.text) < 0 Or _
    Val(txtRoundC.text) > 10 Then
    MsgBox "Current round input must be between 0 - 10 !"
    filter_is_OK = False
    txtRoundC.text = "2"
    Exit Function
  End If
  
  If Not IsNumeric(txtRoundR.text) Or Val(txtRoundR.text) < 0 Or _
    Val(txtRoundR.text) > 10 Then
    MsgBox "Resistance round input must be between 0 - 10 !"
    filter_is_OK = False
    txtRoundR.text = "2"
    Exit Function
  End If
  
  If Not IsNumeric(txtRoundRho.text) Or Val(txtRoundRho.text) < 0 Or _
    Val(txtRoundRho.text) > 10 Then
    MsgBox "Resistivity round input must be between 0 - 10 !"
    filter_is_OK = False
    txtRoundRho.text = "2"
    Exit Function
  End If
  
  For i = 0 To cboExpV.ListCount - 1
    If cboExpV.text = cboExpV.List(i) Then ' OK
      Exit For
    Else
      filter_is_OK = False
      If i = cboExpV.ListCount - 1 Then
        MsgBox "Exponent for voltage is invalid"
        cboExpV.text = "1E-05"
        Exit Function
      End If
    End If
  Next i
  
  For i = 0 To cboExpC.ListCount - 1
    If cboExpC.text = cboExpC.List(i) Then ' OK
      Exit For
    Else
      filter_is_OK = False
      If i = cboExpC.ListCount - 1 Then
        MsgBox "Exponent for current is invalid"
        cboExpC.text = "1E-03"
        Exit Function
      End If
    End If
  Next i
  
  For i = 0 To cboExpR.ListCount - 1
    If cboExpR.text = cboExpR.List(i) Then ' OK
      Exit For
    Else
      filter_is_OK = False
      If i = cboExpR.ListCount - 1 Then
        MsgBox "Exponent for resistance is invalid"
        cboExpR.text = "1E-03"
        Exit Function
      End If
    End If
  Next i
  
  For i = 0 To cboExpRho.ListCount - 1
    If cboExpRho.text = cboExpRho.List(i) Then ' OK
      Exit For
    Else
      filter_is_OK = False
      If i = cboExpRho.ListCount - 1 Then
        MsgBox "Exponent for resistivity is invalid"
        cboExpRho.text = "1E-07"
        Exit Function
      End If
    End If
  Next i
  
  ' filter chart info
  If Not IsNumeric(txtXMinScale.text) Or Not IsNumeric(txtXMaxScale.text) Or _
    Not IsNumeric(txtYMinScale.text) Or Not IsNumeric(txtYMaxScale.text) Or _
    Not IsNumeric(txtXGrid.text) Or Not IsNumeric(txtYGrid.text) Then
    
    MsgBox "One or more chart info is non-numeric !"
    
    filter_is_OK = False
    
    Exit Function
  End If
  
  If Val(txtXMinScale.text) >= Val(txtXMaxScale.text) Then
    MsgBox "Minimum X scale greater than maximum X scale !"
    filter_is_OK = False
    
    Exit Function
  End If
  
  If Val(txtYMinScale.text) >= Val(txtYMaxScale.text) Then
    MsgBox "Minimum Y scale greater than maximum Y scale !"
    filter_is_OK = False
    
    Exit Function
  End If
  
  If Val(txtXGrid.text) < 0 Or Val(txtYGrid.text) < 0 Then
    MsgBox "Grid values must be positive !"
    filter_is_OK = False
    
    Exit Function
  End If
  
  filter_is_OK = True
  
  Exit Function
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".filter_is_OK)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call frmMainForm.save_settings
  Call frmMainForm.save_status
End Function
