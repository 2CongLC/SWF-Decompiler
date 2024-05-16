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

    Public Function GetNextByte(Option index as integer =0) as Byte
        Dim result as byte = source.ToArray()(index)
        index +=1
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
        source.Read(content, offset, length)
        bytes.WriteBytes(New ByteArray(content), 0, content.Length)
    End Sub
                                            
    Public Function ReadMultiByte(length As UInteger, charset As String) As String
        Dim bytes As Byte() = ReadBytesEndian(CInt(length))
        Return Encoding.GetEncoding(charset).GetString(bytes)
    End Function 
                                            
    Public Function ReadBoolean() As Boolean
        Return source.ReadByte = 1
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
        Dim buffer As SByte = CSByte(source.ReadByte)
        Return buffer
    End Function
                                            
    Public Function ReadByte() As Byte
        Return source.ReadByte
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
                                            
    Public Function ReadInteger() As Integer
        Dim bytes As Byte() = ReadBytesEndian(4)
        Dim value As Integer = bytes(3) << 24 Or CInt(bytes(2)) << 16 Or CInt(bytes(1)) << 8 Or bytes(0)
        Return value
    End Function
                                            
    Public Function ReadUInteger() As UInteger
        Dim bytes As Byte() = ReadBytesEndian(4)
        Return BitConverter.ToUInt32(bytes, 0)
    End Function

     Public Function ReadLong() As Long
        Dim bytes As Byte() = ReadBytesEndian(8)
        Return BitConverter.ToLong(bytes, 0)
    End Function
                                            
    Public Function ReadULong() As ULong
        Dim bytes As Byte() = ReadBytesEndian(8)
        Return BitConverter.ULong(bytes, 0)
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

Public Function ReadReverseInt() As Integer
    Dim bytes As Byte() = ReadBytesEndian(4)
    Dim val As Integer = 0
    val += bytes(3) << 24
    val += bytes(2) << 16
    val += bytes(1) << 8
    val += bytes(0)
    Return val
End Function

 Public Function ReadUInt24() As Integer
    Dim bytes As Byte() = Me.ReadBytes(3)
    Dim value As Integer = (bytes(0) << 16) Or (bytes(1) << 8) Or bytes(2)
    Return value
End Function
                                    
#End Region

#Region "Ghi dữ liệu"

    Private Sub WriteLittleEndian(bytes As Byte())
        If bytes Is Nothing Then
            Return
        End If
        source.Write(bytes, 0, bytes.Length)
    End Sub
                                    
    Private Sub WriteBigEndian(bytes As Byte())
        If bytes Is Nothing Then
            Return
        End If
        For i = bytes.Length - 1 To 0 Step -1
            source.WriteByte(bytes(i))
        Next
    End Sub
                                        
    Friend Sub WriteBytesEndian(bytes As Byte())
        If _endian = Endians.LITTLE_ENDIAN Then
            WriteLittleEndian(bytes)
        Else
            WriteBigEndian(bytes)
        End If
    End Sub

    Public Sub WriteBoolean(value As Boolean)
        source.WriteByte(If(value, CByte(1), CByte(0)))
    End Sub

    Public Sub WriteSByte(value As SByte)
        source.WriteByte(value)
    End Sub

    Public Sub WriteUByte(value As Byte)
        source.WriteByte(value)
    End Sub                               

    Public Sub WriteBytes(bytes As ByteArray, Optional offset As UInteger = 0, Optional length As UInteger = 0)
        Dim offsetlength As Integer = bytes.ToArray().Take(offset).ToArray().Length
        Dim currentlength As Integer = bytes.ToArray().Length - offsetlength 'Lấy số length bị cắt ra
        If length = 0 Then
            length = currentlength
        End If
        source.Write(bytes.ToArray(), offset, length)
    End Sub

    Public Sub WriteDouble(value As Double)
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        WriteBytesEndian(bytes)
    End Sub

    Public Sub WriteFloat(value As Single)
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

    Public Sub WriteLong(Byval value As Long)
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

Public Sub WriteXDocument(Byval value As XDocument)
    If value IsNot Nothing Then
        Dim xmlstring As String = value.ToString()
        WriteLongUTF(xmlstring)
    End If
End Sub

Public Sub WriteXElement(Byval value As XElement)
    If value IsNot Nothing Then
        Dim xml As String = value.ToString()
        Me.WriteLongUTF(xml)
    End If
End Sub                                  
    

#End Region

#Region "Kiểm tra định dạng"

    Public Function TryGetXml(<out> ByRef output As XDocument) As Boolean
        
        Try

            source.Position = 0
            Dim options As LoadOptions = LoadOptions.None
            output = XDocument.Load(source, options)
            Dim root as XElement = output.root
            If Root.IsEmpty = False Then
                Return True
            Else
                Return False
            End If

        Catch ex as Exception     
            Return False
        End Try
    End Function

    Public Function TryGetJson(<out>ByRef output As JsonDocument) As Boolean
      
        Try  
             source.Position = 0
             Dim options as JsonDocumentOptions =  New JsonDocumentOptions() With {
               .CommentHandling = JsonCommentHandling.Skip }
            output = JsonDocument.Parse(source, options)
            Dim root As JsonElement = output.RootElement
            If root.ValueKind = JsonValueKind.Object Then
                Return True
            Else
                Return False
            End if                                           
             
        Catch ex as Exception
             Return False
         End Try 
                                            
    End Function
                                                
    Public Function TryGetHex(<out>Byref data as Byte()) as Boolean
      Try
          Dim hexstring as String = Encoding.Default.Getstring(source.ToArray())
          If hexstring.All(char.IsAsciiHexDigit) = True Then
             data = ConvertFromHex(hexstring)
              Return True
           Else
              Return False
           End if
         Catch Ex as Exception
                Return False
         End Try 
       End Function                                             
                                                    
#End Region

#Region "Mã hóa dữ liệu"

Public Sub MochiDecrypt() 
    
    Dim data as ByteArray = New ByteArray()
    data.WriteBytes(source,8)
    Dim payload as Byte() = data.ToArray()
    
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
    Dim ms as MemoryStream = New MemoryStream(payload)
    source = ms                                               
    source.position = 0 
    ms.close()                                               
End Sub

                                                
#End Region

#Region " Trích xuất dữ liệu "
    
Public Function BitmapFromBytes(Byval offset As Integer,Byval length As Integer) As Bitmap
    Dim data As Byte() = New Byte(length - 1) {}
    Array.Copy(source.ToArray(), offset, data, 0, length)
    Return DirectCast(System.ComponentModel.TypeDescriptor.GetConverter(GetType(Bitmap)).ConvertFrom(data), Bitmap)
End Function

Public Function ConvertToHex() As String
    Return String.Join("", source.ToArray().Select(Function(by) by.ToString("X2")))
End Function

Private Function ConvertFromHex(Byval hexstring as string) As Byte()
    
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
                          
End Class
