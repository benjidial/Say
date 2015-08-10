Module Program
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")> Function Main(ByVal cmdArgs() As String) As Integer
        Dim rm As New System.Resources.ResourceManager("Benji.Say.Resources", System.Reflection.Assembly.GetExecutingAssembly())
        If (cmdArgs.Length = 0) Or (cmdArgs.Length = 1) Then
            Console.WriteLine(rm.GetString("CommandLineFormat"), Int32.MaxValue)
            Return -1
        End If
        Dim Interval As Integer
        If Integer.TryParse(cmdArgs(0), Interval) Then
            If Interval <= 0 Then
                Console.WriteLine(rm.GetString("InvalidInterval"), cmdArgs(0), Int32.MaxValue)
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
                            Console.WriteLine(rm.GetString("NoFileName"))
                            Return -3
                        End If
                    Catch ex As System.IO.FileNotFoundException
                        If ReadFile Is Nothing Then
                            Console.WriteLine(rm.GetString("FileNotFound"), filename)
                            Return -4
                        End If
                    Catch ex As System.IO.DirectoryNotFoundException
                        If ReadFile Is Nothing Then
                            Console.WriteLine(rm.GetString("InvalidPath"), filename)
                            Return -5
                        End If
                    Catch ex As System.IO.IOException
                        If ReadFile Is Nothing Then
                            Console.WriteLine(rm.GetString("InvalidFileName"), filename)
                            Return -6
                        Else
                            Console.WriteLine(rm.GetString("UnexpectedIOError"), ex.Message)
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
                        Console.WriteLine(rm.GetString("NoFileName"))
                        Return -3
                    Catch ex As System.IO.FileNotFoundException When ReadFile Is Nothing
                        Console.WriteLine(rm.GetString("FileNotFound"), filename)
                        Return -4
                    Catch ex As System.IO.DirectoryNotFoundException When ReadFile Is Nothing
                        Console.WriteLine(rm.GetString("InvalidPath"), filename)
                        Return -5
                    Catch ex As System.IO.IOException
                        If ReadFile Is Nothing Then
                            Console.WriteLine(rm.GetString("InvalidFileName"), filename)
                            Return -6
                        Else
                            Console.WriteLine(rm.GetString("UnexpectedIOError"), ex.Message)
                            Return -7
                        End If
                    Catch ex As OutOfMemoryException When ReadFile IsNot Nothing
                        Console.WriteLine(rm.GetString("OutOfMemoryCreatingBuffer"), filename)
                        Environment.FailFast(String.Format(System.Globalization.CultureInfo.CurrentCulture, rm.GetString("OutOfMemoryCreatingBufferLog"), ex.Message))
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
        Console.WriteLine(rm.GetString("InvalidInterval"), cmdArgs(0), Int32.MaxValue)
        Return -2
    End Function
End Module
