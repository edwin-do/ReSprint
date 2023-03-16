VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "comdlg32.ocx"
Begin VB.Form frmMethod 
   Caption         =   "Method"
   ClientHeight    =   4800
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6000
   LinkTopic       =   "Form2"
   ScaleHeight     =   4800
   ScaleWidth      =   6000
   StartUpPosition =   3  'Windows Default
   Visible         =   0   'False
   Begin VB.Frame Frame1 
      Caption         =   "Schedule"
      Height          =   2895
      Left            =   120
      TabIndex        =   7
      Top             =   1320
      Width           =   5775
      Begin VB.VScrollBar VScroll1 
         Height          =   375
         Left            =   1800
         TabIndex        =   13
         Top             =   2400
         Width           =   255
      End
      Begin VB.TextBox txtTotalPoints 
         Height          =   375
         Left            =   1080
         TabIndex        =   11
         Text            =   "10"
         Top             =   2400
         Width           =   735
      End
      Begin VB.CommandButton Command2 
         Caption         =   "Delete"
         Height          =   375
         Left            =   4920
         TabIndex        =   10
         Top             =   840
         Width           =   735
      End
      Begin VB.CommandButton Command1 
         Caption         =   "Add"
         Height          =   375
         Left            =   4920
         TabIndex        =   9
         Top             =   360
         Width           =   735
      End
      Begin VB.ListBox List1 
         Height          =   1815
         Left            =   120
         TabIndex        =   8
         Top             =   360
         Width           =   4695
      End
      Begin VB.Label Label3 
         Caption         =   "pt/s"
         Height          =   255
         Left            =   2160
         TabIndex        =   14
         Top             =   2520
         Width           =   375
      End
      Begin VB.Label Label2 
         Caption         =   "Data Points :"
         Height          =   255
         Left            =   120
         TabIndex        =   12
         Top             =   2520
         Width           =   975
      End
   End
   Begin VB.CommandButton cmdCancel 
      Caption         =   "Cancel"
      Height          =   375
      Left            =   4920
      TabIndex        =   6
      Top             =   4320
      Width           =   975
   End
   Begin VB.CommandButton cmdApply 
      Caption         =   "Apply"
      Height          =   375
      Left            =   3840
      TabIndex        =   5
      Top             =   4320
      Width           =   975
   End
   Begin VB.CommandButton cmdBrowse 
      Caption         =   "Browse"
      Height          =   375
      Left            =   4920
      TabIndex        =   3
      Top             =   600
      Width           =   855
   End
   Begin VB.TextBox txtFile 
      Height          =   375
      Left            =   1080
      TabIndex        =   2
      Text            =   "File Path & File Name"
      Top             =   600
      Width           =   3615
   End
   Begin VB.TextBox txtFileName 
      Height          =   375
      Left            =   1080
      TabIndex        =   1
      Text            =   "File Name"
      Top             =   120
      Width           =   3615
   End
   Begin MSComDlg.CommonDialog dlgFile 
      Left            =   5040
      Top             =   0
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
      DialogTitle     =   "SAVE FILE - Powered by Daisman"
   End
   Begin VB.Label Label1 
      Caption         =   "File Path :"
      Height          =   255
      Left            =   120
      TabIndex        =   4
      Top             =   720
      Width           =   855
   End
   Begin VB.Label lblFileName 
      Caption         =   "File Name :"
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Width           =   855
   End
End
Attribute VB_Name = "frmMethod"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const APP_NAME As String = "open_file_dialog"
Private m_FilterSelected As Integer

Private Sub cmdApply_Click()
   Call cmdCancel_Click
End Sub

Private Sub cmdBrowse_Click()
    dlgFile.Flags = cdlOFNOverwritePrompt
    dlgFile.CancelError = True
    dlgFile.Filter = "Text Files (*.txt)|*.txt|DAT Files (*.dat)|*.dat"
    dlgFile.FilterIndex = m_FilterSelected
    dlgFile.filename = txtFile.Text

    On Error Resume Next
    dlgFile.ShowSave

    If Err.Number = cdlCancel Then Exit Sub
    If Err.Number <> 0 Then
        MsgBox "Error " & Err.Number & " selecting database." & _
            vbCrLf & Err.Description
        Exit Sub
    End If
    On Error GoTo 0
    'mFileSysObj.CreateTextFile (dlgFile.filename) 'INI trik aku buat save, krn ga tau kok ga otomatis saved pas klik save
    
    txtFile.Text = dlgFile.filename
    txtFileName.Text = dlgFile.FileTitle
    m_FilterSelected = dlgFile.FilterIndex
End Sub


Private Sub cmdCancel_Click()
   Unload Me
   frmMainForm.Enabled = True
End Sub

Private Sub Form_Load()
    'load previous path
    txtFile.Text = GetSetting(APP_NAME, "Files", "File", "")
    m_FilterSelected = GetSetting(APP_NAME, "Files", "Filter", "0")
End Sub

Private Sub Form_Unload(Cancel As Integer)
' save path to load when program start
    SaveSetting APP_NAME, "Files", "File", txtFile.Text
    SaveSetting APP_NAME, "Files", "Filter", m_FilterSelected
End Sub

Private Sub Form_Terminate()
   Call cmdCancel_Click
End Sub
