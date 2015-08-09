Module Program
    Function Main(ByVal cmdArgs() As String) As Integer
        Dim NL As String = Environment.NewLine
        If (cmdArgs.Length = 0) Or (cmdArgs.Length = 1) Then
            Console.WriteLine("Say interval text" + NL +
                              "Say interval /f [/s] file" + NL + NL +
                              "  interval     The interval in milliseconds between the output" + NL +
                              "                 of each character (must be a positive integer" + NL +
                              "                 less than or equal to " + Integer.MaxValue.ToString(Globalization.CultureInfo.InstalledUICulture) + ")" + NL +
                              "  text         The text to output (may contain whitespace)" + NL +
                              "  /f           Specifies that text will be loaded from a file" + NL +
                              "  /s           If this is not specified, it will load the whole" + NL +
                              "                 file at once, close the file (allowing you and" + NL +
                              "                 other programs to use it), then output what it" + NL +
                              "                 read one character at a time. If this is" + NL +
                              "                 specified, it will read the file one character" + NL +
                              "                 at a time and output each character, waiting the" + NL +
                              "                 specified amount of time between each character" + NL +
                              "                 and keeping the file open (not allowing you or" + NL +
                              "                 other programs to use it) until the entire file" + NL +
                              "                 has been output." + NL +
                              "  file         The name and location of the file to load (If" + NL +
                              "                 the location is not specified it will look in" + NL +
                              "                 the directory in which Say is located.)")
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
            If cmdArgs(1) = "/f" Or cmdArgs(1) = "/F" Then
                If cmdArgs.Length > 2 AndAlso (cmdArgs(2) = "/s" Or cmdArgs(2) = "/S") Then
                    Dim filename As String = String.Empty
                    Dim IsThird As Boolean = True
                    Dim IsFourth As Boolean = True
                    For Each arg As String In cmdArgs
                        If IsFirst Then
                            IsFirst = False
                        ElseIf IsSecond Then
                            IsSecond = False
                        ElseIf IsThird Then
                            IsThird = False
                        ElseIf IsFourth Then
                            filename += arg
                            IsFourth = False
                        Else
                            filename += (" " + arg)
                        End If
                    Next
                    Dim ReadFile As System.IO.StreamReader = Nothing
                    Try
                        ReadFile = New System.IO.StreamReader(filename)
                        While ReadFile.Peek() <> -1
                            System.Threading.Thread.Sleep(Interval)
                            Console.Write(Chr(ReadFile.Read()))
                        End While
                    Catch ex As ArgumentException
                        If ReadFile Is Nothing Then
                            Console.WriteLine("No file name was supplied!")
                            Return -3
                        End If
                    Catch ex As System.IO.FileNotFoundException
                        If ReadFile Is Nothing Then
                            Console.WriteLine("Could not find the file " + filename + "!")
                            Return -4
                        End If
                    Catch ex As System.IO.DirectoryNotFoundException
                        If ReadFile Is Nothing Then
                            Console.WriteLine("Invalid path in " + filename + "!")
                            Return -5
                        End If
                    Catch ex As System.IO.IOException
                        If ReadFile Is Nothing Then
                            Console.WriteLine(filename + " could not be read because the name is invalid!")
                            Return -6
                        Else
                            Console.WriteLine("An unexpected error occured trying to read the file: " + ex.Message)
                            Return -7
                        End If
                    Finally
                        If ReadFile IsNot Nothing Then
                            ReadFile.Close()
                        End If
                    End Try
                Else
                    Dim filename As String = String.Empty
                    Dim IsThird As Boolean = True
                    For Each arg As String In cmdArgs
                        If IsFirst Then
                            IsFirst = False
                        ElseIf IsSecond Then
                            IsSecond = False
                        ElseIf IsThird Then
                            filename += arg
                            IsThird = False
                        Else
                            filename += (" " + arg)
                        End If
                    Next
                    Dim ReadFile As System.IO.StreamReader = Nothing
                    Dim text As String = Nothing
                    Try
                        ReadFile = New System.IO.StreamReader(filename)
                        text = ReadFile.ReadToEnd()
                    Catch ex As ArgumentException When ReadFile Is Nothing
                        Console.WriteLine("No file name was supplied!")
                        Return -3
                    Catch ex As System.IO.FileNotFoundException When ReadFile Is Nothing
                        Console.WriteLine("Could not find the file " + filename + "!")
                        Return -4
                    Catch ex As System.IO.DirectoryNotFoundException When ReadFile Is Nothing
                        Console.WriteLine("Invalid path in " + filename + "!")
                        Return -5
                    Catch ex As System.IO.IOException
                        If ReadFile Is Nothing Then
                            Console.WriteLine(filename + " could not be read because the name is invalid!")
                            Return -6
                        Else
                            Console.WriteLine("An unexpected error occured trying to read the file: " + ex.Message)
                            Return -7
                        End If
                    Catch ex As OutOfMemoryException When ReadFile IsNot Nothing
                        Console.WriteLine("Ran out of memory trying to create buffer for " + filename + "!  Try using the /s switch.")
                        Environment.FailFast("Out of Memory: " + ex.Message)
                    Finally
                        If ReadFile IsNot Nothing Then
                            ReadFile.Close()
                        End If
                    End Try
                    If text IsNot Nothing Then
                        For Each character As Char In text
                            System.Threading.Thread.Sleep(Interval)
                            Console.Write(character)
                        Next
                    End If
                End If
            Else
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
            End If
            Console.WriteLine()
            Return 0
        End If
        Console.WriteLine(cmdArgs(0) + " is not a positive integer less than or equal to " + Integer.MaxValue.ToString(Globalization.CultureInfo.InstalledUICulture) + ".")
        Return -2
    End Function
End Module
