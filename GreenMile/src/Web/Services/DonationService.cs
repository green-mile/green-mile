using System;

using Microsoft.EntityFrameworkCore;

using Web.Data;
using Web.Models;

namespace Web.Services
{
    public class DonationService
    {
        private readonly DataContext _context;

        public DonationService(DataContext context)
        {
            _context = context;
        }

        public List<Donation> GetAll()
        {
            return _context.Donations.Include(d => d.CustomFood).Include(u => u.User).OrderBy(m => m.Id).ToList();
        }

        public void AddDonation(Donation donation)
        {
            _context.Donations.Add(donation);
            _context.SaveChanges();
        }

        public void UpdateDonation(Donation donation)
        {
            _context.Donations.Update(donation);
            _context.SaveChanges();
        }

        public Donation? GetDonationById(string id)
        {
            Donation? donation = _context.Donations.FirstOrDefault(x => x.Id.Equals(id));
            return donation;
        }
    }
}
