using Authorization.Data;
using Authorization.Models;
using Authorization.Rpositories;
using Microsoft.AspNetCore.Identity;
using System;

namespace Authorization.Seeds
{
    public class RolesSeed : ISeed
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IDomainRepository _domainRepository;
        public RolesSeed(RoleManager<ApplicationRole> roleManager, IDomainRepository domainRepository)
        {
            _roleManager = roleManager;
            _domainRepository = domainRepository;
        }
        public double ExecutionOrder => 2;
        public void Seed()
        {
            AddNewRole(SeedOptions.DefaultAdmin);
            Console.WriteLine("Role Seed");
        }

        private void AddNewRole(string role)
        {
            var isExist = _roleManager.RoleExistsAsync(role).Result;
            if (!isExist)
            {
                var domains = _domainRepository.GetAll();
                foreach (var item in domains)
                {
                    var result = _roleManager.CreateAsync(new ApplicationRole { Name = role , DomainId= item.Id});
                }
            }
        }
    }
}
