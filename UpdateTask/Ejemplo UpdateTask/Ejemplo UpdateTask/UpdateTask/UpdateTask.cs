using System;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace UpdateTask
{
    public sealed class UpdateTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            ShowNotification();
            UpdateTileStatus();
        }

        /// <summary>
        /// Muestra una notificación Toast para notificar al usuario de la nueva versión.
        /// </summary>
        private void ShowNotification()
        {
            var toastnotifier = ToastNotificationManager.CreateToastNotifier();

            var toastDescriptor = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            var txtNodes = toastDescriptor.GetElementsByTagName("text");

            txtNodes[0].AppendChild(toastDescriptor.CreateTextNode("Nueva versión!"));
            txtNodes[1].AppendChild(toastDescriptor.CreateTextNode(string.Format("Actualizada : {0}", DateTime.Now)));

            var toast = new ToastNotification(toastDescriptor);

            toastnotifier.Show(toast);
        }

        /// <summary>
        /// Actualiza el Tile de la aplicación notificando de la nueva versión.
        /// 
        /// Catálogo de plantillas de Tiles: http://msdn.microsoft.com/es-es/library/windows/apps/hh761491.aspx
        /// </summary>
        private void UpdateTileStatus()
        {
            var tileContent = TileUpdateManager.GetTemplateContent(
              TileTemplateType.TileSquare150x150Text01);

            var tileText = tileContent.SelectNodes("tile/visual/binding/text");

            tileText[0].InnerText = "Nueva versión!";
            tileText[1].InnerText = "Actualizada";
            tileText[2].InnerText = DateTime.Now.ToString();

            var notification = new TileNotification(tileContent);
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();

            updater.Update(notification);
        }
    }
}
