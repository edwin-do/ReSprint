VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   6945
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   8895
   LinkTopic       =   "Form1"
   ScaleHeight     =   6945
   ScaleWidth      =   8895
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame2 
      Caption         =   "6220/2182A Delta"
      Height          =   3735
      Left            =   120
      TabIndex        =   13
      Top             =   3120
      Width           =   8655
      Begin VB.ComboBox cboVoltageRange 
         Height          =   315
         ItemData        =   "Form1.frx":0000
         Left            =   4200
         List            =   "Form1.frx":0016
         TabIndex        =   49
         Text            =   "Auto"
         Top             =   1800
         Width           =   855
      End
      Begin VB.ComboBox cboAnalogFilter 
         Height          =   315
         ItemData        =   "Form1.frx":0037
         Left            =   3840
         List            =   "Form1.frx":0041
         TabIndex        =   48
         Text            =   "OFF"
         Top             =   360
         Width           =   1215
      End
      Begin VB.TextBox txtRate 
         Height          =   285
         Left            =   4440
         TabIndex        =   45
         Text            =   "1"
         Top             =   1440
         Width           =   615
      End
      Begin VB.TextBox txtFilterCount 
         Height          =   285
         Left            =   4440
         TabIndex        =   41
         Text            =   "5"
         Top             =   1080
         Width           =   615
      End
      Begin VB.ComboBox cboFilterType 
         Height          =   315
         ItemData        =   "Form1.frx":004E
         Left            =   3840
         List            =   "Form1.frx":005B
         TabIndex        =   40
         Text            =   "None"
         Top             =   720
         Width           =   1215
      End
      Begin VB.TextBox txtDelay 
         Height          =   285
         Left            =   1440
         TabIndex        =   37
         Text            =   "0.002"
         Top             =   1800
         Width           =   735
      End
      Begin VB.CommandButton cmdExitDelta 
         Caption         =   "EXIT DELTA"
         Height          =   375
         Left            =   2760
         TabIndex        =   35
         Top             =   3240
         Width           =   1095
      End
      Begin VB.Frame Frame3 
         Caption         =   "Trigger"
         Height          =   2055
         Left            =   5280
         TabIndex        =   27
         Top             =   240
         Width           =   3255
         Begin VB.CheckBox chkDisplayOff 
            Caption         =   "Display Off"
            Height          =   255
            Left            =   1800
            TabIndex        =   39
            Top             =   360
            Width           =   1215
         End
         Begin VB.CheckBox chkContinuous 
            Caption         =   "Continuous"
            Height          =   255
            Left            =   240
            TabIndex        =   33
            Top             =   360
            Width           =   1095
         End
         Begin VB.Timer Timer1 
            Enabled         =   0   'False
            Left            =   720
            Top             =   240
         End
         Begin VB.TextBox txtInterval 
            Enabled         =   0   'False
            Height          =   285
            Left            =   1440
            TabIndex        =   30
            Text            =   "1"
            Top             =   720
            Width           =   495
         End
         Begin VB.TextBox txtElapsedTime 
            Enabled         =   0   'False
            Height          =   285
            Left            =   1440
            TabIndex        =   29
            Text            =   "Elapsed Time"
            Top             =   1080
            Width           =   1575
         End
         Begin VB.TextBox txtAverage 
            Enabled         =   0   'False
            Height          =   285
            Left            =   1200
            TabIndex        =   28
            Text            =   "Average"
            Top             =   1560
            Width           =   1815
         End
         Begin VB.Label Label12 
            Caption         =   "Readings :"
            Height          =   255
            Left            =   240
            TabIndex        =   34
            Top             =   1560
            Width           =   855
         End
         Begin VB.Label Label3 
            Caption         =   "Interval (s) :"
            Height          =   255
            Left            =   240
            TabIndex        =   32
            Top             =   720
            Width           =   975
         End
         Begin VB.Label Label2 
            Caption         =   "Elapsed Time :"
            Height          =   255
            Left            =   240
            TabIndex        =   31
            Top             =   1080
            Width           =   1215
         End
      End
      Begin VB.TextBox txtAddressDelta 
         Height          =   285
         Left            =   1440
         TabIndex        =   25
         Text            =   "1"
         Top             =   360
         Width           =   735
      End
      Begin VB.TextBox txtDeltaCount 
         Height          =   285
         Left            =   1440
         TabIndex        =   24
         Text            =   "5"
         Top             =   1440
         Width           =   735
      End
      Begin VB.TextBox txtCompliance 
         Height          =   285
         Left            =   1440
         TabIndex        =   21
         Text            =   "10"
         Top             =   1080
         Width           =   735
      End
      Begin VB.TextBox txtCurrent 
         Height          =   285
         Left            =   1440
         TabIndex        =   17
         Text            =   "8"
         Top             =   720
         Width           =   735
      End
      Begin VB.CommandButton cmdApplySetupDelta 
         Caption         =   "APPLY DELTA SETUP"
         Height          =   375
         Left            =   120
         TabIndex        =   16
         Top             =   2280
         Width           =   2055
      End
      Begin VB.CommandButton cmdArmDelta 
         Caption         =   "ARM DELTA"
         Height          =   375
         Left            =   120
         TabIndex        =   15
         Top             =   3240
         Width           =   1335
      End
      Begin VB.CommandButton cmdTrigger 
         Caption         =   "TRIGGER"
         Height          =   375
         Left            =   1560
         TabIndex        =   14
         Top             =   3240
         Width           =   1095
      End
      Begin VB.Label Label21 
         Caption         =   "Analog Filter :"
         Height          =   255
         Left            =   2640
         TabIndex        =   47
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label19 
         Caption         =   "Voltage Range (V) :"
         Height          =   255
         Left            =   2640
         TabIndex        =   46
         Top             =   1800
         Width           =   1575
      End
      Begin VB.Label Label18 
         Caption         =   "Rate (NPLCs) :"
         Height          =   255
         Left            =   2640
         TabIndex        =   44
         Top             =   1440
         Width           =   1215
      End
      Begin VB.Label Label17 
         Caption         =   "Digital Filter Count :"
         Height          =   255
         Left            =   2640
         TabIndex        =   43
         Top             =   1080
         Width           =   1695
      End
      Begin VB.Label Label16 
         Caption         =   "Digital Filter :"
         Height          =   255
         Left            =   2640
         TabIndex        =   42
         Top             =   720
         Width           =   1095
      End
      Begin VB.Label Label15 
         Caption         =   "s"
         Height          =   255
         Left            =   2280
         TabIndex        =   38
         Top             =   1800
         Width           =   495
      End
      Begin VB.Label Label14 
         Caption         =   "Delay :"
         Height          =   255
         Left            =   120
         TabIndex        =   36
         Top             =   1800
         Width           =   1335
      End
      Begin VB.Label Label13 
         Caption         =   "Device No. :"
         Height          =   255
         Left            =   120
         TabIndex        =   26
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label11 
         Caption         =   "Count :"
         Height          =   255
         Left            =   120
         TabIndex        =   23
         Top             =   1440
         Width           =   975
      End
      Begin VB.Label Label10 
         Caption         =   "V"
         Height          =   255
         Left            =   2280
         TabIndex        =   22
         Top             =   1200
         Width           =   375
      End
      Begin VB.Label Label9 
         Caption         =   "Compliance :"
         Height          =   375
         Left            =   120
         TabIndex        =   20
         Top             =   1080
         Width           =   975
      End
      Begin VB.Label Label8 
         Caption         =   "mA"
         Height          =   255
         Left            =   2280
         TabIndex        =   19
         Top             =   840
         Width           =   495
      End
      Begin VB.Label Label7 
         Caption         =   "Current :"
         Height          =   255
         Left            =   120
         TabIndex        =   18
         Top             =   720
         Width           =   975
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Send Command"
      Height          =   1935
      Left            =   120
      TabIndex        =   5
      Top             =   1080
      Width           =   8655
      Begin VB.TextBox txtCommand 
         Height          =   375
         Left            =   1200
         TabIndex        =   10
         Text            =   ":trace:data?"
         Top             =   720
         Width           =   7335
      End
      Begin VB.TextBox txtResponse 
         Height          =   615
         Left            =   1200
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   9
         Text            =   "Form1.frx":0075
         Top             =   1200
         Width           =   7335
      End
      Begin VB.CommandButton cmdSendCommand 
         Caption         =   "SEND"
         Height          =   375
         Left            =   120
         TabIndex        =   8
         Top             =   720
         Width           =   975
      End
      Begin VB.TextBox txtDeviceNumber 
         Height          =   285
         Left            =   1200
         TabIndex        =   7
         Text            =   "1"
         Top             =   360
         Width           =   735
      End
      Begin VB.CommandButton cmdSetListen 
         Caption         =   "LISTEN"
         Height          =   255
         Left            =   2160
         TabIndex        =   6
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label6 
         Caption         =   "Device No. :"
         Height          =   375
         Left            =   240
         TabIndex        =   12
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label1 
         Caption         =   "Response :"
         Height          =   255
         Left            =   240
         TabIndex        =   11
         Top             =   1320
         Width           =   975
      End
   End
   Begin VB.TextBox txtNumberOfDevices 
      Height          =   285
      Left            =   1200
      TabIndex        =   4
      Text            =   "2"
      Top             =   240
      Width           =   975
   End
   Begin VB.TextBox txtAddress 
      Height          =   285
      Left            =   1200
      TabIndex        =   2
      Text            =   "12,1"
      Top             =   600
      Width           =   975
   End
   Begin VB.CommandButton cmdInitialize 
      Caption         =   "INITIALIZE"
      Height          =   615
      Left            =   2400
      TabIndex        =   0
      Top             =   240
      Width           =   1575
   End
   Begin VB.Label Label5 
      Caption         =   "Address :"
      Height          =   255
      Left            =   120
      TabIndex        =   3
      Top             =   600
      Width           =   735
   End
   Begin VB.Label Label4 
      Caption         =   "# Of Devices :"
      Height          =   255
      Left            =   120
      TabIndex        =   1
      Top             =   240
      Width           =   1095
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

' related to GPIB devices
Dim BDINDEX As Integer             ' Board Index (set = 0)
Dim PRIMARY_ADDR_OF_DMM As Integer ' Primary address of device (Multi = 7,Quad = 4)
Const NO_SECONDARY_ADDR = 0        ' Secondary address of device
Const TIMEOUT = T10s               ' Timeout value = 10 seconds
Const EOTMODE = 1                  ' Enable the END message
Const EOSMODE = 0                  ' Disable the EOS mode

Dim Dev(0 To 1) As Integer  ' GPIB devices, address = 1, 2

' about instruments
Const GPIB0 = 0
Dim number_of_instruments As Integer
Dim reading_from_all_instruments As String
Dim reading_from_each_instrument() As String
Dim GPIB_addresses_found(30) As Integer

Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

Private Sub chkDisplayOff_Click()
  Dim k As Integer
  Dim command As String

  k = Val(txtAddressDelta.Text)
  If chkDisplayOff.value = 1 Then
    command = ":disp:enab 1"
    Call ilwrt(Dev(k), command, Len(command))
  Else
    command = ":disp:enab 0"
    Call ilwrt(Dev(k), command, Len(command))
  End If
End Sub

Private Sub Form_Load()
  Dim i As Integer
  Dim splitted() As String
  
  GPIBglobalsRegistered = 0
  
  number_of_instruments = Val(txtNumberOfDevices.Text)
  
  splitted = Split(Trim(txtAddress.Text), ",")
      
  If number_of_instruments <> UBound(splitted) + 1 Then
    MsgBox "Number of instruments not the same as the number of the addresses"
    Exit Sub
  End If
  For i = 0 To number_of_instruments - 1
    GPIB_addresses_found(i) = splitted(i)
  Next i
  GPIB_addresses_found(i + 1) = NOADDR

  ReDim reading_from_each_instrument(0 To number_of_instruments - 1)
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnLoadMode As Integer)
  ' Take the board offline
  'ilonl GPIB0, 0
  'If (ibsta And EERR) Then
    'GpibErr ("Error putting board offline.")
  'End If
      
  End
End Sub

Private Sub cmdInitialize_Click()
  Call initialization
End Sub

' GPIB devices initilization
Private Sub initialization()
  Dim i As Integer
  
  BDINDEX = GPIB0                ' Board Index
  
  For i = 0 To number_of_instruments - 1
    PRIMARY_ADDR_OF_DMM = GPIB_addresses_found(i) ' 0 = Keithley 6220, 1 = HP DMM
  
    Dev(i) = ildev(BDINDEX, PRIMARY_ADDR_OF_DMM, NO_SECONDARY_ADDR, _
      TIMEOUT, EOTMODE, EOSMODE)
    If (ibsta And EERR) Then
      Call GpibErr("Error in initialization of GPIB device " & i + 1)
    Else
      'MsgBox ("Initialization step 1 of GPIB device " & i + 1 & " OK")
    End If
    
    ' reset the GPIB portion of the device
    ilclr Dev(i)
    If (ibsta And EERR) Then
      Call GpibErr("Error in clearing GPIB device " & i + 1 & " in initialization sub")
    Else
      'MsgBox ("initialization step 2 of GPIB device " & i + 1 & " OK")
    End If
  Next i
End Sub

Private Sub cmdSendCommand_Click()
  Call cmdSetListen_Click
  
  Dim k As Integer
  k = Val(txtDeviceNumber.Text)
  
  reading_from_all_instruments = Space$(200)
  
  ' Send command or query
  Call ilwrt(Dev(k), Trim(txtCommand.Text), Len(Trim(txtCommand.Text)))
  If (ibsta And EERR) Then
    GpibErr ("Error in writing command to device. ")
  End If
  
  ' Receive response
  Call ilrd(Dev(k), reading_from_all_instruments, Len(reading_from_all_instruments))
  If (ibsta And EERR) Then
    GpibErr ("Error in receiving response to ilrd. ")
  End If

  'reading_from_each_instrument(k) = Left$(reading_from_all_instruments, ibcntl - 1)
  reading_from_each_instrument(k) = reading_from_all_instruments
  
  txtResponse.Text = reading_from_each_instrument(k)
        
  ' set back the device to listen condition
  Call cmdSetListen_Click
End Sub

Private Sub cmdSetListen_Click()
  Dim k As Integer
  k = Val(txtDeviceNumber.Text)
  
  ' reset the GPIB portion of the device
  ilclr Dev(k)
  If (ibsta And EERR) Then
    Call GpibErr("Error setting GPIB device to listen")
  Else
    'MsgBox ("initialization step 2 of GPIB device " & i + 1 & " OK")
  End If
End Sub

Private Sub cmdApplySetupDelta_Click()

  Dim k As Integer
  k = Val(txtAddressDelta.Text)
  
  Dim command As String
  
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
  
  command = ":sour:delt:high " & Trim(txtCurrent.Text) & "e-3"
  Call ilwrt(Dev(k), command, Len(command))
  
  'command = ":sour:delt:low <DELTALOW>"
  'Call ilwrt(Dev(k), command, Len(command))
  
  command = ":sour:delt:count " & txtDeltaCount.Text
  Call ilwrt(Dev(k), command, Len(command))

  command = ":sour:delt:delay " & txtDelay.Text
  Call ilwrt(Dev(k), command, Len(command))
  
  If cboAnalogFilter.Text = "ON" Then
    command = ":sour:curr:filt:stat 1"
  Else
    command = ":sour:curr:filt:stat 0"
  End If
  Call ilwrt(Dev(k), command, Len(command))
  
  command = ":sour:curr:comp " & txtCompliance.Text
  Call ilwrt(Dev(k), command, Len(command))

  command = ":sour:curr:rang 105e-3"
  Call ilwrt(Dev(k), command, Len(command))

  'command = ":SYST:COMM:SERIal:Send" & " *rst"
  'Call ilwrt(Dev(k), command, Len(command))

  ''Sleep 400
  'command = ":SYST:COMM:SERIal:Send" & " :sens:volt:nplc " & txtRate.Text
  'Call ilwrt(Dev(k), command, Len(command))
  ''Sleep 1500
  
  If cboVoltageRange.Text = "Auto" Then
    'command = ":SYST:COMM:SERIal:Send" & " :sens:volt:rang:auto ON"
  Else
    'command = ":SYST:COMM:SERIal:Send" & " :sens:volt:rang:auto OFF"
    'command = ":SYST:COMM:SERIal:Send" & " :sens:volt:rang " & cboVoltageRange.Text
  End If
  'Call ilwrt(Dev(k), command, Len(command))
  
  'command = ":sens:aver:wind 0"
  'Call ilwrt(Dev(k), command, Len(command))
  If cboFilterType.Text = "None" Then
    command = ":sens:aver:stat OFF"
  Else
    command = ":sens:aver:stat ON"
    'Call ilwrt(Dev(k), command, Len(command))
    command = ":sens:aver:tcon " & Left(cboFilterType.Text, 3)
  End If
  'Call ilwrt(Dev(k), command, Len(command))
  command = ":sens:aver:coun " & txtFilterCount.Text
  'Call ilwrt(Dev(k), command, Len(command))
End Sub

Private Sub cmdArmDelta_Click()
  Dim k As Integer
  Dim command As String

  k = Val(txtAddressDelta.Text)
  
  command = ":sour:delt:arm"
  Call ilwrt(Dev(k), command, Len(command))
End Sub

Private Sub cmdExitDelta_Click()
  Dim k As Integer
  Dim command As String

  k = Val(txtAddressDelta.Text)
  
  command = ":sour:swe:abor"
  Call ilwrt(Dev(k), command, Len(command))
End Sub

Private Sub cmdTrigger_Click()
  Dim command As String
  Dim k As Integer
    
  k = Val(txtAddressDelta.Text)
  
  command = ":init:imm"
  Call ilwrt(Dev(k), command, Len(command))
End Sub

Private Sub chkContinuous_Click()
  If chkContinuous.value = 1 Then
    Call cmdTrigger_Click
    Sleep Val(txtInterval.Text) * 1000
    Timer1.Interval = Val(txtInterval.Text) * 1000
    Timer1.Enabled = True
    
    txtInterval.Enabled = True
    txtElapsedTime.Enabled = True
    txtAverage.Enabled = True
  Else
    Timer1.Enabled = False
    
    txtInterval.Enabled = False
    txtElapsedTime.Enabled = False
    txtAverage.Enabled = False
  End If
End Sub

Private Sub Timer1_Timer()
  Call read_device
  
  txtElapsedTime.Text = Format(Now, "h:m:s")
  
  Call cmdTrigger_Click
End Sub

Private Sub read_device()
  Dim k As Integer
  k = Val(txtDeviceNumber.Text)
  
  reading_from_all_instruments = Space$(200)
  
  ' Send command or query
  Call ilwrt(Dev(k), ":trace:data?", 12)
  If (ibsta And EERR) Then
    GpibErr ("Error in writing command to device. ")
  End If
  
  ' Receive response
  Call ilrd(Dev(k), reading_from_all_instruments, Len(reading_from_all_instruments))
  If (ibsta And EERR) Then
    GpibErr ("Error in receiving response to ilrd. ")
  End If

  'reading_from_each_instrument(k) = Left$(reading_from_all_instruments, ibcntl - 1)
  reading_from_each_instrument(k) = reading_from_all_instruments
  
  txtResponse.Text = reading_from_each_instrument(k)
    
  Dim splitted() As String
  Dim average As Double
  Dim count As Integer
  
  splitted = Split(reading_from_each_instrument(k), ",")
  count = UBound(splitted) + 1
  
  Dim i As Integer
  For i = 0 To count - 1
    average = average + Val(splitted(i))
  Next i
  txtAverage.Text = average / count
End Sub
