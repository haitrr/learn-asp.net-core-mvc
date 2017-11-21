using LibraryData;
using System;
using LibraryData.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryDbContext _context;
        public bool isBook(int id)
        {
            return _context.LibraryAssets.OfType<Book>()
                .Any(book => book.Id == id);
        }

        public bool isVideo(int id)
        {
            return _context.LibraryAssets.OfType<Video>()
                .Any(video => video.Id == id);
        }

        public LibraryAssetService(LibraryDbContext context)
        {
            _context = context;
        }
        public void Add(LibraryAsset newAssest)
        {
            _context.Add(newAssest);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include( assest => assest.Status)
                .Include(assest => assest.Location)
                ;
        }

        public string GetAuthorOrDirector(int id)
        {
            if (isBook(id))
            {
                return _context.Books.FirstOrDefault(book => book.Id == id).Author;
            }
            else if (isVideo(id))
            {
                return _context.Videos.FirstOrDefault(video => video.Id == id).Director;
            }
            else{
                throw new Exception("This type of assest is not supported");
            }
        }

        public LibraryAsset GetById(int id)
        {
            return GetAll()
                 .Single(assest => assest.Id == id)
                ;
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetById(id).Location;
            //return _context.LibraryAssets.FirstOrDefault(assest => assest.Id == id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            if (_context.Books.Any(book => book.Id == id))
            {
                return _context.Books.Single(book => book.Id == id).DeweyIndex;
            }
            else return "";
        }

        public string GetIsbn(int id)
        {
            if (_context.Books.Any(book => book.Id == id))
            {
                return _context.Books.Single(book => book.Id == id).ISBN;
            }
            else return "";
        }

        public string GetTitle(int id)
        {
            return GetById(id).Title;
        }

        public string GetType(int id)
        {
            return GetById(id).GetType().Name;
        }
    }
}
