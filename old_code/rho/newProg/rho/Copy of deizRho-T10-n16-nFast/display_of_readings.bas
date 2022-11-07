Attribute VB_Name = "rho_display"
' rho_display module
Option Explicit

Public Sub display_of_readings()

With frmMainForm
For k = 0 To 4 'lowerbound(.lblDisplay) To upperbound(.lblDisplay)
  Select Case .lblDisplay(k).Caption
    Case "Elapsed Time (s) :"
      .cboDisplay(k).text = hhmmss(time_elapsed(times_of_readings))
    Case "Remaining Time (s) :"
      .cboDisplay(k).text = hhmmss(time_remained(times_of_readings))
    Case "Temperature (C) :"
      .cboDisplay(k).text = Temperature(times_of_readings)
    Case "Voltage (V) :"
      '.cboDisplay(k).text = Format(voltage(times_of_readings), "Scientific")
      .cboDisplay(k).text = Round(voltage(times_of_readings) / Val(expV), roundV) & _
        " x " & expV
    Case "Current (A) :"
      '.cboDisplay(k).text = Format(current(times_of_readings), "Scientific")
      .cboDisplay(k).text = Round(current(times_of_readings) / Val(expC), roundC) & _
        " x " & expC
    Case "Resistance (Ohm) :"
      '.cboDisplay(k).text = Format(resistance(times_of_readings), "Scientific")
      .cboDisplay(k).text = Round(resistance(times_of_readings) / Val(expR), roundR) & _
        " x " & expR
    Case "Resistivity (Ohm.m) :"
      '.cboDisplay(k).text = Format(resistivity(times_of_readings), "Scientific")
      .cboDisplay(k).text = Round(resistivity(times_of_readings) / Val(expRho), roundRho) & _
        " x " & expRho
    Case "Ramping (K/min) :"
      .cboDisplay(k).text = heating_or_cooling_rate
  End Select
Next k
End With
End Sub

Public Sub display_of_readings2()
With frmMainForm
For k = 0 To 4 'lowerbound(.lblDisplay) To upperbound(.lblDisplay)
  Select Case .lblDisplay(k).Caption
    Case "Elapsed Time (s) :"
      .cboDisplay(k).text = "Elapsed Time"
    Case "Remaining Time (s) :"
      .cboDisplay(k).text = "Remaining Time"
    Case "Temperature (C) :"
      .cboDisplay(k).text = Temperature2
    Case "Voltage (V) :"
      '.cboDisplay(k).text = Format(voltage2, "Scientific")
      .cboDisplay(k).text = Round(voltage2 / Val(expV), roundV) & _
        " x " & expV
    Case "Current (A) :"
      '.cboDisplay(k).text = Format(current2, "Scientific")
      .cboDisplay(k).text = Round(current2 / Val(expC), roundC) & _
        " x " & expC
    Case "Resistance (Ohm) :"
      '.cboDisplay(k).text = Format(resistance2, "scientific")
      .cboDisplay(k).text = Round(resistance2 / Val(expR), roundR) & _
        " x " & expR
    Case "Resistivity (Ohm.m) :"
      '.cboDisplay(k).text = Format(resistivity2, "scientific")
      .cboDisplay(k).text = Round(resistivity2 / Val(expRho), roundRho) & _
        " x " & expRho
    Case "Ramping (K/min) :"
      .cboDisplay(k).text = "Ramping Rate"
  End Select
  Next k
End With
End Sub
