﻿using System;

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
            return _context.Donations
                .Include(d => d.FoodItem)
                .Include(u => u.User)
                .OrderBy(m => m.Id)
                .ToList();
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

        public Donation? GetDonationById(int id)
        {

            return _context.Donations
                .Include(d => d.FoodItem)
                .FirstOrDefault(x => x.Id.Equals(id)); ;
        }

        public List<Donation> GetDonationsByUser(string id)
        {
            return _context.Donations
                .Include(d => d.FoodItem)
                .Where(x => x.User.Id.Equals(id))
                .OrderByDescending(m => m.Date)
                .ToList();
        }
    }
}
