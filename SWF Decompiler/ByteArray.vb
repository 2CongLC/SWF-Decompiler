Imports System
Imports System.Text
Imports System.Text.Json
Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.Serialization.Json
Imports SevenZip
Imports System.Xml
Imports System.Runtime.InteropServices
Imports Lzma
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Security.Cryptography

Public Enum Endians
    BIG_ENDIAN = 0
    LITTLE_ENDIAN = 1
End Enum
Public Enum CompressionAlgorithm
    Deflate
    Gzip
    Zlib
    Lzma
    Brotli
End Enum

Public Class ByteArray

    Private source As MemoryStream = Nothing
    Private br As BinaryReader = Nothing
    Private bw As BinaryWriter = Nothing
    Private _endian As Endians = Nothing


    Public Sub New()

        source = New MemoryStream()
        br = New BinaryReader(source)
        bw = New BinaryWriter(source)
        _endian = Endians.LITTLE_ENDIAN

    End Sub

    Public Sub New(ByVal buffer As Byte(),
                   ByVal Optional position As Integer = 0,
                   ByVal Optional length As Integer = -1)


        If length = -1 Then
            length = buffer.Length
        End If

        source = New MemoryStream()
        source.Write(buffer, position, length)
        source.Position = 0
        br = New BinaryReader(source)
        bw = New BinaryWriter(source)
        _endian = Endians.LITTLE_ENDIAN

    End Sub

#Region "Thuộc tính chung"

    Public ReadOnly Property Length As UInteger
        Get
            Return source.Length
        End Get
    End Property

    Public Property Position As UInteger
        Get
            Return source.Position
        End Get
        Set(value As UInteger)
            source.Position = value
        End Set
    End Property

    Public ReadOnly Property BytesAvailable As UInteger
        Get
            Return source.Length - source.Position
        End Get
    End Property

    Public Property Endian As Endians
        Set(value As Endians)
            _endian = value
        End Set
        Get
            Return _endian
        End Get
    End Property

    Public Sub Clear()
        Dim buffer As Byte() = source.GetBuffer()
        Array.Clear(buffer, 0, buffer.Length)
        source.Position = 0
        source.SetLength(0)
    End Sub

    Public Function ToArray() As Byte()
        Return source.ToArray()
    End Function

    Public Function GetBuffer() As Byte()
        Return source.GetBuffer()
    End Function

    Public Function GetNextByte(Optional index As Integer = 0) As Byte
        Dim result As Byte = source.ToArray()(index)
        index += 1
        Return result
    End Function

#End Region

#Region "Nén và giải nén data"

    Public Sub Compress(ByVal Optional algorithm As CompressionAlgorithm = CompressionAlgorithm.Zlib)
        Select Case algorithm
            Case CompressionAlgorithm.Deflate
                Using _inms As MemoryStream = New MemoryStream(source.ToArray())
                    Using _outms As MemoryStream = New MemoryStream()
                        Using dfs As IO.Compression.DeflateStream = New IO.Compression.DeflateStream(_outms, IO.Compression.CompressionMode.Compress, True)
                            _inms.CopyTo(dfs)
                        End Using
                        source = _outms
                    End Using
                End Using
                Exit Select
            Case CompressionAlgorithm.Gzip
                Using _inms As MemoryStream = New MemoryStream(source.ToArray())
                    Using _outms As MemoryStream = New MemoryStream()
                        Using zls As IO.Compression.GZipStream = New IO.Compression.GZipStream(_outms, IO.Compression.CompressionMode.Compress, True)
                            _inms.CopyTo(zls)
                        End Using
                        source = _outms
                    End Using
                End Using
                Exit Select
            Case CompressionAlgorithm.Zlib
                Using _inms As MemoryStream = New MemoryStream(source.ToArray())
                    Using _outms As MemoryStream = New MemoryStream()
                        Using zls As IO.Compression.ZLibStream = New IO.Compression.ZLibStream(_outms, IO.Compression.CompressionMode.Compress, True)
                            _inms.CopyTo(zls)
                        End Using
                        source = _outms
                    End Using
                End Using
                Exit Select
            Case CompressionAlgorithm.Lzma
                Using _inms As MemoryStream = New MemoryStream(source.ToArray())
                    Using _outms As MemoryStream = New MemoryStream()
                        Using lzs As LzmaStream = New LzmaStream(_outms, CompressionMode.Compress)
                            _inms.CopyTo(lzs)
                        End Using
                        source = _outms
                    End Using
                End Using
                Exit Select
            Case CompressionAlgorithm.Brotli
                Using _inms As MemoryStream = New MemoryStream(source.ToArray())
                    Using _outms As MemoryStream = New MemoryStream()
                        Using brs As IO.Compression.BrotliStream = New IO.Compression.BrotliStream(_outms, CompressionMode.Compress)
                            _inms.CopyTo(brs)
                        End Using
                        source = _outms
                    End Using
                End Using
                Exit Select

        End Select

    End Sub

    Public Sub Uncompress(ByVal Optional algorithm As CompressionAlgorithm = CompressionAlgorithm.Zlib)
        Select Case algorithm
            Case CompressionAlgorithm.Deflate
                Position = 0
                Using _inms As MemoryStream = New MemoryStream(source.ToArray())
                    Using _outms As MemoryStream = New MemoryStream()
                        Using dfs As IO.Compression.DeflateStream = New IO.Compression.DeflateStream(_inms, IO.Compression.CompressionMode.Decompress, False)
                            dfs.CopyTo(_outms)
                        End Using
                        source = _outms
                        source.Position = 0
                    End Using
                End Using
                Exit Select
            Case CompressionAlgorithm.Gzip
                Position = 0
                Using _inms As MemoryStream = New MemoryStream(source.ToArray())
                    Using _outms As MemoryStream = New MemoryStream()
                        Using zls As IO.Compression.GZipStream = New IO.Compression.GZipStream(_inms, IO.Compression.CompressionMode.Decompress, False)
                            zls.CopyTo(_outms)
                        End Using
                        source = _outms
                        source.Position = 0
                    End Using
                End Using
                Exit Select
            Case CompressionAlgorithm.Zlib
                Position = 0
                Using _inms As MemoryStream = New MemoryStream(source.ToArray())
                    Using _outms As MemoryStream = New MemoryStream()
                        Using zls As IO.Compression.ZLibStream = New IO.Compression.ZLibStream(_inms, IO.Compression.CompressionMode.Decompress, False)
                            zls.CopyTo(_outms)
                        End Using
                        source = _outms
                        source.Position = 0
                    End Using
                End Using
                Exit Select
            Case CompressionAlgorithm.Lzma
                Position = 0
                Using _inms As MemoryStream = New MemoryStream(source.ToArray())
                    Using _outms As MemoryStream = New MemoryStream()
                        Using lzs As LzmaStream = New LzmaStream(_inms, CompressionMode.Decompress)
                            lzs.CopyTo(_outms)
                        End Using
                        source = _outms
                        source.Position = 0
                    End Using
                End Using
                Exit Select
            Case CompressionAlgorithm.Brotli
                Position = 0
                Using _inms As MemoryStream = New MemoryStream(source.ToArray())
                    Using _outms As MemoryStream = New MemoryStream()
                        Using brs As IO.Compression.BrotliStream = New IO.Compression.BrotliStream(_inms, CompressionMode.Decompress)
                            brs.CopyTo(_outms)
                        End Using
                        source = _outms
                        source.Position = 0
                    End Using
                End Using
                Exit Select

        End Select
    End Sub

#End Region

#Region "Đọc dữ liệu"

    Private Function ReadLittleEndian(length As Integer) As Byte()
        Return br.ReadBytes(length)
    End Function

    Private Function ReadBigEndian(length As Integer) As Byte()
        Dim little As Byte() = ReadLittleEndian(length)
        Dim reverse As Byte() = New Byte(length - 1) {}
        Dim i As Integer = length - 1, j As Integer = 0
        While i >= 0
            reverse(j) = little(i)
            i -= 1
            j += 1
        End While
        Return reverse
    End Function

    Public Function ReadBytesEndian(length As Integer) As Byte()
        If _endian = Endians.LITTLE_ENDIAN Then
            Return ReadLittleEndian(length)
        Else
            Return ReadBigEndian(length)
        End If
    End Function

    Public Sub ReadBytes(bytes As ByteArray, offset As UInteger, length As UInteger)
        Dim content As Byte() = New Byte(length - 1) {}
        br.Read(content, offset, length)
        bytes.WriteBytes(New ByteArray(content), 0, content.Length)
    End Sub

    Public Function ReadMultiByte(length As UInteger, charset As String) As String
        Dim bytes As Byte() = ReadBytesEndian(CInt(length))
        Return Encoding.GetEncoding(charset).GetString(bytes)
    End Function

    Public Function ReadBoolean() As Boolean
        Return br.ReadByte = 1
    End Function

    Public Function ReadDouble() As Double
        Dim bytes As Byte() = ReadBytesEndian(8)
        Dim reverse As Byte() = New Byte(7) {}

        For i As Integer = 7 To 0 Step -1
            reverse(7 - i) = bytes(i)
        Next
        Dim value As Double = BitConverter.ToDouble(reverse, 0)
        Return value
    End Function

    Public Function ReadSByte() As SByte
        Dim buffer As SByte = CSByte(br.ReadByte)
        Return buffer
    End Function

    Public Function ReadByte() As Byte
        Return br.ReadByte
    End Function

    Public Function ReadSingle() As Single
        Dim bytes As Byte() = ReadBytesEndian(4)
        Dim invertedBytes As Byte() = New Byte(3) {}
        ' Grab the bytes in reverse order from the backwards index
        For i As Integer = 3 To 0 Step -1
            invertedBytes(3 - i) = bytes(i)
        Next
        Dim value As Single = BitConverter.ToSingle(invertedBytes, 0)
        Return value
    End Function

    Public Function ReadShort() As Short
        Dim bytes As Byte() = ReadBytesEndian(2)
        Return bytes(1) << 8 Or bytes(0)
    End Function

    Public Function ReadUShort() As UShort
        Dim bytes As Byte() = ReadBytesEndian(2)
        Return CUShort(((bytes(0) And &HFF) << 8) Or (bytes(1) And &HFF))
    End Function

    Public Function ReadInt() As Integer
        Dim bytes As Byte() = ReadBytesEndian(4)
        'Dim value As Integer = bytes(3) << 24 Or CInt(bytes(2)) << 16 Or CInt(bytes(1)) << 8 Or bytes(0)
        Dim value As Integer = (bytes(0) << 24) Or (bytes(1) << 16) Or (bytes(2) << 8) Or bytes(3)
        Return value
    End Function

    Public Function ReadUInteger() As UInteger
        Dim bytes As Byte() = ReadBytesEndian(4)
        Return BitConverter.ToUInt32(bytes, 0)
    End Function

    Public Function ReadLong() As Long
        Dim bytes As Byte() = ReadBytesEndian(8)
        Return BitConverter.ToInt64(bytes, 0)
    End Function

    Public Function ReadULong() As ULong
        Dim bytes As Byte() = ReadBytesEndian(8)
        Return BitConverter.ToUInt64(bytes, 0)
    End Function

    Public Function ReadUTFBytes(length As Integer) As String
        If length = 0 Then
            Return String.Empty
        End If
        Return New UTF8Encoding(False, True).GetString(br.ReadBytes(length))
        ' Return Encoding.GetEncoding("GB2312").GetString(br.ReadBytes(length))
        ' Return Encoding.GetEncoding("UTF-8", New EncoderReplacementFallback(String.Empty), New DecoderReplacementFallback(String.Empty)).GetString(br.ReadBytes(length))
    End Function

    Public Function ReadUTF() As String
        Dim length As Integer = ReadShort()
        Return ReadUTFBytes(length)
    End Function


    Public Function ReadUInt24() As Integer
        Dim bytes As Byte() = ReadBytesEndian(3)
        Dim value As Integer = (bytes(0) << 16) Or (bytes(1) << 8) Or bytes(2)
        Return value
    End Function

 Public Function ReadReverseInt() As Integer
    Dim bytes() As Byte = ReadBytesEndian(4)
    Dim val As Integer = 0
    val += bytes(3) << 24
    val += bytes(2) << 16
    val += bytes(1) << 8
    val += bytes(0)
    Return val
End Function                             

Public Function ReadString() As String
    'Get the length of the string (first 2 bytes).
    Dim length As Integer = ReadUShort()
    Return ReadUTF(length)
End Function

   Public Function ReadLongString() as String
        Dim length As Integer = ReadInt()
        Return ReadUTF(length)
   End Function                                     
                                    
#End Region

#Region "Ghi dữ liệu"

    Private Sub WriteLittleEndian(bytes As Byte())
        If bytes Is Nothing Then
            Return
        End If
        bw.Write(bytes, 0, bytes.Length)
    End Sub

    Private Sub WriteBigEndian(bytes As Byte())
        If bytes Is Nothing Then
            Return
        End If
        For i = bytes.Length - 1 To 0 Step -1
            bw.Write(bytes(i))
        Next
    End Sub

    Public Sub WriteBytesEndian(bytes As Byte())
        If _endian = Endians.LITTLE_ENDIAN Then
            WriteLittleEndian(bytes)
        Else
            WriteBigEndian(bytes)
        End If
    End Sub

    Public Sub WriteBoolean(value As Boolean)
     bw.Write(If(value, CByte(1), CByte(0)))
    End Sub

    Public Sub WriteByte(value As Byte)
        bw.Write(value)
    End Sub

    Public Sub WriteBytes(bytes As ByteArray, Optional offset As UInteger = 0, Optional length As UInteger = 0)
        Dim offsetlength As Integer = bytes.ToArray().Take(offset).ToArray().Length
        Dim currentlength As Integer = bytes.ToArray().Length - offsetlength 'Lấy số length bị cắt ra
        If length = 0 Then
            length = currentlength
        End If
        bw.Write(bytes.ToArray(), offset, length)
    End Sub

    Public Sub WriteDouble(value As Double)
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        WriteBytesEndian(bytes)
    End Sub

    Public Sub WriteSingle(value As Single)
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        WriteBytesEndian(bytes)
    End Sub

    Public Sub WriteInt(value As Integer)
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        WriteBytesEndian(bytes)
    End Sub

    Public Sub WriteReverseInt(value As UInteger)
        Dim bytes As Byte() = New Byte(3) {}
        bytes(3) = CByte(&HFF And value >> 24)
        bytes(2) = CByte(&HFF And value >> 16)
        bytes(1) = CByte(&HFF And value >> 8)
        bytes(0) = CByte(&HFF And value >> 0)
        WriteBytesEndian(bytes)
    End Sub

    Public Sub WriteMultiByte(value As String, charset As String)
        Dim bytes As Byte() = Encoding.GetEncoding(charset).GetBytes(value)
        WriteBytesEndian(bytes)
    End Sub

    Public Sub WriteShort(value As Short)
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        WriteBytesEndian(bytes)
    End Sub

    Public Sub WriteUShort(value As UShort)
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        WriteBigEndian(bytes)
    End Sub

    Public Sub WriteUTF(value As String)
        Dim utf8 As UTF8Encoding = New UTF8Encoding()
        Dim count As Integer = utf8.GetByteCount(value)
        Dim buffer As Byte() = utf8.GetBytes(value)
        WriteShort(count)
        If buffer.Length > 0 Then
            bw.Write(buffer)
        End If
    End Sub

    Public Sub WriteUTFBytes(value As String)
        Dim utf8 As UTF8Encoding = New UTF8Encoding()
        Dim buffer As Byte() = utf8.GetBytes(value)
        If buffer.Length > 0 Then
            bw.Write(buffer)
        End If
    End Sub

    Public Sub WriteLong(ByVal value As Long)
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        WriteBigEndian(bytes)
    End Sub

    Public Sub WriteUInt24(value As Integer)
        Dim bytes As Byte() = New Byte(2) {}
        bytes(0) = CByte(&HFF And (value >> 16))
        bytes(1) = CByte(&HFF And (value >> 8))
        bytes(2) = CByte(&HFF And value)
        WriteBigEndian(bytes)
    End Sub


Private Sub WriteLongUTF(ByVal value As String)
    Dim utf8Encoding As New UTF8Encoding(True, True)
    Dim byteCount As UInteger = CUInt(utf8Encoding.GetByteCount(value))
    Dim buffer As Byte() = New Byte(byteCount + 4 - 1) {}
    'unsigned long (always 32 bit, big endian byte order)
    buffer(0) = CByte((byteCount >> &H18) And &HFF)
    buffer(1) = CByte((byteCount >> &H10) And &HFF)
    buffer(2) = CByte((byteCount >> 8) And &HFF)
    buffer(3) = CByte((byteCount And &HFF))
    Dim bytesEncodedCount As Integer = utf8Encoding.GetBytes(value, 0, value.Length, buffer, 4)
    If buffer.Length > 0 Then
        bw.Write(buffer, 0, buffer.Length)
    End If
End Sub

                              
   Public Sub WriteString(ByVal value As String)
    Dim utf8Encoding As New UTF8Encoding(True, True)
    Dim byteCount As Integer = utf8Encoding.GetByteCount(value)
    If byteCount < 65536 Then
        WriteUTF(value)
    Else
        WriteLongUTF(value)
    End If
End Sub



#End Region

#Region "Kiểm tra định dạng"

    Public Function TryGetXml(<Out> ByRef output As XDocument) As Boolean

        Try

            source.Position = 0
            Dim options As LoadOptions = LoadOptions.None
            output = XDocument.Load(source, options)
            Dim root As XElement = output.Root
            If root.IsEmpty = False Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TryGetJson(<Out> ByRef output As JsonDocument) As Boolean

        Try
            source.Position = 0
            Dim options As JsonDocumentOptions = New JsonDocumentOptions() With {
              .CommentHandling = JsonCommentHandling.Skip}
            output = JsonDocument.Parse(source, options)
            Dim root As JsonElement = output.RootElement
            If root.ValueKind = JsonValueKind.Object Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function


#End Region

#Region " Trích xuất dữ liệu "

    Public Function BitmapFromBytes(ByVal offset As Integer, ByVal length As Integer) As Bitmap
        Dim data As Byte() = New Byte(length - 1) {}
        Array.Copy(source.ToArray(), offset, data, 0, length)
        Return DirectCast(System.ComponentModel.TypeDescriptor.GetConverter(GetType(Bitmap)).ConvertFrom(data), Bitmap)
    End Function

    Public Function ConvertToHex() As String
        Return String.Join("", source.ToArray().Select(Function(by) by.ToString("X2")))
    End Function

    Public Function ConvertFromHex() As Byte()
                                                    
        Dim hexstring as String = Encoding.UTF8.GetString(source.ToArray())
        Dim NumberChars As Integer = hexstring.Length
        Dim bytes As Byte() = New Byte(NumberChars \ 2 - 1) {}
        For i As Integer = 0 To NumberChars - 1 Step 2
            bytes(i \ 2) = Convert.ToByte(hexstring.Substring(i, 2), 16)
        Next
        Return bytes
    End Function





#End Region

#Region "Lấy mã Hash"

    Public Function MD5Hash() As String
        Return BitConverter.ToString(MD5.Create().ComputeHash(source)).Replace("-", "").ToLower()
    End Function

    Public Function SHA1Hash() As String
        Return BitConverter.ToString(SHA1.Create().ComputeHash(source)).Replace("-", "").ToLower()
    End Function

    Public Function SHA256Hash() As String
        Return BitConverter.ToString(SHA256.Create().ComputeHash(source)).Replace("-", "").ToLower()
    End Function

    Public Function SHA384Hash() As String
        Return BitConverter.ToString(SHA384.Create().ComputeHash(source)).Replace("-", "").ToLower()
    End Function

    Public Function SHA512Hash() As String
        Return BitConverter.ToString(SHA512.Create().ComputeHash(source)).Replace("-", "").ToLower()
    End Function

#End Region

#Region "Serializator"

    Public Function SerializeXml(Of T)() As String
        Try
            Dim value As String = Encoding.UTF8.GetString(source.ToArray())

            Dim xmlSerializer = New Xml.Serialization.XmlSerializer(GetType(T))
            Using stringWriter = New IO.StringWriter()
                Using write = Xml.XmlWriter.Create(stringWriter, New Xml.XmlWriterSettings With {
                .Indent = True
            })
                    xmlSerializer.Serialize(write, value)
                    Return stringWriter.ToString()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("An error occurred", ex)
        End Try
    End Function

    Public Function DeserializeXml(Of T)() As T
        Try
            Dim value As String = Encoding.UTF8.GetString(source.ToArray())
            Dim xmlSerializer = New Xml.Serialization.XmlSerializer(GetType(T))
            Return CType(xmlSerializer.Deserialize(New IO.StringReader(value)), T)
        Catch ex As Exception
            Throw New Exception("An error occurred", ex)
        End Try
    End Function

<Obsolete>
    Public Function SerializeBinary(Of T)() As Byte()
        Dim obj As String = Encoding.UTF8.GetString(source.ToArray())
        Using memStream As New MemoryStream()
            Dim binSerializer As New BinaryFormatter()
            binSerializer.Serialize(memStream, obj)
            Return memStream.ToArray()
        End Using
    End Function

    <Obsolete>
    Public Function DeserializeBinary(Of T)() As T
        Dim obj As T = Nothing
        Using memStream As New MemoryStream(source.ToArray())
            Dim binSerializer As New BinaryFormatter()
            obj = DirectCast(binSerializer.Deserialize(memStream), T)
        End Using
        Return obj
    End Function
                                                
Public Function SerializeJson(Of T)(Optional Indented As Boolean = True) as String
        Dim obj As String = Encoding.UTF8.GetString(source.ToArray())
        Dim options As New JsonSerializerOptions With {.WriteIndented = Indented}
            Dim result As String = JsonSerializer.Serialize(obj, options)
            Return result
    End Function

Public Function DeSerializeJson(Of T)() As t
            Dim obj As String = Encoding.UTF8.GetString(source.ToArray())
            Dim result As t = JsonSerializer.Deserialize(Of t)(obj)
            Return result
    End Function



#End Region

End Class
