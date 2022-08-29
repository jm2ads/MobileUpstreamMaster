using Frontend.Business.Attributes;
using Frontend.Business.Centros;
using Frontend.Business.Commons;
using Frontend.Business.Usuarios;
using Frontend.Commons.Commons;
using SQLiteNetExtensions.Attributes;
using System;

namespace Frontend.Business.Settings
{
    [IgnoreDbReset]
    public class Setting : PersistebleEntity
    {
        [ForeignKey(typeof(Usuario))]
        public int UsuarioActivoId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Usuario UsuarioActivo { get; set; }

        [ForeignKey(typeof(Centro))]
        public int CentroActivoId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Centro CentroActivo { get; set; }

        public string ApplicationName { get; set; }

        public DateTime LastSync { get; set; }

        public DateTime LastSolicitudesSync { get; set; }

        public string VersionNumber { get; set; }

        public string BuildNumber { get; set; }

        public bool HasSyncWithError { get; set; }

        public bool IsPendingToSync { get; set; }

        public Setting()
        {
            SetCommonProperties();
        }

        private void SetCommonProperties()
        {
            ApplicationName = ApplicationConstants.ApplicationName;
            LastSync = ApplicationConstants.DefaultDateSync;
            LastSolicitudesSync = ApplicationConstants.DefaultDateSync;
        }
    }
}
