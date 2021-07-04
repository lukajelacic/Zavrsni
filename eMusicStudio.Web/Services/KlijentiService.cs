using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eMusicStudio.Model;
using eMusicStudio.Model.Requests;
using eMusicStudio.Web.Models;
using eMusicStudio.Web.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eMusicStudio.Web.Services
{
    public class KlijentiService : IKlijentiService
    {
        private readonly _150192V1Context _context;
        private readonly IMapper _mapper;
        public KlijentiService(_150192V1Context context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Model.Klijenti Authenticiraj(string username, string pass)
        {
            var user = _context.Klijenti.FirstOrDefault(x => x.KorisnickoIme == username);
            if (user != null)
            {
                var newHash = GenerateHash(user.LozinkaSalt, pass);
                if (newHash == user.LozinkaHash)
                {
                    return _mapper.Map<Model.Klijenti>(user);
                }
            }
            return null;
        }
        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
        public List<Model.Klijenti> Get(KlijentiSearchRequest request)
        {
            var query = _context.Klijenti.Include(x=>x.Grad).AsQueryable();
            if (!string.IsNullOrWhiteSpace(request?.Ime))
            {
                query = query.Where(x => x.Ime.StartsWith(request.Ime));
            }
            if (!string.IsNullOrWhiteSpace(request?.Prezime))
            {
                query = query.Where(x => x.Prezime.StartsWith(request.Prezime));
            }
            if (!string.IsNullOrWhiteSpace(request?.KorisnickoIme))
            {
                query = query.Where(x => x.KorisnickoIme.StartsWith(request.KorisnickoIme));
            }
            var list = query.ToList();
            return _mapper.Map<List<Model.Klijenti>>(list);
        }

        public Model.Klijenti GetById(int id)
        {
            var result = _context.Klijenti.Find(id);
            return _mapper.Map<Model.Klijenti>(result);
        }

        public Model.Klijenti Insert(KlijentiInsertRequest request)
        {
            var entity = _mapper.Map<Models.Klijenti>(request);
            if (request.Password != request.PasswordConfirmation)
            {
                throw new UserException("Passwordi se ne slazu");
            }
            entity.LozinkaSalt = GenerateSalt();
            entity.LozinkaHash = GenerateHash(entity.LozinkaSalt,request.Password);
            entity.Slika = request.Slika;
            _context.Klijenti.Add(entity);
            _context.SaveChanges(); 
            return _mapper.Map<Model.Klijenti>(entity);
        }

        public Model.Klijenti Update(int id, KlijentiInsertRequest request)
        {
            var entity = _context.Klijenti.Find(id);
            _context.Klijenti.Attach(entity);
            _context.Klijenti.Update(entity);
            if (request.Password != request.PasswordConfirmation)
            {
                throw new Exception("Passwordi se ne poklapaju.");
            }
            
            entity.LozinkaSalt = GenerateSalt();
            entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Password);
            
            _mapper.Map(request, entity);

            _context.SaveChanges();
            return _mapper.Map<Model.Klijenti>(entity);

        }
        public bool Delete(int id)
        {
            var entity = _context.Klijenti.Find(id);
            _context.Klijenti.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public Model.Requests.KlijentBanovanRequest GetKlijenta(int id)
        {
            var result = _context.Klijenti.Find(id);
            return _mapper.Map<Model.Requests.KlijentBanovanRequest>(result);
        }

        public Model.Klijenti UpdateBanovan(int id, KlijentBanovanRequest request)
        {
            var entity = _context.Klijenti.Find(id);
            _context.Klijenti.Attach(entity);
            _context.Klijenti.Update(entity);
           
            _mapper.Map(request, entity);

            _context.SaveChanges();
            return _mapper.Map<Model.Klijenti>(entity);
        }
    }
}
