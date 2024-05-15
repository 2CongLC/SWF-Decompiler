Imports System
Imports System.Text
Imports System.IO

Public Class DataTags

Public Property TagCode As TagType
Private Set(value As TagType)
End Set
End Property

Private _Offset As Integer
Public Property Offset As Integer
    Get
        Return _Offset
    End Get
    Private Set(value As Integer)
        _Offset = value
    End Set
End Property

Protected _DataOffset As Integer
Public Property DataOffset As Integer
    Get
        Return _DataOffset
    End Get
    Set(value As Integer)
        _DataOffset = value
    End Set
End Property

Private _Length As Integer
Public Property Length As Integer
    Get
        Return _Length
    End Get
    Private Set(value As Integer)
        _Length = value
    End Set
End Property

Public ReadOnly Property DataLength As Integer
    Get
        Return Length - (DataOffset - Offset)
    End Get
End Property

Protected Property RawData As Byte()



    Public Function PickBits(data As Byte(), offsetByte As Integer, offsetBit As Integer, lengthBit As Integer, Optional isLittleEndian As Boolean = True) As UInteger
        If lengthBit > 32 Then
            Throw New ArgumentOutOfRangeException()
        End If

        Dim ret As UInteger = 0
        Dim len As Integer = CInt(Math.Ceiling((offsetBit + lengthBit) / 8.0))

        For i As Integer = 0 To lengthBit - 1
            Dim index As Integer
            If isLittleEndian Then
                index = offsetByte + len - 1 - (offsetBit + i) \ 8
            Else
                index = offsetByte + (offsetBit + i) \ 8
            End If

            If (data(index) And (1 << (7 - (i + offsetBit) Mod 8))) <> 0 Then
                ret = ret Or (1UI << (31 - i))
            End If
        Next

        Return ret >> (32 - lengthBit)
    End Function

    Public Function PickBytes(data As Byte(), offsetByte As Integer, lengthByte As Integer, Optional isLittleEndian As Boolean = True) As UInteger
        If lengthByte > 4 Then
            Throw New ArgumentOutOfRangeException()
        End If

        Dim ret As UInteger = 0
        For i As Integer = 0 To lengthByte - 1
            Dim value As UInteger = data(offsetByte + i)

            If isLittleEndian Then
                value <<= i * 8
            Else
                value <<= (lengthByte - 1 - i) * 8
            End If

            ret = ret Or value
        Next

        Return ret
    End Function

    Public Function PickBytes16(data As Byte(), offsetByte As Integer, Optional isLittleEndian As Boolean = True) As UShort
        Return CUShort(PickBytes(data, offsetByte, 2, isLittleEndian))
    End Function

    Public Function PickBytes32(data As Byte(), offsetByte As Integer, Optional isLittleEndian As Boolean = True) As UInteger
        Return PickBytes(data, offsetByte, 4, isLittleEndian)
    End Function

    Public Function PickSignedBits(data As Byte(), offsetByte As Integer, offsetBit As Integer, lengthBit As Integer, Optional isLittleEndian As Boolean = True) As Integer
        Dim ret As Integer = CInt(PickBits(data, offsetByte, offsetBit, lengthBit, isLittleEndian))
        If (ret And (1 << (lengthBit - 1))) <> 0 Then ' msb == 1; minus;
            ret = ret Or (Not 0 << (lengthBit - 1)) ' fill by 1
        End If
        Return ret
    End Function

    Public Function GetStride(width As Integer) As Integer
        Return (width + 3) And -4
    End Function







End Class

  
Public Enum TagType
    [End] = 0
    ShowFrame = 1
    DefineShape = 2
    PlaceObject = 4
    RemoveObject = 5
    DefineBits = 6
    DefineButton = 7
    JPEGTables = 8
    SetBackgroundColor = 9
    DefineFont = 10
    DefineText = 11
    DoAction = 12
    DefineFontInfo = 13
    DefineSound = 14
    StartSound = 15
    DefineButtonSound = 17
    SoundStreamHead = 18
    SoundStreamBlock = 19
    DefineBitsLossless = 20
    DefineBitsJPEG2 = 21
    DefineShape2 = 22
    DefineButtonCxform = 23
    [Protect] = 24
    PlaceObject2 = 26
    RemoveObject2 = 28
    DefineShape3 = 32
    DefineText2 = 33
    DefineButton2 = 34
    DefineBitsJPEG3 = 35
    DefineBitsLossless2 = 36
    DefineEditText = 37
    DefineSprite = 39
    ProductInfo = 41
    FrameLabel = 43
    SoundStreamHead2 = 45
    DefineMorphShape = 46
    DefineFont2 = 48
    ExportAssets = 56
    ImportAssets = 57
    EnableDebugger = 58
    DoInitAction = 59
    DefineVideoStream = 60
    VideoFrame = 61
    DefineFontInfo2 = 62
    EnableDebugger2 = 64
    ScriptLimits = 65
    SetTabIndex = 66
    FileAttributes = 69
    PlaceObject3 = 70
    ImportAssets2 = 71
    DefineFontAlignZones = 73
    CSMTextStrings = 74
    DefineFont3 = 75
    SymbolClass = 76
    [Metadata] = 77
    DefineScalingGrid = 78
    DoABC = 82
    DefineShape4 = 83
    DefineMorphShape2 = 84
    DefineSceneAndFrameLabelData = 86
    DefineBinaryData = 87
    DefineFontName = 88
    StartSound2 = 89
    DefineBitsJPEG4 = 90
    DefineFont4 = 91
    EnableTelemetry = 93
End Enum

