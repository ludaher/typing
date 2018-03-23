using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Log
{
    public static class Logger
    {
        public static void Info(string message)
        {
            Trace.Write(message);
            //await Task.Run(() => logger.Info("{0} - {1} - API - {2}: \r\n{3}", correlationId, requestInfo, type, body));
        }
        public static void Error(string message)
        {
            Trace.Write(message);
            //await Task.Run(() => logger.Info("{0} - {1} - API - {2}: \r\n{3}", correlationId, requestInfo, type, body));
        }
        public static void Error(string message, Exception ex)
        {
            Trace.Write(message, ex.ToString());
            //await Task.Run(() => logger.Info("{0} - {1} - API - {2}: \r\n{3}", correlationId, requestInfo, type, body));
        }
        public static void Warning(string message)
        {
            Trace.Write(message);
            //await Task.Run(() => logger.Info("{0} - {1} - API - {2}: \r\n{3}", correlationId, requestInfo, type, body));
        }
        public static void Warning(string message, Exception ex)
        {
            Trace.Write(message);
            //await Task.Run(() => logger.Info("{0} - {1} - API - {2}: \r\n{3}", correlationId, requestInfo, type, body));
        }
        public static void Debug(string message)
        {
            Trace.Write(message);
            //await Task.Run(() => logger.Info("{0} - {1} - API - {2}: \r\n{3}", correlationId, requestInfo, type, body));
        }

        public static async Task InfoAsync(string message)
        {
            await Task.Run(() => Trace.Write(message));
            //await Task.Run(() => logger.Info("{0} - {1} - API - {2}: \r\n{3}", correlationId, requestInfo, type, body));
        }
        public static async Task ErrorAsync(string message)
        {
            await Task.Run(() => Trace.Write(message));
        }
        public static async Task ErrorAsync(string message, Exception ex)
        {
            await Task.Run(() => Trace.Write(message, ex.ToString()));
        }
        public static async Task WarningAsync(string message)
        {
            await Task.Run(() => Trace.Write(message));
        }
        public static async Task WarningAsync(string message, Exception ex)
        {
            await Task.Run(() => Trace.Write(message, ex.ToString()));
        }
        public static async Task DebugAsync(string message)
        {
            await Task.Run(() => Trace.Write(message));
        }
    }
}
