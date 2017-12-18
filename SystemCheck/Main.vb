Imports System.Threading
Public Class Main
    Dim form As Main
    Dim alltrue As Integer = 0
    Dim running As Integer = 0
    Dim cont As Boolean = True
    Dim Ttime As Integer = 0
    Dim minimized As Boolean = False
    Private locck As New Object
#Region "Form State"
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Private Sub PaneTop_1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub PaneTop_1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub
    Private Sub PaneTop_1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseUp
        drag = False
    End Sub
    Private Sub menter(pb As PictureBox)
        pb.BackColor = Color.FromArgb(127, 77, 0)
    End Sub
    Private Sub mout(pb As PictureBox)
        pb.BackColor = Color.FromArgb(64, 38, 0)
    End Sub
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs)
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Me.Close()
    End Sub

    Private Sub PictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox3.MouseEnter
        menter(sender)
    End Sub

    Private Sub PictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox3.MouseLeave
        mout(sender)
    End Sub

    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs)
        menter(sender)
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs)
        mout(sender)
    End Sub

    Private Sub PictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox4.MouseEnter
        menter(sender)
    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox4.MouseLeave
        mout(sender)
    End Sub
#End Region

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        cont = False
    End Sub
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        form = Me
        startGUI()
    End Sub

    Private Delegate Sub changeTXD()
    Private Sub changeTX()
        Try
            If statuslb.InvokeRequired Then
                statuslb.Invoke(New changeTXD(AddressOf changeTX))
            Else
                If alltrue = 2 Then
                    statuslb.BackColor = Color.FromArgb(8, 161, 20)
                    statuslb.Text = "Safe"
                Else
                    statuslb.BackColor = Color.FromArgb(232, 44, 12)
                    statuslb.Text = "@ Risk"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Delegate Sub changeBGD(lpanel As LoadingPanel, bit As Bitmap)
    Private Sub changeBG(lpanel As LoadingPanel, bit As Bitmap)
        Try
            If lpanel.InvokeRequired Then
                lpanel.Invoke(New changeBGD(AddressOf changeBG), New Object() {lpanel, bit})
            Else
                lpanel.BackgroundImage = bit
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub drawGUIThread1(lpanel As LoadingPanel, img As String, label As String, bg As Color)
        Try
            Dim bit As Bitmap
            Dim g As Graphics
            Dim width, height, x, y As Integer
            Dim font As Font = New Font("Century Gothic", 12.0)
            width = lpanel.ClientRectangle.Width
            height = lpanel.ClientRectangle.Height
            bit = New Bitmap(width, height)
            g = Graphics.FromImage(bit)
            g.FillRectangle(New SolidBrush(bg), 0, 0, width, height)
            x = (width - 32) / 2
            y = (height - 32) / 2
            g.DrawImage(My.Resources.ResourceManager.GetObject(img), x, y)
            x = lpanel.ClientRectangle.Width / 2 - g.MeasureString(label, font).Width / 2
            g.DrawString(label, font, New SolidBrush(Color.White), x, y + 35)
            changeBG(lpanel, bit)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub drawGUIThread(cparameter As cParameter)

        Dim lpanel As LoadingPanel = cparameter.lpanel
        Dim img As String = cparameter.img
        Dim label As String = cparameter.label
        Dim bg As Color = cparameter.bg
        Dim time As Integer = cparameter.time * 1000
        Dim err As Boolean = cparameter.err
        If time = 0 Then
            time = 60000
        End If
        Randomize()
        Dim pos As Integer = CInt(Math.Ceiling(Rnd() * lpanel.ClientRectangle.Width))
        Dim rev As Boolean = False
        'Dim alpha As Integer
        Dim curwidth As Integer = 0
        Dim perwidth As Integer = lpanel.ClientRectangle.Width - 54
        Dim per As Integer = CInt((perwidth / time) * 1000)
        While time > 0
            If Not cont Then
                Exit While
            End If
            If form.WindowState = FormWindowState.Minimized Then
                Thread.Sleep(20)
                time = time - 20
                If time > 0 Then
                    If time Mod 1000 = 0 Then
                        curwidth = curwidth + per
                        Continue While
                    End If
                Else
                    form.WindowState = FormWindowState.Normal
                End If
            End If
            Thread.Sleep(20)
            time = time - 20
            Try
                Dim bit As Bitmap
                Dim g As Graphics
                Dim width, height, x, y As Integer
                Dim font As Font = New Font("Century Gothic", 12.0)

                If form.WindowState <> FormWindowState.Minimized Then
                    width = lpanel.ClientRectangle.Width
                    If perwidth <> width - 54 Then
                        perwidth = width - 54
                        per = CInt(((perwidth - curwidth) / time) * 1000)
                    End If
                    height = lpanel.ClientRectangle.Height
                End If

                bit = New Bitmap(width, height)
                g = Graphics.FromImage(bit)
                g.FillRectangle(New SolidBrush(bg), 0, 0, width, height)
                x = (width - 32) / 2
                y = (height - 32) / 2
                g.DrawImage(My.Resources.ResourceManager.GetObject(img), x, y - 25)
                x = width / 2 - g.MeasureString(label, font).Width / 2
                g.DrawString(label, font, New SolidBrush(Color.White), x, y + 10)
                x = 25
                y = height - 55
                g.FillRectangle(New SolidBrush(Color.FromArgb(64, 38, 0)), x, y, width - 50, 30)
                x = 27
                y = y + 2
                'MsgBox(time Mod 1000)
                If time Mod 1000 = 0 Then
                    curwidth = curwidth + per
                End If
                If time > 0 And perwidth > curwidth Then
                    g.FillRectangle(New SolidBrush(Color.FromArgb(217, 156, 65)), x, y, curwidth, 26)

                    'For i As Integer = 0 To 20
                    '    alpha = i * 10 + 35 + i
                    g.FillRectangle(New SolidBrush(Color.FromArgb(255, 255, 255)), pos, 0, 1, height)
                    If rev Then
                        pos = pos - 1
                    Else
                        pos = pos + 1
                    End If
                    'Next
                    If pos >= width Then
                        rev = True
                    ElseIf pos <= 0 Then
                        rev = False
                    End If
                Else
                    g.FillRectangle(New SolidBrush(Color.FromArgb(217, 156, 65)), x, y, perwidth, 26)
                    If err Then
                        g.FillRectangle(New SolidBrush(Color.FromArgb(232, 44, 12)), x, y + 11, perwidth, 4)
                        x = x + perwidth / 2
                        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                        g.FillEllipse(New SolidBrush(Color.FromArgb(232, 44, 12)), x - 18, y - 5, 36, 36)
                        g.DrawImage(My.Resources.ResourceManager.GetObject("wrong"), x - 12, y + 1)
                    Else
                        g.FillRectangle(New SolidBrush(Color.FromArgb(0, 127, 3)), x, y + 11, perwidth, 4)
                        x = x + perwidth / 2
                        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                        g.FillEllipse(New SolidBrush(Color.FromArgb(0, 127, 3)), x - 18, y - 5, 36, 36)
                        g.DrawImage(My.Resources.ResourceManager.GetObject("right"), x - 12, y + 1)
                    End If
                End If
                changeBG(lpanel, bit)
                If perwidth < curwidth Then
                    Exit While
                End If
            Catch ex As Exception
            End Try
        End While
        SyncLock locck
            running = running - 1
            If running = 0 Then
                changeTX()
            End If
        End SyncLock
    End Sub

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        statuslb.Text = ""
        statuslb.BackColor = Color.FromArgb(140, 84, 0)
        If running <> 0 Then
            Exit Sub
        End If
        running = 6
        If Ttime = 0 Then
            Ttime = 180
        End If
        Dim ti(6) As Integer
        Randomize()
        'CInt(Math.Ceiling(Rnd() * lpanel.ClientRectangle.Width))
        Dim temp As Integer = CInt(Ttime / 2)
        ti(1) = CInt(Math.Ceiling(Rnd() * temp)) + temp
        ti(2) = CInt(Math.Ceiling(Rnd() * temp)) + temp
        ti(3) = CInt(Math.Ceiling(Rnd() * temp)) + temp
        ti(4) = CInt(Math.Ceiling(Rnd() * temp)) + temp
        ti(5) = CInt(Math.Ceiling(Rnd() * temp)) + temp
        ti(6) = CInt(Math.Ceiling(Rnd() * temp)) + temp
        Ttime = 0
        If alltrue = 2 Then
            Dim t As New Thread(AddressOf drawGUIThread)
            t.Start(New cParameter(LoadingPanel1, "registry", "Registry", Color.FromArgb(255, 153, 0), ti(1), False))
            Dim t2 As New Thread(AddressOf drawGUIThread)
            t2.Start(New cParameter(LoadingPanel2, "antivirus", "Antivirus", Color.FromArgb(127, 77, 0), ti(2), False))
            Dim t3 As New Thread(AddressOf drawGUIThread)
            t3.Start(New cParameter(LoadingPanel3, "virus", "Viruses", Color.FromArgb(255, 153, 0), ti(3), False))
            Dim t4 As New Thread(AddressOf drawGUIThread)
            t4.Start(New cParameter(LoadingPanel4, "spyware", "Spyware", Color.FromArgb(127, 77, 0), ti(4), False))
            Dim t5 As New Thread(AddressOf drawGUIThread)
            t5.Start(New cParameter(LoadingPanel5, "junk", "System Junk", Color.FromArgb(255, 153, 0), ti(5), False))
            Dim t6 As New Thread(AddressOf drawGUIThread)
            t6.Start(New cParameter(LoadingPanel6, "files", "System Files", Color.FromArgb(127, 77, 0), ti(6), False))
        Else
            Dim v As New Random
            Dim arr(3) As Boolean
            For j As Integer = 0 To 3
                If v.Next(0, 2) = 1 Then
                    arr(j) = True
                Else
                    arr(j) = False
                End If
            Next

            Dim t As New Thread(AddressOf drawGUIThread)
            t.Start(New cParameter(LoadingPanel1, "registry", "Registry", Color.FromArgb(255, 153, 0), ti(1), True))
            Dim t2 As New Thread(AddressOf drawGUIThread)
            t2.Start(New cParameter(LoadingPanel2, "antivirus", "Antivirus", Color.FromArgb(127, 77, 0), ti(2), False))
            Dim t3 As New Thread(AddressOf drawGUIThread)
            t3.Start(New cParameter(LoadingPanel3, "virus", "Viruses", Color.FromArgb(255, 153, 0), ti(3), arr(0)))
            Dim t4 As New Thread(AddressOf drawGUIThread)
            t4.Start(New cParameter(LoadingPanel4, "spyware", "Spyware", Color.FromArgb(127, 77, 0), ti(4), arr(1)))
            Dim t5 As New Thread(AddressOf drawGUIThread)
            t5.Start(New cParameter(LoadingPanel5, "junk", "System Junk", Color.FromArgb(255, 153, 0), ti(5), arr(2)))
            Dim t6 As New Thread(AddressOf drawGUIThread)
            t6.Start(New cParameter(LoadingPanel6, "files", "System Files", Color.FromArgb(127, 77, 0), ti(6), arr(3)))

        End If
        'Dim str As String = ""
        'For Each i As Integer In ti
        '    str = str & vbNewLine & i
        'Next
        'MsgBox(str)
    End Sub
    Private Sub startGUI()
        drawGUIThread1(LoadingPanel1, "registry", "Registry", Color.FromArgb(255, 153, 0))
        drawGUIThread1(LoadingPanel2, "antivirus", "Antivirus", Color.FromArgb(127, 77, 0))
        drawGUIThread1(LoadingPanel3, "virus", "Viruses", Color.FromArgb(255, 153, 0))
        drawGUIThread1(LoadingPanel4, "spyware", "Spyware", Color.FromArgb(127, 77, 0))
        drawGUIThread1(LoadingPanel5, "junk", "System Junk", Color.FromArgb(255, 153, 0))
        drawGUIThread1(LoadingPanel6, "files", "System Files", Color.FromArgb(127, 77, 0))
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If running <> 0 Then
            Exit Sub
        End If
        alltrue = alltrue + 1
    End Sub

    Private Sub LoadingPanel1_Paint(sender As Object, e As EventArgs) Handles LoadingPanel1.Click
        If running <> 0 Then
            Exit Sub
        End If
        Ttime = Ttime + 60
    End Sub

End Class
