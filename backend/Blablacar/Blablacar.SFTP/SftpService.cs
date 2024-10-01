using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Models;
using Blablacar.SFTP.Exceptions;
using Blablacar.SFTP.Extensions;
using Renci.SshNet.Abstractions.Sftp;
using ISftpClient = Renci.SshNet.Abstractions.Sftp.ISftpClient;

namespace Blablacar.SFTP;

/// <inheritdoc />
public class SftpService(
    SftpOptions sftpOptions,
    ISftpClientFactory factory) : ISftpService
{
    /// <inheritdoc />
    public async Task<string> UploadAsync(
        FileContent fileContent, 
        bool needAutoCloseStream = true,
        CancellationToken cancellationToken = default)
    {
        if (fileContent.FileName == null)
            throw new ArgumentNullException(nameof(fileContent.FileName));

        if (fileContent.Content == null)
            throw new ArgumentNullException(nameof(fileContent.Content));
        
        var client = CreateClient();

        try
        {
            client.Connect();
        }
        catch (Exception ex)
        {
            throw new ConnectionFailedException(ex.Message);
        }

        var guid = Guid.NewGuid().ToString();

        await client.UploadFileAsync(
            fileContent.Content, 
            guid + fileContent.FileName, 
            cancellationToken: cancellationToken);
        
        client.Disconnect();
        client.Dispose();
        return guid + fileContent.FileName;
    }

    /// <inheritdoc />
    public async Task<FileContent?> DownloadFileAsync(
        string address, 
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException(nameof(address));
        
        var client = CreateClient();
        
        try
        {
            client.Connect();
        }
        catch (Exception ex)
        {
            throw new ConnectionFailedException(ex.Message);
        }

        var stream = new MemoryStream();
        try
        {
            await client.DownloadFileAsync(address, stream, cancellationToken: cancellationToken);

            stream.Seek(0, SeekOrigin.Begin);

            client.Disconnect();
            client.Dispose();
        }
        catch 
        {
            return new FileContent(
                stream,
                Path.GetFileName(address),
                "application/octet-stream"
            );
        }
        
        return new FileContent(
            stream,
            Path.GetFileName(address),
            "application/octet-stream"
        );

        
    }

    private ISftpClient CreateClient() =>
        factory.CreateClient(sftpOptions.Host, sftpOptions.Port, sftpOptions.User, sftpOptions.Password);
}