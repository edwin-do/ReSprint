Attribute VB_Name = "Module4"
Public Sub Calculate_Temperature()
  If emf(times_of_readings) >= 0 Then
      Temperature(times_of_readings) = (25.928 * emf(times_of_readings)) + (-0.7602961 * emf(times_of_readings) ^ 2) + (0.04637791 * emf(times_of_readings) ^ 3) + (-0.002165394 * emf(times_of_readings) ^ 4) _
      + (0.00006048144 * emf(times_of_readings) ^ 5) + (-0.0000007293422 * emf(times_of_readings) ^ 6)
  Else
      Temperature(times_of_readings) = (25.949192 * emf(times_of_readings)) + (-0.21316967 * emf(times_of_readings) ^ 2) + (0.79018692 * emf(times_of_readings) ^ 3) + (0.42527777 * emf(times_of_readings) ^ 4) _
      + (0.13304473 * emf(times_of_readings) ^ 5) + (0.020241446 * emf(times_of_readings) ^ 6) + (0.0012668171 * emf(times_of_readings) ^ 7)
  End If
  Temperature(times_of_readings) = Round(CStr(Temperature(times_of_readings) * 1000), 1) 'have to be times 100, also i round it
End Sub


