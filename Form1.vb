Imports CoreAudio

Public Class Amplificador

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    'Obtendo as coordenadas do Mouse
    Private Declare Function GetCursorPos Lib "user32" (ByRef point As POINTAPI) As Integer

    Public Structure POINTAPI
        Dim X As Int32
        Dim Y As Int32
    End Structure
    Dim OVolume As Int32
    Dim Sensor As Boolean

    Private Sub Volume_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Dim Posicao As POINTAPI
        Posicao.X = Posicao.Y = 5
        GetCursorPos(Posicao)
        Label2.Text = "X: " & Posicao.X.ToString & " Y :" & Posicao.Y.ToString
        If (Sensor) Then
            OVolume = (1000 - Posicao.Y) / 10
            Label3.Text = OVolume.ToString
            Svol = Val(OVolume)
            If Svol < 0 Then Svol = 0
            If Svol > 100 Then Svol = 100
            TextBox1.Text = Svol.ToString
            SetVol()
            Label1.Text = "Volume Level = " & GetVol().ToString & "%"
        End If
    End Sub

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    Dim Svol As Integer = 0   'Select Volume.  Global variable.

    Private Sub Button1_Click() Handles Button1.Click
        ' Click this button to set a new volume level. First type the
        ' required level into TextBox1, as a percentage 0 to 100 (%)

        Svol = Val(TextBox1.Text)             ' Get the required value
        If Svol < 0 Then Svol = 0 ' Ensure it's within limits
        If Svol > 100 Then Svol = 100
        TextBox1.Text = Svol.ToString       ' Echo the level back into TextBox1
        SetVol()                                       ' Call the subroutine.
        Label1.Text = "Volume Level = " & GetVol().ToString & "%"
    End Sub

    Private Function GetVol() As Integer        'Function to read current volume setting
        Dim MasterMinimum As Integer = 0
        Dim DevEnum As New MMDeviceEnumerator()
        Dim device As MMDevice = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia)
        Dim Vol As Integer = 0

        With device.AudioEndpointVolume
            Vol = CInt(.MasterVolumeLevelScalar * 100)
            If Vol < MasterMinimum Then
                Vol = MasterMinimum / 100.0F
            End If
        End With
        Return Vol
    End Function

    Private Sub SetVol()
        Dim DevEnum As New MMDeviceEnumerator()
        Dim device As MMDevice = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia)
        device.AudioEndpointVolume.MasterVolumeLevelScalar = Svol / 100.0F
    End Sub

    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Sensor = True
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

    End Sub
End Class
