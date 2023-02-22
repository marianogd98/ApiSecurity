using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WebApiPosRio.Models.Common;
using WebApiPosRio.Models.DB;
using WebApiPosRio.Models.Helper;
using WebApiPosRio.Models.Response;
using WebApiPosRio.Models.ViewModels;

namespace WebApiPosRio.Models.Repository
{
    public class UsuarioRepository : RIOPOSContext, IUsuarioRepository
    {
        private readonly ISpRepository _spAcciones;
        private readonly IAccionRepository _Acciones;
        private readonly ISpRepository _SpRepo;
        private readonly ITiendaRepository _tiendaRepository;

        private readonly AppSettings _appSettings;
        public UsuarioRepository(ISpRepository accionesSp, IAccionRepository accionesR, ISpRepository spRepo, ITiendaRepository tiendaRepository, IOptions<AppSettings> appSettings)
        {
            _spAcciones = accionesSp;
            _Acciones = accionesR;
            _SpRepo = spRepo;
            _tiendaRepository = tiendaRepository;
            _appSettings = appSettings.Value;
        }

        public List<UsuarioConfViewModel> GetUsuarios()
        {   //estatus{ 1:Activo, 0: inactivo, -1:eliminado }
            List<UsuarioConfViewModel> lstUser = (from d in Usuarios
                                                  where (d.Id > 0) && (d.Estatus >= 0)
                                                  orderby d.Apellido ascending
                                                  select new UsuarioConfViewModel
                                                  {
                                                      Id = d.Id,
                                                      Nick = d.Nick,
                                                      Cedula = d.Cedula,
                                                      Nombre = d.Nombre,
                                                      Apellido = d.Apellido,
                                                      Password = "",//d.Password,
                                                      Estatus = d.Estatus,
                                                      Sucursal = d.TiendaId,
                                                      Tipo = d.Tipo,
                                                      RolId = d.RolId,
                                                      CreatedAt = d.CreatedAt.ToString(),
                                                      UpdatedAt = d.UpdatedAt.ToString(),
                                                      Acciones = null
                                                  }).ToList();

            return lstUser;
        }

        public IEnumerable<UsuarioConfViewModel> GetUsuarios(int page, int perPage, Userfiltro filtro, int Tipo = 3)
        {
            //int Tipos = Tipo==0?
            //estatus{ 1:Activo, 0: inactivo, -1:eliminado }
            List<UsuarioConfViewModel> lstUser = (from d in Usuarios
                                                  where (d.Id > 0) && ((d.Tipo == Tipo || Tipo==-1) || (Tipo != 3)) && (d.Estatus >= 0 && d.Estatus == filtro.Estatus)
                                                  orderby d.Apellido ascending
                                                  select new UsuarioConfViewModel
                                                  {
                                                      Id = d.Id,
                                                      Nick = d.Nick,
                                                      Cedula = d.Cedula,
                                                      Nombre = d.Nombre,
                                                      Apellido = d.Apellido,
                                                      Password = "",//d.Password,
                                                      Estatus = d.Estatus,
                                                      Sucursal = d.TiendaId,
                                                      Tipo = d.Tipo,
                                                      RolId = d.RolId,
                                                      CreatedAt = d.CreatedAt.ToString(),
                                                      UpdatedAt = d.UpdatedAt.ToString(),
                                                      Acciones = null
                                                  }).ToList();

            if (Tipo !=3)
            {
                if (Tipo == -1)
                    lstUser = lstUser.Where(lu => lu.Tipo > 0 && lu.Tipo != 3).ToList();
                else
                    lstUser = lstUser.Where(lu => lu.Tipo == Tipo ).ToList();
            }

            if (filtro.NombreUser != null)
            {
                lstUser = lstUser.Where(nu => nu.Nombre.ToUpper().Contains(filtro.NombreUser.ToUpper()) || nu.Apellido.ToUpper().Contains(filtro.NombreUser.ToUpper())).ToList();
            }

            if (filtro.Nick != null)
            {
                lstUser = lstUser.Where(nu => nu.Nick.ToUpper().Contains(filtro.Nick.ToUpper())).ToList();
            }

            if (filtro.Cedula != null)
            {
                lstUser = lstUser.Where(nu => nu.Cedula.Contains(filtro.Cedula, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (filtro.RolId > 0)
            {
                lstUser = lstUser.Where(nu => nu.RolId == filtro.RolId).ToList();
            }

            return lstUser;
        }

        public int getIdUserbyName(string nombreCajera)
        {
            var objUser = (from u in Usuarios
                                            where (u.Id > 0) && (u.Estatus >= 0) && (u.Tipo>2) && (u.RolId>2) && (u.Nombre.ToUpper().Contains(nombreCajera) || u.Apellido.ToUpper().Contains(nombreCajera))
                                            select new 
                                            {
                                                Id = u.Id
                                            }).FirstOrDefault();

            return objUser!=null? objUser.Id:-1;
        }

        public ResponseData GetUserTurno(int turnoId)
        {
            UsuarioConfViewModel lstUser = (from u in Usuarios
                                            join t in Turnos
                                            on u.Id equals t.UsuarioId
                                            where (u.Id > 0) && (u.Estatus >= 0) && (t.Id == turnoId)                                                  
                                                  select new UsuarioConfViewModel
                                                  {
                                                      Id = u.Id,
                                                      Nick = u.Nick,
                                                      //Cedula = u.Cedula,
                                                      Nombre = u.Nombre,
                                                      Apellido = u.Apellido,
                                                      Password = "",//d.Password,
                                                      Estatus = u.Estatus,
                                                      Sucursal = u.TiendaId,
                                                      Tipo = u.Tipo,
                                                      RolId = u.RolId,
                                                      CreatedAt = u.CreatedAt.ToString(),
                                                      UpdatedAt = u.UpdatedAt.ToString(),
                                                      Acciones = null
                                                  }).FirstOrDefault();

            return new ResponseData { 
                Success = 1,
                Data = lstUser,
                Message = "Usuario encontrado!!"
            };
        }

        //Roles
        public List<RolConfViewModel> GetRols()
        {
            List<RolConfViewModel> lstRol = (from d in Rols where d.Id>0 orderby d.Descripcion ascending
                                             select new RolConfViewModel
                                             {
                                                 Id = d.Id,
                                                 Descripcion = d.Descripcion,
                                                 Activo = d.Activo,
                                                 CreatedAt = d.CreatedAt.ToString(),
                                                 UpdatedAt = d.UpdatedAt.ToString(),
                                                 Selected = false
                                             }).ToList();

            return lstRol;
        }

        //Modulos
        public List<ModuloConfViewModel> GetModulos()
        {
            List<ModuloConfViewModel> lstMod = (from d in Modulos                                             
                                             orderby d.Descripcion ascending
                                             select new ModuloConfViewModel
                                             {
                                                 Id = d.Id,
                                                 Descripcion = d.Descripcion,                                                 
                                                 CreatedAt = d.CreatedAt.ToString(),
                                                 UpdatedAt = d.UpdatedAt.ToString(),
                                                 Selected = false,
                                                 Expand = false                                                 
                                             }).ToList();
            
            foreach (ModuloConfViewModel itemMod in lstMod)
           {
                List<AccionConfViewModel> lstActions = (from a in Accions
                                                        where a.ModuloId == itemMod.Id
                                                        orderby a.Descripcion ascending
                                                        select new AccionConfViewModel
                                                        {
                                                            Id = a.Id,
                                                            ModuloId = a.ModuloId,
                                                            Descripcion = a.Descripcion,
                                                            CreatedAt = a.CreatedAt.ToString(),
                                                            UpdatedAt = a.UpdatedAt.ToString(),
                                                            Selected = true,
                                                            Enable = true
                                                        }).ToList();
                if (lstActions != null)
                {                     
                    itemMod.Acciones = lstActions;
                }
            }
            return lstMod;
        }

       public bool BuscarEle(AccionRolVieModel[] e, int idAccion)
        {
            foreach(var item in e)
            {
                return !(item.IdAccion == idAccion);
            }

            return false;
        }

        public List<ModuloConfViewModel> GetModulosAccionByRol(int rolId)
        {
            List<ModuloConfViewModel> lstMod = (from d in Modulos
                                                orderby d.Descripcion ascending
                                                select new ModuloConfViewModel
                                                {
                                                    Id = d.Id,
                                                    Descripcion = d.Descripcion,
                                                    CreatedAt = d.CreatedAt.ToString(),
                                                    UpdatedAt = d.UpdatedAt.ToString(),
                                                    Selected = false,
                                                    Expand = false
                                                }).ToList();

            List<AccionRolVieModel> _enable = (from ra in RolAccions
                                           where ra.RolId == rolId
                                               select new AccionRolVieModel
                                           {
                                               Enable = false,
                                               IdAccion = ra.AccionId
                                           }).ToList();

            foreach (ModuloConfViewModel itemMod in lstMod)
            {
                List<AccionConfViewModel> lstActions = (from a in Accions
                                                        where a.ModuloId == itemMod.Id
                                                        orderby a.Descripcion ascending
                                                        select new AccionConfViewModel
                                                        {
                                                            Id = a.Id,
                                                            ModuloId = a.ModuloId,
                                                            Descripcion = a.Descripcion,
                                                            CreatedAt = a.CreatedAt.ToString(),
                                                            UpdatedAt = a.UpdatedAt.ToString(),
                                                            Selected = true,
                                                            Enable = true
                                                        }).ToList();

                lstActions.ForEach(i => {
                    var result = _enable.FirstOrDefault(en => en.IdAccion == i.Id);
                    if (result != null)
                    {
                        itemMod.Expand = true;
                        i.Enable = result.Enable;
                    }
                    else {
                        i.Selected = false;
                        itemMod.Expand = false;
                    }
                    });

                if (lstActions != null)
                {                    
                    itemMod.Acciones = lstActions;
                    itemMod.Expand = true;
                }
            }
            return lstMod;
        }

        public XmlDocument CreateXmlSaveActions(int idUser, UsuarioConfViewModel usuarioViewModel)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Root");
            XmlElement eAcciones = doc.CreateElement("Acciones");
            eAcciones.SetAttribute("UsuarioId", idUser.ToString());
            //id.SetAttribute("passWord", "Tushar");
            usuarioViewModel.Acciones.ForEach( a => {
                XmlElement accion = doc.CreateElement("Accion");
                accion.SetAttribute("Id", a.Id.ToString());
                eAcciones.AppendChild(accion);
            });

            //eAcciones.AppendChild(age);
            root.AppendChild(eAcciones);
            doc.AppendChild(root);

            return doc;
        }

        public bool existeUser(string Nick, string Cedula)
        {
            var objUser = (from u in Usuarios
                           where ((u.Id > 0) && (u.Estatus >= 0)) && (u.Nick.ToUpper().Contains(Nick) || u.Cedula.Contains(Cedula))
                           select new
                           {
                               Id = u.Id
                           }).FirstOrDefault();

            return objUser != null;
        }

        public ResponseData AddUsuario(UsuarioConfViewModel usuarioViewModel)
        {
            ResponseData oR = new ResponseData();
            try
            {
                bool existe = existeUser(usuarioViewModel.Nick, usuarioViewModel.Cedula);
                if (!existe) {
                    var usuarioData = new Usuario
                    {
                        Nick = usuarioViewModel.Nick,
                        Password = Util.Encrypt(usuarioViewModel.Password),
                        Nombre = usuarioViewModel.Nombre.ToUpper(),
                        Apellido = usuarioViewModel.Apellido.ToUpper(),
                        Cedula = usuarioViewModel.Cedula.ToUpper(),
                        Estatus = usuarioViewModel.Estatus,
                        Tipo = usuarioViewModel.Tipo,
                        RolId = usuarioViewModel.RolId,
                        TiendaId = usuarioViewModel.Sucursal,
                        RecordarToken = Util.Encrypt(usuarioViewModel.Nick)
                    };

                    Usuarios.Add(usuarioData);
                    this.SaveChanges();
                    if (usuarioViewModel.RolId != 0)
                    {
                        if (usuarioViewModel.Acciones.Count > 0)
                        {
                            XmlDocument xml = CreateXmlSaveActions(usuarioData.Id, usuarioViewModel);
                            _spAcciones.SetAccionesUsuario(xml);
                        }
                    }
                    oR.Success = 1;
                    //oR.Data = GetModulos();
                    oR.Message = "Datos Almacenados Satisfactoriamente";
                }
                else
                {
                    oR.Success = 0;
                    oR.Message = "Usuario ya existe!!, -- Verifique Cédula y Nick --";
                }
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData UpdateUser(UsuarioConfViewModel usuarioViewModel, int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                Usuario rolData;

                if (usuarioViewModel.Password == "") {
                    rolData = new Usuario
                    {
                        Id = Id,
                        Nick = usuarioViewModel.Nick,
                        //Password = Util.ComputeSha256Hash(usuarioViewModel.Password),
                        Nombre = usuarioViewModel.Nombre.ToUpper(),
                        Apellido = usuarioViewModel.Apellido.ToUpper(),
                        Cedula = usuarioViewModel.Cedula.ToUpper(),
                        Estatus = usuarioViewModel.Estatus,
                        Tipo = usuarioViewModel.Tipo,
                        RolId = usuarioViewModel.RolId,
                        TiendaId = usuarioViewModel.Sucursal,
                        RecordarToken = Util.Encrypt(usuarioViewModel.Nick)
                    };
                }
                else
                {
                    rolData = new Usuario
                    {
                        Id = Id,
                        Nick = usuarioViewModel.Nick,
                        Password = Util.Encrypt(usuarioViewModel.Password),
                        Nombre = usuarioViewModel.Nombre,
                        Apellido = usuarioViewModel.Apellido,
                        Cedula = usuarioViewModel.Cedula,
                        Estatus = usuarioViewModel.Estatus,
                        Tipo = usuarioViewModel.Tipo,
                        RolId = usuarioViewModel.RolId,
                        TiendaId = usuarioViewModel.Sucursal,
                        RecordarToken = Util.Encrypt(usuarioViewModel.Nick)
                    };
                }

                Usuarios.Attach(rolData);
                Entry(rolData).Property(u => u.Nick).IsModified = true;
                if (usuarioViewModel.Password != "")
                {
                    Entry(rolData).Property(u => u.Password).IsModified = true;
                }
                Entry(rolData).Property(u => u.Nombre).IsModified = true;
                Entry(rolData).Property(u => u.Apellido).IsModified = true;
                Entry(rolData).Property(u => u.Cedula).IsModified = true;
                Entry(rolData).Property(u => u.Tipo).IsModified = true;
                Entry(rolData).Property(u => u.Estatus).IsModified = true;
                Entry(rolData).Property(u => u.RolId).IsModified = true;
                Entry(rolData).Property(u => u.TiendaId).IsModified = true;
                Entry(rolData).Property(u => u.RecordarToken).IsModified = true;
                //Entry(rolData).Property(u => u.Estatus).IsModified = true;

                this.SaveChanges();
                oR.Success = 1;
                oR.Message = "Datos Actualizados Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData DeleteUser(int Id)
        {
            ResponseData oR = new ResponseData();
            try
            {
                Usuario rolData = new Usuario
                    {
                        Id = Id,
                        Estatus = -1
                    };

                Usuarios.Attach(rolData);
                Entry(rolData).Property(u => u.Estatus).IsModified = true;
                this.SaveChanges();
                oR.Success = 1;
                oR.Message = "Dato Eliminado Satisfactoriamente";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }


        /*Login para Pos*/
        public ResponseData Login(LoginSecurityViewModel user)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var userSelect = (from u in Usuarios
                                  where (u.Nick == user.Username && u.Password==Util.Encrypt(user.Password) && u.Estatus > -1)
                                  select new UserData
                                  {
                                      Id = u.Id,
                                      Nombre = u.Nombre+ "-"+u.Apellido,
                                      Cedula = u.Cedula,
                                      Estatus = u.Estatus
                                  }).FirstOrDefault();

                if (userSelect == null) { 
                    oR.Data = null; 
                    oR.Message = "Error: Verifique usuario y contraseña"; 
                    oR.Success = 0; 
                    return oR; 
                }

                var na = userSelect.Nombre.Trim().Split('-');
                if (na.Length > 0 )
                {
                    string[] cn = na[0].Trim().Split(' ');
                    string[] ca = na[1].Trim().Split(' ');
                    userSelect.Nombre = cn[0] + " " + ca[0];
                }
                //if (user.Username.Contains("supervisa1")) { oR.Data = null; oR.Message = "Error: Este Usuario no puede ser usado para Efectuar operaciones de Facturación"; oR.Success = 0; return oR; }

                if (userSelect.Estatus==0) { oR.Data = null; oR.Message = "Error: Usuario inhabilitado"; oR.Success = 0; return oR; }

                SpPruebaData dataTurno =  _SpRepo.VerificarTurno(userSelect.Id, Int32.Parse(user.IdCaja));

                if (dataTurno != null)
                {
                    if (dataTurno.Id ==0)//si el turno es estatus 1
                    {
                        oR.Data = null; oR.Message = (dataTurno.Estatus == 2) ? "Error: Caja en Uso y tiene turno Abierto" : "Error: Usuario ya posee un turno abierto en otra Caja";oR.Success = 0; return oR;
                    }

                    userSelect.IdTurno = dataTurno.Id;
                }
                
                oR.Success = 1;
                oR.Data = userSelect;
                oR.Message = "Usuario Encontrado!!";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        /*Login para sistema administrativo*/
        public ResponseData Login(AuthRequest user)
        {
            ResponseData oR = new ResponseData {
                Success = 0,
                Data = null,
                Message = ""
            };
            try
            {
                string pwd = Util.Encrypt(user.Password);
                var userSelect = (from u in Usuarios
                                  where (u.Nick == user.Username && u.Password == pwd && u.Estatus > -1 && (u.Tipo <= 2 || u.Tipo==21 || u.Tipo == 22 || u.Tipo == 11 || u.Tipo == 10 || (u.Tipo >= 500 && u.Tipo <= 599) || (u.Tipo >= 400 && u.Tipo<=499) || (u.Tipo >= 100 && u.Tipo <= 199) || u.Tipo == 4 || u.Tipo == 5))
                                  select new UserDataAdm
                                  {
                                      Id = u.Id,
                                      Nick = user.Username,
                                      Nombre = u.Nombre+" "+u.Apellido,
                                      Estatus = u.Estatus,
                                      Sucursal = u.TiendaId,                                     
                                      Lxvl = u.Tipo,
                                  }).FirstOrDefault();
                if (userSelect == null) {oR.Message = "Error: Verifique usuario y contraseña"; oR.Success = 0; return oR; }

                if (userSelect.Estatus == 0) {oR.Message = "Error: Usuario inhabilitado"; oR.Success = 0; return oR; }

                if (userSelect.Estatus == 2) { oR.Message = "Error: Usuario Se encuentra Logueado"; oR.Success = 0; return oR; }

                bool esOnline = true;// (userSelect.Id==0)?true: UpdateEstatusOnline(1);
                if (esOnline) {
                    userSelect.Token = GetToken(userSelect);
                    oR.Success = 1;
                    oR.Data = userSelect;
                    oR.Message = "Usuario Encontrado!!";
                }
                else
                {
                    oR.Success = 0;
                    oR.Message = "POr favor comuniquese con soporte Tecnico el usuario no puede habilitar la sesion";
                }
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        private  string GetToken(UserDataAdm userAct)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, userAct.Id.ToString()),
                            new Claim(ClaimTypes.Name, userAct.Nick),
                        }
                    ),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        } 

        private bool UpdateEstatusOnline(int Id)
        {
            try
            {
                Usuario userData = new Usuario
                {
                    Id = Id,
                    Estatus = 2
                };

               /* var entity = Usuarios.Attach(userData);
                Entry(userData).State = EntityState.Modified;
                this.SaveChanges();*/
                

                Usuarios.Attach(userData);
                Entry(userData).Property(u => u.Estatus).IsModified = true;
                Usuarios.Update(userData);
                this.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                string s = ex.Message.ToString();
                return false;
            }
        }
        public ResponseData GetProfileUser(int idUser)
        {
            ResponseData oR = new ResponseData();
            try
            {
                UserPerfil userSelect = (from u in Usuarios
                                  join r in Rols
                                  on u.RolId equals r.Id
                                  where (u.Id == idUser)
                                  select new UserPerfil
                                  {                                     
                                      Nombre = u.Nombre+" "+u.Apellido,                                     
                                      Acciones = new object(),
                                      RolId = u.RolId,
                                      RolUser = r.Descripcion,
                                      IdTienda = u.TiendaId,
                                      Estatus = u.Estatus==1?true:false
                                  }).FirstOrDefault();
                if (userSelect != null)
                {
                    if (!userSelect.Estatus) { oR.Data = null; oR.Message = "Error: Usuario inhabilitado"; oR.Success = 0; return oR; }
                    else
                    {
                         var accionesUser =
                             (from ua in UsuarioAccions
                              join a in Accions
                              on ua.AccionId equals a.Id
                              where ua.UsuarioId == idUser
                              select new
                              {
                                  IdAccion = ua.AccionId,
                                  Nombre = a.Descripcion,
                                  Permitido = true
                              })
                             .Union
                                 (from ra in RolAccions
                                  join a in Accions
                                  on ra.AccionId equals a.Id
                                  where ra.RolId == userSelect.RolId
                                  select new
                                  {
                                      IdAccion = ra.AccionId,
                                      Nombre = a.Descripcion,
                                      Permitido = true
                                  });


                        userSelect.Acciones = accionesUser;
                    }
                }else
                { oR.Data = null; oR.Message = "Error: Usuario No encontrado"; oR.Success = 0; return oR; }

                
                oR.Success = 1;
                oR.Data = userSelect;
                oR.Message = "Usuario Encontrado!!";
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public ResponseData GetPermisionUser(int idUser, string item)
        {
            ResponseData oR = new ResponseData();            

            if (Util.EsNumero(item))
            {
                oR.Data = (from pu in base.UsuarioAccions
                               join a in Accions
                               on pu.AccionId equals a.Id
                               where (pu.UsuarioId == idUser && a.Id == Int32.Parse(item))
                               select (new
                               {
                                   Permitido = true
                               })
                               ).FirstOrDefault();
            }
            else {
                oR.Data = (from pu in base.UsuarioAccions
                                   join a in Accions
                                   on pu.AccionId equals a.Id
                                   where (pu.UsuarioId == idUser && a.Descripcion.Contains(item))
                                   select (new
                                   {
                                       Permitido = true
                                   })
                                ).FirstOrDefault();
            }
           
            oR.Message = (oR.Data != null)?"El Usuario tiene permiso para la acción":"No tiene permiso para esta acción";
            oR.Success = (oR.Data != null)?1:0;

            return oR;
        }

        public ResponseData GetPermissionLogin(UserPermissionViewModel user)
        {
            ResponseData oR = new ResponseData();
            try
            {
                var userSelect = (from u in Usuarios
                                  where (u.Nick == user.Username && u.Password == Util.Encrypt(user.Password))
                                  select new UserData
                                  {
                                      Id = u.Id,
                                      Nombre = u.Nombre,
                                      Estatus = u.Estatus
                                  }).FirstOrDefault();
                if (userSelect == null) { oR.Data = null; oR.Message = "Error: Vefifique usuario y contraseña"; oR.Success = 0; return oR; }

                if (userSelect.Estatus == 0) { oR.Data = null; oR.Message = "Error: Usuario inhabilitado"; oR.Success = 0; return oR; }
                oR.Data = new { User= userSelect, AccionPermitida =_Acciones.GetAccionsProfile(userSelect.Id, user.Accion) };
                oR.Message = (oR.Data != null) ? "El Usuario tiene permiso para la acción" : "No tiene permiso para esta acción";
                oR.Success = (oR.Data != null) ? 1 : 0;
            }
            catch (Exception ex)
            {
                oR.Success = 0;
                oR.Message = ex.Message;
            }
            return oR;
        }

        public bool CargaDataExcelUser(DataMasivaUser dataMasivaUser)
        {
            try
            {
                List<Usuario> DataUserSave = new List<Usuario>();
                var elem = dataMasivaUser.data.Count(d => d.Cedula.Contains("V"));
                int Tienda = _tiendaRepository.GetTiendas().FirstOrDefault(t => t.Id > 0).Id;
                if (elem == dataMasivaUser.data.Count())
                {
                    foreach (var dmu in dataMasivaUser.data)
                    {
                        string Nombres = "";
                        string Apellidos = "";
                        int Tipo = 3;
                        int Rol = 0;
                        var cn = dmu.Nombres.Trim().Split(' ');
                        var ca = dmu.Apellidos.Trim().Split(' ');                        

                        if (cn.Length <= 1)
                        {
                            Apellidos = ca[0];
                            Nombres = cn[0];
                        }
                        else
                        {
                            Apellidos = ca[0] + " " + ca[1];
                            Nombres = cn[0] + " " + cn[1];
                        }

                        if (dmu.Cargo.Contains("CAJERA"))
                        {
                            Tipo = 3;
                            Rol = 5;
                        }
                        else
                        if (dmu.Cargo.Contains("SUPERVISOR DE CAJA OPERATIVA") || dmu.Cargo.Contains("JEFE DE CAJA OPERATIVA"))//Supervisira de caja
                        {
                            Tipo = 3;
                            Rol = 3;
                        }
                        else
                        if (dmu.Cargo.Contains("CAJERA DE FARMACIA"))//caja farmacia
                        {
                            Tipo = 3;
                            Rol = 2;
                        }
                        else
                        if (dmu.Cargo.Contains("REGENTE DE FARMACIA"))//Regente farmacia
                        {
                            Tipo = 3;
                            Rol = 8;
                        }
                        else
                        if (dmu.Cargo.Contains("SUPERVISOR DE RECAUDACION Y CONTROL") || dmu.Cargo.Contains("SUPERVISOR DE CAJA OPERATIVA"))//RCaja Admin
                        {
                            Tipo = 3;
                            Rol = 3;
                        }
                        else
                        if (dmu.Cargo.Contains("JEFE DE RECAUDACION Y CONTROL") || dmu.Cargo.Contains("JEFE DE CAJA OPERATIVA"))//RCaja Admin
                        {
                            Tipo = 2;
                            Rol = 3;
                        }
                        else
                        if (dmu.Cargo.Contains("ANALISTA DE RECAUDACION Y CONTROL"))//RCaja Admin
                        {
                            Tipo = 21;
                            Rol = 3;
                        }

                        dmu.Clave = cn[0].ElementAt(0) + ca[0];
                        dmu.Usuario = cn[0].ElementAt(0).ToString() + ca[0].ElementAt(0).ToString() + dmu.Cedula.ToString();
                        if (!dmu.Cargo.Contains("EMPAQUETADOR"))
                            DataUserSave.Add(new Usuario
                            {
                                Nick = dmu.Usuario,
                                Password = Util.Encrypt(dmu.Clave),
                                Nombre = Nombres.ToUpper(),
                                Apellido = Apellidos.ToUpper(),
                                Cedula = dmu.Cedula.ToString(),
                                Estatus = 1,
                                Tipo = Tipo,
                                RolId = Rol,
                                TiendaId = Tienda,
                                RecordarToken = Util.Encrypt(dmu.Usuario)
                            });
                    }

                    Usuarios.AddRange(DataUserSave);
                    this.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
