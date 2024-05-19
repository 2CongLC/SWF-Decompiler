Imports System
Imports System.IO
Imports System.Text


Public Class SWFFile



    Private source As ByteArray
    Private _signature As String
    Private _version As Integer
    Private _fileSize As UInteger

    Private mWidth As Integer = 0
    Private mHeigth As Integer = 0
    Private _framerate As double
    Private _framecount As Byte()
    Private data as ByteArray

    


    Public Sub New(ByVal buffer As Byte())

        source = New ByteArray(buffer)
        source.Position = 0
        _signature = Encoding.ASCII.GetString(source.ReadBytesEndian(3))
        source.Position = 3
        _version = souce.ReadByte
        source.Position = 4
        Dim len As UInteger = 0
        len = len Or (source.ReadByte << 0)
        len = len Or (source.ReadByte << 8)
        len = len Or (source.ReadByte << 16)
        len = len Or (source.Readbyte << 24)
        _filesize = len
        source.Position  = 8
        If (_signature = "CWS") Then
        source.Uncompress()
        End if
        Dim b As Integer = source.GetNextByte()
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
                    b = source.GetNextByte()
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
    
        source.Position = 17
        Dim a1 As Byte = source.ReadByte
        Dim b1 As Byte = source.ReadByte
        Dim c1 As Double = (a1 + b1) / 100
        _framerate = c1
       source.Position = 19
        Dim a2 As Byte = source.Readbyte
        Dim b2 As Byte = source.Readbyte
        Dim c2 As Integer
        c2 += (a2 << 8 * b2)
        _framecount = c2
    source.Position = 21
    data = New ByteArray()
    data.WriteBytes(source,21)
    
    End Sub


    Public Function Signature() As String
        Return _signature
    End Function

    Public Function Version() As UInteger
        Return _version
    End Function
    

    Public Function Filesize() As UInteger
        Return _filesize
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

    Public Function FrameRate() As Double
  Return _framerate
    End Function

    Public Function FrameCount() As Integer
        Return _framecount
    End Function

    Public Function Tags() As Integer
            Return data.ToArray().Length
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
