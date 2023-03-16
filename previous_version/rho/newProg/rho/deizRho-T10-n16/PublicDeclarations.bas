Attribute VB_Name = "rho_public_declaration"
' rho_public_declaration module
Option Explicit

Public Const program_name As String = "deizRho v.0.1"

'Public Const GPIB0 = 0
Public k As Integer
Public Temperature() As Double
Public time_elapsed() As Long ' in sec
Public time_remained() As Long ' in sec
Public times_of_readings As Long
Public time_clock() As Date ' correction real time
Public resistance() As Double
Public voltage() As Double
Public current() As Double

Public heating_or_cooling_rate As Double
Public resistivity() As Double
Public Rho_factor As Double

Public Temperature2 As Double
Public time_elapsed2 As Double ' NOT NEEDED, USELESS SEE AGAIN
Public resistance2 As Double
Public voltage2 As Double
Public current2 As Double
Public resistivity2 As Double
Public voltage2before As Double
Public current2before As Double

Public dataY() As Double ' public cause called in frmSettings
Public dataX() As String

' Display
Public expV As String ' eg. 1E-7, 1E-8
Public expC As String
Public expR As String
Public expRho As String
Public roundV As Long ' digits after decimal point
Public roundC As Long
Public roundR As Long
Public roundRho As Long

