using Authorization.Data;
using Authorization.Models;
using Authorization.Rpositories;
using Authorization.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Seeds
{
    public class DomainSeed : ISeed
    {
        private readonly IDomainRepository _domainRepository;
        public DomainSeed(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }
        public double ExecutionOrder => 1;
        public void Seed()
        {
            var isExisted = _domainRepository.FirstOrDefault(i => i.Name.Equals(SeedOptions.DefaultDomain)) != null;
            
            if (!isExisted) {
                var addedDomain = _domainRepository.Add(new Domain
                {
                    Name = SeedOptions.DefaultDomain
                });
                _domainRepository.SaveChanges();
            }
            
            Console.WriteLine("Domain Seed");
        }
    }
}
