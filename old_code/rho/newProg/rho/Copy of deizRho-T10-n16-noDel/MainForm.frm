VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "comdlg32.ocx"
Object = "{84E5CF37-E467-4AC2-89C4-C6002FFB5055}#25.1#0"; "ChartViewer.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{38911DA0-E448-11D0-84A3-00DD01104159}#1.1#0"; "COMCT332.OCX"
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "TABCTL32.OCX"
Object = "{7CAC59E5-B703-4CCF-B326-8B956D962F27}#12.0#0"; "CODAF7~1.OCX"
Begin VB.Form frmMainForm 
   BackColor       =   &H00E0E0E0&
   Caption         =   "deizRho v.0.0"
   ClientHeight    =   10305
   ClientLeft      =   165
   ClientTop       =   -1395
   ClientWidth     =   15195
   Icon            =   "MainForm.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   10305
   ScaleWidth      =   15195
   StartUpPosition =   1  'CenterOwner
   Begin XtremeReportControl.ReportControl tableResult 
      Height          =   2775
      Left            =   6480
      TabIndex        =   109
      Top             =   7080
      Width           =   8655
      _Version        =   786432
      _ExtentX        =   15266
      _ExtentY        =   4895
      _StockProps     =   64
   End
   Begin VB.TextBox txtStatus 
      BackColor       =   &H00E0E0E0&
      Height          =   285
      Left            =   120
      TabIndex        =   108
      Top             =   9970
      Width           =   15015
   End
   Begin MSComctlLib.ProgressBar ProgressBar1 
      Height          =   255
      Left            =   6720
      TabIndex        =   104
      Top             =   6480
      Visible         =   0   'False
      Width           =   8055
      _ExtentX        =   14208
      _ExtentY        =   450
      _Version        =   393216
      Appearance      =   1
      Max             =   1000
      Scrolling       =   1
   End
   Begin VB.TextBox txtRemainingTime 
      Height          =   285
      Left            =   11400
      TabIndex        =   24
      Text            =   "RemainingTime"
      Top             =   1080
      Visible         =   0   'False
      Width           =   1335
   End
   Begin VB.Frame Frame7 
      BackColor       =   &H00E0E0E0&
      Height          =   855
      Left            =   5040
      TabIndex        =   23
      Top             =   0
      Width           =   9975
      Begin VB.ComboBox cboDisplay 
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00C00000&
         Height          =   420
         Index           =   4
         ItemData        =   "MainForm.frx":0B3A
         Left            =   7920
         List            =   "MainForm.frx":0B56
         TabIndex        =   30
         Top             =   360
         Width           =   1815
      End
      Begin VB.ComboBox cboDisplay 
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00C00000&
         Height          =   420
         Index           =   3
         ItemData        =   "MainForm.frx":0BBE
         Left            =   6000
         List            =   "MainForm.frx":0BDA
         TabIndex        =   29
         Top             =   360
         Width           =   1815
      End
      Begin VB.ComboBox cboDisplay 
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00C00000&
         Height          =   420
         Index           =   2
         ItemData        =   "MainForm.frx":0C42
         Left            =   4080
         List            =   "MainForm.frx":0C5E
         TabIndex        =   28
         Top             =   360
         Width           =   1815
      End
      Begin VB.ComboBox cboDisplay 
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00C00000&
         Height          =   420
         Index           =   1
         ItemData        =   "MainForm.frx":0CC6
         Left            =   2160
         List            =   "MainForm.frx":0CE2
         TabIndex        =   27
         Top             =   360
         Width           =   1815
      End
      Begin VB.ComboBox cboDisplay 
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00C00000&
         Height          =   420
         Index           =   0
         ItemData        =   "MainForm.frx":0D4A
         Left            =   240
         List            =   "MainForm.frx":0D66
         TabIndex        =   26
         Top             =   360
         Width           =   1815
      End
      Begin VB.Label lblDisplay 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Current (A) :"
         Height          =   255
         Index           =   4
         Left            =   8040
         TabIndex        =   35
         Top             =   120
         Width           =   1575
      End
      Begin VB.Label lblDisplay 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Voltage (V) :"
         Height          =   255
         Index           =   3
         Left            =   6120
         TabIndex        =   34
         Top             =   120
         Width           =   1575
      End
      Begin VB.Label lblDisplay 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Resistance (Ohm) :"
         Height          =   255
         Index           =   2
         Left            =   4200
         TabIndex        =   33
         Top             =   120
         Width           =   1575
      End
      Begin VB.Label lblDisplay 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Temperature (C) :"
         Height          =   255
         Index           =   1
         Left            =   2280
         TabIndex        =   32
         Top             =   120
         Width           =   1575
      End
      Begin VB.Label lblDisplay 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Elapsed Time (s) :"
         Height          =   255
         Index           =   0
         Left            =   360
         TabIndex        =   31
         Top             =   120
         Width           =   1575
      End
   End
   Begin VB.CommandButton Command1 
      BackColor       =   &H00E0E0E0&
      Height          =   975
      Left            =   4920
      Style           =   1  'Graphical
      TabIndex        =   22
      Top             =   0
      Width           =   10215
   End
   Begin ComCtl3.CoolBar CoolBar1 
      Height          =   960
      Left            =   0
      TabIndex        =   18
      Top             =   0
      Width           =   4935
      _ExtentX        =   8705
      _ExtentY        =   1693
      ForeColor       =   14737632
      BackColor       =   14737632
      EmbossHighlight =   14737632
      EmbossShadow    =   14737632
      EmbossPicture   =   -1  'True
      _CBWidth        =   4935
      _CBHeight       =   960
      _Version        =   "6.0.8169"
      Child1          =   "Toolbar1"
      MinHeight1      =   390
      Width1          =   2535
      NewRow1         =   0   'False
      Child2          =   "lstStatus"
      MinHeight2      =   420
      Width2          =   4830
      NewRow2         =   -1  'True
      Child3          =   "txtTotalPoints"
      MinHeight3      =   480
      Width3          =   660
      NewRow3         =   0   'False
      Begin MSComctlLib.Toolbar Toolbar1 
         Height          =   390
         Left            =   165
         TabIndex        =   21
         Top             =   30
         Width           =   4680
         _ExtentX        =   8255
         _ExtentY        =   688
         ButtonWidth     =   609
         ButtonHeight    =   582
         Appearance      =   1
         ImageList       =   "ImageList1"
         DisabledImageList=   "ImageList2"
         _Version        =   393216
         BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
            NumButtons      =   6
            BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Object.ToolTipText     =   "Open Data"
               ImageKey        =   "data"
            EndProperty
            BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Object.ToolTipText     =   "Start Experiment"
               ImageKey        =   "go"
            EndProperty
            BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Object.ToolTipText     =   "Stop Experiment"
               ImageKey        =   "stop"
            EndProperty
            BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Object.ToolTipText     =   "Pause Expeirment"
               ImageKey        =   "pause"
            EndProperty
            BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Style           =   3
            EndProperty
            BeginProperty Button6 {66833FEA-8583-11D1-B16A-00C0F0283628} 
               Object.ToolTipText     =   "Chart"
               ImageKey        =   "chart"
            EndProperty
         EndProperty
      End
      Begin VB.ListBox lstStatus 
         Appearance      =   0  'Flat
         BackColor       =   &H00000000&
         ForeColor       =   &H0000FF00&
         Height          =   420
         ItemData        =   "MainForm.frx":0DCE
         Left            =   165
         List            =   "MainForm.frx":0DD0
         TabIndex        =   20
         ToolTipText     =   "Status"
         Top             =   480
         Width           =   4515
      End
      Begin VB.TextBox txtTotalPoints 
         BeginProperty DataFormat 
            Type            =   1
            Format          =   "hh:mm:ss"
            HaveTrueFalseNull=   0
            FirstDayOfWeek  =   0
            FirstWeekOfYear =   0
            LCID            =   1033
            SubFormatType   =   4
         EndProperty
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   13.5
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H0000C000&
         Height          =   480
         Left            =   4905
         TabIndex        =   19
         Text            =   "Time Elapsed"
         Top             =   450
         Width           =   0
      End
   End
   Begin VB.Frame frameDevices 
      BackColor       =   &H00E0E0E0&
      Caption         =   "Device Settings"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2895
      Left            =   120
      TabIndex        =   10
      Top             =   7060
      Width           =   6255
      Begin VB.CommandButton cmdInitialize 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Initialize"
         Height          =   375
         Left            =   3840
         Style           =   1  'Graphical
         TabIndex        =   97
         Top             =   360
         Width           =   855
      End
      Begin VB.TextBox txtAddress 
         Height          =   285
         Left            =   3000
         TabIndex        =   73
         Text            =   "12,1,10"
         Top             =   360
         Width           =   735
      End
      Begin VB.TextBox txtNumberOfDevices 
         Height          =   285
         Left            =   1320
         TabIndex        =   72
         Text            =   "3"
         Top             =   360
         Width           =   615
      End
      Begin VB.Frame Frame3 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Send Command"
         Height          =   1935
         Left            =   120
         TabIndex        =   64
         Top             =   840
         Width           =   6015
         Begin VB.CommandButton cmdSetListen 
            BackColor       =   &H00E0E0E0&
            Caption         =   "LISTEN"
            Height          =   255
            Left            =   2160
            Style           =   1  'Graphical
            TabIndex        =   69
            Top             =   360
            Width           =   1095
         End
         Begin VB.TextBox txtDeviceNumber 
            Height          =   285
            Left            =   1200
            TabIndex        =   68
            Text            =   "0"
            Top             =   360
            Width           =   735
         End
         Begin VB.CommandButton cmdSendCommand 
            BackColor       =   &H00E0E0E0&
            Caption         =   "SEND"
            Height          =   375
            Left            =   120
            Style           =   1  'Graphical
            TabIndex        =   67
            Top             =   720
            Width           =   975
         End
         Begin VB.TextBox txtResponse 
            BackColor       =   &H00000000&
            ForeColor       =   &H0000FF00&
            Height          =   735
            Left            =   1200
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   66
            Top             =   1140
            Width           =   4695
         End
         Begin VB.TextBox txtCommand 
            BackColor       =   &H00000000&
            ForeColor       =   &H0000FF00&
            Height          =   375
            Left            =   1200
            TabIndex        =   65
            Text            =   ":trace:data?"
            Top             =   720
            Width           =   4695
         End
         Begin VB.Label Label30 
            BackColor       =   &H00E0E0E0&
            Caption         =   "Response :"
            Height          =   255
            Left            =   240
            TabIndex        =   71
            Top             =   1320
            Width           =   975
         End
         Begin VB.Label Label24 
            BackColor       =   &H00E0E0E0&
            Caption         =   "Device No. :"
            Height          =   375
            Left            =   240
            TabIndex        =   70
            Top             =   360
            Width           =   1095
         End
      End
      Begin VB.CommandButton cmdFindingDevices 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Fiind"
         Height          =   375
         Left            =   5040
         Style           =   1  'Graphical
         TabIndex        =   63
         Top             =   360
         Width           =   855
      End
      Begin VB.Label Label4 
         BackColor       =   &H00E0E0E0&
         Caption         =   "# Of Devices :"
         Height          =   255
         Index           =   1
         Left            =   240
         TabIndex        =   75
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label31 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Address :"
         Height          =   255
         Left            =   2160
         TabIndex        =   74
         Top             =   360
         Width           =   735
      End
   End
   Begin VB.Frame Frame2 
      BackColor       =   &H00E0E0E0&
      Caption         =   "Method"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   5895
      Left            =   120
      TabIndex        =   3
      Top             =   1080
      Width           =   6255
      Begin VB.TextBox txtOperatorName 
         Height          =   375
         Left            =   1200
         TabIndex        =   59
         Text            =   "Operator Name"
         Top             =   5400
         Width           =   2415
      End
      Begin TabDlg.SSTab SSTab1 
         Height          =   3855
         Left            =   120
         TabIndex        =   36
         Top             =   1440
         Width           =   6030
         _ExtentX        =   10636
         _ExtentY        =   6800
         _Version        =   393216
         Tabs            =   2
         Tab             =   1
         TabHeight       =   520
         BackColor       =   14737632
         TabCaption(0)   =   "Sample Info"
         TabPicture(0)   =   "MainForm.frx":0DD2
         Tab(0).ControlEnabled=   0   'False
         Tab(0).Control(0)=   "Label22"
         Tab(0).Control(0).Enabled=   0   'False
         Tab(0).Control(1)=   "lblLength"
         Tab(0).Control(1).Enabled=   0   'False
         Tab(0).Control(2)=   "lblWidth"
         Tab(0).Control(2).Enabled=   0   'False
         Tab(0).Control(3)=   "lblThickness"
         Tab(0).Control(3).Enabled=   0   'False
         Tab(0).Control(4)=   "Label26"
         Tab(0).Control(4).Enabled=   0   'False
         Tab(0).Control(5)=   "lblThicknessUnit"
         Tab(0).Control(5).Enabled=   0   'False
         Tab(0).Control(6)=   "Label28"
         Tab(0).Control(6).Enabled=   0   'False
         Tab(0).Control(7)=   "Label29"
         Tab(0).Control(7).Enabled=   0   'False
         Tab(0).Control(8)=   "txtSampleID"
         Tab(0).Control(8).Enabled=   0   'False
         Tab(0).Control(9)=   "txtSampleLength"
         Tab(0).Control(9).Enabled=   0   'False
         Tab(0).Control(10)=   "txtSampleWidth"
         Tab(0).Control(10).Enabled=   0   'False
         Tab(0).Control(11)=   "txtSampleThickness"
         Tab(0).Control(11).Enabled=   0   'False
         Tab(0).Control(12)=   "txtComment"
         Tab(0).Control(12).Enabled=   0   'False
         Tab(0).Control(13)=   "optRectangular"
         Tab(0).Control(13).Enabled=   0   'False
         Tab(0).Control(14)=   "optCylinder"
         Tab(0).Control(14).Enabled=   0   'False
         Tab(0).ControlCount=   15
         TabCaption(1)   =   "Program"
         TabPicture(1)   =   "MainForm.frx":0DEE
         Tab(1).ControlEnabled=   -1  'True
         Tab(1).Control(0)=   "Label5"
         Tab(1).Control(0).Enabled=   0   'False
         Tab(1).Control(1)=   "Label4(0)"
         Tab(1).Control(1).Enabled=   0   'False
         Tab(1).Control(2)=   "Label25"
         Tab(1).Control(2).Enabled=   0   'False
         Tab(1).Control(3)=   "Label27"
         Tab(1).Control(3).Enabled=   0   'False
         Tab(1).Control(4)=   "Label32"
         Tab(1).Control(4).Enabled=   0   'False
         Tab(1).Control(5)=   "Label33"
         Tab(1).Control(5).Enabled=   0   'False
         Tab(1).Control(6)=   "Label34"
         Tab(1).Control(6).Enabled=   0   'False
         Tab(1).Control(7)=   "Label35"
         Tab(1).Control(7).Enabled=   0   'False
         Tab(1).Control(8)=   "Label36"
         Tab(1).Control(8).Enabled=   0   'False
         Tab(1).Control(9)=   "Label37"
         Tab(1).Control(9).Enabled=   0   'False
         Tab(1).Control(10)=   "Label38"
         Tab(1).Control(10).Enabled=   0   'False
         Tab(1).Control(11)=   "Label39"
         Tab(1).Control(11).Enabled=   0   'False
         Tab(1).Control(12)=   "Label40"
         Tab(1).Control(12).Enabled=   0   'False
         Tab(1).Control(13)=   "Label41"
         Tab(1).Control(13).Enabled=   0   'False
         Tab(1).Control(14)=   "Label1"
         Tab(1).Control(14).Enabled=   0   'False
         Tab(1).Control(15)=   "Label2"
         Tab(1).Control(15).Enabled=   0   'False
         Tab(1).Control(16)=   "lstSchedulle"
         Tab(1).Control(16).Enabled=   0   'False
         Tab(1).Control(17)=   "Command7"
         Tab(1).Control(17).Enabled=   0   'False
         Tab(1).Control(18)=   "Command6"
         Tab(1).Control(18).Enabled=   0   'False
         Tab(1).Control(19)=   "txtDataPoints"
         Tab(1).Control(19).Enabled=   0   'False
         Tab(1).Control(20)=   "VScroll1"
         Tab(1).Control(20).Enabled=   0   'False
         Tab(1).Control(21)=   "cboVoltageRange"
         Tab(1).Control(21).Enabled=   0   'False
         Tab(1).Control(22)=   "cboAnalogFilter"
         Tab(1).Control(22).Enabled=   0   'False
         Tab(1).Control(23)=   "txtRate"
         Tab(1).Control(23).Enabled=   0   'False
         Tab(1).Control(24)=   "txtDigitalFilterCount"
         Tab(1).Control(24).Enabled=   0   'False
         Tab(1).Control(25)=   "cboDigitalFilter"
         Tab(1).Control(25).Enabled=   0   'False
         Tab(1).Control(26)=   "txtDelay"
         Tab(1).Control(26).Enabled=   0   'False
         Tab(1).Control(27)=   "txtDeltaCount"
         Tab(1).Control(27).Enabled=   0   'False
         Tab(1).Control(28)=   "txtCompliance"
         Tab(1).Control(28).Enabled=   0   'False
         Tab(1).Control(29)=   "txtCurrent"
         Tab(1).Control(29).Enabled=   0   'False
         Tab(1).Control(30)=   "txtStopTime"
         Tab(1).Control(30).Enabled=   0   'False
         Tab(1).Control(31)=   "chkDeltaMode"
         Tab(1).Control(31).Enabled=   0   'False
         Tab(1).ControlCount=   32
         Begin VB.CheckBox chkDeltaMode 
            Caption         =   "Delta Mode"
            Height          =   255
            Left            =   240
            TabIndex        =   110
            Top             =   1440
            Width           =   1335
         End
         Begin VB.TextBox txtStopTime 
            Height          =   285
            Left            =   4560
            TabIndex        =   105
            Text            =   "43200"
            Top             =   840
            Width           =   855
         End
         Begin VB.TextBox txtCurrent 
            Height          =   285
            Left            =   1320
            TabIndex        =   84
            Text            =   "8"
            Top             =   1800
            Width           =   735
         End
         Begin VB.TextBox txtCompliance 
            Height          =   285
            Left            =   1320
            TabIndex        =   83
            Text            =   "10"
            Top             =   2160
            Width           =   735
         End
         Begin VB.TextBox txtDeltaCount 
            Height          =   285
            Left            =   1320
            TabIndex        =   82
            Text            =   "5"
            Top             =   2520
            Width           =   735
         End
         Begin VB.TextBox txtDelay 
            Height          =   285
            Left            =   1320
            TabIndex        =   81
            Text            =   "0.002"
            Top             =   2880
            Width           =   735
         End
         Begin VB.ComboBox cboDigitalFilter 
            Height          =   315
            ItemData        =   "MainForm.frx":0E0A
            Left            =   4440
            List            =   "MainForm.frx":0E17
            TabIndex        =   80
            Text            =   "None"
            Top             =   2160
            Width           =   1215
         End
         Begin VB.TextBox txtDigitalFilterCount 
            Height          =   285
            Left            =   5040
            TabIndex        =   79
            Text            =   "5"
            Top             =   2520
            Width           =   615
         End
         Begin VB.TextBox txtRate 
            Height          =   285
            Left            =   5040
            TabIndex        =   78
            Text            =   "1"
            Top             =   2880
            Width           =   615
         End
         Begin VB.ComboBox cboAnalogFilter 
            Height          =   315
            ItemData        =   "MainForm.frx":0E31
            Left            =   4440
            List            =   "MainForm.frx":0E3B
            TabIndex        =   77
            Text            =   "OFF"
            Top             =   1800
            Width           =   1215
         End
         Begin VB.ComboBox cboVoltageRange 
            Height          =   315
            ItemData        =   "MainForm.frx":0E48
            Left            =   4800
            List            =   "MainForm.frx":0E5E
            TabIndex        =   76
            Text            =   "Auto"
            Top             =   3240
            Width           =   855
         End
         Begin VB.OptionButton optCylinder 
            Caption         =   "Cylinder"
            Height          =   255
            Left            =   -70680
            TabIndex        =   58
            Top             =   600
            Width           =   975
         End
         Begin VB.OptionButton optRectangular 
            Caption         =   "Rectangular"
            Height          =   255
            Left            =   -72120
            TabIndex        =   57
            Top             =   600
            Value           =   -1  'True
            Width           =   1335
         End
         Begin VB.TextBox txtComment 
            Height          =   855
            Left            =   -74760
            MultiLine       =   -1  'True
            TabIndex        =   55
            Text            =   "MainForm.frx":0E7F
            Top             =   2880
            Width           =   5415
         End
         Begin VB.TextBox txtSampleThickness 
            Height          =   375
            Left            =   -73800
            TabIndex        =   50
            Text            =   "0.075"
            Top             =   1920
            Width           =   975
         End
         Begin VB.TextBox txtSampleWidth 
            Height          =   375
            Left            =   -73800
            TabIndex        =   48
            Text            =   "2.5"
            Top             =   1440
            Width           =   975
         End
         Begin VB.TextBox txtSampleLength 
            Height          =   375
            Left            =   -73800
            TabIndex        =   46
            Text            =   "1.05"
            Top             =   960
            Width           =   975
         End
         Begin VB.TextBox txtSampleID 
            Height          =   375
            Left            =   -73800
            TabIndex        =   44
            Text            =   "Copper"
            Top             =   480
            Width           =   1455
         End
         Begin VB.VScrollBar VScroll1 
            Height          =   255
            Left            =   1920
            TabIndex        =   41
            Top             =   3240
            Width           =   255
         End
         Begin VB.TextBox txtDataPoints 
            Height          =   285
            Left            =   1320
            TabIndex        =   40
            Text            =   "1"
            Top             =   3240
            Width           =   615
         End
         Begin VB.CommandButton Command6 
            Caption         =   "Delete"
            Height          =   255
            Left            =   3480
            Style           =   1  'Graphical
            TabIndex        =   39
            Top             =   1200
            Width           =   735
         End
         Begin VB.CommandButton Command7 
            Caption         =   "Add"
            Height          =   255
            Left            =   2640
            Style           =   1  'Graphical
            TabIndex        =   38
            Top             =   1200
            Width           =   735
         End
         Begin VB.ListBox lstSchedulle 
            Height          =   645
            ItemData        =   "MainForm.frx":0E8A
            Left            =   120
            List            =   "MainForm.frx":0E97
            TabIndex        =   37
            Top             =   480
            Width           =   4215
         End
         Begin VB.Label Label2 
            Caption         =   "min"
            Height          =   255
            Left            =   5520
            TabIndex        =   107
            Top             =   840
            Width           =   375
         End
         Begin VB.Label Label1 
            Caption         =   "Stop Time :"
            Height          =   255
            Left            =   4560
            TabIndex        =   106
            Top             =   600
            Width           =   855
         End
         Begin VB.Label Label41 
            Caption         =   "Current :"
            Height          =   255
            Left            =   240
            TabIndex        =   96
            Top             =   1800
            Width           =   975
         End
         Begin VB.Label Label40 
            Caption         =   "mA"
            Height          =   255
            Left            =   2160
            TabIndex        =   95
            Top             =   1920
            Width           =   495
         End
         Begin VB.Label Label39 
            Caption         =   "Compliance :"
            Height          =   375
            Left            =   240
            TabIndex        =   94
            Top             =   2160
            Width           =   975
         End
         Begin VB.Label Label38 
            Caption         =   "V"
            Height          =   255
            Left            =   2160
            TabIndex        =   93
            Top             =   2280
            Width           =   375
         End
         Begin VB.Label Label37 
            Caption         =   "Count :"
            Height          =   255
            Left            =   240
            TabIndex        =   92
            Top             =   2520
            Width           =   975
         End
         Begin VB.Label Label36 
            Caption         =   "Delay :"
            Height          =   255
            Left            =   240
            TabIndex        =   91
            Top             =   2880
            Width           =   1335
         End
         Begin VB.Label Label35 
            Caption         =   "s"
            Height          =   255
            Left            =   2160
            TabIndex        =   90
            Top             =   2880
            Width           =   255
         End
         Begin VB.Label Label34 
            Caption         =   "Digital Filter :"
            Height          =   255
            Left            =   3240
            TabIndex        =   89
            Top             =   2160
            Width           =   1095
         End
         Begin VB.Label Label33 
            Caption         =   "Digital Filter Count :"
            Height          =   255
            Left            =   3240
            TabIndex        =   88
            Top             =   2520
            Width           =   1695
         End
         Begin VB.Label Label32 
            Caption         =   "Rate (NPLCs) :"
            Height          =   255
            Left            =   3240
            TabIndex        =   87
            Top             =   2880
            Width           =   1215
         End
         Begin VB.Label Label27 
            Caption         =   "Voltage Range (V) :"
            Height          =   255
            Left            =   3240
            TabIndex        =   86
            Top             =   3240
            Width           =   1575
         End
         Begin VB.Label Label25 
            Caption         =   "Analog Filter :"
            Height          =   255
            Left            =   3240
            TabIndex        =   85
            Top             =   1800
            Width           =   1095
         End
         Begin VB.Label Label29 
            Caption         =   "Comment :"
            Height          =   255
            Left            =   -74760
            TabIndex        =   56
            Top             =   2520
            Width           =   975
         End
         Begin VB.Label Label28 
            Caption         =   "mm"
            Height          =   255
            Left            =   -72720
            TabIndex        =   54
            Top             =   1560
            Width           =   375
         End
         Begin VB.Label lblThicknessUnit 
            Caption         =   "mm"
            Height          =   255
            Left            =   -72720
            TabIndex        =   53
            Top             =   2040
            Width           =   375
         End
         Begin VB.Label Label26 
            Caption         =   "mm"
            Height          =   255
            Left            =   -72720
            TabIndex        =   52
            Top             =   1080
            Width           =   375
         End
         Begin VB.Label lblThickness 
            Caption         =   "Thickness :"
            Height          =   255
            Left            =   -74760
            TabIndex        =   51
            Top             =   2040
            Width           =   975
         End
         Begin VB.Label lblWidth 
            Caption         =   "Width        :"
            Height          =   255
            Left            =   -74760
            TabIndex        =   49
            Top             =   1560
            Width           =   975
         End
         Begin VB.Label lblLength 
            Caption         =   "Length      :"
            Height          =   255
            Left            =   -74760
            TabIndex        =   47
            Top             =   1080
            Width           =   975
         End
         Begin VB.Label Label22 
            Caption         =   "Sample ID :"
            Height          =   255
            Left            =   -74760
            TabIndex        =   45
            Top             =   600
            Width           =   975
         End
         Begin VB.Label Label4 
            Caption         =   "s/pt"
            Height          =   255
            Index           =   0
            Left            =   2280
            TabIndex        =   43
            Top             =   3240
            Width           =   375
         End
         Begin VB.Label Label5 
            Caption         =   "Data Points :"
            Height          =   255
            Left            =   240
            TabIndex        =   42
            Top             =   3240
            Width           =   975
         End
      End
      Begin VB.TextBox txtFileName 
         Height          =   375
         Left            =   1200
         TabIndex        =   7
         Text            =   "rho_data.rho"
         Top             =   360
         Width           =   3975
      End
      Begin VB.TextBox txtFile 
         Height          =   375
         Left            =   1200
         Locked          =   -1  'True
         TabIndex        =   6
         Text            =   "c:\rho_data.rho"
         Top             =   840
         Width           =   3975
      End
      Begin VB.CommandButton cmdBrowse 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Browse"
         Height          =   375
         Left            =   5280
         Style           =   1  'Graphical
         TabIndex        =   5
         Top             =   840
         Width           =   855
      End
      Begin VB.CommandButton cmdApplyMethod 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Apply"
         Height          =   375
         Left            =   4920
         Style           =   1  'Graphical
         TabIndex        =   4
         Top             =   5400
         Width           =   975
      End
      Begin MSComDlg.CommonDialog dlgFile 
         Left            =   5520
         Top             =   840
         _ExtentX        =   847
         _ExtentY        =   847
         _Version        =   393216
         DialogTitle     =   "SAVE FILE - Powered by Daisman"
      End
      Begin VB.Label Label23 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Operator :"
         Height          =   255
         Left            =   240
         TabIndex        =   60
         Top             =   5520
         Width           =   855
      End
      Begin VB.Label lblFileName 
         BackColor       =   &H00E0E0E0&
         Caption         =   "File Name :"
         Height          =   255
         Left            =   240
         TabIndex        =   9
         Top             =   480
         Width           =   855
      End
      Begin VB.Label Label6 
         BackColor       =   &H00E0E0E0&
         Caption         =   "File Path   :"
         Height          =   255
         Left            =   240
         TabIndex        =   8
         Top             =   960
         Width           =   855
      End
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00E0E0E0&
      Caption         =   "Readout"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   5895
      Left            =   6480
      TabIndex        =   0
      Top             =   1080
      Width           =   8655
      Begin VB.TextBox txtYcoor 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         Height          =   220
         Left            =   7560
         TabIndex        =   103
         Top             =   5400
         Width           =   615
      End
      Begin VB.TextBox txtXcoor 
         Appearance      =   0  'Flat
         BackColor       =   &H00FFFFFF&
         Height          =   220
         Left            =   6840
         TabIndex        =   102
         Top             =   5400
         Width           =   615
      End
      Begin VB.OptionButton PointerPB 
         Height          =   360
         Left            =   6720
         Picture         =   "MainForm.frx":0F0D
         Style           =   1  'Graphical
         TabIndex        =   100
         Top             =   720
         Value           =   -1  'True
         Width           =   525
      End
      Begin VB.OptionButton ZoomInPB 
         BackColor       =   &H00E0E0E0&
         Height          =   360
         Left            =   7320
         Picture         =   "MainForm.frx":1193
         Style           =   1  'Graphical
         TabIndex        =   99
         Top             =   720
         Width           =   525
      End
      Begin VB.OptionButton ZoomOutPB 
         BackColor       =   &H00E0E0E0&
         Height          =   360
         Left            =   7920
         Picture         =   "MainForm.frx":142D
         Style           =   1  'Graphical
         TabIndex        =   98
         Top             =   720
         Width           =   525
      End
      Begin VB.CheckBox chkAllDataPoints 
         BackColor       =   &H00E0E0E0&
         Caption         =   "All"
         Height          =   375
         Left            =   2520
         TabIndex        =   62
         Top             =   600
         Width           =   615
      End
      Begin VB.TextBox txtMaxNumberDataX 
         Height          =   375
         Left            =   1320
         TabIndex        =   61
         Text            =   "100"
         Top             =   600
         Width           =   735
      End
      Begin VB.Timer tmrReadDevicesOnlyToDisplay 
         Enabled         =   0   'False
         Interval        =   1000
         Left            =   240
         Top             =   1440
      End
      Begin VB.TextBox txtCustom 
         Height          =   285
         Left            =   3960
         TabIndex        =   25
         Text            =   "Custom"
         Top             =   0
         Visible         =   0   'False
         Width           =   855
      End
      Begin VB.Timer tmrReadDevices 
         Enabled         =   0   'False
         Interval        =   1000
         Left            =   240
         Top             =   1920
      End
      Begin VB.Timer tmrDrawChart 
         Enabled         =   0   'False
         Interval        =   1000
         Left            =   240
         Top             =   2400
      End
      Begin VB.Timer tmrShiftData 
         Enabled         =   0   'False
         Interval        =   500
         Left            =   240
         Top             =   3120
      End
      Begin MSComctlLib.ImageList ImageList1 
         Left            =   240
         Top             =   4080
         _ExtentX        =   1005
         _ExtentY        =   1005
         BackColor       =   -2147483643
         ImageWidth      =   16
         ImageHeight     =   16
         MaskColor       =   12632256
         _Version        =   393216
         BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
            NumListImages   =   5
            BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "MainForm.frx":16A3
               Key             =   "data"
            EndProperty
            BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "MainForm.frx":1AF7
               Key             =   "go"
            EndProperty
            BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "MainForm.frx":1C53
               Key             =   "stop"
            EndProperty
            BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "MainForm.frx":1F6D
               Key             =   "pause"
            EndProperty
            BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "MainForm.frx":23BF
               Key             =   "chart"
            EndProperty
         EndProperty
      End
      Begin MSComctlLib.ImageList ImageList2 
         Left            =   240
         Top             =   4680
         _ExtentX        =   1005
         _ExtentY        =   1005
         BackColor       =   -2147483643
         ImageWidth      =   16
         ImageHeight     =   16
         MaskColor       =   12632256
         _Version        =   393216
         BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
            NumListImages   =   5
            BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "MainForm.frx":2813
               Key             =   "data"
            EndProperty
            BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "MainForm.frx":2C67
               Key             =   "go"
            EndProperty
            BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "MainForm.frx":2DC3
               Key             =   "stop"
            EndProperty
            BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "MainForm.frx":30DD
               Key             =   "pause"
            EndProperty
            BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
               Picture         =   "MainForm.frx":352F
               Key             =   "chart"
            EndProperty
         EndProperty
      End
      Begin VB.ComboBox cmbSelectYAxis 
         Height          =   315
         ItemData        =   "MainForm.frx":3983
         Left            =   4920
         List            =   "MainForm.frx":399C
         TabIndex        =   15
         Text            =   "Resistivity"
         Top             =   600
         Width           =   1455
      End
      Begin VB.ComboBox cmbSelectXAxis 
         Height          =   315
         ItemData        =   "MainForm.frx":39EE
         Left            =   3240
         List            =   "MainForm.frx":39FE
         TabIndex        =   14
         Text            =   "Temperature"
         Top             =   600
         Width           =   1455
      End
      Begin VB.TextBox txtChartPeriod 
         Height          =   405
         Left            =   240
         TabIndex        =   1
         Text            =   "1000"
         Top             =   600
         Width           =   615
      End
      Begin CDChartViewer.ChartViewer ChartViewer1 
         Height          =   4575
         Left            =   240
         Top             =   1200
         Width           =   8295
         _ExtentX        =   14631
         _ExtentY        =   8070
         ZoomDirection   =   2
         ScrollDirection =   2
      End
      Begin MSComctlLib.Slider ZoomBar 
         Height          =   375
         Left            =   6720
         TabIndex        =   101
         Top             =   240
         Width           =   1725
         _ExtentX        =   3043
         _ExtentY        =   661
         _Version        =   393216
         LargeChange     =   10
         Min             =   1
         Max             =   100
         SelStart        =   1
         TickStyle       =   3
         TickFrequency   =   10
         Value           =   1
      End
      Begin VB.Label Label20 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Y Axis :"
         Height          =   375
         Left            =   4920
         TabIndex        =   17
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label19 
         BackColor       =   &H00E0E0E0&
         Caption         =   "X Axis :"
         Height          =   375
         Left            =   3240
         TabIndex        =   16
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label18 
         BackColor       =   &H00E0E0E0&
         Caption         =   "pts"
         Height          =   375
         Left            =   2160
         TabIndex        =   13
         Top             =   720
         Width           =   375
      End
      Begin VB.Label Label17 
         BackColor       =   &H00E0E0E0&
         Caption         =   "ms"
         Height          =   375
         Left            =   960
         TabIndex        =   12
         Top             =   720
         Width           =   375
      End
      Begin VB.Label Label16 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Data Points :"
         Height          =   375
         Left            =   1320
         TabIndex        =   11
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label lblChartPeriod 
         BackColor       =   &H00E0E0E0&
         Caption         =   "Period :"
         Height          =   375
         Left            =   240
         TabIndex        =   2
         Top             =   360
         Width           =   1575
      End
   End
   Begin VB.Menu mnuFile 
      Caption         =   "File"
      Begin VB.Menu mnuOpenData 
         Caption         =   "Open Data"
         Shortcut        =   ^O
      End
      Begin VB.Menu mnuFileSep1 
         Caption         =   "-"
      End
      Begin VB.Menu mnuOpenMethod 
         Caption         =   "Open Method"
         Shortcut        =   ^M
      End
      Begin VB.Menu mnuSaveMethod 
         Caption         =   "Save Method"
      End
      Begin VB.Menu mnuFileSep2 
         Caption         =   "-"
      End
      Begin VB.Menu mnuSaveChart 
         Caption         =   "Save Chart"
      End
      Begin VB.Menu mnuSep4 
         Caption         =   "-"
      End
      Begin VB.Menu mnuExit 
         Caption         =   "Exit"
         Shortcut        =   ^E
      End
   End
   Begin VB.Menu mnuExperiment 
      Caption         =   "Experiment"
      Begin VB.Menu mnuStart 
         Caption         =   "Start"
         Enabled         =   0   'False
         Shortcut        =   ^S
      End
      Begin VB.Menu mnuStop 
         Caption         =   "Stop"
         Enabled         =   0   'False
         Shortcut        =   ^X
      End
      Begin VB.Menu mnuPause 
         Caption         =   "Pause"
         Enabled         =   0   'False
      End
   End
   Begin VB.Menu mnuTool 
      Caption         =   "Tool"
      Begin VB.Menu mnuConnectDevices 
         Caption         =   "Connect Devices"
         Shortcut        =   ^I
      End
      Begin VB.Menu mnuSep3 
         Caption         =   "-"
      End
      Begin VB.Menu mnuSettings 
         Caption         =   "Settings"
         Shortcut        =   ^C
      End
   End
   Begin VB.Menu mnuChart 
      Caption         =   "Chart"
      Begin VB.Menu mnuChartOn 
         Caption         =   "Chart On"
         Enabled         =   0   'False
      End
      Begin VB.Menu mnuAutoScale 
         Caption         =   "Auto Scale - Chart"
         Checked         =   -1  'True
      End
   End
   Begin VB.Menu mnuTable 
      Caption         =   "Table"
      Begin VB.Menu mnuPlot 
         Caption         =   "Plot Selected Row(s)"
      End
      Begin VB.Menu mnuDeleteRows 
         Caption         =   "Delete Selected Row(s)"
      End
      Begin VB.Menu mnuCopyToClipboard 
         Caption         =   "Copy Selected Row(s)"
      End
   End
   Begin VB.Menu mnuHelp 
      Caption         =   "Help"
      Begin VB.Menu mnuHelpChild 
         Caption         =   "Help"
         Shortcut        =   {F1}
      End
      Begin VB.Menu separator 
         Caption         =   "-"
      End
      Begin VB.Menu mnuAbout 
         Caption         =   "About"
      End
   End
End
Attribute VB_Name = "frmMainForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

' frmMain

' Program   : deizRho v.0.0 beta 1
' Developer : DPBA
' Copyright (c) March 2007
'
' This program controls Keithley 6220 Current Source, Keithley 2182A Nanovoltmeter,
' and HP 3478A DMM for measuring electrical resistivity. The communication between
' CCR 400 and PC uses GPIB interface.
'
' When the application starts, all devices are not connected yet.
' To connect to the device, select Tools -> Connect Devices
' When connected, the software reads voltage (Keithley 2182A)  and temperature (HP 3478A).
' To check the settings of the device, select Tools -> Settings

' To start the experiment, select File -> Start. To stop, select File -> Start
' To exit the software select File -> Exit

Dim i As Long

' related to GPIB devices
Dim Dev() As Integer               ' GPIB devices, address = 7, 12
Const GPIB0 = 0
Dim BDINDEX As Integer             ' Board Index (set = 0)
Dim PRIMARY_ADDR_OF_DMM As Integer ' Primary address of device (Multi = 7,6220 = 12)
Const NO_SECONDARY_ADDR = 0        ' Secondary address of device
Const TIMEOUT = T10s               ' Timeout value = 10 seconds
Const EOTMODE = 1                  ' Enable the END message
Const EOSMODE = 0                  ' Disable the EOS mode

' about devices
Dim number_of_devices As Integer
Dim GPIB_address(30) As Integer
Dim reading_from_all_devices As String
Dim reading_from_each_instrument() As String

' status of experiments
Dim initialize_devices_is_done As Boolean
Public devices_are_connected As Boolean
Public experiment_is_running As Boolean
Dim experiment_is_paused As Boolean
Dim method_is_applied As Boolean
Dim charting_is_running As Boolean
Dim times_of_using_same_file_name As Integer
Dim experiment_info As String 'later can be redim

' readings of devices, others declared public
Dim emf() As Double  ' voltage reading of thermocopule to be converted to T
Dim stop_time As Long
Dim data_points As Double ' sec/points

' monitor delta settings change
Dim delta_current As String
Dim delta_compliance As String
Dim delta_count As String
Dim delta_delay As String
Dim analog_filter As String
Dim digital_filter As String
Dim digital_filter_count As String
Dim delta_rate As String
Dim voltage_range As String
Dim delta_settings_change As Boolean

' for file writing/reading
Dim mFileSysObj As New FileSystemObject
Dim mFile As File
Dim mTxtStream As TextStream
Dim path_and_file_name As String

' declarations for methods
Const APP_NAME As String = "deizRho"
Dim m_FilterSelected As Integer

' declarations for drawing chart
Dim cd As New chartdirector.API
Dim c As XYChart
' The full x-axis and y-axis scales at no zooming, first time plotting
Public minX As Double
Public maxX As Double
Private MinY As Double
Private MaxY As Double
' draw_chart, x axis = time(h:m:s)
Const sampleSize = 100
Dim dataSeries(sampleSize - 1)
Dim timeStamps(sampleSize - 1)
Dim nextDateTime As Double
' use the GetSystemTime API to get ms resolution
Private Declare Sub GetSystemTime Lib "kernel32" (lpSystemTime As SYSTEMTIME)
Private Type SYSTEMTIME
  wYear As Integer
  wMonth As Integer
  wDayOfWeek As Integer
  wDay As Integer
  wHour As Integer
  wMinute As Integer
  wSecond As Integer
  wMilliseconds As Integer
End Type

' draw_chart2, x axis = time (s) or T (C)
'Dim dataY() As Double ' public cause called in frmSettings
'Dim dataX() As String
Dim dataY_exponent As String
Public maxNumberDataX As Integer
Public maxNumberDataY As Integer
Public minNumberDataX As Integer
Dim xLabelShown As Integer
Dim FixedShownAxisX As Integer
Dim XAxisTitle As String
Dim YAxisTitle As String
Dim YGrid As Double
Dim XGrid As Double

' for table
Const COLUMN_TIME = 0
Const COLUMN_T = 1
Const COLUMN_RESISTIVITY = 2
Const COLUMN_RESISTANCE = 3
Const COLUMN_VOLTAGE = 4
Const COLUMN_CURRENT = 5
Const COLUMN_RAMPING = 6
Dim cfItem As Integer
Dim ItemHot As ReportRecordItem
Dim isRecordSelect As Boolean
Dim row_selected() As Long
Dim row_sel As Long


Private Sub Form_Load()
  On Error GoTo myErrorHandler

  Dim i As Integer
   
  GPIBglobalsRegistered = 0
  
  Call load_settings
      
  ' draw chart
  Dim viewer As ChartViewer
  Call draw_chart_empty(ChartViewer1)
    
  ' set false the stop, pause, and chart menu
  Toolbar1.Buttons.item(2).Enabled = False
  Toolbar1.Buttons.item(3).Enabled = False
  Toolbar1.Buttons.item(4).Enabled = False
  Toolbar1.Buttons.item(6).Enabled = False
  
  Me.Caption = program_name
  
  Call show_table
      
  write_status "Application starts"
    
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".Form_Load)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  'Call save_settings
  Call save_status
End Sub

Private Sub show_table()
  ' table
  Dim Column As ReportColumn
  ' set column 1-9 for a ReportControl
  Set Column = tableResult.Columns.Add(COLUMN_TIME, "t (s)", 50, True)
  Column.HeaderAlignment = xtpAlignmentCenter
    
  Set Column = tableResult.Columns.Add(COLUMN_T, "T (C)", 40, True)
  Column.HeaderAlignment = xtpAlignmentCenter
    
  Set Column = tableResult.Columns.Add(COLUMN_RESISTIVITY, "Rho (Ohm.m)", 80, True)
  Column.HeaderAlignment = xtpAlignmentCenter
    
  Set Column = tableResult.Columns.Add(COLUMN_RESISTANCE, "R (Ohm)", 80, True)
  Column.HeaderAlignment = xtpAlignmentCenter
      
  Set Column = tableResult.Columns.Add(COLUMN_VOLTAGE, "V (Volts)", 80, True)
  Column.HeaderAlignment = xtpAlignmentCenter
    
  Set Column = tableResult.Columns.Add(COLUMN_CURRENT, "I (A)", 50, True)
  Column.HeaderAlignment = xtpAlignmentCenter
         
  Set Column = tableResult.Columns.Add(COLUMN_RAMPING, "Ramp (C/min)", 60, True)
  Column.HeaderAlignment = xtpAlignmentCenter
        
  For i = 1 To 8
    add_row "", "", "", "", "", "", ""
  Next i

  ' set other ReportControl options
  tableResult.PaintManager.VerticalGridStyle = xtpGridSolid
  tableResult.AllowEdit = False
  tableResult.FocusSubItems = False
  tableResult.AllowColumnRemove = False
         
  tableResult.SetCustomDraw xtpCustomBeforeDrawRow
  tableResult.PaintManager.FixedRowHeight = False
    
  tableResult.PaintManager.ColumnStyle = xtpColumnOffice2003 ' = xtpColumnOffice2007
  tableResult.PaintManager.ColumnOffice2007CustomThemeBaseColor = vbBlue
  'tableResult.PaintManager.GridLineColor = vbBlack
  tableResult.PaintManager.HighlightBackColor = RGB(255, 238, 194)
  tableResult.PaintManager.HighlightForeColor = vbBlack
    
  tableResult.BorderStyle = xtpBorderFrame
        
  ' Apply changes
  tableResult.Populate
  cfItem = tableResult.EnableDragDrop("TaskList", xtpReportAllowDrag Or xtpReportAllowDrop)
  
End Sub

Private Sub load_settings() ' called by Form_Load sub
  txtFileName.text = GetSetting(APP_NAME, "Files", "FileName")
  txtFile.text = GetSetting(APP_NAME, "Files", "File", "")
  m_FilterSelected = GetSetting(APP_NAME, "Files", "Filter", "0")
    
  txtSampleID.text = GetSetting(APP_NAME, "Sample", "Material")
  txtSampleLength.text = GetSetting(APP_NAME, "Sample", "Length")
  txtSampleWidth.text = GetSetting(APP_NAME, "Sample", "Width")
  txtSampleThickness.text = GetSetting(APP_NAME, "Sample", "Thickness")
  optRectangular.value = GetSetting(APP_NAME, "Sample", "optRectangular")
  If optRectangular.value = False Then
    optCylinder.value = True ' will call optCylinder_click
  End If
  txtComment.text = GetSetting(APP_NAME, "Sample", "Comment")
  txtOperatorName.text = GetSetting(APP_NAME, "Sample", "Operator Name")
    
  txtCurrent.text = GetSetting(APP_NAME, "Program", "Current")
  txtCompliance.text = GetSetting(APP_NAME, "Program", "Compliance")
  txtDeltaCount.text = GetSetting(APP_NAME, "Program", "DeltaCount")
  txtDelay.text = GetSetting(APP_NAME, "Program", "Delay")
  txtDataPoints.text = GetSetting(APP_NAME, "Program", "DataPoints")
  cboAnalogFilter.text = GetSetting(APP_NAME, "Program", "AnalogFilter")
  cboDigitalFilter.text = GetSetting(APP_NAME, "Program", "DigitalFilter")
  txtDigitalFilterCount.text = GetSetting(APP_NAME, "Program", "DigitalFilterCount")
  txtRate.text = GetSetting(APP_NAME, "Program", "Rate")
  cboVoltageRange.text = GetSetting(APP_NAME, "Program", "VoltageRange")
  
  txtStopTime.text = GetSetting(APP_NAME, "Program", "StopTime")
  
  txtNumberOfDevices.text = GetSetting(APP_NAME, "Device", "#Devices")
  txtAddress.text = GetSetting(APP_NAME, "Device", "Address")
  txtDeviceNumber.text = GetSetting(APP_NAME, "Device", "DeviceNumber")
  txtCommand.text = GetSetting(APP_NAME, "Device", "Command")
  
  delta_current = txtCurrent.text
  delta_compliance = txtCompliance.text
  delta_count = txtDeltaCount.text
  delta_delay = txtDelay.text
  analog_filter = cboAnalogFilter.text
  digital_filter = cboDigitalFilter.text
  digital_filter_count = txtDigitalFilterCount.text
  delta_rate = txtRate.text
  voltage_range = cboVoltageRange.text
End Sub

Public Sub save_settings() ' called by Form_QuryUnLoad sub and error handler
  SaveSetting APP_NAME, "Files", "FileName", txtFileName.text
  SaveSetting APP_NAME, "Files", "File", txtFile.text
  SaveSetting APP_NAME, "Files", "Filter", m_FilterSelected
      
  SaveSetting APP_NAME, "Sample", "Material", txtSampleID.text
  SaveSetting APP_NAME, "Sample", "Length", txtSampleLength.text
  SaveSetting APP_NAME, "Sample", "Width", txtSampleWidth.text
  SaveSetting APP_NAME, "Sample", "Thickness", txtSampleThickness.text
  SaveSetting APP_NAME, "Sample", "optRectangular", optRectangular.value
  SaveSetting APP_NAME, "Sample", "Comment", txtComment.text
  SaveSetting APP_NAME, "Sample", "Operator Name", txtOperatorName.text
  
  SaveSetting APP_NAME, "Program", "Current", txtCurrent.text
  SaveSetting APP_NAME, "Program", "Compliance", txtCompliance.text
  SaveSetting APP_NAME, "Program", "DeltaCount", txtDeltaCount.text
  SaveSetting APP_NAME, "Program", "Delay", txtDelay.text
  SaveSetting APP_NAME, "Program", "DataPoints", txtDataPoints.text
  SaveSetting APP_NAME, "Program", "AnalogFilter", cboAnalogFilter.text
  SaveSetting APP_NAME, "Program", "DigitalFilter", cboDigitalFilter.text
  SaveSetting APP_NAME, "Program", "DigitalFilterCount", txtDigitalFilterCount.text
  SaveSetting APP_NAME, "Program", "Rate", txtRate.text
  SaveSetting APP_NAME, "Program", "VoltageRange", cboVoltageRange.text
  
  SaveSetting APP_NAME, "Program", "StopTime", txtStopTime.text
  
  SaveSetting APP_NAME, "Device", "#Devices", txtNumberOfDevices.text
  SaveSetting APP_NAME, "Device", "Address", txtAddress.text
  SaveSetting APP_NAME, "Device", "DeviceNumber", txtDeviceNumber.text
  SaveSetting APP_NAME, "Device", "Command", txtCommand.text
End Sub

Private Sub Form_QueryUnLoad(cancel As Integer, UnLoadMode As Integer)
  On Error GoTo myErrorHandler
  
  Dim chosen_button As Integer
  Dim chosen_button2 As Integer
  
  'If experiment_is_started_for_1st_time = False Or _
    experiment_is_stopped = True Then
  If experiment_is_running = False Then
    chosen_button = MsgBox("Do you want to exit deizRho v.0.0 ?", _
      vbYesNo + vbQuestion, "Confirm Exit")
  Else ' paused or still running
    If experiment_is_paused = True Then
      chosen_button2 = MsgBox("An experiment with file name " & txtFile.text & _
        " is being paused." & vbCr & "Do you want to exit " & program_name & " ?" _
        & vbCr & "(The running-experiment data will be saved)", _
        vbYesNo + vbExclamation, "Confirm Exit")
    Else 'experiment is still running
      chosen_button2 = MsgBox("An experiment with file name " & txtFile.text & _
        " is running." & vbCr & "Do you want to exit " & program_name & " ?" _
        & vbCr & "(The running-experiment data will be saved)", _
        vbYesNo + vbExclamation, "Confirm Exit")
    End If
    
    Select Case chosen_button2
    Case vbYes
      experiment_info = experiment_info & _
        "Experiment stops while running at " & Now & vbCrLf & "" & vbCrLf
                        
      'If experiment_is_stopped = False Then 'even if being paused it is saved
        Call save_file ' OK, but if executed wrong time, could destroy
      'End If           ' the experiment saved file
    
      ' **************************
      chosen_button = vbYes ' for next lines below
      ' **************************
    Case vbNo
      cancel = 1
    End Select
  End If
  
  Select Case chosen_button
    Case vbYes
      write_status "Application exits"
      Call save_settings
      Call save_status
      
      ' *** Exit Delta
      If devices_are_connected = True Then
        tmrReadDevicesOnlyToDisplay.Enabled = False
        tmrReadDevices.Enabled = False
  
        Call exit_delta
        Sleep 1000
        Call ilwrt(Dev(0), ":disp:enab 1", 12)
        If (ibsta And EERR) Then
          GpibErr ("Error enabling front panel of Keithley 6220")
        End If
        Sleep 1000
            
        Call disconnect_device
      End If
      ' *************************************************
                  
      Unload frmSettings
      Unload frmAbout
      'Unload Me ' NOT End to quit program
      'End ' if not, if exit by press X on the top corner, app still there
    Case vbNo
      cancel = 1
  End Select
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".Form_UnLoad)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Public Sub initialize_devices_o() ' Original initialization sub, by board not by device
  ' INITIALIZATION SECTION
  ' The GPIB board needs to be the Controller-In-Charge in order to find all
  ' listeners on the GPIB.  To accomplish this, the subroutine SendIFC
  ' is called.  If the error bit EERR is set in ibsta, call GpibErr with
  ' an error message.
  Call SendIFC(GPIB0)
  If (ibsta And EERR) Then
    GpibErr ("Error sending IFC.")
  End If

  ' DevClearList will send the GPIB Selected Device Clear (SDC) command
  ' message to all the devices on the bus. If the error bit EERR is set in
  ' ibsta, call GpibErr with an error message.
  Call DevClearList(GPIB0, GPIB_address%)
  If (ibsta And EERR) Then
    GpibErr ("Error in clearing the devices. ")
  End If

  initialize_devices_is_done = True
End Sub

' GPIB devices initilization
Private Sub initialize_devices()
  Dim i As Integer
  
  BDINDEX = GPIB0                ' Board Index
  
  For i = 0 To number_of_devices - 1
    PRIMARY_ADDR_OF_DMM = GPIB_address(i) ' 0 = Keithley 6220, 1 = HP DMM
  
    Dev(i) = ildev(BDINDEX, PRIMARY_ADDR_OF_DMM, NO_SECONDARY_ADDR, _
      TIMEOUT, EOTMODE, EOSMODE)
    If (ibsta And EERR) Then
      Call GpibErr("Error in initialization of GPIB device " & i + 1)
    Else
      write_status "Initialization step 1 of GPIB device " & i + 1 & " OK"
    End If
    
    ' reset the GPIB portion of the device
    ilclr Dev(i)
    If (ibsta And EERR) Then
      Call GpibErr("Error in clearing GPIB device " & i + 1 & " in initialize_devices sub")
    Else
      write_status "Initialization step 2 of GPIB device " & i + 1 & " OK"
    End If
  Next i
  
  initialize_devices_is_done = True
End Sub

Private Sub disconnect_device()
  ' Take the board offline
  ilonl GPIB0, 0
  If (ibsta And EERR) Then
    GpibErr ("Error disconnecting devices.")
  End If
End Sub

Private Sub read_devices()
  On Error GoTo myErrorHandler
  times_of_readings = times_of_readings + 1
          
  Call read_6220     ' first trigger by mnuStart_Click
  Call trigger_delta ' delta operation need 1 sec (in 6220 0.4 sec for 5 counts)

  ' Reading temperature from HP DMM = Dev(1)
  reading_from_all_devices = Space$(&H32)
    
  Call ilrd(Dev(1), reading_from_all_devices, Len(reading_from_all_devices))
  If (ibsta And EERR) Then
    GpibErr ("Error in receiving response to ilrd. ")
  End If

  reading_from_each_instrument(1) = Left$(reading_from_all_devices, ibcntl - 1)
    
  ''ReDim Preserve emf(1 To times_of_readings) As Double
  ''ReDim Preserve Temperature(1 To times_of_readings) As Double
  emf(times_of_readings) = Val(reading_from_each_instrument(1)) * 1000 ' in mV
  If emf(times_of_readings) < 0 Then ' -230 - 0 C (-5.891 mV to 0 mV), K Thermocouple
    Temperature(times_of_readings) = (25.173462 * emf(times_of_readings)) - _
      (1.1662878 * emf(times_of_readings) ^ 2) - _
      (1.0833638 * emf(times_of_readings) ^ 3) - _
      (0.8977354 * emf(times_of_readings) ^ 4) - _
      (0.37342377 * emf(times_of_readings) ^ 5) - _
      (0.086632643 * emf(times_of_readings) ^ 6) - _
      (0.010450598 * emf(times_of_readings) ^ 7) - _
      (0.00051920577 * emf(times_of_readings) ^ 8)
  ElseIf emf(times_of_readings) < 20.644 Then ' 0 - 500 C (0 mV to 20.644 mV), K Thermocouple
    Temperature(times_of_readings) = (25.08355 * emf(times_of_readings)) + _
      (0.07860106 * emf(times_of_readings) ^ 2) - _
      (0.2503131 * emf(times_of_readings) ^ 3) + _
      (0.0831527 * emf(times_of_readings) ^ 4) - _
      (0.01228034 * emf(times_of_readings) ^ 5) + _
      (0.0009804036 * emf(times_of_readings) ^ 6) - _
      (0.0000441303 * emf(times_of_readings) ^ 7) + _
      (0.000001057734 * emf(times_of_readings) ^ 8) - _
      (0.00000001052755 * emf(times_of_readings) ^ 9)
  Else  ' 500 - 1372 C (20.644 mV to 54.886 mV), K Thermocouple
    Temperature(times_of_readings) = -131.8058 + _
      (48.30222 * emf(times_of_readings)) - _
      (1.646031 * emf(times_of_readings) ^ 2) + _
      (0.05464731 * emf(times_of_readings) ^ 3) - _
      (0.0009650715 * emf(times_of_readings) ^ 4) + _
      (0.000008802193 * emf(times_of_readings) ^ 5) - _
      (0.0000000311081 * emf(times_of_readings) ^ 6)
  End If
  Temperature(times_of_readings) = Round(Temperature(times_of_readings), 1)
  'If emf(times_of_readings) >= 0 Then 'this is for T Type Thermocouple
    'Temperature(times_of_readings) = (25.928 * emf(times_of_readings)) + (-0.7602961 * emf(times_of_readings) ^ 2) + (0.04637791 * emf(times_of_readings) ^ 3) + (-0.002165394 * emf(times_of_readings) ^ 4) _
      '+ (0.00006048144 * emf(times_of_readings) ^ 5) + (-0.0000007293422 * emf(times_of_readings) ^ 6)
  'Else
    'Temperature(times_of_readings) = (25.949192 * emf(times_of_readings)) + (-0.21316967 * emf(times_of_readings) ^ 2) + (0.79018692 * emf(times_of_readings) ^ 3) + (0.42527777 * emf(times_of_readings) ^ 4) _
      '+ (0.13304473 * emf(times_of_readings) ^ 5) + (0.020241446 * emf(times_of_readings) ^ 6) + (0.0012668171 * emf(times_of_readings) ^ 7)
  'End If
  'Temperature(times_of_readings) = Round(Temperature(times_of_readings), 1)
    
  ' CORRECT TIME
  ''ReDim Preserve time_elapsed(times_of_readings) As Long
  ''ReDim Preserve time_clock(times_of_readings) As Date
 
  time_clock(times_of_readings) = Now
  time_elapsed(times_of_readings) = DateDiff("s", time_clock(1), _
  time_clock(times_of_readings)) + 1 ' + 1 so time_elapsed(1) not 0 or starts at 1 s
  time_remained(times_of_readings) = stop_time - time_elapsed(times_of_readings)
  
  ''ReDim Preserve voltage(1 To times_of_readings) As Double
  ''ReDim Preserve current(1 To times_of_readings) As Double
  ''ReDim Preserve resistance(1 To times_of_readings) As Double
  ''ReDim Preserve resistivity(1 To times_of_readings) As Double
  voltage(times_of_readings) = Val(reading_from_each_instrument(0))
  current(times_of_readings) = Val(txtCurrent.text) / 1000 'Val(reading_from_each_instrument(2))
  If current(times_of_readings) <> 0 Then
    resistance(times_of_readings) = Round(voltage(times_of_readings) / _
      current(times_of_readings), 13)
  Else
    resistance(times_of_readings) = Round(voltage(times_of_readings) / 0.000001, 13)
  End If
    
  resistivity(times_of_readings) = Round(resistance(times_of_readings) * Rho_factor, 14)
  If times_of_readings > 1 Then
    If time_elapsed(times_of_readings) <> time_elapsed(times_of_readings - 1) Then
      heating_or_cooling_rate = Round _
        (((Temperature(times_of_readings) - Temperature(times_of_readings - 1)) * 60 _
        / CDbl(time_elapsed(times_of_readings) - time_elapsed(times_of_readings - 1))), 2)
    Else ' =
      heating_or_cooling_rate = Round _
        (((Temperature(times_of_readings) - Temperature(times_of_readings - 1)) / _
        0.00000001), 2)
    End If
  End If

  If chkAllDataPoints = 1 Then
    'If txtMaxNumberDataX.text < 50 Then
      txtMaxNumberDataX.text = times_of_readings
    'Else
      'txtMaxNumberDataX.text = "50"
    'End If
  End If
  txtTotalPoints.text = time_elapsed(times_of_readings) ' real time in secons
  
  If time_elapsed(times_of_readings) > stop_time Then ' stop_time declared
    times_of_readings = times_of_readings - 1 ' so last data not save in save_file
    Call mnuStop_Click               ' in filter_is_OK sub and mnuStart_Click
    
    Exit Sub
  End If
  
  Call display_of_readings
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".read_devices)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub read_devices_non_delta()
  On Error GoTo myErrorHandler
  times_of_readings = times_of_readings + 1
          
  Call read_2821
  'Call trigger_delta

  ' Reading temperature from HP DMM = Dev(1)
  reading_from_all_devices = Space$(&H32)
    
  Call ilrd(Dev(1), reading_from_all_devices, Len(reading_from_all_devices))
  If (ibsta And EERR) Then
    GpibErr ("Error in receiving response to ilrd. ")
  End If

  reading_from_each_instrument(1) = Left$(reading_from_all_devices, ibcntl - 1)
    
  ''ReDim Preserve emf(1 To times_of_readings) As Double
  ''ReDim Preserve Temperature(1 To times_of_readings) As Double
  emf(times_of_readings) = Val(reading_from_each_instrument(1)) * 1000 ' in mV
  If emf(times_of_readings) < 0 Then ' -230 - 0 C (-5.891 mV to 0 mV), K Thermocouple
    Temperature(times_of_readings) = (25.173462 * emf(times_of_readings)) - _
      (1.1662878 * emf(times_of_readings) ^ 2) - _
      (1.0833638 * emf(times_of_readings) ^ 3) - _
      (0.8977354 * emf(times_of_readings) ^ 4) - _
      (0.37342377 * emf(times_of_readings) ^ 5) - _
      (0.086632643 * emf(times_of_readings) ^ 6) - _
      (0.010450598 * emf(times_of_readings) ^ 7) - _
      (0.00051920577 * emf(times_of_readings) ^ 8)
  ElseIf emf(times_of_readings) < 20.644 Then ' 0 - 500 C (0 mV to 20.644 mV), K Thermocouple
    Temperature(times_of_readings) = (25.08355 * emf(times_of_readings)) + _
      (0.07860106 * emf(times_of_readings) ^ 2) - _
      (0.2503131 * emf(times_of_readings) ^ 3) + _
      (0.0831527 * emf(times_of_readings) ^ 4) - _
      (0.01228034 * emf(times_of_readings) ^ 5) + _
      (0.0009804036 * emf(times_of_readings) ^ 6) - _
      (0.0000441303 * emf(times_of_readings) ^ 7) + _
      (0.000001057734 * emf(times_of_readings) ^ 8) - _
      (0.00000001052755 * emf(times_of_readings) ^ 9)
  Else  ' 500 - 1372 C (20.644 mV to 54.886 mV), K Thermocouple
    Temperature(times_of_readings) = -131.8058 + _
      (48.30222 * emf(times_of_readings)) - _
      (1.646031 * emf(times_of_readings) ^ 2) + _
      (0.05464731 * emf(times_of_readings) ^ 3) - _
      (0.0009650715 * emf(times_of_readings) ^ 4) + _
      (0.000008802193 * emf(times_of_readings) ^ 5) - _
      (0.0000000311081 * emf(times_of_readings) ^ 6)
  End If
  Temperature(times_of_readings) = Round(Temperature(times_of_readings), 1)
  'If emf(times_of_readings) >= 0 Then 'this is for T Type Thermocouple
    'Temperature(times_of_readings) = (25.928 * emf(times_of_readings)) + (-0.7602961 * emf(times_of_readings) ^ 2) + (0.04637791 * emf(times_of_readings) ^ 3) + (-0.002165394 * emf(times_of_readings) ^ 4) _
      '+ (0.00006048144 * emf(times_of_readings) ^ 5) + (-0.0000007293422 * emf(times_of_readings) ^ 6)
  'Else
    'Temperature(times_of_readings) = (25.949192 * emf(times_of_readings)) + (-0.21316967 * emf(times_of_readings) ^ 2) + (0.79018692 * emf(times_of_readings) ^ 3) + (0.42527777 * emf(times_of_readings) ^ 4) _
      '+ (0.13304473 * emf(times_of_readings) ^ 5) + (0.020241446 * emf(times_of_readings) ^ 6) + (0.0012668171 * emf(times_of_readings) ^ 7)
  'End If
  'Temperature(times_of_readings) = Round(Temperature(times_of_readings), 1)
    
  ' CORRECT TIME
  ''ReDim Preserve time_elapsed(times_of_readings) As Long
  ''ReDim Preserve time_clock(times_of_readings) As Date
 
  time_clock(times_of_readings) = Now
  time_elapsed(times_of_readings) = DateDiff("s", time_clock(1), _
  time_clock(times_of_readings)) + 1 ' + 1 so time_elapsed(1) not 0 or starts at 1 s
  time_remained(times_of_readings) = stop_time - time_elapsed(times_of_readings)
  
  ''ReDim Preserve voltage(1 To times_of_readings) As Double
  ''ReDim Preserve current(1 To times_of_readings) As Double
  ''ReDim Preserve resistance(1 To times_of_readings) As Double
  ''ReDim Preserve resistivity(1 To times_of_readings) As Double
  voltage(times_of_readings) = Val(reading_from_each_instrument(0))
  current(times_of_readings) = Val(txtCurrent.text) / 1000 'Val(reading_from_each_instrument(2))
  If current(times_of_readings) <> 0 Then
    resistance(times_of_readings) = Round(voltage(times_of_readings) / _
      current(times_of_readings), 13)
  Else
    resistance(times_of_readings) = Round(voltage(times_of_readings) / 0.000001, 13)
  End If
    
  resistivity(times_of_readings) = Round(resistance(times_of_readings) * Rho_factor, 14)
  If times_of_readings > 1 Then
    If time_elapsed(times_of_readings) <> time_elapsed(times_of_readings - 1) Then
      heating_or_cooling_rate = Round _
        (((Temperature(times_of_readings) - Temperature(times_of_readings - 1)) * 60 _
        / CDbl(time_elapsed(times_of_readings) - time_elapsed(times_of_readings - 1))), 2)
    Else ' =
      heating_or_cooling_rate = Round _
        (((Temperature(times_of_readings) - Temperature(times_of_readings - 1)) / _
        0.00000001), 2)
    End If
  End If

  If chkAllDataPoints = 1 Then
    'If txtMaxNumberDataX.text < 50 Then
      txtMaxNumberDataX.text = times_of_readings
    'Else
      'txtMaxNumberDataX.text = "50"
    'End If
  End If
  txtTotalPoints.text = time_elapsed(times_of_readings) ' real time in secons
  
  If time_elapsed(times_of_readings) > stop_time Then ' stop_time declared
    times_of_readings = times_of_readings - 1 ' so last data not save in save_file
    Call mnuStop_Click               ' in filter_is_OK sub and mnuStart_Click
    
    Exit Sub
  End If
  
  Call display_of_readings
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".read_devices)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub read_6220() ' Dev(0) = Keithley 6220
On Error GoTo myErrorHandler
  reading_from_all_devices = Space$(200)
  
  ' Send command or query
  Call ilwrt(Dev(0), ":trace:data?", 12)
  If (ibsta And EERR) Then
    GpibErr ("Error sending ':trace:data?'. ")
  End If
  
  ' Receive response
  Call ilrd(Dev(0), reading_from_all_devices, Len(reading_from_all_devices))
  If (ibsta And EERR) Then
    GpibErr ("Error receiving response of 'trace:data?'")
  End If

  'reading_from_each_instrument(k) = Left$(reading_from_all_devices, ibcntl - 1)
  'reading_from_each_instrument(1) = reading_from_all_devices
  
  txtResponse.text = reading_from_all_devices
    
  Dim splitted() As String
  Dim sum As Double
  Dim count As Integer
  
  splitted = Split(reading_from_all_devices, ",")
  count = UBound(splitted) + 1
  
  Dim i As Integer
  For i = 0 To count - 1
    sum = sum + Val(splitted(i))
  Next i
  reading_from_each_instrument(0) = Round(sum / count, 9)
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".read_6220)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub read_2821() ' Dev(2) = Keithley 2821
On Error GoTo myErrorHandler
  
  Call ilwrt(Dev(2), ":trig:sour:imm", 13)
  If (ibsta And EERR) Then
    GpibErr ("Error sending ':trig:sou:imm'. ")
  End If
  
  Call ilwrt(Dev(2), ":samp:coun 2", 12)
  If (ibsta And EERR) Then
    GpibErr ("Error sending ':samp:coun 2'. ")
  End If
  
  reading_from_all_devices = Space$(200)
  ' Send command or query
  Call ilwrt(Dev(2), ":read?", 6)
  If (ibsta And EERR) Then
    GpibErr ("Error sending ':read?'. ")
  End If
  
  ' Receive response
  Call ilrd(Dev(2), reading_from_all_devices, Len(reading_from_all_devices))
  If (ibsta And EERR) Then
    GpibErr ("Error receiving response of 'read?'")
  End If

  'reading_from_each_instrument(k) = Left$(reading_from_all_devices, ibcntl - 1)
  'reading_from_each_instrument(1) = reading_from_all_devices
  
  txtResponse.text = reading_from_all_devices
    
  Dim splitted() As String
  Dim sum As Double
  Dim count As Integer
  
  splitted = Split(reading_from_all_devices, ",")
  'count = UBound(splitted) + 1
  
  'Dim i As Integer
  ''For i = 0 To count - 1
    sum = sum + Val(splitted(i))
  'Next i
  reading_from_each_instrument(0) = splitted(0) 'Round(sum / count, 9)
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".read_2182)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub read_devices_only_to_display()
On Error GoTo myErrorHandler
  Call read_6220     ' first trigger by mnuInitialize_Click
  Call trigger_delta ' delta operation need 1 sec (in 6220 0.4 sec for 5 counts)

  ' Reading temperature from HP DMM = Dev(1)
  reading_from_all_devices = Space$(&H32)
    
  Call ilrd(Dev(1), reading_from_all_devices, Len(reading_from_all_devices))
  If (ibsta And EERR) Then
    GpibErr ("Error receiving response from HP 3478A.")
  End If
  reading_from_each_instrument(1) = Left$(reading_from_all_devices, ibcntl - 1)
    
  Dim emf2 As Double
  emf2 = Val(reading_from_each_instrument(1)) * 1000 ' in mV
  If emf2 < 0 Then ' -230 - 0 C (-5.891 mV to 0 mV), K Thermocouple
    Temperature2 = (25.173462 * emf2) - (1.1662878 * emf2 ^ 2) - _
      (1.0833638 * emf2 ^ 3) - (0.8977354 * emf2 ^ 4) - _
      (0.37342377 * emf2 ^ 5) - (0.086632643 * emf2 ^ 6) - _
      (0.010450598 * emf2 ^ 7) - (0.00051920577 * emf2 ^ 8)
  ElseIf emf2 < 20.644 Then ' 0 - 500 C (0 mV to 20.644 mV), K Thermocouple
    Temperature2 = (25.08355 * emf2) + (0.07860106 * emf2 ^ 2) - _
      (0.2503131 * emf2 ^ 3) + (0.0831527 * emf2 ^ 4) - _
      (0.01228034 * emf2 ^ 5) + (0.0009804036 * emf2 ^ 6) - _
      (0.0000441303 * emf2 ^ 7) + (0.000001057734 * emf2 ^ 8) - _
      (0.00000001052755 * emf2 ^ 9)
  Else  ' 500 - 1372 C (20.644 mV to 54.886 mV), K Thermocouple
    Temperature2 = -131.8058 + (48.30222 * emf2) - (1.646031 * emf2 ^ 2) + _
      (0.05464731 * emf2 ^ 3) - (0.0009650715 * emf2 ^ 4) + _
      (0.000008802193 * emf2 ^ 5) - (0.0000000311081 * emf2 ^ 6)
  End If
  Temperature2 = Round(Temperature2, 1)
  
  'If emf2 >= 0 Then 'this is for T Type thermocouple
    'Temperature2 = (25.928 * emf2) + (-0.7602961 * emf2 ^ 2) + _
    '(0.04637791 * emf2 ^ 3) + (-0.002165394 * emf2 ^ 4) _
    '+ (0.00006048144 * emf2 ^ 5) + (-0.0000007293422 * emf2 ^ 6)
  'Else
    'Temperature2 = (25.949192 * emf2) + (-0.21316967 * emf2 ^ 2) + _
    '(0.79018692 * emf2 ^ 3) + (0.42527777 * emf2 ^ 4) _
    '+ (0.13304473 * emf2 ^ 5) + (0.020241446 * emf2 ^ 6) + (0.0012668171 * emf2 ^ 7)
  'End If
  'Temperature2 = Round(Temperature2, 1)
       
  voltage2 = Val(reading_from_each_instrument(0)) ' in V
  current2 = Val(txtCurrent.text) / 1000 ' in A 'Val(reading_from_each_instrument(2))
  
  If current2 <> 0 Then
    resistance2 = Round(voltage2 / current2, 13) ' in Ohm
  Else
    resistance2 = Round(voltage2 / 0.000001, 13) ' this is to avoid dividing by 0
  End If
  
  resistivity2 = Round(resistance2 * Rho_factor, 14) ' in Ohm.m
      
  Call display_of_readings2
  
  'tableResult.ClearContent
  
  ''add_row "", CStr(Temperature2), CStr(resistivity2), CStr(resistance2), _
  CStr(voltage2), CStr(current2), ""
  
  ''tableResult.Populate
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".read_devices_only_to_display)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub read_dev_only_display_non_delta()
On Error GoTo myErrorHandler
  Call read_2821
  'Call trigger_delta

  ' Reading temperature from HP DMM = Dev(1)
  reading_from_all_devices = Space$(&H32)
    
  Call ilrd(Dev(1), reading_from_all_devices, Len(reading_from_all_devices))
  If (ibsta And EERR) Then
    GpibErr ("Error receiving response from HP 3478A.")
  End If
  reading_from_each_instrument(1) = Left$(reading_from_all_devices, ibcntl - 1)
    
  Dim emf2 As Double
  emf2 = Val(reading_from_each_instrument(1)) * 1000 ' in mV
  If emf2 < 0 Then ' -230 - 0 C (-5.891 mV to 0 mV), K Thermocouple
    Temperature2 = (25.173462 * emf2) - (1.1662878 * emf2 ^ 2) - _
      (1.0833638 * emf2 ^ 3) - (0.8977354 * emf2 ^ 4) - _
      (0.37342377 * emf2 ^ 5) - (0.086632643 * emf2 ^ 6) - _
      (0.010450598 * emf2 ^ 7) - (0.00051920577 * emf2 ^ 8)
  ElseIf emf2 < 20.644 Then ' 0 - 500 C (0 mV to 20.644 mV), K Thermocouple
    Temperature2 = (25.08355 * emf2) + (0.07860106 * emf2 ^ 2) - _
      (0.2503131 * emf2 ^ 3) + (0.0831527 * emf2 ^ 4) - _
      (0.01228034 * emf2 ^ 5) + (0.0009804036 * emf2 ^ 6) - _
      (0.0000441303 * emf2 ^ 7) + (0.000001057734 * emf2 ^ 8) - _
      (0.00000001052755 * emf2 ^ 9)
  Else  ' 500 - 1372 C (20.644 mV to 54.886 mV), K Thermocouple
    Temperature2 = -131.8058 + (48.30222 * emf2) - (1.646031 * emf2 ^ 2) + _
      (0.05464731 * emf2 ^ 3) - (0.0009650715 * emf2 ^ 4) + _
      (0.000008802193 * emf2 ^ 5) - (0.0000000311081 * emf2 ^ 6)
  End If
  Temperature2 = Round(Temperature2, 1)
  
  'If emf2 >= 0 Then 'this is for T Type thermocouple
    'Temperature2 = (25.928 * emf2) + (-0.7602961 * emf2 ^ 2) + _
    '(0.04637791 * emf2 ^ 3) + (-0.002165394 * emf2 ^ 4) _
    '+ (0.00006048144 * emf2 ^ 5) + (-0.0000007293422 * emf2 ^ 6)
  'Else
    'Temperature2 = (25.949192 * emf2) + (-0.21316967 * emf2 ^ 2) + _
    '(0.79018692 * emf2 ^ 3) + (0.42527777 * emf2 ^ 4) _
    '+ (0.13304473 * emf2 ^ 5) + (0.020241446 * emf2 ^ 6) + (0.0012668171 * emf2 ^ 7)
  'End If
  'Temperature2 = Round(Temperature2, 1)
       
  voltage2 = Val(reading_from_each_instrument(0)) ' in V
  current2 = Val(txtCurrent.text) / 1000 ' in A 'Val(reading_from_each_instrument(2))
  
  If current2 <> 0 Then
    resistance2 = Round(voltage2 / current2, 13) ' in Ohm
  Else
    resistance2 = Round(voltage2 / 0.000001, 13) ' this is to avoid dividing by 0
  End If
  
  resistivity2 = Round(resistance2 * Rho_factor, 14) ' in Ohm.m
      
  Call display_of_readings2
  
  'tableResult.ClearContent
  
  ''add_row "", CStr(Temperature2), CStr(resistivity2), CStr(resistance2), _
  CStr(voltage2), CStr(current2), ""
  
  ''tableResult.Populate
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".read_devices_only_to_display)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub read_data()
On Error GoTo myErrorHandler
  ' path_and_file_name assigned in mnuOpenMethod
  Set mFile = mFileSysObj.GetFile(path_and_file_name)
  Set mTxtStream = mFile.OpenAsTextStream(ForReading)
  
  Dim count As Long
  Dim count_prog As Long
  Dim count_first As Long
  Dim string_read As String
  Dim splitted() As String
  
  Dim count_data_start As Long
  Dim count_tot As Long
  count_tot = 0
  Do While (mTxtStream.AtEndOfStream = False)
    count_tot = count_tot + 1
    string_read = mTxtStream.ReadLine
    If Left$(string_read, 7) = "Time(s)" Then
      count_data_start = count_tot ' data starts actually >= count_data_start + 6
    End If                         ' but this separator count is correct in the following
  Loop
  
  tableResult.ClearContent
  
  times_of_readings = count_tot - count_data_start
  
  If times_of_readings > 7200 Then
    Dim answer As Long
    answer = MsgBox("The data points are more than " & times_of_readings & _
      ". It would take a long time to load. Continue ?", _
      vbYesNo + vbQuestion, "Data Points Too Many")
    If answer = vbYes Then
      ' OK
    Else
      Exit Sub
    End If
  End If
  
  txtMaxNumberDataX.text = CStr(times_of_readings)
  
  ReDim time_elapsed(times_of_readings) As Long
  ReDim Temperature(1 To times_of_readings) As Double
  ReDim voltage(1 To times_of_readings) As Double
  ReDim current(1 To times_of_readings) As Double
  ReDim resistance(1 To times_of_readings) As Double
  ReDim resistivity(1 To times_of_readings) As Double
    
     
  Set mTxtStream = mFile.OpenAsTextStream(ForReading)
  
  count = 0
  count_prog = 0
  
  ProgressBar1.Visible = True
  Do While (mTxtStream.AtEndOfStream = False)
    count = count + 1
    string_read = mTxtStream.ReadLine
    
    If count > count_data_start Then 'read experiment method/info
      splitted = Split(string_read, " ") ' all lines splitted during read line
          
      ' The data has 8 columns BUT any line with 8 words WILL be inserted to table
      If UBound(splitted) = 6 Then ' e.g. comment and name if 8 words can be inserted but unlikely
        add_row splitted(0), splitted(1), splitted(2), splitted(3), splitted(4), splitted(5), splitted(6)
        ' Apply changes
        tableResult.Populate
        ProgressBar1.value = count * (1000 / count_tot)

        time_elapsed(count - count_data_start) = splitted(0)
        Temperature(count - count_data_start) = splitted(1)
        resistivity(count - count_data_start) = splitted(2)
        resistance(count - count_data_start) = splitted(3)
        voltage(count - count_data_start) = splitted(4)
        current(count - count_data_start) = splitted(5)
      End If
    End If
  Loop
  tableResult.Navigator.MoveLastRow
  ProgressBar1.Visible = False
    
  Call cmbSelectXAxis_Click ' to show the chart
  
  Call mTxtStream.Close 'closing file
      
  Exit Sub
myErrorHandler:
  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".read_data)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".read_data", vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub read_method()
On Error GoTo myErrorHandler
  ' path_and_file_name assigned in mnuOpenMethod
  Set mFile = mFileSysObj.GetFile(path_and_file_name)
  Set mTxtStream = mFile.OpenAsTextStream(ForReading)
  
  Dim string_read As String
  Dim splitted() As String
  
  Do While (mTxtStream.AtEndOfStream = False)
    string_read = mTxtStream.ReadLine
    
    splitted = Split(string_read, ":")
    If UBound(splitted) > 0 Then
      If Trim(splitted(0)) = "Operator" Then
        txtOperatorName.text = Trim(splitted(1))
      ElseIf Trim(splitted(0)) = "File Name" Then
        txtFileName.text = Trim(splitted(1))
      ElseIf Trim(splitted(0)) = "File Path" Then
        txtFile.text = Trim(splitted(1) & ":" & splitted(2))
      ElseIf Trim(splitted(0)) = "Sample ID" Then
        If Right(Trim(splitted(1)), 3) = "ar)" Then
          txtSampleID.text = Replace(Trim(splitted(1)), "(Rectangular)", "")
          optRectangular.value = True
        Else
          txtSampleID.text = Replace(Trim(splitted(1)), "(Cylinder)", "")
          optCylinder.value = True
        End If
      ElseIf Trim(splitted(0)) = "Length" Then
        txtSampleLength.text = Replace(Trim(splitted(1)), "mm", "")
      ElseIf Trim(splitted(0)) = "Width" Then
        txtSampleWidth.text = Replace(Trim(splitted(1)), "mm", "")
      ElseIf Trim(splitted(0)) = "Thickness" Then
        txtSampleThickness.text = Replace(Trim(splitted(1)), "mm", "")
      ElseIf Trim(splitted(0)) = "Comment" Then
        txtComment.text = Trim(splitted(1))
      ElseIf Trim(splitted(0)) = "# Devices" Then
        txtNumberOfDevices.text = Trim(splitted(1))
      ElseIf Trim(splitted(0)) = "Address" Then
        txtAddress.text = Trim(splitted(1))
      ElseIf Trim(splitted(0)) = "Current" Then
        txtCurrent.text = Replace(Trim(splitted(1)), "mA", "")
      ElseIf Trim(splitted(0)) = "Compliance" Then
        txtCompliance.text = Replace(Trim(splitted(1)), "V", "")
      ElseIf Trim(splitted(0)) = "Count" Then
        txtDeltaCount.text = Trim(splitted(1))
      ElseIf Trim(splitted(0)) = "Delay" Then
        txtDelay.text = Replace(Trim(splitted(1)), "s", "")
      ElseIf Trim(splitted(0)) = "Analog Filter" Then
        cboAnalogFilter.text = Trim(splitted(1))
      ElseIf Trim(splitted(0)) = "Digital Filter" Then
        cboDigitalFilter.text = Trim(splitted(1))
      ElseIf Trim(splitted(0)) = "Digital Filter Count" Then
        txtDigitalFilterCount.text = Trim(splitted(1))
      ElseIf Trim(splitted(0)) = "Rate" Then
        txtRate.text = Replace(Trim(splitted(1)), "NPLCs", "")
      ElseIf Trim(splitted(0)) = "Voltage Range" Then
        cboVoltageRange.text = Replace(Trim(splitted(1)), "V", "")
      ElseIf Trim(splitted(0)) = "Data Points" Then
        txtDataPoints.text = Replace(Trim(splitted(1)), "s/pt", "")
        Exit Do ' correct if reading rho file
      End If
    End If
  Loop
  
  Call mTxtStream.Close
    
  Exit Sub
myErrorHandler:
  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".read_method)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".read_method", vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub save_method() ' same as save_file info part
  On Error GoTo myErrorHandler
  'path_and_file_name = txtFile.text ' set in dialog window
  
  mFileSysObj.CreateTextFile (path_and_file_name) 'if already there, destroying the old file
  Set mFile = mFileSysObj.GetFile(path_and_file_name)
  Set mTxtStream = mFile.OpenAsTextStream(ForWriting)
      
  experiment_info = program_name
  experiment_info = experiment_info & "Operator  : " & txtOperatorName.text & vbCrLf
  experiment_info = experiment_info & "File Name : " & txtFileName.text & vbCrLf
  experiment_info = experiment_info & "File Path : " & txtFile.text & vbCrLf
  If optRectangular.value = True Then
    experiment_info = experiment_info & "Sample ID : " & txtSampleID.text & _
      " (Rectangular)" & vbCrLf
  Else
    experiment_info = experiment_info & "Sample ID : " & txtSampleID.text & _
      " (Cylinder)" & vbCrLf
  End If
  experiment_info = experiment_info & "Length    : " & txtSampleLength.text & _
    " mm" & vbCrLf
  If optRectangular.value = True Then
    experiment_info = experiment_info & "Width     : " & txtSampleWidth.text & _
      " mm" & vbCrLf
    experiment_info = experiment_info & "Thickness : " & txtSampleThickness.text & _
      " mm" & vbCrLf
  Else
    experiment_info = experiment_info & "Diameter  : " & txtSampleWidth.text & _
      " mm" & vbCrLf
  End If
  experiment_info = experiment_info & "Comment : " & txtComment.text & vbCrLf & vbCrLf
  
  experiment_info = experiment_info & "Application Settings : " & vbCrLf
  
  experiment_info = experiment_info & "# Devices  : " & txtNumberOfDevices.text & vbCrLf
  experiment_info = experiment_info & "Address    : " & txtAddress.text & vbCrLf
  experiment_info = experiment_info & "Current    : " & txtCurrent.text & _
    " mA" & vbCrLf
  experiment_info = experiment_info & "Compliance : " & txtCompliance.text & _
    " V" & vbCrLf
  experiment_info = experiment_info & "Count      : " & txtDeltaCount.text & vbCrLf
  experiment_info = experiment_info & "Delay      : " & txtDelay.text & _
    " s" & vbCrLf
  experiment_info = experiment_info & "Analog Filter  : " & cboAnalogFilter.text & vbCrLf
  experiment_info = experiment_info & "Digital Filter : " & cboDigitalFilter.text & vbCrLf
  experiment_info = experiment_info & "Digital Filter Count : " & _
    txtDigitalFilterCount.text & vbCrLf
  experiment_info = experiment_info & "Rate       : " & txtRate.text & _
    " NPLCs" & vbCrLf
  experiment_info = experiment_info & "Voltage Range : " & cboVoltageRange.text & _
    " V" & vbCrLf
  experiment_info = experiment_info & "Stop Time : " & txtStopTime.text & _
    " min" & vbCrLf
  experiment_info = experiment_info & "Data Points : " & txtDataPoints.text & _
    " s/pt" & vbCrLf & "" & vbCrLf
      
  mTxtStream.WriteLine (experiment_info) 'two blank lines written at the bottom ?
            
  Call mTxtStream.Close 'closing file for writing
  
  Exit Sub
myErrorHandler:
  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".save_method)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".save_method", vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub cboDisplay_Click(Index As Integer)
  'lblDisplay(Index).Caption = cboDisplay(Index).Text
  'later to get better
  Select Case cboDisplay(Index).text
    Case "Elapsed Time"
      lblDisplay(Index).Caption = "Elapsed Time (s) :"
    Case "Temperature"
      lblDisplay(Index).Caption = "Temperature (C) :"
    Case "Remaining Time"
      lblDisplay(Index).Caption = "Remaining Time (s) :"
    Case "Voltage"
      lblDisplay(Index).Caption = "Voltage (V) :"
    Case "Current"
      lblDisplay(Index).Caption = "Current (A) :"
    Case "Resistance"
      lblDisplay(Index).Caption = "Resistance (Ohm) :"
    Case "Resistivity"
      lblDisplay(Index).Caption = "Resistivity (Ohm.m) :"
    Case "Ramping Rate"
      lblDisplay(Index).Caption = "Ramping (K/min) :"
  End Select
End Sub

Private Sub cmbSelectXAxis_Click()
  On Error GoTo myErrorHandler
    
  If cmbSelectXAxis.text <> XAxisTitle Then ' axis title changed
    mnuAutoScale.Checked = True
  End If
  
  ' This block for charting experiment data, app is not connected to devices
  If devices_are_connected = False Then
    If cmbSelectXAxis.text = "Time (h:m:s)" Then
      MsgBox "Data 'Time(h:m:s)' not available"
      'cmbSelectXAxis.text = "Time (s)"
      Exit Sub
    End If
    
    XAxisTitle = cmbSelectXAxis.text ' save the last x axis title
    
    Call ChartViewer1.updateViewPort(True, False) ' for drawing experiment file
    
    Exit Sub
  End If
  
  ' This block for charting of reading from devices, app is connected to devices
  If Toolbar1.Buttons.item(6).Enabled = True Then ' Item(6) is chart button
    Select Case cmbSelectXAxis.text
      Case "Time (h:m:s)"
        nextDateTime = getCurrentTime()
        For i = 0 To UBound(timeStamps)
          timeStamps(i) = cd.NoValue
          dataSeries(i) = cd.NoValue
        Next
        tmrShiftData.Enabled = True
      Case Else   'Time (s) & Temperature
        tmrShiftData.Enabled = False
    End Select
    
    XAxisTitle = cmbSelectXAxis.text ' save the last x axis title
    
    tmrDrawChart.Enabled = True
  End If
    
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".cmbSelectXAxix_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub cmbSelectYAxis_Click()
  On Error GoTo myErrorHandler
  
  If cmbSelectYAxis.text <> YAxisTitle Then ' axis title changed
    mnuAutoScale.Checked = True
  End If
  
  ' This block for charting experiment data, app is not connected to devices
  If devices_are_connected = False Then
    If cmbSelectXAxis.text = "Time (h:m:s)" Then
      MsgBox "Data 'Time(h:m:s)' not available"
      'cmbSelectXAxis.text = "Time (s)"
      Exit Sub
    End If
    
    YAxisTitle = cmbSelectYAxis.text ' save the last y axis title
    
    Call ChartViewer1.updateViewPort(True, False) ' for drawing experiment file
    
    Exit Sub
  End If
  
  ' This block for charting of reading from devices, app is connected to devices
  For i = 0 To UBound(timeStamps)
    timeStamps(i) = cd.NoValue
    dataSeries(i) = cd.NoValue
  Next
  YAxisTitle = cmbSelectYAxis.text ' save the last y axis title
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".cmbSelectYAxis_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub cmdAdvancedChart_Click()
  frmSettings.Visible = True
End Sub

Private Sub mnuAbout_Click()
  frmAbout.Show
End Sub

Private Sub mnuAutoScale_Click()
  If mnuAutoScale.Checked = False Then
    frmSettings.chkCustomScale.value = 0
    frmSettings.chkCustomScale.Enabled = False
    
    mnuAutoScale.Checked = True
  Else
    frmSettings.chkCustomScale.Enabled = True
    
    mnuAutoScale.Checked = False
  End If
End Sub

Private Sub mnuChartOn_Click()
  mnuAutoScale.Checked = True ' force must be auto scale
  If charting_is_running = False Then
    Call cmbSelectXAxis_Click
    mnuChartOn.Checked = True
    charting_is_running = True
  Else 'charting stopped
    mnuChartOn.Checked = False
    tmrShiftData.Enabled = False
    tmrDrawChart.Enabled = False
    charting_is_running = False
  End If
End Sub

Private Sub mnuPlot_Click() ' data to be plotted is from the table
  On Error GoTo myErrorHandler ' in read_data sub, data to be plotted is from reading file
  Dim row As ReportRow
  Dim num As Integer
  'tableResult.Rows.count
  num = 0
  For Each row In tableResult.SelectedRows
    num = num + 1
    
    ReDim Preserve time_elapsed(num) As Long
    ReDim Preserve Temperature(1 To num) As Double
    ReDim Preserve voltage(1 To num) As Double
    ReDim Preserve current(1 To num) As Double
    ReDim Preserve resistance(1 To num) As Double
    ReDim Preserve resistivity(1 To num) As Double
    
    time_elapsed(num) = tableResult.Rows(row.Index).Record(0).value
    Temperature(num) = tableResult.Rows(row.Index).Record(1).value
    resistivity(num) = tableResult.Rows(row.Index).Record(2).value
    resistance(num) = tableResult.Rows(row.Index).Record(3).value
    voltage(num) = tableResult.Rows(row.Index).Record(4).value
    current(num) = tableResult.Rows(row.Index).Record(5).value
  Next
  txtMaxNumberDataX.text = CStr(num)
  
  Call cmbSelectXAxis_Click ' draw data
   
Exit Sub
myErrorHandler:
  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".mnuPlot_Click)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".mnuPlotClick", vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub mnuDeleteRows_Click()
  On Error GoTo myErrorHandler
  Dim row As ReportRow
  For Each row In tableResult.SelectedRows
    tableResult.Records.RemoveAt (row.Record.Index)
  Next
  tableResult.Populate
  
  Exit Sub
myErrorHandler:
  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".mnuDeleteRows_Click)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".mnuDeleteRows_Click", vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub mnuCopyToClipboard_Click()
  On Error GoTo myErrorHandler
  Dim row As ReportRow
  Dim text_copy As String
 
  For Each row In tableResult.SelectedRows
    text_copy = text_copy & tableResult.Rows(row.Index).Record(0).value & " "
    text_copy = text_copy & tableResult.Rows(row.Index).Record(1).value & " "
    text_copy = text_copy & tableResult.Rows(row.Index).Record(2).value & " "
    text_copy = text_copy & tableResult.Rows(row.Index).Record(3).value & " "
    text_copy = text_copy & tableResult.Rows(row.Index).Record(4).value & " "
    text_copy = text_copy & tableResult.Rows(row.Index).Record(5).value & " "
    text_copy = text_copy & tableResult.Rows(row.Index).Record(6).value & vbCrLf
  Next
  
  Clipboard.Clear
  Clipboard.SetText text_copy
  
  Exit Sub
myErrorHandler:
  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".mnuCopyToClipboard_Click)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".mnuCopyToClipboard_Click", vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub mnuExit_Click()
  Unload Me 'actually this calls Form_UnLoadQuery to quit the program or not
  End ' This clean exit, without this the app still there when closed
End Sub

Private Sub mnuHelpChild_Click()
  MsgBox "Help file not available yet. Ask DPBA for help !"
End Sub

Private Sub mnuOpenData_Click()
  On Error GoTo myErrorHandler
  
  If devices_are_connected = True Then
    MsgBox "The software can only open data file if the devices are not connected !"
    Exit Sub
  End If
  
  dlgFile.FLAGS = cdlOFNFileMustExist
  dlgFile.CancelError = True
  dlgFile.Filter = "Exp Data (*.rho)|*.rho"
  'dlgFile.filename = txtFile.Text
  dlgFile.DialogTitle = "Open Experiment Data"
  
  dlgFile.ShowOpen
  
  path_and_file_name = dlgFile.filename
      
  Call read_data
         
  Exit Sub
myErrorHandler:
  If Err.Number = cdlCancel Then Exit Sub

  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".mnuOpenData_Click)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".mnuOpenData_Click", vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub mnuOpenMethod_Click()
  On Error GoTo myErrorHandler
  
  dlgFile.FLAGS = cdlOFNFileMustExist
  dlgFile.CancelError = True
  dlgFile.Filter = "Exp Method (*.met)|*.met|Exp Method (*.rho)|*.rho"
  'dlgFile.filename = txtFile.Text
  dlgFile.DialogTitle = "Open Method"
  
  dlgFile.ShowOpen
  
  path_and_file_name = dlgFile.filename
      
  Call read_method
     
  Exit Sub
myErrorHandler:
  If Err.Number = cdlCancel Then Exit Sub

  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".mnuOpenMethod_Click)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".mnuOpenMethod_Click", vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub mnuSaveMethod_Click()
On Error GoTo myErrorHandler
  dlgFile.FLAGS = cdlOFNOverwritePrompt
  dlgFile.CancelError = True
  dlgFile.Filter = "Exp Method (*.met)|*.met"
  'dlgFile.filename = txtFile.Text
  dlgFile.DialogTitle = "Save Method"
  
  dlgFile.ShowSave
  
  path_and_file_name = dlgFile.filename
      
  Call save_method
   
  Exit Sub
myErrorHandler:
  If Err.Number = cdlCancel Then Exit Sub

  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".mnuSaveMethod_Click)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".mnuSaveMethod_Click", vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub mnuSaveChart_Click()
  On Error GoTo myErrorHandler
  dlgFile.DialogTitle = "Save Chart Image"
  dlgFile.FLAGS = cdlOFNOverwritePrompt
  dlgFile.CancelError = True
  dlgFile.Filter = "Bitmap File (*.bmp)|*.bmp|PNG Files (*.png)|*.png|" & _
    "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|" & _
    "GIF Files (*.gif)|*.gif|WBMP Files (*.wbmp)|*.wbmp|" & _
    "WMP Files (*.wmp)|*.wmp|SVG Files (*.svg)|*.svg|" & _
    "SVGZ Files (*.svgz)|*.svgz|"
  
  dlgFile.filename = "deizRhoChart"
  
  ' To output true vector graphics in SVG or SVGZ format,
  ' please ensure BaseChart.enableVectorOutput is called
  ' immediately after creating the BaseChart object. (NOT YET BY ME)
  ' Otherwise the output will be a bitmap image embedded in SVG or SVGZ.
  dlgFile.ShowSave
    
  c.makeChart dlgFile.filename
  
  Exit Sub
myErrorHandler:
  If Err.Number = cdlCancel Then Exit Sub
  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".mnuSaveChart)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".mnuSaveChart", vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub mnuPause_Click()
  Toolbar1.Buttons.item(2).Enabled = True  'start button
  Toolbar1.Buttons.item(3).Enabled = True  'stop button
  Toolbar1.Buttons.item(4).Enabled = False  'pause button
  Toolbar1.Buttons.item(6).Enabled = False  'chart button
  
  Call mnuChartOn_Click
  
  experiment_is_paused = True
    
  tmrReadDevices.Enabled = False
    
  tmrDrawChart.Enabled = False
  tmrShiftData.Enabled = False
  tmrReadDevicesOnlyToDisplay.Enabled = True ' reading bu not saved
  
  write_status "Experiment is paused"
End Sub

Private Sub mnuSettings_Click()
  frmSettings.Visible = True
End Sub

Private Sub mnuStart_Click()
On Error GoTo myErrorHandler
  If devices_are_connected = False Then
    MsgBox "Devices are not connected yet !"
    Exit Sub
  End If

  If Not filter_result_is_OK Then Exit Sub
  
  ' stop_time is updated in filter_result_is_OK function
  ' data_points is updated in filter_result_is_OK function, used in redim_variables below
  
  If method_is_applied = False Then
    'MsgBox "Method has not been confirmed." & vbCr & _
      "Press Apply button to confirm method.", vbExclamation, _
      "Experiment can not be started"
    'Exit Sub
  End If
   
  Call validate_file_name

  If experiment_is_paused = False Then
    experiment_info = program_name & " (c) 2007" & vbCrLf
    experiment_info = experiment_info & "Experiment starts at " & Now & vbCrLf
  
    times_of_readings = 0
    Call redim_variables
  End If
    
  ' *** MAIN CODES
  tmrReadDevicesOnlyToDisplay.Enabled = False 'reading while not save disabled
    
  'Call trigger_delta ' First trigger before first reading
  'Sleep 1000 * Val(txtDataPoints.text) ' delta operation needs 350 sec for 5 counts
    
  tmrReadDevices.Enabled = True
  ' ************************************************************************************
   
  experiment_is_running = True

  write_status "Experiment starts"
              
  Call input_boxes_off
  
  charting_is_running = False ' set false before mnu_chartOn_Click below
  Call mnuChartOn_Click ' in input_boxes_off toolbar for chart disabled first
                         ' so in cmdSelextXAxis_Click executed
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".mnuStart_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub input_boxes_off()
  'txtFile.Enabled = False
  txtFileName.Enabled = False
  cmdBrowse.Enabled = False
  
  optRectangular.Enabled = False
  optCylinder.Enabled = False
  txtSampleLength.Enabled = False
  txtSampleWidth.Enabled = False
  txtSampleThickness.Enabled = False
  
  txtCurrent.Enabled = False
  txtCompliance.Enabled = False
  txtDeltaCount.Enabled = False
  txtDelay.Enabled = False
  cboAnalogFilter.Enabled = False
  cboDigitalFilter.Enabled = False
  txtDigitalFilterCount.Enabled = False
  txtRate.Enabled = False
  cboVoltageRange.Enabled = False
  
  frameDevices.Enabled = False
  
  mnuOpenData.Enabled = False
  mnuOpenMethod.Enabled = False
  mnuSaveMethod.Enabled = False
  
  mnuStart.Enabled = False
  mnuStop.Enabled = True
  mnuPause.Enabled = True
  
  mnuConnectDevices.Enabled = False
  mnuChartOn.Enabled = False
  'mnuAutoScale.Enabled = False
  
  Toolbar1.Buttons.item(1).Enabled = False  'open data button
  Toolbar1.Buttons.item(2).Enabled = False  'start button
  Toolbar1.Buttons.item(3).Enabled = True  'stop button
  Toolbar1.Buttons.item(4).Enabled = True  'pause stop
  Toolbar1.Buttons.item(6).Enabled = True  'chart button
End Sub

Private Sub input_boxes_on()
  'txtFile.Enabled = True
  txtFileName.Enabled = True
  cmdBrowse.Enabled = True
  
  optRectangular.Enabled = True
  optCylinder.Enabled = True
  txtSampleLength.Enabled = True
  txtSampleWidth.Enabled = True
  txtSampleThickness.Enabled = True
  
  txtCurrent.Enabled = True
  txtCompliance.Enabled = True
  txtDeltaCount.Enabled = True
  txtDelay.Enabled = True
  cboAnalogFilter.Enabled = True
  cboDigitalFilter.Enabled = True
  txtDigitalFilterCount.Enabled = True
  txtRate.Enabled = True
  cboVoltageRange.Enabled = True
  
  frameDevices.Enabled = True
  
  mnuOpenData.Enabled = False
  mnuOpenMethod.Enabled = True
  mnuSaveMethod.Enabled = True
  
  mnuStart.Enabled = True
  mnuStop.Enabled = False
  mnuPause.Enabled = False
  
  mnuConnectDevices.Enabled = True
  mnuChartOn.Enabled = True
  'mnuAutoScale.Enabled = True
  
  Toolbar1.Buttons.item(1).Enabled = False  'open data button
  Toolbar1.Buttons.item(2).Enabled = True  'start button
  Toolbar1.Buttons.item(3).Enabled = False  'stop button
  Toolbar1.Buttons.item(4).Enabled = False  'pause stop
  Toolbar1.Buttons.item(6).Enabled = False  'chart button
End Sub

Public Sub save_file()
On Error GoTo myErrorHandler

  path_and_file_name = txtFile.text
  
  mFileSysObj.CreateTextFile (path_and_file_name) 'if already there, destroying the old file
  Set mFile = mFileSysObj.GetFile(path_and_file_name)
  Set mTxtStream = mFile.OpenAsTextStream(ForWriting)
      
  experiment_info = experiment_info & "Operator  : " & txtOperatorName.text & vbCrLf
  experiment_info = experiment_info & "File Name : " & txtFileName.text & vbCrLf
  experiment_info = experiment_info & "File Path : " & txtFile.text & vbCrLf
  If optRectangular.value = True Then
    experiment_info = experiment_info & "Sample ID : " & txtSampleID.text & _
      " (Rectangular)" & vbCrLf
  Else
    experiment_info = experiment_info & "Sample ID : " & txtSampleID.text & _
      " (Cylinder)" & vbCrLf
  End If
  experiment_info = experiment_info & "Length    : " & txtSampleLength.text & _
    " mm" & vbCrLf
  If optRectangular.value = True Then
    experiment_info = experiment_info & "Width     : " & txtSampleWidth.text & _
      " mm" & vbCrLf
    experiment_info = experiment_info & "Thickness : " & txtSampleThickness.text & _
      " mm" & vbCrLf
  Else
    experiment_info = experiment_info & "Diameter  : " & txtSampleWidth.text & _
      " mm" & vbCrLf
  End If
  experiment_info = experiment_info & "Comment : " & txtComment.text & vbCrLf & vbCrLf
  
  experiment_info = experiment_info & "Application Settings : " & vbCrLf
  
  experiment_info = experiment_info & "# Devices  : " & txtNumberOfDevices.text & vbCrLf
  experiment_info = experiment_info & "Address    : " & txtAddress.text & vbCrLf
  experiment_info = experiment_info & "Current    : " & txtCurrent.text & _
    " mA" & vbCrLf
  experiment_info = experiment_info & "Compliance : " & txtCompliance.text & _
    " V" & vbCrLf
  experiment_info = experiment_info & "Count      : " & txtDeltaCount.text & vbCrLf
  experiment_info = experiment_info & "Delay      : " & txtDelay.text & _
    " s" & vbCrLf
  experiment_info = experiment_info & "Analog Filter  : " & cboAnalogFilter.text & vbCrLf
  experiment_info = experiment_info & "Digital Filter : " & cboDigitalFilter.text & vbCrLf
  experiment_info = experiment_info & "Digital Filter Count : " & _
    txtDigitalFilterCount.text & vbCrLf
  experiment_info = experiment_info & "Rate       : " & txtRate.text & _
    " NPLCs" & vbCrLf
  experiment_info = experiment_info & "Voltage Range : " & cboVoltageRange.text & _
    " V" & vbCrLf
  experiment_info = experiment_info & "Stop Time : " & txtStopTime.text & _
    " min" & vbCrLf
  experiment_info = experiment_info & "Data Points : " & txtDataPoints.text & _
    " s/pt" & vbCrLf & "" & vbCrLf
  
  experiment_info = experiment_info & "Time(s) " & "Temperature(oC) " & _
    "Resistivity(Ohm.m) " & "Resistance(Ohm) " & "Voltage(V) " & "Current(A)"
      
  mTxtStream.WriteLine (experiment_info)
      
  For i = 1 To times_of_readings
    Call mTxtStream.WriteLine(time_elapsed(i) & " " & Temperature(i) & " " & _
      resistivity(i) & " " & resistance(i) & " " & voltage(i) & " " & _
      current(i) & " " & hhmmss(time_elapsed(i)))
  Next i
      
  Call mTxtStream.Close 'closing file for writing
  
  'times_of_readings = 0 'reset to 0
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".save_file)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Public Sub save_status() ' public cause called also in frmSettings
  On Error GoTo myErrorHandler
  
  Dim experiment_info As String
  Dim text As String
  
  path_and_file_name = App.Path & "\log\rho_status_" & _
    Replace(Replace(Now, "/", "-"), ":", "-") & ".log"
    
  mFileSysObj.CreateTextFile (path_and_file_name)
  Set mFile = mFileSysObj.GetFile(path_and_file_name)
  Set mTxtStream = mFile.OpenAsTextStream(ForWriting)
      
  experiment_info = program_name & vbCrLf & "Events at " & Now & vbCrLf
  ' These lines same as in save_file sub
  experiment_info = experiment_info & "Operator  : " & txtOperatorName.text & vbCrLf
  experiment_info = experiment_info & "File Name : " & txtFileName.text & vbCrLf
  experiment_info = experiment_info & "File Path : " & txtFile.text & vbCrLf
  If optRectangular.value = True Then
    experiment_info = experiment_info & "Sample ID : " & txtSampleID.text & _
      " (Rectangular)" & vbCrLf
  Else
    experiment_info = experiment_info & "Sample ID : " & txtSampleID.text & _
      " (Cylinder)" & vbCrLf
  End If
  experiment_info = experiment_info & "Length    : " & txtSampleLength.text & _
    " mm" & vbCrLf
  If optRectangular.value = True Then
    experiment_info = experiment_info & "Width     : " & txtSampleWidth.text & _
      " mm" & vbCrLf
    experiment_info = experiment_info & "Thickness : " & txtSampleThickness.text & _
      " mm" & vbCrLf
  Else
    experiment_info = experiment_info & "Diameter  : " & txtSampleWidth.text & _
      " mm" & vbCrLf
  End If
  experiment_info = experiment_info & "Comment : " & txtComment.text & vbCrLf & vbCrLf
  
  experiment_info = experiment_info & "Application Settings : " & vbCrLf
  
  experiment_info = experiment_info & "# Devices  : " & txtNumberOfDevices.text & vbCrLf
  experiment_info = experiment_info & "Address    : " & txtAddress.text & vbCrLf
  experiment_info = experiment_info & "Current    : " & txtCurrent.text & _
    " mA" & vbCrLf
  experiment_info = experiment_info & "Compliance : " & txtCompliance.text & _
    " V" & vbCrLf
  experiment_info = experiment_info & "Count      : " & txtDeltaCount.text & vbCrLf
  experiment_info = experiment_info & "Delay      : " & txtDelay.text & _
    " s" & vbCrLf
  experiment_info = experiment_info & "Analog Filter  : " & cboAnalogFilter.text & vbCrLf
  experiment_info = experiment_info & "Digital Filter : " & cboDigitalFilter.text & vbCrLf
  experiment_info = experiment_info & "Digital Filter Count : " & _
    txtDigitalFilterCount.text & vbCrLf
  experiment_info = experiment_info & "Rate       : " & txtRate.text & _
    " NPLCs" & vbCrLf
  experiment_info = experiment_info & "Voltage Range : " & cboVoltageRange.text & _
    " V" & vbCrLf
  experiment_info = experiment_info & "Stop Time : " & txtStopTime.text & _
    " min" & vbCrLf
  experiment_info = experiment_info & "Data Points : " & txtDataPoints.text & _
    " s/pt" & vbCrLf
  
  mTxtStream.WriteLine (experiment_info)
    
  'mTxtStream.WriteLine ("Number of critical errors: " & count_error & vbCrLf)
  'mTxtStream.WriteLine (vbCrLf & "Events :")
  
  For i = 0 To lstStatus.ListCount - 1
    mTxtStream.WriteLine (lstStatus.List(i))
  Next i
            
  Call mTxtStream.Close 'closing file for writing

  Exit Sub
myErrorHandler:
  'Call turn_off_all_timers
  write_status "Error " & Err.Number & " :  " & Err.Description & _
  " (Sub. : " & Me.Name & ".save_status)"
  
  MsgBox "Error " & Err.Number & " :  " & Err.Description & vbCrLf & _
  "Sub. : " & Me.Name & ".save_status", vbCritical, "Error"
  Call save_settings
  'Call save_status
End Sub

Private Sub txtMaxNumberDataX_Change()
  If IsNumeric(txtMaxNumberDataX.text) = False Or Val(txtMaxNumberDataX.text) < 2 _
    Or Val(txtMaxNumberDataX.text) > times_of_readings Then  ' trap non numric an < 2, min 2 data points
    
    txtMaxNumberDataX.text = times_of_readings
  End If
  maxNumberDataX = CLng(txtMaxNumberDataX.text) ' value > times_of_readings not trapped
End Sub

Private Sub chkAllDataPoints_Click()
  If devices_are_connected = False Then
    If chkAllDataPoints.value = 1 Then
      txtMaxNumberDataX.text = tableResult.Rows.count
    End If
  End If
End Sub

Private Sub mnuConnectdevices_Click()
  On Error GoTo myErrorHandler

  If Not filter_result_is_OK Then Exit Sub
  
  If Not frmSettings.filter_is_OK Then Exit Sub
  Call frmSettings.cmdApply_Click ' filter_is_OK is OK at this line
  
  If mnuConnectDevices.Checked = False Then
    ' read the number and adresses of GPIB devices, filter is OK above
    Dim splitted() As String
    number_of_devices = Val(txtNumberOfDevices.text)
    splitted = Split(Trim(txtAddress.text), ",")
      
    For i = 0 To number_of_devices - 1
      GPIB_address(i) = splitted(i)
    Next i
    GPIB_address(i + 1) = NOADDR

    ReDim Dev(0 To number_of_devices - 1)
    ReDim reading_from_each_instrument(0 To number_of_devices - 1)
    ' *********************************************************************
       
    Call calculate_RhoFactor
  
    Call initialize_devices
  
  If chkDeltaMode.value = 1 Then
    Call apply_setup_delta
    Call arm_delta
    Sleep 1000
    Call ilwrt(Dev(0), ":disp:enab 0", 12)
    Sleep 1000
    Call trigger_delta  ' First trigger before first reading
    Sleep 1000 * Val(txtDataPoints.text) ' delta operation needs 350 sec for 5 counts
  Else
    Call apply_setup_non_delta
  End If
  
    tmrReadDevicesOnlyToDisplay.Enabled = True
    
    mnuStart.Enabled = True
    mnuStop.Enabled = False
    mnuPause.Enabled = False
    mnuOpenData.Enabled = False
    
    Toolbar1.Buttons.item(1).Enabled = False  'open data button
    Toolbar1.Buttons.item(2).Enabled = True  'start button
    Toolbar1.Buttons.item(3).Enabled = False  'stop button
    Toolbar1.Buttons.item(4).Enabled = False 'pause stop
    Toolbar1.Buttons.item(6).Enabled = False  'chart button
    
    mnuConnectDevices.Checked = True
    devices_are_connected = True
    write_status "Devices are connected"
  Else
    tmrReadDevicesOnlyToDisplay.Enabled = False
    
    If chkDeltaMode.value = 1 Then
      Call exit_delta
      Sleep 1000
      Call ilwrt(Dev(0), ":disp:enab 1", 12)
      Sleep 1000
    Else
      Call exit_non_delta
    End If
        
    Call disconnect_device
  
    mnuStart.Enabled = False
    mnuStop.Enabled = False
    mnuPause.Enabled = False
    mnuOpenData.Enabled = True
    
    Toolbar1.Buttons.item(1).Enabled = True  'open data button
    Toolbar1.Buttons.item(2).Enabled = False  'start button
    Toolbar1.Buttons.item(3).Enabled = False 'stop button
    Toolbar1.Buttons.item(4).Enabled = False  'pause stop
    Toolbar1.Buttons.item(6).Enabled = False  'chart button
  
    mnuConnectDevices.Checked = False
    devices_are_connected = False
    write_status "Devices are disconnected"
  End If
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".mnuConnectDevices_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub mnuStop_Click()
  experiment_info = experiment_info & "Experiment stops at " & Now & _
    vbCrLf & "" & vbCrLf
  
  tmrReadDevices.Enabled = False
      
  tmrDrawChart.Enabled = False
  tmrShiftData.Enabled = False
  tmrReadDevicesOnlyToDisplay.Enabled = True ' reading while not saved
  
  experiment_is_running = False
  experiment_is_paused = False
  method_is_applied = False
  charting_is_running = False
  
  write_status "Experiment is stopped"
  
  charting_is_running = True ' set true before mnu_chartOn_Click below
  Call mnuChartOn_Click
  
  Call save_file
  
  Call input_boxes_on
End Sub

Private Sub tmrReaddevices_Timer()
  ''Call read_devices
  Call read_devices_non_delta
End Sub

Private Sub tmrReaddevicesOnlyToDisplay_Timer()
  ''Call read_devices_only_to_display
  Call read_dev_only_display_non_delta
End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
Select Case Button.Image
  Case "data"
    Call mnuOpenData_Click
  Case "go"
    Call mnuStart_Click
  Case "stop"
    Call mnuStop_Click
  Case "pause"
    Call mnuPause_Click
  Case "chart"
    If experiment_is_running = False Then Exit Sub
    Call mnuChartOn_Click
  End Select
End Sub

'***THESE FOLLOWING BLOCKS are subs & functions for drawing chart***

' get the current time at millisecond resolution
Private Function getCurrentTime() As Double
  Dim currentDate As Date
  currentDate = Now  ' get the year, month, day, hour from the VB date
    
  ' get the minute and seconds using GetSystemTime for millisecond resolution
  Dim currentTime As SYSTEMTIME
  GetSystemTime currentTime
    
  ' Return the date/time in chartTime format, which can have millisecond resolution
  getCurrentTime = cd.chartTime(Year(currentDate), Month(currentDate), _
    Day(currentDate), Hour(currentDate), currentTime.wMinute, _
    currentTime.wSecond) + currentTime.wMilliseconds / 1000#
End Function

Private Sub shift_data(data, newValue)
  Dim j
  For j = LBound(data) + 1 To UBound(data)
      data(j - 1) = data(j)
  Next
  data(UBound(data)) = newValue
End Sub

Private Sub txtChartPeriod_Change()
  If txtChartPeriod.text < 1000 Or IsNumeric(txtChartPeriod.text) = False Then
    txtChartPeriod.text = 1000 ' ms
  End If
  
  tmrDrawChart.Interval = CInt(txtChartPeriod.text)
End Sub

Private Sub tmrShiftData_Timer()
  On Error GoTo myErrorHandler

  Dim currentTime As Double
  currentTime = getCurrentTime()
  
  Dim p, dataA
  Do While nextDateTime < currentTime
    ' Get a data sample
    'p = nextDateTime - Int(nextDateTime / 86400) * 86400
    Select Case cmbSelectYAxis.text
      Case "Resistivity"
        'If Len(resistivity(times_of_readings)) > 7 Then
          'dataA = Left(Format(resistivity(times_of_readings), "scientific"), 4)
          'dataY_exponent = "x" & Right(Format(resistivity(times_of_readings), "scientific"), 4)
        'Else
          'dataA = resistivity(times_of_readings)
        'End If
        dataA = Round(resistivity(times_of_readings) / expRho, roundRho)
      Case "Resistance"
        'If Len(resistance(times_of_readings)) > 7 Then
          'dataA = Left(Format(resistance(times_of_readings), "scientific"), 4)
          'dataY_exponent = "x" & Right(Format(resistance(times_of_readings), "scientific"), 4)
        'Else
          'dataA = resistance(times_of_readings)
        'End If
        dataA = Round(resistance(times_of_readings) / expR, roundR)
      Case "Voltage"
        'If Len(voltage(times_of_readings)) > 7 Then
          'dataA = Left(Format(voltage(times_of_readings), "scientific"), 4)
          'dataY_exponent = "x" & Right(Format(voltage(times_of_readings), "scientific"), 4)
        'Else
          'dataA = voltage(times_of_readings)
        'End If
        dataA = Round(voltage(times_of_readings) / expV, roundV)
      Case "Current"
        'If Len(current(times_of_readings)) > 7 Then
          'dataA = Left(Format(current(times_of_readings), "scientific"), 4)
          'dataY_exponent = "x" & Right(Format(current(times_of_readings), "scientific"), 4)
        'Else
          'dataA = current(times_of_readings)
        'End If
        dataA = Round(current(times_of_readings) / expC, roundC)
      Case "Temperature"
        dataA = Temperature(times_of_readings)
      Case "Time"
        dataA = time_elapsed(times_of_readings)
    End Select
           
    shift_data dataSeries, dataA
    shift_data timeStamps, nextDateTime
        
    nextDateTime = nextDateTime + tmrShiftData.Interval / 1000#
  Loop
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".tmrShiftData_Timer)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub tmrDrawChart_Timer() ' X axis = time (h:m:s)
  Call ChartViewer1.updateViewPort(True, False)
End Sub

Private Sub draw_chart(viewer As ChartViewer) ' X axis = time (h:m:s)
  On Error GoTo myErrorHandler
  
  'Dim c As XYChart
  Set c = cd.XYChart(555, 300, &HF4F4F4, &H0, 0)
  Call c.setPlotArea(60, 33, 485, 220, &HFFFFFF, -1, -1, &HCCCCCC, &HCCCCCC)
  Call c.setClipping
    
  Dim ChartTitle As String
  ChartTitle = cmbSelectYAxis.text & " vs " & cmbSelectXAxis.text
  Call c.addTitle(ChartTitle, "timesb.ttf", 11). _
    setBackground(&HDDDDDD, &H0, cd.glassEffect())
      
  Dim XAxisTitle As String
  Dim YAxisTitle As String
  Select Case cmbSelectXAxis.text
    Case "Time (s)"
      XAxisTitle = "Time (s)"
    Case "Time (min)"
      XAxisTitle = "Time (min)"
    Case "Time (h:m:s)"
      XAxisTitle = "Time (h:m:s)"
  End Select
  Select Case cmbSelectYAxis.text
    Case "Resistance"
      dataY_exponent = "x" & frmSettings.cboExpR.text
      YAxisTitle = "Resistance " & "(" & dataY_exponent & " Ohm)"
    Case "Voltage"
      dataY_exponent = "x" & frmSettings.cboExpV.text
      YAxisTitle = "Voltage " & "(" & dataY_exponent & " V)"
    Case "Current"
      dataY_exponent = "x" & frmSettings.cboExpC.text
      YAxisTitle = "Current " & "(" & dataY_exponent & " A)"
    Case "Temperature"
      YAxisTitle = "Temperature (oC)"
    Case "Time (s)"
      YAxisTitle = "Time (s)"
    Case "Time (min)"
      YAxisTitle = "Time (min)"
    Case "Resistivity"
      dataY_exponent = "x" & frmSettings.cboExpRho.text
      YAxisTitle = "Resistivity " & "(" & dataY_exponent & " Ohm.m)"
  End Select
    
  Call c.yAxis().setTitle(YAxisTitle, "arialbd.ttf", 10)
  Call c.xAxis().setTitle(XAxisTitle, "arialbd.ttf", 10)
    
  ' Set the x-axis to auto-scale with at least 75 pixels between major tick and 15
  ' pixels between minor ticks. This shows more minor grid lines on the chart.
  Call c.xAxis().setTickDensity(75, 15)
    
  Call c.xAxis().setWidth(2)
  Call c.yAxis().setWidth(2)
    
  ' add the data to the chart, data set in tmrShiftData
  Dim lastTime As Double
  lastTime = timeStamps(UBound(timeStamps))
  If lastTime <> cd.NoValue Then 'can i avoid if, this is only o avoid time = 0
    ' Set up the x-axis to show the time range in the data buffer
    Call c.xAxis().setDateScale(lastTime - tmrShiftData.Interval * _
      (UBound(timeStamps) + 1) / 1000#, lastTime)
            
    Call c.xAxis().setLabelFormat("{value|hh:nn:ss}")
        
    Dim layer As LineLayer
    Set layer = c.addLineLayer2()
        
    Call layer.setXData(timeStamps)
        
    Call layer.addDataSet(dataSeries) ', &HFF0000, c.formatValue(dataSeries( _
            UBound(dataSeries)), "Temperature: <*bgColor=FFCCCC*> {value|2} "))
  End If
  
  ' OK but check again. X axis always fixed range. set above
  If mnuAutoScale.Checked = True Then 'maxX = minX Then  ' The first time plotting = no zooming. set in form_load and chkSelectXAxis & YAxis
    
      'Call c.yAxis().setAutoScale(0.05, 0.05, 0) ' this result good margin, if not auto default not good
      'Call c.xAxis().setAutoScale(0.05, 0.05, 0) ' if placed after c.layout useless
        
      ' explicitly auto-scale axes so we can get the axis scales
      Call c.layout ' this important if not, zooming error
      
      ' save the axis scales for future use
      minX = c.xAxis().getMinValue()
      maxX = c.xAxis().getMaxValue()
      MinY = c.yAxis().getMinValue()
      MaxY = c.yAxis().getMaxValue()
    
      With frmSettings
        .txtXMinScale.text = minX
        .txtXMaxScale.text = maxX
        .txtYMinScale.text = MinY
        .txtYMaxScale.text = MaxY
      End With
  Else
    If frmSettings.chkCustomScale.value = 0 Then
      ' Compute the zoomed-in axis scales using the overall axis scales and ViewPort size
      ' The full x-axis and y-axis scales after zooming first time, me
      Dim xScaleMin As Double
      Dim xScaleMax As Double
      Dim yScaleMin As Double
      Dim yScaleMax As Double

      xScaleMin = minX + (maxX - minX) * ChartViewer1.ViewportLeft
      xScaleMax = minX + (maxX - minX) * (ChartViewer1.ViewportLeft + ChartViewer1.ViewportWidth)
      yScaleMin = MaxY - (MaxY - MinY) * (ChartViewer1.ViewportTop + ChartViewer1.ViewportHeight)
      yScaleMax = MaxY - (MaxY - MinY) * ChartViewer1.ViewportTop
    
      With frmSettings
      .txtXMinScale.text = xScaleMin
      .txtXMaxScale.text = xScaleMax
      .txtYMinScale.text = yScaleMin
      .txtYMaxScale.text = yScaleMax
      XGrid = CDbl(.txtXGrid.text)
      YGrid = CDbl(.txtYGrid.text)
      End With
        
      ' Set the axis scales
      'Call c.xAxis().setLinearScale(xScaleMin, xScaleMax) ', XGrid)
      'Call c.xAxis().setRounding(False, False)
      Call c.yAxis().setLinearScale(yScaleMin, yScaleMax) ', YGrid)
      Call c.yAxis().setRounding(False, False)
    Else
      With frmSettings
      minX = CDbl(.txtXMinScale.text)
      maxX = CDbl(.txtXMaxScale.text)
      MinY = CDbl(.txtYMinScale.text)
      MaxY = CDbl(.txtYMaxScale.text)
      XGrid = CDbl(.txtXGrid.text)
      YGrid = CDbl(.txtYGrid.text)
      Call c.yAxis().setLinearScale(MinY, MaxY) ', YGrid)
      'Call c.xAxis().setLinearScale(minX, maxX) ', XGrid)
      End With
    End If
  End If
     
  'Set ChartViewer1.Picture = c.makePicture()
  ''Set viewer.Picture = c.makePicture()
  Set viewer.Chart = c ' This makes Zoom In/Out buttons work
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".draw_chart)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub draw_chart2(viewer As ChartViewer)  ' X axis = time (s) or Temperature
'  On Error GoTo myErrorHandler
 On Error Resume Next
  maxNumberDataX = txtMaxNumberDataX.text
  minNumberDataX = 1 'frmSettings.txtXMinScale.text
  'maxNumberDataY = times_of_readings ' maxnumberdatay may < maxnumberdatax ?
  maxNumberDataY = maxNumberDataX
       
  ReDim dataY(minNumberDataX To maxNumberDataY)
  Dim newIndexY As Long
      
  For i = minNumberDataX To maxNumberDataY ' from 1 to 90 is formulated
    newIndexY = CLng(((i - 1) * (times_of_readings - 1) / (maxNumberDataX - 1)) + 1)  ' Clng(String -- auto round to 0 digit), without clng ok too, auto clng
    
    Select Case cmbSelectYAxis.text
      Case "Resistivity"
        dataY(i) = Round(resistivity(newIndexY) / expRho, roundRho)
      Case "Resistance"
        dataY(i) = Round(resistance(newIndexY) / expR, roundR)
      Case "Voltage"
        dataY(i) = Round(voltage(newIndexY) / expV, roundV)
      Case "Current"
        dataY(i) = Round(current(newIndexY) / expC, roundC)
      Case "Temperature"
        dataY(i) = Temperature(newIndexY)
      Case "Time (s)"
        dataY(i) = time_elapsed(newIndexY)
      Case "Time (min)"
        dataY(i) = Round(time_elapsed(newIndexY) / 60, 1)
    End Select
  Next i
    
  ' The datax for the chart, seperated iteration from y because datay may < data x
  ReDim dataX(minNumberDataX To maxNumberDataX) As String
  For i = minNumberDataX To maxNumberDataX
    newIndexY = CLng(((i - 1) * (times_of_readings - 1) / (maxNumberDataX - 1)) + 1) ' Clng -- auto round
    Select Case cmbSelectXAxis.text
      Case "Time (s)"
        dataX(i) = time_elapsed(newIndexY)
      Case "Time (min)"
        dataX(i) = Round(time_elapsed(newIndexY) / 60, 1)
      Case "Temperature"
        dataX(i) = Temperature(newIndexY)
    End Select
  Next i
  
  
  'Dim c As XYChart
  Set c = cd.XYChart(555, 300, &HF4F4F4, &H0, 0)
  
  Call c.setPlotArea(60, 33, 485, 220, &HFFFFFF, -1, -1, &HCCCCCC, &HCCCCCC)
  Call c.setClipping
    
  Dim ChartTitle As String
  ChartTitle = cmbSelectYAxis.text & " vs " & cmbSelectXAxis.text
  Call c.addTitle(ChartTitle, "timesb.ttf", 11). _
    setBackground(&HDDDDDD, &H0, cd.glassEffect())
      
  Dim XAxisTitle As String
  Dim YAxisTitle As String
  
  Select Case cmbSelectXAxis.text
    Case "Temperature"
      XAxisTitle = "Temperature (oC)"
    Case "Time (s)" ', "Time (h:m:s)"
      XAxisTitle = "Time (s)"
    Case "Time (min)"
      XAxisTitle = "Time (min)"
  End Select
  
  Select Case cmbSelectYAxis.text
    Case "Resistance"
      dataY_exponent = "x" & frmSettings.cboExpR.text
      YAxisTitle = "Resistance " & "(" & dataY_exponent & " Ohm)"
    Case "Voltage"
      dataY_exponent = "x" & frmSettings.cboExpV.text
      YAxisTitle = "Voltage " & "(" & dataY_exponent & " V)"
    Case "Current"
      dataY_exponent = "x" & frmSettings.cboExpC.text
      YAxisTitle = "Current " & "(" & dataY_exponent & " A)"
    Case "Temperature"
      YAxisTitle = "Temperature (oC)"
    Case "Time (s)"
      YAxisTitle = "Time (s)"
    Case "Time (min)"
      YAxisTitle = "Time (min)"
    Case "Resistivity"
      dataY_exponent = "x" & frmSettings.cboExpRho.text
      YAxisTitle = "Resistivity " & "(" & dataY_exponent & " Ohm.m)"
  End Select
  Call c.yAxis().setTitle(YAxisTitle, "arialbd.ttf", 10)
  Call c.xAxis().setTitle(XAxisTitle, "arialbd.ttf", 10)
    
  Call c.xAxis().setWidth(2)
  Call c.yAxis().setWidth(2)
      
  Dim layer As LineLayer
  Set layer = c.addLineLayer(dataY) ', &HFF3333, YAxisTitle)
  Call layer.setXData(dataX)
  
  ' This if block is the same as in draw_chart & draw_chart_exp
  If mnuAutoScale.Checked = True Then 'maxX = minX Then  ' The first time plotting = no zooming. set in form_load and chkSelectXAxis & YAxis
    
      Call c.yAxis().setAutoScale(0.05, 0.05, 0) ' this result good margin, if not auto default not good
      Call c.xAxis().setAutoScale(0.05, 0.05, 0) ' if placed after c.layout useless
        
      ' explicitly auto-scale axes so we can get the axis scales
      Call c.layout ' this important if not, zooming error
      
      ' save the axis scales for future use
      minX = c.xAxis().getMinValue()
      maxX = c.xAxis().getMaxValue()
      MinY = c.yAxis().getMinValue()
      MaxY = c.yAxis().getMaxValue()
    
      With frmSettings
        .txtXMinScale.text = minX
        .txtXMaxScale.text = maxX
        .txtYMinScale.text = MinY
        .txtYMaxScale.text = MaxY
      End With
  Else
    If frmSettings.chkCustomScale.value = 0 Then
      ' Compute the zoomed-in axis scales using the overall axis scales and ViewPort size
      ' The full x-axis and y-axis scales after zooming first time, me
      Dim xScaleMin As Double
      Dim xScaleMax As Double
      Dim yScaleMin As Double
      Dim yScaleMax As Double

      xScaleMin = minX + (maxX - minX) * ChartViewer1.ViewportLeft
      xScaleMax = minX + (maxX - minX) * (ChartViewer1.ViewportLeft + ChartViewer1.ViewportWidth)
      yScaleMin = MaxY - (MaxY - MinY) * (ChartViewer1.ViewportTop + ChartViewer1.ViewportHeight)
      yScaleMax = MaxY - (MaxY - MinY) * ChartViewer1.ViewportTop
    
      With frmSettings
      .txtXMinScale.text = xScaleMin
      .txtXMaxScale.text = xScaleMax
      .txtYMinScale.text = yScaleMin
      .txtYMaxScale.text = yScaleMax
      XGrid = CDbl(.txtXGrid.text)
      YGrid = CDbl(.txtYGrid.text)
      End With
    
      ' *** use the following formula if you are using a log scale axis ***
      ' xScaleMin = minX * ((maxX / minX) ^ ChartViewer1.ViewPortLeft)
      ' xScaleMax = minX * ((maxX / minX) ^ (ChartViewer1.ViewPortLeft + _
      '     ChartViewer1.ViewPortWidth))
      ' yScaleMin = maxY * ((minY / maxY) ^ (ChartViewer1.ViewPortTop + _
      '     ChartViewer1.ViewPortHeight))
      ' yScaleMax = maxY * ((minY / maxY) ^ ChartViewer1.ViewPortTop)
    
      ' Set the axis scales
      Call c.xAxis().setLinearScale(xScaleMin, xScaleMax) ', XGrid)
      Call c.xAxis().setRounding(False, False)
      Call c.yAxis().setLinearScale(yScaleMin, yScaleMax) ', YGrid)
      Call c.yAxis().setRounding(False, False)
    Else
      With frmSettings
      minX = CDbl(.txtXMinScale.text)
      maxX = CDbl(.txtXMaxScale.text)
      MinY = CDbl(.txtYMinScale.text)
      MaxY = CDbl(.txtYMaxScale.text)
      XGrid = CDbl(.txtXGrid.text)
      YGrid = CDbl(.txtYGrid.text)
      Call c.yAxis().setLinearScale(MinY, MaxY) ', YGrid)
      Call c.xAxis().setLinearScale(minX, maxX) ', XGrid)
      End With
    End If
  End If

  'Set ChartViewer1.Picture = c.makePicture()
  ''Set viewer.Picture = c.makePicture()
  Set viewer.Chart = c ' This makes Zoom In/Out buttons work
    
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".draw_chart2)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub draw_chart_empty(viewer As ChartViewer) 'just to show when program starts
  Dim c As XYChart
  Set c = cd.XYChart(555, 300, &HF4F4F4, &H0, 0)
    
  Call c.setPlotArea(60, 33, 485, 220, &HFFFFFF, -1, -1, &HCCCCCC, &HCCCCCC)
  Call c.setClipping
    
  Dim ChartTitle As String
  ChartTitle = cmbSelectYAxis.text & " vs " & cmbSelectXAxis.text
  Call c.addTitle(ChartTitle, "timesb.ttf", 11 _
    ).setBackground(&HDDDDDD, &H0, cd.glassEffect())
    
  Dim XAxisTitle As String
  Dim YAxisTitle As String
  XAxisTitle = cmbSelectXAxis.text
  YAxisTitle = cmbSelectYAxis.text
  Call c.yAxis().setTitle(YAxisTitle, "arialbd.ttf", 10)
  Call c.xAxis().setTitle(XAxisTitle, "arialbd.ttf", 10)
    
  Call c.xAxis().setTickDensity(75, 15)
    
  Call c.xAxis().setWidth(2)
  Call c.yAxis().setWidth(2)
    
  Set viewer.Picture = c.makePicture()
End Sub

Private Sub draw_chart_exp(viewer As ChartViewer)  ' X axis = time (s) or Temperature
  On Error GoTo myErrorHandler
  
  Call frmSettings.cmdApply_Click ' prepare the expRho,..., and roundRho,...
  
  'maxNumberDataX = txtMaxNumberDataX.text, set = times_of_readings in read_data sub
  Call txtMaxNumberDataX_Change ' set & filter maxNumberDataX
  minNumberDataX = 1
  maxNumberDataY = maxNumberDataX
      
  ReDim dataY(minNumberDataX To maxNumberDataY)
        
  For i = minNumberDataX To maxNumberDataY
    Select Case cmbSelectYAxis.text
      Case "Resistivity"
        dataY(i) = Round(resistivity(i) / expRho, roundRho)
      Case "Resistance"
        dataY(i) = Round(resistance(i) / expR, roundR)
      Case "Voltage"
        dataY(i) = Round(voltage(i) / expV, roundV)
      Case "Current"
        dataY(i) = Round(current(i) / expC, roundC)
      Case "Temperature"
        dataY(i) = Temperature(i)
      Case "Time (s)"
        dataY(i) = time_elapsed(i)
      Case "Time (min)"
        dataY(i) = Round(time_elapsed(i) / 60, 1)
    End Select
  Next i
    
  ' The datax for the chart, sperated iteration from y because datay may < data x
  ReDim dataX(minNumberDataX To maxNumberDataX) As String
  For i = minNumberDataX To maxNumberDataX
    Select Case cmbSelectXAxis.text
      Case "Time (s)"
        dataX(i) = time_elapsed(i)
      Case "Time (min)"
        dataX(i) = Round(time_elapsed(i) / 60, 1)
      Case "Temperature"
        dataX(i) = Temperature(i)
    End Select
  Next i
  
  
  'Dim c As XYChart
  Set c = cd.XYChart(555, 300, &HF4F4F4, &H0, 0)
  
  Call c.setPlotArea(60, 33, 485, 220, &HFFFFFF, -1, -1, &HCCCCCC, &HCCCCCC)
  Call c.setClipping
    
  Dim ChartTitle As String
  ChartTitle = cmbSelectYAxis.text & " vs " & cmbSelectXAxis.text
  Call c.addTitle(ChartTitle, "timesb.ttf", 11). _
    setBackground(&HDDDDDD, &H0, cd.glassEffect())
      
  Dim XAxisTitle As String
  Dim YAxisTitle As String
  
  Select Case cmbSelectXAxis.text
    Case "Temperature"
      XAxisTitle = "Temperature (oC)"
    Case "Time (s)"
      XAxisTitle = "Time (s)"
    Case "Time (min)"
      XAxisTitle = "Time (min)"
  End Select
  
  Select Case cmbSelectYAxis.text
    Case "Resistance"
      dataY_exponent = "x" & frmSettings.cboExpR.text
      YAxisTitle = "Resistance " & "(" & dataY_exponent & " Ohm)"
    Case "Voltage"
      dataY_exponent = "x" & frmSettings.cboExpV.text
      YAxisTitle = "Voltage " & "(" & dataY_exponent & " V)"
    Case "Current"
      dataY_exponent = "x" & frmSettings.cboExpC.text
      YAxisTitle = "Current " & "(" & dataY_exponent & " A)"
    Case "Temperature"
      YAxisTitle = "Temperature (oC)"
    Case "Time (s)"
      YAxisTitle = "Time (s)"
    Case "Time (min)"
      YAxisTitle = "Time (min)"
    Case "Resistivity"
      dataY_exponent = "x" & frmSettings.cboExpRho.text
      YAxisTitle = "Resistivity " & "(" & dataY_exponent & " Ohm.m)"
  End Select
  Call c.yAxis().setTitle(YAxisTitle, "arialbd.ttf", 10)
  Call c.xAxis().setTitle(XAxisTitle, "arialbd.ttf", 10)
    
  Call c.xAxis().setWidth(2)
  Call c.yAxis().setWidth(2)
      
  Dim layer As LineLayer
  Set layer = c.addLineLayer(dataY) ', &HFF3333, YAxisTitle)
  Call layer.setXData(dataX)
  
  ' This if block is the same as in draw_chart & draw_chart2
  If mnuAutoScale.Checked = True Then 'maxX = minX Then  ' The first time plotting = no zooming. set in form_load and chkSelectXAxis & YAxis
    
      Call c.yAxis().setAutoScale(0.05, 0.05, 0) ' this result good margin, if not auto default not good
      Call c.xAxis().setAutoScale(0.05, 0.05, 0) ' if placed after c.layout useless
        
      ' explicitly auto-scale axes so we can get the axis scales
      Call c.layout ' this important if not, zooming error
      
      ' save the axis scales for future use
      minX = c.xAxis().getMinValue()
      maxX = c.xAxis().getMaxValue()
      MinY = c.yAxis().getMinValue()
      MaxY = c.yAxis().getMaxValue()
    
      With frmSettings
        .txtXMinScale.text = minX
        .txtXMaxScale.text = maxX
        .txtYMinScale.text = MinY
        .txtYMaxScale.text = MaxY
      End With
  Else
    If frmSettings.chkCustomScale.value = 0 Then
      ' Compute the zoomed-in axis scales using the overall axis scales and ViewPort size
      ' The full x-axis and y-axis scales after zooming first time, me
      Dim xScaleMin As Double
      Dim xScaleMax As Double
      Dim yScaleMin As Double
      Dim yScaleMax As Double

      xScaleMin = minX + (maxX - minX) * ChartViewer1.ViewportLeft
      xScaleMax = minX + (maxX - minX) * (ChartViewer1.ViewportLeft + ChartViewer1.ViewportWidth)
      yScaleMin = MaxY - (MaxY - MinY) * (ChartViewer1.ViewportTop + ChartViewer1.ViewportHeight)
      yScaleMax = MaxY - (MaxY - MinY) * ChartViewer1.ViewportTop
    
      With frmSettings
      .txtXMinScale.text = xScaleMin
      .txtXMaxScale.text = xScaleMax
      .txtYMinScale.text = yScaleMin
      .txtYMaxScale.text = yScaleMax
      XGrid = CDbl(.txtXGrid.text)
      YGrid = CDbl(.txtYGrid.text)
      End With
    
      ' *** use the following formula if you are using a log scale axis ***
      ' xScaleMin = minX * ((maxX / minX) ^ ChartViewer1.ViewPortLeft)
      ' xScaleMax = minX * ((maxX / minX) ^ (ChartViewer1.ViewPortLeft + _
      '     ChartViewer1.ViewPortWidth))
      ' yScaleMin = maxY * ((minY / maxY) ^ (ChartViewer1.ViewPortTop + _
      '     ChartViewer1.ViewPortHeight))
      ' yScaleMax = maxY * ((minY / maxY) ^ ChartViewer1.ViewPortTop)
    
      ' Set the axis scales
      Call c.xAxis().setLinearScale(xScaleMin, xScaleMax) ', XGrid)
      Call c.xAxis().setRounding(False, False)
      Call c.yAxis().setLinearScale(yScaleMin, yScaleMax) ', YGrid)
      Call c.yAxis().setRounding(False, False)
    Else
      With frmSettings
      minX = CDbl(.txtXMinScale.text)
      maxX = CDbl(.txtXMaxScale.text)
      MinY = CDbl(.txtYMinScale.text)
      MaxY = CDbl(.txtYMaxScale.text)
      XGrid = CDbl(.txtXGrid.text)
      YGrid = CDbl(.txtYGrid.text)
      Call c.yAxis().setLinearScale(MinY, MaxY) ', YGrid)
      Call c.xAxis().setLinearScale(minX, maxX) ', XGrid)
      End With
    End If
  End If

  'Set ChartViewer1.Picture = c.makePicture()
  ''Set viewer.Picture = c.makePicture()
  Set viewer.Chart = c ' This makes Zoom In/Out buttons work
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".draw_chart_exp)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' *******************************************************************************************

' *** THESE SUBS ARE FOR ZOOMING
' The ViewPortChanged event handler. This event occurs when the user changes the ChartViewer
' view port by dragging scrolling, or by zoom in/out, or the ChartViewer.updateViewPort method
' is being called.



Private Sub ChartViewer1_ViewPortChanged(needUpdateChart As Boolean, needUpdateImageMap As Boolean)
On Error GoTo myErrorHandler
    
    ' ZOomBar itu scroller
    ' Synchronize the zoom bar value with the view port width/height
    ZoomBar.value = Int(0.5 + IIf(ChartViewer1.ViewportWidth > ChartViewer1.ViewportHeight, _
        ChartViewer1.ViewportHeight, ChartViewer1.ViewportWidth) * ZoomBar.Max)
        
    ' Update chart and image map if necessary
    If needUpdateChart Then
      If experiment_is_running = True Then
        If cmbSelectXAxis.text = "Time (h:m:s)" Then
          Call draw_chart(ChartViewer1)
        Else
          Call draw_chart2(ChartViewer1)
        End If
      Else
        Call draw_chart_exp(ChartViewer1)
      End If
    End If
    If needUpdateImageMap Then
        Call updateImageMap(ChartViewer1)
    End If
    
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".ChartViewer1_ViewPortChanged)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' Apply image map used on the chart if not already applied
Private Sub updateImageMap(viewer As ChartViewer)
On Error GoTo myErrorHandler
    If viewer.ImageMap = "" Then
        viewer.ImageMap = viewer.Chart.getHTMLImageMap("clickable", "", _
            "title='[{dataSetName}] X = {x}, Y = {value}'")
    End If

Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".updateImageMap)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub ChartViewer1_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
  On Error Resume Next
    If Button = 0 Then '=0 no button just move, = 1 left drag, = 2 right drag
      txtXcoor.text = Round((((X / 15) - c.getXCoor(c.xAxis().getMinValue)) / _
        ((c.getXCoor(c.xAxis().getMaxValue) - c.getXCoor(c.xAxis().getMinValue)))) _
        * ((c.xAxis().getMaxValue - c.xAxis().getMinValue)) + c.xAxis().getMinValue, 3)
      txtYcoor.text = Round((((Y / 15) - c.getYCoor(c.yAxis().getMaxValue)) / _
        ((c.getYCoor(c.yAxis().getMinValue) - c.getYCoor(c.yAxis().getMaxValue)))) _
        * ((c.yAxis().getMinValue - c.yAxis().getMaxValue)) + c.yAxis().getMaxValue, 3)
    End If
End Sub

Private Sub ChartViewer1_ClickHotSpot(hotSpot As Collection, Button As Integer, Shift As Integer, _
    X As Single, Y As Single)
On Error GoTo myErrorHandler
  If Button = 1 Then 'by me
    'If ChartViewer1.MouseUsage <> cvZoomIn And ChartViewer1.MouseUsage <> cvZoomOut Then
      'ParamViewer.Display hotSpot
    'End If
  ElseIf Button = 2 Then
    If hotSpot(5) = "Glassy state" Then
      MsgBox "glassy"
    ElseIf hotSpot(5) = "Liquid state" Then
      MsgBox "liquid"
    End If
  End If

Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".ChartViewer1_ClickHotSpot)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' *** ZOOMING CODES FOR BUTTONS & EVENTS

' User clicks on the Pointer pushbutton
Private Sub PointerPB_Click()
On Error GoTo myErrorHandler
  Call set_auto_and_custom_off

  ChartViewer1.MouseUsage = cvScrollOnDrag
    
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".PointerPB_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' User clicks on the Zoom In pushbutton
Private Sub ZoomInPB_Click()
On Error GoTo myErrorHandler
  Call set_auto_and_custom_off

  ChartViewer1.MouseUsage = cvZoomIn
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".ZoomInPB_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' User clicks on the Zoom Out pushbutton
Private Sub ZoomOutPB_Click()
On Error GoTo myErrorHandler
  Call set_auto_and_custom_off

  ChartViewer1.MouseUsage = cvZoomOut

Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".ZoomOutPB_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub ZoomBar_Click()
  Call set_auto_and_custom_off
End Sub

Private Sub set_auto_and_custom_off()
  mnuAutoScale.Checked = 0
  frmSettings.chkCustomScale.value = 0
End Sub

Private Sub ZoomBar_Scroll()
On Error GoTo myErrorHandler
    ' Remember the center point
    Dim centerX As Double, centerY As Double
    centerX = ChartViewer1.ViewportLeft + ChartViewer1.ViewportWidth / 2
    centerY = ChartViewer1.ViewportTop + ChartViewer1.ViewportHeight / 2

    ' Aspect ratio and zoom factor
    Dim aspectRatio As Double, zoomTo As Double
    aspectRatio = ChartViewer1.ViewportWidth / ChartViewer1.ViewportHeight
    zoomTo = CDbl(ZoomBar.value) / ZoomBar.Max

    ' Zoom while preserving aspect ratio
    ChartViewer1.ViewportWidth = zoomTo * IIf(aspectRatio > 1, aspectRatio, 1)
    ChartViewer1.ViewportHeight = zoomTo * IIf(aspectRatio > 1, 1, 1 / aspectRatio)
        
    ' Adjust ViewPortLeft and ViewPortTop to keep center point unchanged
    ChartViewer1.ViewportLeft = centerX - ChartViewer1.ViewportWidth / 2
    ChartViewer1.ViewportTop = centerY - ChartViewer1.ViewportHeight / 2
        
    ' Update the chart
    Call ChartViewer1.updateViewPort(True, False)
    
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".ZoomBar_Scroll)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' *** DRAWING CHARTS END ***

' *** TABLE SUBS

Private Sub add_row(time As String, temp As String, rho As String, res As String, _
  volt As String, cur As String, ramp As String)
  
    ' Method is used to add a new row to ReportControl
    Dim Record As ReportRecord
    Dim item As ReportRecordItem
    
    Set Record = tableResult.Records.Add()
    
    Set item = Record.AddItem(time)
    Set item = Record.AddItem(temp)
    Set item = Record.AddItem(rho)
    Set item = Record.AddItem(res)
    Set item = Record.AddItem(volt)
    Set item = Record.AddItem(cur)
    Set item = Record.AddItem(ramp)
End Sub

Private Sub tableResult_RowRClick(ByVal row As ReportRow, ByVal item As ReportRecordItem)
  On Error GoTo myErrorHandler
  
  Call PopupMenu(mnuTable)
    
Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".tableResult_RowRClick)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub tableResult_KeyDown(KeyCode As Integer, Shift As Integer)
  On Error GoTo myErrorHandler
  Dim row As ReportRow
    
  If Shift = vbCtrlMask And KeyCode = vbKeyA Then
    For Each row In tableResult.Rows
      tableResult.Rows(row.Index).Selected = True
    Next
  End If
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".tableResult_KeyDown)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub
  
Private Sub tableResult_MouseMove(Button As Integer, Shift As Integer, X As Long, Y As Long)
  On Error GoTo myErrorHandler
  
  Dim htInfo As ReportHitTestInfo
  Set htInfo = tableResult.HitTest(X, Y)
    
  Dim item As ReportRecordItem
  If (Not htInfo.item Is Nothing) Then
    If (htInfo.item.Index = COLUMN_TIME) Then
      Set item = htInfo.item
    ElseIf (htInfo.item.Index = COLUMN_T) Then
      Set item = htInfo.item
    ElseIf (htInfo.item.Index = COLUMN_RESISTIVITY) Then
      Set item = htInfo.item
    ElseIf (htInfo.item.Index = COLUMN_RESISTANCE) Then
      Set item = htInfo.item
    ElseIf (htInfo.item.Index = COLUMN_VOLTAGE) Then
      Set item = htInfo.item
    ElseIf (htInfo.item.Index = COLUMN_CURRENT) Then
      Set item = htInfo.item
    ElseIf (htInfo.item.Index = COLUMN_RAMPING) Then
      Set item = htInfo.item
    End If
  End If
    
  If (Not item Is ItemHot) Then
    If (Not item Is Nothing) Then
      item.BackColor = RGB(255, 220, 190) 'RGB(255, 238, 194) ' on the item = yellowish
    End If
    If (Not ItemHot Is Nothing) Then
      'try change
      ItemHot.BackColor = RGB(255, 255, 255) ' leave = white
    End If
    Set ItemHot = item
    tableResult.Redraw
  End If

Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".tableResult_MouseMove)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' *** TABLE SUBS END

Private Sub cmdFindingdevices_Click()
On Error GoTo myErrorHandler

  Dim result_after_finding_devices As String
  Dim GPIB_primary_addresses(31) As Integer ' array of primary addresses
  'Dim GPIB_address(30) As Integer
  'Dim number_of_devices As Integer

  '  INITIALIZATION SECTION

  '  The GPIB board board needs to be the Controller-In-Charge in order to find all
  '  listeners on the GPIB.  To accomplish this, the subroutine SendIFC
  '  is called.  If the error bit EERR is set in ibsta, call GpibErr with
  '  an error message.

  Call SendIFC(GPIB0)
  If (ibsta And EERR) Then
    GpibErr ("Error sending IFC.")
  End If

  '  Create an array containing all valid GPIB primary addresses, except
  '  for the primary address 0. Your GPIB interface board is at address 0
  '  by default.  This array (devices%) will be given to the subroutine
  '  FindLstn to find all listeners.  The constant NOADDR, defined in
  '  NIGLOBAL.BAS, signifies the end of the array.

  For k = 0 To 29
    GPIB_primary_addresses(k) = k + 1
  Next k
  GPIB_primary_addresses(30) = NOADDR

  '  Print message to tell user that the program is searching for all active
  '  listeners.  Find all of the listeners on the bus.  Store the listen
  '  addresses in the array GPIB_address%.  If the error bit ERR is set in ibsta,
  '  call GpibErr with an error message.

  Screen.MousePointer = 11    ' Wait(hourglass) cursor

  txtResponse.AddItem "Finding all devices on the bus..."
  txtResponse.Refresh

  Call FindLstn(GPIB0, GPIB_primary_addresses(), GPIB_address(), 31)
  If (ibsta And EERR) Then
    Screen.MousePointer = 0
    GpibErr ("Error finding all listeners.")
  End If

  Screen.MousePointer = 0

  '  ibcntl contains the actual number of addresses stored in the GPIB_address%
  '  array. Assign the value of ibcntl to the variable number_of_devices.
  '  Print the number of listeners found.

  number_of_devices = ibcnt

  result_after_finding_devices = "Number of devices found = " + Str$(number_of_devices%)
  txtResponse.AddItem result_after_finding_devices
   
  ReDim reading_from_each_instrument(number_of_devices - 1)

  '  The GPIB_address% array contains the addresses of all listening devices
  '  found by FindLstn. Use the constant NOADDR, as defined in NIGLOBAL.BAS,
  '  to signify the end of the array.

  GPIB_address(ibcntl) = NOADDR 'the last array, I do not need to display it
  For k = 0 To number_of_devices - 1
    txtResponse.AddItem "GPIB Address bus" & k & " = " & GPIB_address(k)
  Next k

  '  DevClearList will send the GPIB Selected Device Clear (SDC) command
  '  message to all the devices on the bus. If the error bit EERR is set in
  '  ibsta, call GpibErr with an error message.

  Call DevClearList(GPIB0, GPIB_address)
  If (ibsta And EERR) Then
    GpibErr ("Error in clearing the devices. ")
  End If

  Set mFile = mFileSysObj.GetFile("c:\number_of_devices.dat")
  Set mTxtStream = mFile.OpenAsTextStream(ForWriting)
  mTxtStream.WriteLine (number_of_devices)
  For k = 0 To number_of_devices - 1
    mTxtStream.WriteLine (GPIB_address(k))
  Next k
  Call mTxtStream.Close
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".cmdFindingDevices_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' *** METHOD SUBS ***

Private Sub cmdBrowse_Click()
  On Error GoTo myErrorHandler ' look again on error handler
  dlgFile.FLAGS = cdlOFNOverwritePrompt
  dlgFile.CancelError = True
  dlgFile.Filter = "deizRho (*.rho)|*.rho)"
  dlgFile.FilterIndex = m_FilterSelected
  dlgFile.filename = txtFile.text
  dlgFile.DialogTitle = "Save Experiment Data"

  dlgFile.ShowSave
    
  txtFile.text = dlgFile.filename
  txtFileName.text = dlgFile.FileTitle
  m_FilterSelected = dlgFile.FilterIndex
  
  Exit Sub
myErrorHandler:
  If Err.Number = cdlCancel Then Exit Sub
  
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".cmdBrowse_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub txtFile_Change()
  On Error Resume Next

  'If devices_are_connected = False Then
    'Dim splitted_path() As String
    'splitted_path = Split(txtFile.text, "\")
    'txtFileName.text = splitted_path(UBound(splitted_path))
  'End If
  
  txtFile.ToolTipText = txtFile.text
End Sub

Private Sub txtFileName_Change()
  On Error GoTo myErrorHandler

  Dim splitted_path() As String
  splitted_path = Split(txtFile.text, "\")
  txtFile.text = ""
  For i = 0 To UBound(splitted_path) - 1
    txtFile.text = txtFile.text & splitted_path(i) & "\"
  Next i
  txtFile.text = txtFile.text & txtFileName.text
    
  txtFileName.ToolTipText = txtFileName.text
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".txtFileName_Change)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub calculate_RhoFactor()
  On Error GoTo myErrorHandler

  If optRectangular = True Then
    Rho_factor = CDbl(txtSampleWidth.text) * _
      CDbl(txtSampleThickness.text) / (CDbl(txtSampleLength.text) * 1000)
  Else ' cylinder
    Rho_factor = ((CDbl(txtSampleWidth.text)) ^ 2) * 3.14159 / _
      (4 * CDbl(txtSampleLength.text) * 1000)
  End If
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".calculate_RhoFactor)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub optCylinder_Click()
  lblWidth.Caption = "Diameter :"
  lblThickness.Enabled = False
  txtSampleThickness.Enabled = False
  lblThicknessUnit.Enabled = False
End Sub

Private Sub optRectangular_Click()
  lblWidth.Caption = "Width :"
  lblThickness.Enabled = True
  txtSampleThickness.Enabled = True
  lblThicknessUnit.Enabled = True
End Sub

Private Sub cmdApplyMethod_Click() ' similar codes in mnuConnectdevices_Click
  On Error GoTo myErrorHandler
  
  If devices_are_connected = False Then
    MsgBox "Devices are not connected yet."
    Exit Sub
  End If
    
  If Not filter_result_is_OK Then Exit Sub
  
  ' stop_time is updated in filter_result_is_OK function
  ' data_points is updated in filter_result_is_OK function
  
  If experiment_is_running = True Then
    If 60 * Val(txtStopTime.text) < time_elapsed(times_of_readings) + 10 Then
      txtStopTime.text = time_elapsed(times_of_readings) + 10
    End If
  End If
  If stop_time <> Val(txtStopTime.text) And experiment_is_running = True Then
    Call redim_variables
  End If
    
  Call calculate_RhoFactor
       
  Call check_delta_settings_change ' set delta_setttings_change
       
  If delta_settings_change = True And experiment_is_running = False Then
       
    tmrReadDevicesOnlyToDisplay.Enabled = False
  
    Call exit_delta
    Call ilwrt(Dev(0), ":disp:enab 1", 12)
    If (ibsta And EERR) Then
      GpibErr ("Error enabling front panel of Keithley 6220 [Apply Button]")
    End If
    
    Call apply_setup_delta
    Call arm_delta
    Sleep 1000
    Call ilwrt(Dev(0), ":disp:enab 0", 12)
    If (ibsta And EERR) Then
      GpibErr ("Error disabling front panel of Keithley 6220 [Apply Button]")
    End If
    Sleep 1000
  
    'Call trigger_delta ' First trigger before first reading
    'Sleep 1000 * Val(txtDataPoints.text) ' delta operation needs 350 sec for 5 counts
  
    tmrReadDevicesOnlyToDisplay.Enabled = True
  
    method_is_applied = True
  End If
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".cmdApplyMethod_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub validate_file_name()
  On Error GoTo myErrorHandler
  
  If txtFileName.text = "" Then
    txtFileName.text = "my data.rho"
    txtFile.text = "C:\my data.rho"
  End If
  If txtFile.text = "" Then
    txtFile.text = "C:\my data.rho"
  End If
  If Right$(txtFile.text, 4) <> ".rho" Then
    txtFile.text = txtFile.text & ".rho"
  End If
    
  If mFileSysObj.FileExists(txtFile.text) = True Then
    Dim splitted() As String
    splitted = Split(txtFile.text, "_")
    txtFile.text = splitted(0)
    txtFile.text = Replace(txtFile.text, ".rho", "") & _
      "_" & Replace(Replace(Now, "/", "-"), ":", "-") & ".rho"
  End If
  ' If no folder path, eg. "blabla.flo", specified, the filename is saved in c:

  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".validate_file_name)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' This sub is called by mnuStart_Click
Private Sub redim_variables()
  Dim max_data As Long
  max_data = stop_time / data_points + 100 ' + 100 just for save play
                        ' data_points set in filter_result_is_OK in mnuStart_Click
  ReDim Preserve emf(1 To max_data) As Double ' max data = 12 hours if data 1 s/pt
  ReDim Preserve Temperature(1 To max_data) As Double

  ReDim Preserve time_elapsed(max_data) As Long
  ReDim Preserve time_clock(max_data) As Date
  ReDim Preserve time_remained(max_data) As Long
  
  ReDim Preserve voltage(1 To max_data) As Double
  ReDim Preserve current(1 To max_data) As Double
  ReDim Preserve resistance(1 To max_data) As Double
  ReDim Preserve resistivity(1 To max_data) As Double
End Sub

' In this sub, stop_time is set, interval of tmrReadDevices and tmrReadDevicesOnlyToDisplay
' is set
Private Function filter_result_is_OK() As Boolean
  On Error GoTo myErrorHandler
  
  Dim i As Integer
  
  Call validate_file_name
  
  If Val(txtSampleLength.text) <= 0 Or Not IsNumeric(txtSampleLength.text) Then
    MsgBox "Sample length is invalid !"
    filter_result_is_OK = False
    Exit Function
  End If
  
  If Val(txtSampleWidth.text) <= 0 Or Not IsNumeric(txtSampleWidth.text) Then
    MsgBox "Sample width is invalid !"
    filter_result_is_OK = False
    Exit Function
  End If
  
  If Val(txtSampleThickness.text) <= 0 Or Not IsNumeric(txtSampleThickness.text) Then
    MsgBox "Sample thickness is invalid !"
    filter_result_is_OK = False
    Exit Function
  End If
  
  If Val(txtCurrent.text) <= 0 Or Not IsNumeric(txtCurrent.text) Or _
    Val(txtCurrent.text) > 105 Then
    MsgBox "Source current is invalid !"
    filter_result_is_OK = False
    Exit Function
  End If
  
  If Val(txtCompliance.text) <= 0.2 Or Not IsNumeric(txtCompliance.text) Or _
    Val(txtCompliance.text) > 40 Then ' max 6220 = 105 V, min = 0.1
    MsgBox "Compliance is invalid !"
    filter_result_is_OK = False
    Exit Function
  End If
  
  If Val(txtDeltaCount.text) <= 3 Or Not IsNumeric(txtDeltaCount.text) Or _
    Val(txtDeltaCount.text) > 60000 Then ' related with txtDataPoints
    MsgBox "Delta count is invalid !"  ' max 6220 = 65536, min = 1
    filter_result_is_OK = False
    Exit Function
  End If
  
  If Val(txtDelay.text) < 0.001 Or Not IsNumeric(txtDelay.text) Or _
    Val(txtDelay.text) > 3600 Then
    MsgBox "Delta delay is invalid !" ' min 6220 = 0.001, max = 9999.999 s
    filter_result_is_OK = False
    Exit Function
  End If
  
  If Val(txtDataPoints.text) < 1 Or Not IsNumeric(txtDataPoints.text) Or _
    Val(txtDataPoints.text) > 60 Then
    MsgBox "Data points is invalid !"
    filter_result_is_OK = False
    Exit Function
  Else ' OK
    data_points = Val(txtDataPoints.text) ' data_points needed in redim_variables
    tmrReadDevicesOnlyToDisplay.Interval = 1000 * data_points
    tmrReadDevices.Interval = 1000 * data_points
  End If
  
  For i = 0 To cboAnalogFilter.ListCount - 1
    If cboAnalogFilter.text = cboAnalogFilter.List(i) Then ' OK
      Exit For
    Else
      filter_result_is_OK = False
      If i = cboAnalogFilter.ListCount - 1 Then
        MsgBox "Analog filter is invalid"
        Exit Function
      End If
    End If
  Next i
  
  For i = 0 To cboDigitalFilter.ListCount - 1
    If cboDigitalFilter.text = cboDigitalFilter.List(i) Then ' OK
      Exit For
    Else
      filter_result_is_OK = False
      If i = cboDigitalFilter.ListCount - 1 Then
        MsgBox "Digital filter is invalid"
        Exit Function
      End If
    End If
  Next i
  
  If Val(txtDigitalFilterCount.text) < 1 Or Not IsNumeric(txtDigitalFilterCount.text) Or _
    Val(txtDigitalFilterCount.text) > 50 Then
    MsgBox "Digital filter count is invalid !"
    filter_result_is_OK = False
    Exit Function
  End If
  
  If Val(txtRate.text) < 1 Or Not IsNumeric(txtRate.text) Or _
    Val(txtRate.text) > 50 Then
    MsgBox "Rate is invalid !"
    filter_result_is_OK = False
    Exit Function
  End If
  
  For i = 0 To cboVoltageRange.ListCount - 1
    If cboVoltageRange.text <> cboVoltageRange.List(i) Then ' OK
      Exit For
    Else
      filter_result_is_OK = False
      If i = cboVoltageRange.ListCount - 1 Then
        MsgBox "Voltage range is invalid"
        Exit Function
      End If
    End If
  Next i
  
  If Val(txtNumberOfDevices.text) < 1 Or Not IsNumeric(txtNumberOfDevices.text) Or _
    Val(txtNumberOfDevices.text) > 30 Then
    MsgBox "Number of devices is invalid !"
    filter_result_is_OK = False
    Exit Function
  End If
  
  If txtAddress.text = "" Then
    MsgBox "Address box is empty ! Enter addresses seperated by comma."
    filter_result_is_OK = False
    Exit Function
  End If
  
  Dim splitted() As String
  number_of_devices = Val(txtNumberOfDevices.text)
  splitted = Split(Trim(txtAddress.text), ",")
  For i = LBound(splitted) To UBound(splitted)
    If splitted(i) = "" Or Not IsNumeric(splitted(i)) Or _
      Val(splitted(i)) < 0 Or Val(splitted(i)) > 30 Then
      
      MsgBox "Address info is invalid ! Enter addresses seperated by comma."
      filter_result_is_OK = False
      Exit Function
    End If
  Next i
  If number_of_devices <> UBound(splitted) + 1 Then
    MsgBox "Number of devices not the same as the number of the addresses."
      filter_result_is_OK = False
      Exit Function
  End If
  
  If Val(txtDeviceNumber.text) < 0 Or Not IsNumeric(txtDeviceNumber.text) Or _
    Val(txtDeviceNumber.text) > 29 Then
    MsgBox "Device number is invalid !"
    filter_result_is_OK = False
    Exit Function
  End If
  
  If Val(txtStopTime.text) < 0.1 Or Not IsNumeric(txtStopTime.text) Or _
    Val(txtStopTime.text) > 604800 Then ' 7 days
    MsgBox "Stop Time is invalid !"
    filter_result_is_OK = False
    Exit Function
  Else ' OK
    stop_time = 60 * Val(txtStopTime.text) ' sec
  End If
  
  filter_result_is_OK = True

Exit Function
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Function. : " & Me.Name & ".filter_result_is_OK)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Function

Private Sub check_delta_settings_change()
  On Error GoTo myErrorHandler

  delta_settings_change = False
  If txtCurrent.text <> delta_current Then ' delta_current set in load_settings sub
    delta_settings_change = True           ' and apply_delta sub
    Exit Sub
  End If
  
  If txtCompliance.text <> delta_compliance Then ' delta_current set in load_settings sub
    delta_settings_change = True           ' and apply_delta sub
    Exit Sub
  End If
  
  If txtDeltaCount.text <> delta_count Then ' delta_current set in load_settings sub
    delta_settings_change = True           ' and apply_delta sub
    Exit Sub
  End If
  
  If txtDelay.text <> delta_delay Then ' delta_current set in load_settings sub
    delta_settings_change = True           ' and apply_delta sub
    Exit Sub
  End If
  
  If cboAnalogFilter.text <> analog_filter Then ' delta_current set in load_settings sub
    delta_settings_change = True           ' and apply_delta sub
    Exit Sub
  End If
  
  If cboDigitalFilter.text <> digital_filter Then ' delta_current set in load_settings sub
    delta_settings_change = True           ' and apply_delta sub
    Exit Sub
  End If
  
  If txtDigitalFilterCount.text <> digital_filter_count Then ' delta_current set in load_settings sub
    delta_settings_change = True           ' and apply_delta sub
    Exit Sub
  End If
  
  If txtRate.text <> delta_rate Then ' delta_current set in load_settings sub
    delta_settings_change = True           ' and apply_delta sub
    Exit Sub
  End If
  
  If cboVoltageRange.text <> voltage_range Then ' delta_current set in load_settings sub
    delta_settings_change = True           ' and apply_delta sub
    Exit Sub
  End If
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".check_delta_settings_change)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' This sub is called by mnuConnectDevices_Click and cmdApply_Click subs
Private Sub apply_setup_delta()
  On Error GoTo myErrorHandler

  Dim command As String
  Dim k As Integer
  
  k = Val(txtDeviceNumber.text)
    
  'command = ":sour:swe:abort"
  'Call ilwrt(Dev(k), command, Len(command))
  'MsgBox "OK 1"
  'command = ":sour:wave:abor"
  'Call ilwrt(Dev(k), command, Len(command))
  'MsgBox "OK 2"
  ''command = "*rst"
  'Call ilwrt(Dev(k), command, Len(command))
  'MsgBox "OK 3"
  '<SETUPSTRING>
  'Sleep 500
  
  command = ":Form:elem READ" ', TST, RNUM, AVOL"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting delta element format"
  End If
  
  command = ":sour:curr:rang 105e-3"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting delta current range"
  End If
  
  command = ":sour:delt:high " & Trim(txtCurrent.text) & "e-3"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting delta current"
  End If
  delta_current = txtCurrent.text ' update
  
  'command = ":sour:delt:low <DELTALOW>"
  'Call ilwrt(Dev(k), command, Len(command))
  
  command = ":sour:delt:count " & txtDeltaCount.text
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting delta countt"
  End If
  delta_count = txtDeltaCount.text ' update
  
  command = ":sour:delt:delay " & txtDelay.text
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting delta delay"
  End If
  delta_delay = txtDelay.text ' update
  
  If cboAnalogFilter.text = "ON" Then
    command = ":sour:curr:filt:stat 1"
  Else
    command = ":sour:curr:filt:stat 0"
  End If
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting analog filter"
  End If
  analog_filter = cboAnalogFilter.text ' update
  
  command = ":sour:curr:comp " & txtCompliance.text
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting compliance"
  End If
  delta_compliance = txtCompliance.text ' update
    
  'command = ":SYST:COMM:SERIal:Send" & " *rst"
  'Call ilwrt(Dev(k), command, Len(command))
  'If (ibsta And EERR) Then
    'GpibErr "Error sending *rst to 2182A"
  'End If
  ''Sleep 400
  
  'command = ":SYST:COMM:SERIal:Send" & " :sens:volt:nplc " & txtRate.Text
  'Call ilwrt(Dev(k), command, Len(command))
  'If (ibsta And EERR) Then
    'GpibErr "Error setting delta rate"
  'End If
  'delta_rate = txtRate.text
  ''Sleep 1500
  
  If cboVoltageRange.text = "Auto" Then
    'command = ":SYST:COMM:SERIal:Send" & " :sens:volt:rang:auto ON"
  Else
    'command = ":SYST:COMM:SERIal:Send" & " :sens:volt:rang:auto OFF"
    'Call ilwrt(Dev(k), command, Len(command))
    'If (ibsta And EERR) Then
      'GpibErr "Error setting voltage range OFF"
    'End If
    'Sleep 1500
    
    'command = ":SYST:COMM:SERIal:Send" & " :sens:volt:rang " & cboVoltageRange.Text
  End If
  'Call ilwrt(Dev(k), command, Len(command))
  'If (ibsta And EERR) Then
    'GpibErr "Error setting voltage range"
  'End If
  'voltage_range = cboVoltageRange.text
  
  'command = ":sens:aver:wind 0"
  'Call ilwrt(Dev(k), command, Len(command))
  
  If cboDigitalFilter.text = "None" Then
    command = ":sens:aver:stat OFF"
  Else
    command = ":sens:aver:stat ON"
    'Call ilwrt(Dev(k), command, Len(command))
    'If (ibsta And EERR) Then
      'GpibErr "Error setting digital filter"
    'End If
    digital_filter = cboDigitalFilter.text
    
    command = ":sens:aver:tcon " & Left(cboDigitalFilter.text, 3)
  End If
  'Call ilwrt(Dev(k), command, Len(command))
  'If (ibsta And EERR) Then
    'GpibErr "Error setting digital filter"
  'End If
  digital_filter = cboDigitalFilter.text
  
  command = ":sens:aver:coun " & txtDigitalFilterCount.text
  'Call ilwrt(Dev(k), command, Len(command))
  'If (ibsta And EERR) Then
    'GpibErr "Error setting digital filter count"
  'End If
  digital_filter_count = txtDigitalFilterCount.text
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".apply_setup_delta)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub arm_delta()
  Dim k As Integer
  Dim command As String

  k = Val(txtDeviceNumber.text)
  
  command = ":sour:delt:arm"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error arming delta"
  End If
End Sub

Private Sub apply_setup_non_delta()
  On Error GoTo myErrorHandler

  Dim command As String
  Dim k As Integer
  
  k = Val(txtDeviceNumber.text)
    
  ''command = "*rst"
  'Call ilwrt(Dev(k), command, Len(command))
  'MsgBox "OK 3"
  '<SETUPSTRING>
  'Sleep 500
  
  'command = ":Form:elem READ" ', TST, RNUM, AVOL"
  'Call ilwrt(Dev(k), command, Len(command))
  'If (ibsta And EERR) Then
    'GpibErr "Error setting delta element format"
  'End If
  
  command = ":sour:clear"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting delta current range"
  End If
      
  command = ":sour:curr:rang 105e-3"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting delta current range"
  End If
  
  command = ":sour:curr " & Trim(txtCurrent.text) & "e-3"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting delta current"
  End If
  delta_current = txtCurrent.text ' update
    
  command = ":sour:curr:comp " & txtCompliance.text
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting compliance"
  End If
  delta_compliance = txtCompliance.text ' update
    
  command = ":sour:outp:resp FAST"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting current response Fast or Slow"
  End If
  
  command = ":sour:outp ON"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting current ON"
  End If
  
  
    
  ' *** Voltage reading/setting
  k = 2
  
  ''command = "*rst"
  'Call ilwrt(Dev(k), command, Len(command))
  'MsgBox "OK 3"
  '<SETUPSTRING>
  'Sleep 500
  
  command = ":sens:func 'VOLT'"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting voltage reading selection"
  End If
  
  command = ":sens:chan 1"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting channel number"
  End If
      
  If cboVoltageRange.text = "Auto" Then
    command = ":sens:volt:chan1:rang:auto ON"
  Else
    command = ":sens:volt:chan1:rang:auto OFF"
    Call ilwrt(Dev(k), command, Len(command))
    If (ibsta And EERR) Then
      GpibErr "Error setting voltage range OFF"
    End If
    Sleep 1500
    
    command = ":sens:volt:rang " & cboVoltageRange.text
  End If
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting voltage range"
  End If
  voltage_range = cboVoltageRange.text
  
  command = ":sens:volt:nplc " & txtRate.text
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting NPLC"
  End If
  delta_rate = txtRate.text
  ''Sleep 1500
    
  If cboAnalogFilter.text = "ON" Then
    command = ":sens:volt:chan1:LPAS ON"
  Else
    command = ":sens:volt:chan1:LPAS OFF"
  End If
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting analog filter"
  End If
  analog_filter = cboAnalogFilter.text ' update
  
  If cboDigitalFilter.text = "None" Then
    command = ":sens:volt:chan1:dfilt:STAT OFF"
  Else
    command = ":sens:volt:chan1:dfilt:STAT ON"
    'Call ilwrt(Dev(k), command, Len(command))
    'If (ibsta And EERR) Then
      'GpibErr "Error setting digital filter"
    'End If
    digital_filter = cboDigitalFilter.text
    
    
  End If
  digital_filter = cboDigitalFilter.text
  ' ******************************************************************
      
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".apply_setup_delta)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub exit_delta()
  Dim k As Integer
  Dim command As String

  k = Val(txtDeviceNumber.text)
  
  command = ":sour:swe:abor"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error exit delta"
  End If
End Sub

Private Sub exit_non_delta()
  
  ' Turn off output of power supply
  command = ":sour:outp OFF"
  Call ilwrt(Dev(0), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error setting current off"
  End If
  
  ' Voltage 2821 no need
End Sub

Private Sub trigger_delta()
  Dim command As String
  Dim k As Integer
    
  k = Val(txtDeviceNumber.text)
  
  command = ":init:imm"
  Call ilwrt(Dev(k), command, Len(command))
  If (ibsta And EERR) Then
    GpibErr "Error triggering delta"
  End If
End Sub

Private Sub cmdInitialize_Click()
  On Error GoTo myErrorHandler
  
  If filter_result_is_OK = False Then Exit Sub

  If experiment_is_running = True Then
    MsgBox "Experiment is running !"
    Exit Sub
  End If

  If mnuConnectDevices.Checked = True Then
    Call mnuConnectdevices_Click ' disconnect devices
  End If
  
  Call initialize_devices
  
  write_status "Initialize button pressed"
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".cmdInitialize_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub cmdSendCommand_Click()
  On Error GoTo myErrorHandler
  
  If initialize_devices_is_done = False Then
    MsgBox "Devices are not initialized yet !"
    Exit Sub
  End If
  
  Call cmdSetListen_Click
  
  Dim k As Integer
  k = Val(txtDeviceNumber.text)
  If Not IsNumeric(txtDeviceNumber.text) Or k < 0 Or k > 30 Then
    MsgBox "Device number must be between 0 to 29 !"
    Exit Sub
  End If
    
  reading_from_all_devices = Space$(200)
  
  ' Send command or query
  Call ilwrt(Dev(k), Trim(txtCommand.text), Len(Trim(txtCommand.text)))
  If (ibsta And EERR) Then
    GpibErr ("Error in writing command to device. ")
  End If
  
  ' Receive response
  Call ilrd(Dev(k), reading_from_all_devices, Len(reading_from_all_devices))
  If (ibsta And EERR) Then
    GpibErr ("Error in receiving response to ilrd. ")
  End If

  'reading_from_each_instrument(k) = Left$(reading_from_all_devices, ibcntl - 1)
  reading_from_each_instrument(k) = reading_from_all_devices
  
  txtResponse.text = reading_from_each_instrument(k)
        
  ' set back the device to listen condition
  Call cmdSetListen_Click
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".cmdSendCommand_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

Private Sub cmdSetListen_Click()
  On Error GoTo myErrorHandler
  
  If initialize_devices_is_done = False Then
    MsgBox "Devices are not initialized yet !"
    Exit Sub
  End If

  Dim k As Integer
  k = Val(txtDeviceNumber.text)
  If Not IsNumeric(txtDeviceNumber.text) Or k < 0 Or k > 30 Then
    MsgBox "Device number must be between 0 to 29 !"
    Exit Sub
  End If
    
  ' reset the GPIB portion of the device
  ilclr Dev(k)
  If (ibsta And EERR) Then
    Call GpibErr("Error setting GPIB device to listen")
  Else
    write_status "Setting to Listen of GPIB device " & i + 1 & " OK"
  End If
  
  Exit Sub
myErrorHandler:
  Dim msg As String
  'Call turn_off_all_timers
  msg = "Error " & Err.Number & " :  " & Err.Description & _
    " (Sub. : " & Me.Name & ".cmdSetListen_Click)"
  write_status msg
  MsgBox msg, vbCritical, "Error"
  Call save_settings
  Call save_status
End Sub

' *** METHOD SUBS END ***
