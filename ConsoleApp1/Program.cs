using System.Diagnostics;
using FFMpegCore;
using FFMpegCore.Pipes;

// Set ffmpeg 
GlobalFFOptions.Configure(
    options => options.BinaryFolder = "binaries"
);
// Make executable
using var process = Process.Start("/bin/bash", $"-c \"chmod -R 777 binaries\"");
process.WaitForExit();

try
{
    using var outputFile = File.Create(Path.Combine("media", "Power Slam.mp3"));
    FFMpegArguments
        .FromUrlInput(new Uri("https://github.com/goffincedric/FFMpegCore-dotnet8-replicate-segfault/raw/main/ConsoleApp1/media/Power%20Slam.webm"))
        .OutputToPipe(new StreamPipeSink(outputFile), options => options.ForceFormat("mp3"))
        .ProcessSynchronously();
    Console.WriteLine("Success");
}
catch (Exception e)
{
    Console.WriteLine("Fail" + Environment.NewLine + e);
}
Console.ReadKey();