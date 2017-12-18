Public Class LoadingPanel
    Inherits Panel
    Public Sub New()
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        UpdateStyles()
    End Sub
End Class