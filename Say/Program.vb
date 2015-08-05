Module Program
    Function Main(ByVal cmdArgs() As String) As Integer
        Dim NL As String = Environment.NewLine
        If (cmdArgs.Length = 0) Or (cmdArgs.Length = 1) Then
            Console.WriteLine("Format:  Say interval text" + NL + NL +
                              "  interval     The interval in milliseconds between the output" + NL +
                              "                 of each character (must be a positive integer" + NL +
                              "                 less than or equal to " + Integer.MaxValue.ToString(Globalization.CultureInfo.InstalledUICulture) + ")" + NL +
                              "  text         The text to output (may contain whitespace)")
            Return -1
        End If
        Dim Interval As Integer
        If Integer.TryParse(cmdArgs(0), Interval) Then
            If Interval <= 0 Then
                Console.WriteLine(cmdArgs(0) + " is not a positive integer less than or equal to " + Integer.MaxValue.ToString(Globalization.CultureInfo.InstalledUICulture) + ".")
                Return -2
            End If
            Dim IsFirst As Boolean = True
            Dim IsSecond As Boolean = True
            Dim text As String = String.Empty
            For Each arg As String In cmdArgs
                If IsFirst Then
                    IsFirst = False
                ElseIf IsSecond Then
                    text += arg
                    IsSecond = False
                Else
                    text += (" " + arg)
                End If
            Next
            For Each character As Char In text
                System.Threading.Thread.Sleep(Interval)
                Console.Write(character)
            Next
            Console.WriteLine()
            Return 0
        End If
        Console.WriteLine(cmdArgs(0) + " is not a positive integer less than or equal to " + Integer.MaxValue.ToString(Globalization.CultureInfo.InstalledUICulture) + ".")
        Return -2
    End Function
End Module
