Imports System.IO
Imports System.Runtime.InteropServices

Public Class ImageInfo

#Region " Constants "

    Private Const HSVOLSTART As Integer = 16    ' where we expect a Primary Volume Descriptor */
    Private Const HSTERMSTART As Integer = 17    ' where we expect the Volume Descriptor Terminator */

    Private Const MISSING_DATE As String = "0000000000000000" + ControlChars.NullChar

    Private Const DOT_DIRECTORY As String = ControlChars.NullChar
    Private Const PARENT_DIRECTORY As String = Chr(0)   'I hate to use these VB methods

    Private Const StdVolType As Byte = 1    ' Primary Volume Descriptor type
    Private Const VolEndType As Byte = 255    ' Volume Descriptor Set Terminator type 

    Private Shared ReadOnly imageModesSectorSize As Dictionary(Of ImageModes, Integer)

    '
    ' File Flags for Directory Records
    '
    Private Const existenceBit As Integer = &H1
    Private Const directoryBit As Integer = &H2
    Private Const associatedBit As Integer = &H4
    Private Const recordBit As Integer = &H8
    Private Const protectionBit As Integer = &H10
    Private Const multiextentBit As Integer = &H80

#End Region

#Region " Enumerations "

    Public Enum ImageModes As Integer
        ModeOne2048 = 0
        ModeOne2448 = 1
        ModeOne2368 = 2
        ModeOne2352 = 3
        ModeTwo2336 = 4
        ModeTwo2352 = 5
        ModeTwo2368 = 6
        ModeTwo2448 = 7
    End Enum

#End Region

#Region " Structures "

    '****************************************************************************/
    '*  Exported Types                                                          */
    '****************************************************************************/

    'typedef unsigned char byte;


    '****************************************************************************/
    '*  ISO 9660 (pANS Z39.86 198x) standard format                             */
    '****************************************************************************/

    ' 
    ' Primary Volume Descriptor (PVD)
    '
    Private Structure PrimaryVolumeDescriptor
        Public VDType As Byte           ' Must be 1 for PVD
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=5)> _
        Public VSStdId As String        ' Must be "CD001"
        Public VSStdVersion As Byte     ' Must be 1
        Public Reserved1 As Byte        ' Must be 0's
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public systemIdentifier As String
        <MarshalAs(UnmanagedType.ByValTStr, sizeconst:=32)> _
        Public volumeIdentifier As String
        <MarshalAs(UnmanagedType.ByValTStr, sizeconst:=8)> _
        Public Reserved2 As String      ' Must be 0's
        Public lsbVolumeSpaceSize As Integer
        Public msbVolumeSpaceSize As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public Reserved3 As String      ' Must be 0's
        Public lsbVolumeSetSize As Short
        Public msbVolumeSetSize As Short
        Public lsbVolumeSetSequenceNumber As Short
        Public msbVolumeSetSequenceNumber As Short
        Public lsbLogicalBlockSize As Short
        Public msbLogicalBlockSize As Short
        Public lsbPathTableSize As Integer
        Public msbPathTableSize As Integer
        Public lsbPathTable1 As Integer ' mandatory occurrence
        Public lsbPathTable2 As Integer ' optional occurrence
        Public msbPathTable1 As Integer ' mandatory occurrence
        Public msbPathTable2 As Integer ' optional occurrence
        Public rootDirectoryRecord As DirectoryRecordStruct
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public volumeSetIdentifier As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public publisherIdentifier As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public dataPreparerIdentifier As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public applicationIdentifier As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=37)> _
        Public copyrightFileIdentifier As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=37)> _
        Public abstractFileIdentifier As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=37)> _
        Public bibliographicFileIdentifier As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=17)> _
        Public volumeCreation As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=17)> _
        Public volumeModification As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=17)> _
        Public volumeExpiration As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=17)> _
        Public volumeEffective As String
        '  char  FileStructureStandardVersion;
        '  char  Reserved4;        ' Must be 0's
        '  char  ApplicationUse[512];
        '  char  FutureStandardization[653];
    End Structure

    '
    ' Path Table
    '

    'typedef char  dirIDArray[8];

    Private Structure PathTableRecord
        Public len_di As Byte   ' length of directory identifier
        Public XARlength As Byte    ' Extended Attribute Record Length
        Public dirLocation As Long  ' 1st logical block where directory is stored
        Public parentDN As Short    ' parent directory number
        '  dirIDArray  dirID;    ' directory identifier: actual length is
        '  9 - [8+Len_di]; there is an extra blank
        '  byte if Len_di is odd.
    End Structure

    '
    ' Directory Record
    '  There exists one of these for each file in the directory.
    '  
    Private Structure OldAppleExtension
        '        Public macFlag(2) As Char    ' $42 $41 - 'BA' famous value
        Public systemUseID As Byte  ' 06 = HFS
        '  Public  fileType[4] as byte  ' such as 'TEXT' or 'STAK'
        '  Public  fileCreator[4] as byte  ' such as 'hscd' or 'WILD'
        '  Public  finderFlags[2] as byte
    End Structure

    Friend Structure AppleExtension
        '  char  signature[2];    ' $41 $41 - 'AA' famous value
        Public extensionLength As Byte  ' $0E for this ID
        Public systemUseID As Byte  ' 02 = HFS
        '        Public fileType() As Byte = New Byte(4) {}  ' such as 'TEXT' or 'STAK'
        '        Public fileCreator(4) As Byte  ' such as 'hscd' or 'WILD'
        '        Public finderFlags(2) As Byte
    End Structure

    Friend Structure DirectoryRecordStruct
        Public len_dr As Byte       ' directory record length
        Public XARlength As Byte    ' Extended Attribute Record Length
        Public lsbStart As UInteger
        Public msbStart As UInteger  ' 1st logical block where file starts
        Public lsbDataLength As UInteger
        Public msbDataLength As UInteger
        Public year As Byte         ' since 1900
        Public month As Byte
        Public day As Byte
        Public hour As Byte
        Public minute As Byte
        Public second As Byte
        Public gmtOffset As SByte
        Public fileFlags As Byte
        Public interleaveSize As Byte
        Public interleaveSkip As Byte
        Public lsbVolSetSeqNum As Short
        Public msbVolSetSeqNum As Short  ' which volume in volume set contains this file.
        Public len_fi As Byte       ' length of file identifier which follows
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=37)> _
        Public fi As String         ' file identifier: actual is 37-[36+Len_fi], contains extra blank byte if Len_fi odd
        Public apple As AppleExtension ' this actually fits immediately after the fi[]
        ' field, or after its padding byte. */
    End Structure

#End Region

#Region " Fields "

    Private _sectorSize As Integer
    Private _length As Long
    Private _totalSectors As Integer
    Private _totalTime As String

    Private _primaryVolumeDescriptor As PrimaryVolumeDescriptor
    Private _pathTableRecord As PathTableRecord
    Private _directoryRecords As Dictionary(Of Integer, DirectoryRecord)

#End Region

#Region " Properties "

    Private _creationDate As DateTimeOffset?
    Public ReadOnly Property CreationDate() As DateTimeOffset?
        Get
            ParseAsciiDateTimeIfNull(_primaryVolumeDescriptor.volumeCreation, _creationDate)

            Return _creationDate
        End Get
    End Property

    Private _fileName As String
    Public ReadOnly Property FileName() As String
        Get
            Return _fileName
        End Get
    End Property

    Private _mode As ImageModes
    Public ReadOnly Property Mode() As ImageModes
        Get
            Return _mode
        End Get
    End Property

    Public ReadOnly Property Publisher() As String
        Get
            Return _primaryVolumeDescriptor.publisherIdentifier.TrimEnd()
        End Get
    End Property

    Private _rootDirectory As DirectoryRecord
    Public ReadOnly Property RootDirectory() As DirectoryRecord
        Get
            Return _rootDirectory
        End Get
    End Property

    Public ReadOnly Property SystemIdentifier() As String
        Get
            Return _primaryVolumeDescriptor.systemIdentifier.TrimEnd()
        End Get
    End Property

    Public ReadOnly Property VolumeIdentifier() As String
        Get
            Return _primaryVolumeDescriptor.volumeIdentifier.TrimEnd()
        End Get
    End Property

#End Region

#Region " Constructors "

    Shared Sub New()
        imageModesSectorSize = New Dictionary(Of ImageModes, Integer)()
    End Sub

    Public Sub New(ByVal fileName As String, ByVal mode As ImageModes)
        Dim fileInfo As FileInfo = New FileInfo(fileName)
        _fileName = fileName
        _length = fileInfo.Length
        _mode = mode
        _sectorSize = 2352
        _directoryRecords = New Dictionary(Of Integer, DirectoryRecord)()

        If Not GetPrimaryVolumeDescriptor() Then

        End If
    End Sub

#End Region

#Region " Methods "

    Public Function GetDirectoryRecord(ByVal lba As Integer) As DirectoryRecord
        Dim retVal As DirectoryRecord = Nothing

        If Not _directoryRecords.TryGetValue(lba, retVal) Then

        End If

        Return retVal
    End Function

    ' TODO: Allow wildcards inside the filePath
    Public Function GetDirectoryRecord(ByVal filePath As String) As DirectoryRecord
        If Not filePath.Contains("/"c) Then
            Throw New ArgumentException("The path provided doesn't have a valid format")
        End If

        Dim filePathParts As String() = FileName.Split("/"c)
        Dim currentRecord As DirectoryRecord = _rootDirectory

        For i As Integer = 0 To filePathParts.Length - 1
            Dim j As Integer = i    ' To suppress warning...

            currentRecord = currentRecord.GetFiles().Where(Function(x) x.Name = filePathParts(j)).FirstOrDefault()

            If currentRecord Is Nothing Then Exit For
        Next

        Return currentRecord
    End Function

    Private Function GetDirectoryRecord(ByVal binReader As BinaryReader) As DirectoryRecordStruct
        Dim returnDirectory As New DirectoryRecordStruct()
        Dim restBytes As Short

        With returnDirectory
            .len_dr = binReader.ReadByte()
            If .len_dr <> 0 Then
                .XARlength = binReader.ReadByte()
                .lsbStart = binReader.ReadUInt32()
                .msbStart = binReader.ReadUInt32()
                .lsbDataLength = binReader.ReadUInt32()
                .msbDataLength = binReader.ReadUInt32()
                .year = binReader.ReadByte()
                .month = binReader.ReadByte()
                .day = binReader.ReadByte()
                .hour = binReader.ReadByte()
                .minute = binReader.ReadByte()
                .second = binReader.ReadByte()
                .gmtOffset = binReader.ReadByte()
                .fileFlags = binReader.ReadByte()
                .interleaveSize = binReader.ReadByte()
                .interleaveSkip = binReader.ReadByte()
                .lsbVolSetSeqNum = binReader.ReadInt16()
                .msbVolSetSeqNum = binReader.ReadInt16()
                .len_fi = binReader.ReadByte()
                .fi = System.Text.Encoding.Default.GetString(binReader.ReadBytes(.len_fi))
                restBytes = .len_dr - 33 - .len_fi
                If .len_fi Mod 2 = 0 Then
                    binReader.ReadByte()
                    restBytes -= 1
                End If
                If restBytes > 0 Then
                    binReader.ReadBytes(restBytes)
                End If
            End If
        End With

        Return returnDirectory
    End Function

    Private Function GetPrimaryVolumeDescriptor() As Boolean
        Using imageStream As FileStream = New FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.Read, _sectorSize)
            Using binReader As BinaryReader = New BinaryReader(imageStream, System.Text.Encoding.Default)
                imageStream.Seek(HSVOLSTART * _sectorSize + 24, SeekOrigin.Begin)
                With _primaryVolumeDescriptor
                    .VDType = binReader.ReadByte()

                    If .VDType <> StdVolType Then
                        Return False
                    End If

                    .VSStdId = System.Text.Encoding.Default.GetString(binReader.ReadBytes(5))
                    .VSStdVersion = binReader.ReadByte()
                    .Reserved1 = binReader.ReadByte()
                    .systemIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(32))
                    .volumeIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(32))
                    .Reserved2 = System.Text.Encoding.Default.GetString(binReader.ReadBytes(8))
                    .lsbVolumeSpaceSize = binReader.ReadInt32()
                    .msbVolumeSpaceSize = binReader.ReadInt32()
                    .Reserved3 = System.Text.Encoding.Default.GetString(binReader.ReadBytes(32))
                    .lsbVolumeSetSize = binReader.ReadInt16()
                    .msbVolumeSetSize = binReader.ReadInt16()
                    .lsbVolumeSetSequenceNumber = binReader.ReadInt16()
                    .msbVolumeSetSequenceNumber = binReader.ReadInt16()
                    .lsbLogicalBlockSize = binReader.ReadInt16()
                    .msbLogicalBlockSize = binReader.ReadInt16()
                    .lsbPathTableSize = binReader.ReadInt32()
                    .msbPathTableSize = binReader.ReadInt32()
                    .lsbPathTable1 = binReader.ReadInt32()
                    .lsbPathTable2 = binReader.ReadInt32()
                    .msbPathTable1 = binReader.ReadInt32()
                    .msbPathTable2 = binReader.ReadInt32()
                    .rootDirectoryRecord = GetDirectoryRecord(binReader)
                    .volumeSetIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(128))
                    .publisherIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(128))
                    .dataPreparerIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(128))
                    .applicationIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(128))
                    .copyrightFileIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(37))
                    .abstractFileIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(37))
                    .bibliographicFileIdentifier = System.Text.Encoding.Default.GetString(binReader.ReadBytes(37))
                    .volumeCreation = System.Text.Encoding.Default.GetString(binReader.ReadBytes(17))
                    .volumeModification = System.Text.Encoding.Default.GetString(binReader.ReadBytes(17))
                    .volumeExpiration = System.Text.Encoding.Default.GetString(binReader.ReadBytes(17))
                    .volumeEffective = System.Text.Encoding.Default.GetString(binReader.ReadBytes(17))

                    ParsePathTable(.lsbPathTable1)
                    ParseDirectoryRecord(.rootDirectoryRecord.lsbStart)
                End With
            End Using
        End Using

        Return True
    End Function

    Private Function ParseAsciiDateTime(ByVal value As String) As DateTimeOffset?
        If value = MISSING_DATE Then
            Return Nothing
        Else
            Dim offsetQuarters As Integer = Convert.ToSByte(value.Last())

            Return New DateTimeOffset(DateTime.ParseExact(value.Substring(0, _primaryVolumeDescriptor.volumeCreation.Length - 1), _
                                                            "yyyyMMddHHmmssff", Globalization.CultureInfo.InvariantCulture), _
                                        TimeSpan.FromMinutes(offsetQuarters * 15))
        End If
    End Function

    Private Function ParseAsciiDateTimeIfNull(ByVal value As String, <[In](), Out()> ByRef result As DateTimeOffset?) As Boolean
        If Not result.HasValue Then
            result = ParseAsciiDateTime(value)

            Return True
        Else
            Return False
        End If
    End Function

    Friend Sub ParseDirectoryRecord(ByVal lba As Integer)
        Using imageStream As FileStream = New FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.Read, _sectorSize)
            Using binReader As BinaryReader = New BinaryReader(imageStream)
                Dim directoryRecordStruct As DirectoryRecordStruct
                Dim currentDirectory As DirectoryRecord = Nothing
                Dim parentDirectory As DirectoryRecord = Nothing
                Dim directoryRecord As DirectoryRecord

                imageStream.Seek(lba * _sectorSize + 24, SeekOrigin.Begin)

                Do
                    directoryRecordStruct = GetDirectoryRecord(binReader)

                    If directoryRecordStruct.fi <> DOT_DIRECTORY Then
                        'Let's try get the current directory record
                        _directoryRecords.TryGetValue(directoryRecordStruct.lsbStart, currentDirectory)

                    ElseIf directoryRecordStruct.fi = PARENT_DIRECTORY Then
                        'Let's try get the parent directory record
                        _directoryRecords.TryGetValue(directoryRecordStruct.lsbStart, parentDirectory)
                    Else

                        If directoryRecordStruct.len_dr <> 0 Then
                            directoryRecord = New DirectoryRecord(directoryRecordStruct)

                            If directoryRecord.IsDirectory Then
                                'TODO: Set parent directory
                            Else
                                'TODO: Set current directory as parent
                            End If

                            _directoryRecords(directoryRecord.LBA) = directoryRecord
                        End If

                    End If

                Loop While directoryRecordStruct.len_dr <> 0
            End Using
        End Using
    End Sub

    Private Sub ParsePathTable(ByVal lba As Integer)

    End Sub

#End Region

End Class
