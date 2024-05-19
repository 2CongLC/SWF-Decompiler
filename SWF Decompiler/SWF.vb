Imports System
Imports System.IO
Imports System.Text


Public Class SWF



    Private source As ByteArray

    Private _signature As Byte()

    Private _version As Byte()

    Private _fileSize As Byte()
    Private _filesize1 As ByteArray


    Private _framesize As ByteArray

    Private _framerate As Byte()

    Private _framecount As Byte()

    Private _tags As Byte()

    Private mWidth As Integer = 0

    Private mHeigth As Integer = 0


    Public Sub New(ByVal buffer As Byte())

        source = New ByteArray(buffer)

        Dim sign As ByteArray = New ByteArray()

        sign.WriteBytes(source, 0, 3) ' Offset = 0, Length = 3

        _signature = sign.ToArray()

        Dim ver As ByteArray = New ByteArray()

        ver.WriteBytes(source, 3, 1) ' Offset = 3, Length = 1

        _version = ver.ToArray()

        Dim fsize As ByteArray = New ByteArray()

        fsize.WriteBytes(source, 4, 4) 'Offset = 4, Length = 4

        _fileSize = fsize.ToArray()

        _filesize1 = New ByteArray()
        _filesize1.WriteBytes(source, 4, 4)

        Dim data As ByteArray = New ByteArray()

        If source.read = "CWS" Then
            data.WriteBytes(source, 8)
            data.Uncompress()
        ElseIf Signature() = "FWS" Then
            data.WriteBytes(source, 12)
            data.Uncompress()
        End If


    End Sub


    Public Function Signature() As String
        Return Encoding.Default.GetString(_signature)
    End Function

    Public Function Version() As UInteger
        Return CUInt(_version(0))
    End Function
    Public Function ReadReverseUInt() As UInteger
        Dim bytes() As Byte = _filesize1.ToArray()
        Dim len As UInteger = 0
        len = len Or (CUInt(bytes(0)) << 0)
        len = len Or (CUInt(bytes(1)) << 8)
        len = len Or (CUInt(bytes(2)) << 16)
        len = len Or (CUInt(bytes(3)) << 24)
        Return len
    End Function

    Public Function Filesize() As UInteger
        Return ReadReverseUInt()
    End Function

    Public ReadOnly Property Width as integer
    Get
      Return mWidth
    End Get
End Property

Public ReadOnly Property Heigth as integer
    Get
      Return mHeigth
    End Get
End Property

    Public Sub FrameSize()
        Dim b As Integer = _framesize.GetNextByte()
        Dim nb As Integer = b >> 3
        b = b And 7
        b <<= 5
        Dim cb As Integer = 2
        Dim value As Integer
        For numfield As Integer = 0 To 4 - 1
            value = 0
            Dim bitcount As Integer = 0
            While bitcount < nb
                If (b And 128) = 128 Then
                    value = value + (1 << nb - bitcount - 1)
                End If
                b <<= 1
                b = b And 255
                cb -= 1
                bitcount += 1

                If cb < 0 Then
                    b = _framesize.GetNextByte()
                    cb = 7
                End If
            End While
            value /= 20
            Select Case numfield
                Case 0
                    mWidth = value
                Case 1
                    mWidth = value - mWidth
                Case 2
                    mHeigth = value

                Case 3
                    mHeigth = value - mHeigth
            End Select
        Next
    End Sub

    Public Function FrameRate() As Double

        If (Signature() = "CWS") OrElse (Signature() = "ZWS") Then Exit Function

        Dim a As Byte = _framerate(0)
        Dim b As Byte = _framerate(1)
        Dim c As Double = (a + b) / 100
        Return c

    End Function

    Public Function FrameCount() As Integer

        If (Signature() = "CWS") OrElse (Signature() = "ZWS") Then Exit Function

        Dim a As Byte = _framecount(0)
        Dim b As Byte = _framecount(1)
        Dim c As Integer
        c += (a << 8 * b)
        Return c

    End Function

    Public Function Tags() As Integer

        If (Signature() = "CWS") OrElse (Signature() = "ZWS") Then Exit Function
        If _tags.Length <> 0 Then
            Return _tags.Length
        End If


    End Function


    Private Function CompressCWS() As Byte()

        Dim data As ByteArray = New ByteArray()
        data.WriteBytes(source, 8)
        data.Compress()
        Dim buffer As ByteArray = New ByteArray()
        buffer.WriteMultiByte("CWS", "us-ascii")
        buffer.WriteByte(Version)
        buffer.WriteReverseInt(Filesize)
        buffer.WriteBytes(data)
        Return buffer.ToArray()
    End Function

    Private Function DeCompressCWS() As Byte()

        Dim data As ByteArray = New ByteArray()
        data.WriteBytes(source, 8)
        data.Uncompress()
        Dim buffer As ByteArray = New ByteArray()
        buffer.WriteMultiByte("FWS", "us-ascii")
        buffer.WriteByte(Version)
        buffer.WriteReverseInt(Filesize)
        buffer.WriteBytes(data)
        Return buffer.ToArray()

    End Function

    Private Function CompressZWS() As Byte()

        Dim data As ByteArray = New ByteArray()
        data.WriteBytes(source, 8)

        data.Compress(CompressionAlgorithm.LZMA)

        Dim compressedLen As Long = data.BytesAvailable

        Dim lzmaprops As ByteArray = New ByteArray()
        lzmaprops.WriteBytes(data, 0, 3)
        Dim lzmadata As ByteArray = New ByteArray()
        lzmadata.WriteBytes(data, 12)

        Dim buffer As ByteArray = New ByteArray()
        buffer.WriteMultiByte("ZWS", "us-ascii")
        buffer.WriteByte(Version)
        buffer.WriteReverseInt(Filesize)
        buffer.WriteByte(compressedLen)
        buffer.WriteBytes(lzmaprops)
        buffer.WriteBytes(lzmadata)

         Return buffer.ToArray()

    End Function

    Private Function DeCompressZWS() As Byte()

        Dim data As ByteArray = New ByteArray()
        data.WriteBytes(source, 12)
        data.Uncompress(CompressionAlgorithm.LZMA)
        Dim buffer As ByteArray = New ByteArray()
        buffer.WriteMultiByte("FWS", "us-ascii")
        buffer.WriteByte(Version)
        buffer.WriteReverseInt(Filesize)
        buffer.WriteBytes(data)
        Return buffer.ToArray()

    End Function

Enum CompressTionTypes
    CWS
    ZWS
End Enum
    
Public Sub Compress(Byval outFile as String,Byval CompressTionType as CompressTionTypes)

        If CompressTionType = CompressTionTypes.CWS Then


            If (Signature() = "FWS") AndAlso (Version() >= 6) Then
                IO.File.WriteAllBytes(outFile, CompressCWS)
            End If

        ElseIf CompressTionType = CompressTionTypes.ZWS Then

            If (Signature() = "FWS") AndAlso (Version() >= 13) Then
                IO.File.WriteAllBytes(outFile, CompressZWS)
            End If

        End if

End Sub

    Public Sub DeCompress(ByVal outFile As String)

        If Signature() = "CWS" Then
            IO.File.WriteAllBytes(outFile, DeCompressCWS)
        ElseIf Signature = "ZWS" Then
            IO.File.WriteAllBytes(outFile, DeCompressZWS)
        End If

    End Sub


    Public Function MochiDecrypt() As Byte()

        Dim data As ByteArray = New ByteArray()
        data.WriteBytes(source, 8)
        Dim payload As Byte() = data.ToArray()

        Dim S As New List(Of Byte)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim n As Integer = 0
        Dim u As Integer = 0
        Dim v As Integer = 0

        n = payload.Length - 32
        While i < 256
            S.Add(CByte(i))
            i += 1
        End While
        j = 0
        i = 0
        While i < 256
            j = (j + S(i) + payload(n + (i And 31))) And 255
            u = S(i)
            S(i) = S(j)
            S(j) = CByte(u)
            i += 1
        End While
        If n > 131072 Then
            n = 131072
        End If
        j = 0
        i = 0
        k = 0
        While k < n
            i = (i + 1) And 255
            u = S(i)
            j = (j + u) And 255
            v = S(j)
            S(i) = CByte(v)
            S(j) = CByte(u)
            payload(k) = CByte(payload(k) Xor S((u + v) And 255))
            k += 1
        End While
        Return payload
    End Function



End Class  
