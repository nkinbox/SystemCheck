Public Class cParameter
    Public lpanel As LoadingPanel
    Public img As String
    Public label As String
    Public bg As Color
    Public time As Integer
    Public err As Boolean
    Public Sub New(lpanel1 As LoadingPanel, img1 As String, label1 As String, bg1 As Color, time1 As Integer, err1 As Boolean)
        lpanel = lpanel1
        img = img1
        label = label1
        bg = bg1
        time = time1
        err = err1
    End Sub
End Class
