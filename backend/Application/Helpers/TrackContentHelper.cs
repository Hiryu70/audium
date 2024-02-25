using Audium.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audium.Application.Helpers;

public static class TrackContentHelper
{
    /// <summary>
    /// На основании метаданных трека возвращает его контент в виде массива байтов
    /// </summary>
    /// <param name="track">track object</param>
    /// <returns>трека в виде массива байтов</returns>
    public static byte[] GetBinaryContentForTrack(Track track)
    {
        var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\", track.StorageFileName));
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var binaryReader = new BinaryReader(fileStream);
        long numBytes = new FileInfo(filePath).Length;
        return binaryReader.ReadBytes((int)numBytes);
    }
}
