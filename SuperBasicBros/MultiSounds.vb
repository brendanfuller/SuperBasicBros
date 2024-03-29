﻿Imports System.Runtime.InteropServices



' __  __         _  _    _   _____                           _      
'|  \/  |       | || |  (_) / ____|                         | |     
'| \  / | _   _ | || |_  _ | (___    ___   _   _  _ __    __| | ___ 
'| |\/| || | | || || __|| | \___ \  / _ \ | | | || '_ \  / _` |/ __|
'| |  | || |_| || || |_ | | ____) || (_) || |_| || | | || (_| |\__ \
'|_|  |_| \__,_||_| \__||_||_____/  \___/  \__,_||_| |_| \__,_||___/

'MutiSounds Class - Found on forum website (lost site) - Credit goes to unknown user - Modified by ImportProgram

Public Class MultiSounds
    Private Snds As New Dictionary(Of String, String)
    Private sndcnt As Integer = 0
    Private snds_state As Boolean = True '*

    <DllImport("winmm.dll", EntryPoint:="mciSendStringW")> _
    Private Shared Function mciSendStringW(<MarshalAs(UnmanagedType.LPTStr)> ByVal lpszCommand As String, <MarshalAs(UnmanagedType.LPWStr)> ByVal lpszReturnString As System.Text.StringBuilder, ByVal cchReturn As UInteger, ByVal hwndCallback As IntPtr) As Integer
    End Function

    Public Function AddSound(ByVal SoundName As String, ByVal SndFilePath As String) As Boolean
        If SoundName.Trim = "" Or Not IO.File.Exists(SndFilePath) Then Return False
        If mciSendStringW("open " & Chr(34) & SndFilePath & Chr(34) & " alias " & "Snd_" & sndcnt.ToString, Nothing, 0, IntPtr.Zero) <> 0 Then Return False
        Snds.Add(SoundName, "Snd_" & sndcnt.ToString)
        sndcnt += 1
        Return True
    End Function

    Public Function Play(ByVal SoundName As String) As Boolean
        If snds_state = True Then
            If Not Snds.ContainsKey(SoundName) Then Return False
            mciSendStringW("seek " & Snds.Item(SoundName) & " to start", Nothing, 0, IntPtr.Zero)
            If mciSendStringW("play " & Snds.Item(SoundName), Nothing, 0, IntPtr.Zero) <> 0 Then Return False
            Return True
        Else
            Return False
        End If
    End Function

    Public Function [Stop](ByVal SoundName As String) As Boolean
        If Not Snds.ContainsKey(SoundName) Then Return False
        If mciSendStringW("stop " & Snds.Item(SoundName), Nothing, 0, IntPtr.Zero) <> 0 Then Return False
        Return True
    End Function

    Public Function setState(ByVal State As Boolean) As Boolean '*
        snds_state = State
        Return True
    End Function
    Public Function getState() As Boolean '*
        Return snds_state
    End Function

End Class