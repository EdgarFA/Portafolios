using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using Helper;

namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [Column(TypeName = "text")]
        public string Direccion { get; set; }

        [StringLength(50)]
        public string Ciudad { get; set; }

        public int? Pais_id { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }

        [StringLength(100)]
        public string Facebook { get; set; }

        [StringLength(100)]
        public string Twitter { get; set; }

        [StringLength(100)]
        public string YouTube { get; set; }

        [StringLength(50)]
        public string Foto { get; set; }

        public ResponseModel Acceder(string Email, string Password)
        {
            var rm = new ResponseModel();

            try
            {
                using (var ctx = new PortafolioContext())
                {
                    string hashPassword = HashHelper.MD5(Password);
                    var usuario = ctx.Usuario
                        .Where(x => x.Email == Email).FirstOrDefault(x => x.Password == hashPassword);

                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(usuario.id.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Correo o contraseña incorrecta");
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return rm;
        }

        public Usuario Obtener(int id)
        {
            var usuario = new Usuario();
            try
            {
                using (var ctx = new PortafolioContext())
                {
                    usuario = ctx.Usuario.FirstOrDefault(x => x.id == id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return usuario;
        }

        public ResponseModel Guardar(HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new PortafolioContext())
                {
                    ctx.Configuration.ValidateOnSaveEnabled = false;

                    var eUsuario = ctx.Entry(this);
                    eUsuario.State  = EntityState.Modified;

                    if (Foto != null)
                    {
                        string archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(Foto.FileName);
                        Foto.SaveAs(HttpContext.Current.Server.MapPath("~/uploads/"+ archivo));

                        this.Foto = archivo;
                    }
                    else
                    {
                        eUsuario.Property(x => x.Foto).IsModified = false;
                    }

                    if (this.Password == null)
                    {
                        eUsuario.Property(x => x.Password).IsModified = false;
                    }

                    ctx.SaveChanges();

                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                rm.SetResponse(false);
                throw;
            }

            return rm;
        }
    }
}
