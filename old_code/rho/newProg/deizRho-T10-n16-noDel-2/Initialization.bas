Attribute VB_Name = "Module1"
Option Explicit
Public Sub Initialization(num_listeners As Integer, DisplayStr() As String, sta As String)
Dim instruments(31) As Integer                 ' array of primary addresses
Dim result(30) As Integer, k As Integer
Const GPIB0 = 0

'  ====================================================================
'  INITIALIZATION SECTION
' ====================================================================
'  Your board needs to be the Controller-In-Charge in order to find all
'  listeners on the GPIB.  To accomplish this, the subroutine SendIFC
'  is called.  If the error bit EERR is set in ibsta, call GpibErr with
'  an error message.
   Call SendIFC(GPIB0)
   If (ibsta And EERR) Then
        Call GpibErr
   End If
'  Create an array containing all valid GPIB primary addresses, except
'  for the primary address 0. Your GPIB interface board is at address 0
'  by default.  This array (instruments%) will be given to the subroutine
'  FindLstn to find all listeners.  The constant NOADDR, defined in
'  NIGLOBAL.BAS, signifies the end of the array.
   For k = 0 To 29
        instruments(k) = k + 1
   Next k
   instruments(30) = NOADDR
'  Print message to tell user that the program is searching for all active
'  listeners.  Find all of the listeners on the bus.  Store the listen
'  addresses in the array result%.  If the error bit ERR is set in ibsta,
'  call GpibErr with an error message.
   Screen.MousePointer = 11    ' Wait(hourglass) cursor
frmMainForm.ReadingsList.AddItem "Finding all listeners on the bus..."
   'frmMainForm.txtMessage.Text = "Finding all listeners on the bus..."
   
   Call FindLstn(GPIB0, instruments(), result(), 31)
   If (ibsta And EERR) Then
        Screen.MousePointer = 0
        'GpibErr ("Error finding all listeners.")
   End If

   Screen.MousePointer = 0
'  ibcntl contains the actual number of addresses stored in the result%
'  array. Assign the value of ibcntl to the variable num_listeners.
'  Print the number of listeners found.

   num_listeners = ibcnt
frmMainForm.ReadingsList.AddItem "Number of instruments found = " + Str$(num_listeners)
   'frmMainForm.txtMessage.Text = "Number of instruments found = " + Str$(num_listeners)
   
   ReDim DisplayStr(num_listeners% - 1)

'  The result% array contains the addresses of all listening devices
'  found by FindLstn. Use the constant NOADDR, as defined in NIGLOBAL.BAS,
'  to signify the end of the array.

    result(ibcntl) = NOADDR

'  DevClearList will send the GPIB Selected Device Clear (SDC) command
'  message to all the devices on the bus. If the error bit EERR is set in
'  ibsta, call GpibErr with an error message.

    Call DevClearList(GPIB0, result)
    If (ibsta And EERR) Then
        'GpibErr ("Error in clearing the devices. ")
    End If
End Sub

