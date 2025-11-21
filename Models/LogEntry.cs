using System.Windows.Media;

namespace StudioForge.Models
{
    public class LogEntry
    {
        public string Time { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public Brush Color { get; set; } = Brushes.White;
    }
}
