VERSION 5.00
Begin VB.Form frmAbout 
   BackColor       =   &H00E0E0E0&
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "About MyApp"
   ClientHeight    =   4575
   ClientLeft      =   2340
   ClientTop       =   1935
   ClientWidth     =   5955
   ClipControls    =   0   'False
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3157.746
   ScaleMode       =   0  'User
   ScaleWidth      =   5592.053
   ShowInTaskbar   =   0   'False
   Begin VB.PictureBox picIcon 
      AutoSize        =   -1  'True
      ClipControls    =   0   'False
      Height          =   1215
      Index           =   2
      Left            =   120
      Picture         =   "frmAbout.frx":0000
      ScaleHeight     =   811.195
      ScaleMode       =   0  'User
      ScaleWidth      =   1369.55
      TabIndex        =   7
      Top             =   2400
      Width           =   2010
   End
   Begin VB.PictureBox picIcon 
      AutoSize        =   -1  'True
      ClipControls    =   0   'False
      Height          =   1050
      Index           =   1
      Left            =   120
      Picture         =   "frmAbout.frx":1C52
      ScaleHeight     =   695.31
      ScaleMode       =   0  'User
      ScaleWidth      =   1359.015
      TabIndex        =   6
      Top             =   1200
      Width           =   1995
   End
   Begin VB.CommandButton cmdOK 
      Cancel          =   -1  'True
      Caption         =   "OK"
      Default         =   -1  'True
      Height          =   345
      Left            =   2280
      TabIndex        =   5
      Top             =   4080
      Width           =   1260
   End
   Begin VB.Frame Frame2 
      Height          =   15
      Left            =   240
      TabIndex        =   4
      Top             =   3840
      Width           =   5535
   End
   Begin VB.PictureBox picIcon 
      AutoSize        =   -1  'True
      ClipControls    =   0   'False
      Height          =   1020
      Index           =   0
      Left            =   120
      Picture         =   "frmAbout.frx":27F4
      ScaleHeight     =   674.24
      ScaleMode       =   0  'User
      ScaleWidth      =   1380.085
      TabIndex        =   0
      Top             =   120
      Width           =   2025
   End
   Begin VB.Label lblDisclaimer 
      BackColor       =   &H00E0E0E0&
      Caption         =   "MSE, McMaster University (c) March 2007"
      ForeColor       =   &H00000000&
      Height          =   345
      Left            =   2400
      TabIndex        =   3
      Top             =   3120
      Width           =   3375
   End
   Begin VB.Label lblTitle 
      BackColor       =   &H00E0E0E0&
      Caption         =   "Program Name"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   360
      Left            =   2400
      TabIndex        =   2
      Top             =   240
      Width           =   3405
   End
   Begin VB.Label lblDescription 
      BackColor       =   &H00E0E0E0&
      Caption         =   "This application controls Kethley 6220 Current Source, Keithley 2182A Nanovoltmeter and HP 3478A DMM.     Developed by DPBA."
      ForeColor       =   &H00000000&
      Height          =   810
      Index           =   0
      Left            =   2400
      TabIndex        =   1
      Top             =   960
      Width           =   3405
   End
End
Attribute VB_Name = "frmAbout"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
' frmAbout.frm

Option Explicit

Private Sub Form_Load()
  lblTitle.Caption = program_name
  Me.Caption = "About " & program_name
End Sub

Private Sub cmdOK_Click()
  Unload Me
End Sub

