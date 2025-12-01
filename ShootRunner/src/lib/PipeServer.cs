using System.IO.Pipes;
using System.Text;

#nullable disable

#pragma warning disable IDE0079
#pragma warning disable IDE0130
#pragma warning disable CA2211

namespace ShootRunner
{
    public class PipeServer
    {

        public delegate void MessageReceivedCallback(string message);

        public static event Action<string> MessageReceived;

        public static string PipeName = null;

        public static NamedPipeServerStream pipeServer = null;

        public static void SetPipeName(string name) {
            PipeName = name;
        }

        public static bool StartServerAsync()
        {
            

            if (pipeServer != null || PipeName == null) {
                return true;
            }

            try
            {
                pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Message, PipeOptions.Asynchronous);

                Task.Run(async () =>
                {
                    await StartServer();
                });

                return true;
            }
            catch (Exception)
            {
                pipeServer = null;
            }

            return false;
        }

        public static async System.Threading.Tasks.Task StartServer()
        {
            if (pipeServer == null)
            {
                return;
            }

            try
            {
                while (true)
                {
                    

                    await pipeServer.WaitForConnectionAsync();

                    _ = HandleClientAsync();
                }
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }
        }

        public static async System.Threading.Tasks.Task HandleClientAsync()
        {

            if (pipeServer == null)
            {
                return;
            }

            try
            {
                using (pipeServer) // Ensure resources are freed when done
                {
                    var buffer = new byte[1024];
                    int bytesRead;

                    // Continuously read from the pipe until the client disconnects
                    while ((bytesRead = await pipeServer.ReadAsync(buffer)) > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        MessageReceived?.Invoke(message);
                    }
                }
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }
        }

        public static void SendMessageAsync(string message)
        {
            if (PipeName == null)
            {
                return;

            }

            Task.Run(async () =>
            {
                await PipeServer.SendMessage(message);

            });
        }

        public static async System.Threading.Tasks.Task SendMessage(string message)
        {
            if (PipeName == null)
            {
                return;

            }

            try
            {
                using var pipeClient = new NamedPipeClientStream(".", PipeName, PipeDirection.Out, PipeOptions.Asynchronous);
                await pipeClient.ConnectAsync();
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                await pipeClient.WriteAsync(messageBytes);
            }
            catch (Exception ex)
            {

                Program.Error(ex.Message);
            }

            
        }
    }
}
